using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task;
using Kosha.CustomerManager.Web.Infrastructure.Helper.Task.Model;
using Kosha.CustomerManager.Web.Infrastructure.Models.Task;
using Kosha.CustomerManager.Web.Shared.Results;
using Microsoft.AspNetCore.Http;

namespace Kosha.CustomerManager.Web.Models.Services;

internal sealed class TaskBinderService(
    IHttpContextAccessor accessor
) : ITaskBinderService
{
    private HttpContext Context =>
        accessor.HttpContext ?? throw new InvalidOperationException();


    private IEnumerable<Type> Types =>
        AppDomain.CurrentDomain.GetAssemblies()
            .Where(assembly => !string.IsNullOrEmpty(assembly.FullName) && assembly.FullName.Contains("Kosha"))
            .SelectMany(assembly => assembly.GetTypes());

    private Error FailedBindingError => new Error("ModelBindingFailed", "Failed to bind the model.");


    public async Task<Result<ITaskCreateRequest>> BindCreateAsync(CancellationToken cancellation = default)
    {
        ITaskCreateRequest? data = null;

        /*
        Type modelType = typeof(TRequest);

        ModelMetadata metadata = modelMetadataProvider.GetMetadataForType(modelType);

        ModelBindingContext bindingContext =
            DefaultModelBindingContext.CreateBindingContext(
                new ActionContext(
                    Context, 
                    Context.GetRouteData(),
                    new ActionDescriptor()
                ), 
                await CompositeValueProvider.CreateAsync(
                    new ControllerContext(
                        new ActionContext(
                            Context,
                            Context.GetRouteData(),
                            new ControllerActionDescriptor()
                        )
                    )
                ), 
                metadata, 
                new BindingInfo()
                {
                    BindingSource = BindingSource.Body
                },
                string.Empty
            );

        IModelBinder modelBinder =
            modelBinderFactory.CreateBinder(
                new ModelBinderFactoryContext()
                {
                    Metadata = metadata, 
                    BindingInfo = 
                        new BindingInfo()
                        {
                            BindingSource = BindingSource.Body
                        }
                }
            );

        await modelBinder.BindModelAsync(bindingContext);

        if (bindingContext.Result.IsModelSet)
        {
            data = bindingContext.Result.Model as TRequest;
        }

        */
        /*
        try
        {
            Context.Request.EnableBuffering();

            int position = (int)Context.Request.Body.Position;

            Context.Request.Body.Position = 0;

            using StreamReader reader = new StreamReader(Context.Request.Body, leaveOpen: true);
            string body = await reader.ReadToEndAsync(cancellation);

            data = 
                JsonSerializer.Deserialize<TRequest>(
                    body, 
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                );

            Context.Request.Body.Position = position;
        }
        catch (Exception exception)
        {
            //
        }

        return
            data != null
            ? Result.Success(data)
            : Result.Failed<TRequest>(new Error("ModelBindingFailed", "Failed to bind the model."));
        */

        if (Context.Request.ContentLength is > 0)
        {
            try
            {
                Context.Request.EnableBuffering();

                int position = (int)Context.Request.Body.Position;

                Context.Request.Body.Position = 0;

                using StreamReader reader = new StreamReader(Context.Request.Body, leaveOpen: true);
                string body = await reader.ReadToEndAsync(cancellation);

                TaskCreateRequest? task =
                    JsonSerializer.Deserialize<TaskCreateRequest>(body, JsonSerializerOptions.Default);

                if (task is not null)
                {
                    Type[] types =
                        AccumulateTypes<ITaskCreateRequest, TaskCreateRequest>()
                            .ToArray();

                    if (types.Length != 0)
                    {
                        Type[] validParameterTypes = [typeof(string), typeof(JsonSerializerOptions)];

                        MethodInfo? method =
                            typeof(JsonSerializer)
                                .GetMethods()
                                .Where(method =>
                                    method.Name.Equals(
                                        nameof(JsonSerializer.Deserialize),
                                        StringComparison.OrdinalIgnoreCase
                                    )
                                )
                                .Where(method => method.GetParameters().Length == 2)
                                .FirstOrDefault(method => 
                                    method.GetParameters()
                                        .Aggregate(
                                            0,
                                            (accumulate, parameter) =>
                                            {
                                                if (validParameterTypes.Contains(parameter.ParameterType))
                                                    accumulate += 1;
                                                return accumulate;
                                            }
                                        ) == validParameterTypes.Length
                                );

                        if (method is not null)
                        {
                            foreach (Type type in types)
                            {
                                MethodInfo invokeMethod = method.MakeGenericMethod(type);

                                try
                                {
                                    object? value =
                                        invokeMethod.Invoke(null, [body, JsonSerializerOptions.Default]);

                                    if (
                                        value is not null && 
                                        value is ITaskCreateRequest request &&
                                        !string.IsNullOrEmpty(request.Type) &&
                                        request.Type.Equals(task.Type, StringComparison.OrdinalIgnoreCase)
                                    )
                                    {
                                        data = request;
                                        break;
                                    }
                                        
                                }
                                catch
                                {
                                    //
                                }
                                
                            }
                        }
                    }
                }

                Context.Request.Body.Position = position;
            }
            catch
            {
                //
            }

        }

        return data != null ? Result.Success(data) : Result.Failed<ITaskCreateRequest>(FailedBindingError);
    }

    public Task<Result<ITaskPaginateRequest>> BindPaginateAsync(ITaskCollectorRequest request, IEnumerable<Guid> records, CancellationToken cancellation = default)
    {
        ITaskPaginateRequest? data = null;

        Type[] types =
            AccumulateTypes<ITaskPaginateRequest, TaskPaginateRequest>()
                .ToArray();

        if (types.Length != 0)
        {
            foreach (Type type in types)
            {
                ConstructorInfo? constructorInfo = 
                    type.GetConstructors()
                        .Where(constructor => constructor.GetParameters().Length == 1)
                        .FirstOrDefault(constructor =>
                            constructor.GetParameters()
                                .Any(parameter => parameter.ParameterType == typeof(IEnumerable<Guid>))
                        );

                if (constructorInfo != null)
                {
                    try
                    {
                        object value =
                            constructorInfo.Invoke([records]);

                        if (
                            value is ITaskPaginateRequest convert &&
                            !string.IsNullOrEmpty(convert.Type) &&
                            request.Type.Equals(convert.Type, StringComparison.OrdinalIgnoreCase)
                        )
                        {
                            data = convert;
                            break;
                        }

                    }
                    catch (Exception e)
                    {
                        //
                    }
                    
                }
            }
        }

        return Task.FromResult(data != null ? Result.Success(data) : Result.Failed<ITaskPaginateRequest>(FailedBindingError));
    }



    private IEnumerable<Type> AccumulateTypes<TRequest, TClass>()
        where TRequest : ITaskCollectorRequest
        where TClass : class, TRequest =>
        Types
            .Where(type => type is { IsClass: true, IsAbstract: false } && typeof(TRequest).IsAssignableFrom(type))
            .Where(type => type != typeof(TClass))
            .ToArray();
}