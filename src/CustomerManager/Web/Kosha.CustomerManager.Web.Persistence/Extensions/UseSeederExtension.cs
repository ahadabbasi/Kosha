using System;
using System.Linq;
using System.Reflection;
using Kosha.CustomerManager.Web.Persistence.Attributes;
using Kosha.CustomerManager.Web.Persistence.Helper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kosha.CustomerManager.Web.Persistence.Extensions;

public static class UseSeederExtension
{
    public static async void UseSeeder(this IHost entry, Assembly assembly)
    {
        try
        {
            Func<object, bool> predicateSeedAttribute = 
                attribute => attribute.GetType() == typeof(SeedAttribute);

            Type[] seederTypes =
                assembly.GetTypes()
                    .Where(type => typeof(IDataSeeder).IsAssignableFrom(type))
                    .Where(type => type.GetCustomAttributes().Any(predicateSeedAttribute))
                    .OrderBy(type => 
                        type.GetCustomAttributes()
                            .Where(predicateSeedAttribute)
                            .Select(attribute => ((SeedAttribute)attribute).Version)
                            .First()
                    )
                    .ToArray();

            if (seederTypes.Any())
                using (IServiceScope scope = entry.Services.CreateScope())
                {
                    foreach (Type seederType in seederTypes)
                    {
                        object? instance = null;

                        ConstructorInfo[] constructors = seederType.GetConstructors();

                        Func<ConstructorInfo, bool> parameterLessConstructorDetector =
                            constructor => constructor.GetParameters().Length == 0;

                        if (
                            constructors.Length == 0 ||
                            seederType.GetConstructors()
                                .Any(parameterLessConstructorDetector)
                        )
                        {
                            ConstructorInfo? constructor =
                                seederType.GetConstructors()
                                    .FirstOrDefault(parameterLessConstructorDetector);

                            instance = constructor == null
                                ? Activator.CreateInstance(seederType)
                                : constructor.Invoke([]);
                        }

                        if (instance == null)
                            foreach (ConstructorInfo constructorInfo in constructors.Where(constructor => !parameterLessConstructorDetector(constructor)))
                            {
                                object?[] parameters =
                                    constructorInfo.GetParameters()
                                        .Select(parameter => scope.ServiceProvider.GetService(parameter.ParameterType))
                                        .Where(parameter => parameter is not null)
                                        .ToArray();

                                if (parameters.Length == constructorInfo.GetParameters().Length)
                                    try
                                    {
                                        instance = constructorInfo.Invoke(parameters);

                                        break;
                                    }
                                    catch
                                    {
                                        //
                                    }
                            }

                        if (
                            instance != null &&
                            instance is IDataSeeder seeder
                        )
                            await seeder.InvokeAsync();

                    }
                }
        }
        catch
        {
            //
        }
    }
}