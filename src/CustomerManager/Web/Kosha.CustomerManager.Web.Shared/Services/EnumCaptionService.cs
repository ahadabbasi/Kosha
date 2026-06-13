using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Kosha.CustomerManager.Web.Shared.Helper;
using Kosha.CustomerManager.Web.Shared.Models;

namespace Kosha.CustomerManager.Web.Shared.Services;

internal sealed class EnumCaptionService : IEnumCaptionService
{
    public IEnumerable<EnumCaptionResponse<TKey, TEnum>> Convert<TKey, TEnum>() 
        where TEnum : struct, Enum
    {
        IEnumerable<FieldInfo> fields =
            typeof(TEnum).GetFields()
                .Where(field =>
                    field.GetCustomAttributes(true)
                        .Any(attribute => attribute is EnumMemberAttribute)
                );

        IList<EnumCaptionResponse<TKey, TEnum>> data = 
            new List<EnumCaptionResponse<TKey, TEnum>>();

        foreach (FieldInfo field in fields)
        {
            string caption = field.Name;

            try
            {
                DisplayAttribute? attribute = field.GetCustomAttribute<DisplayAttribute>();

                if (attribute != null && !string.IsNullOrEmpty(attribute.Name))
                    caption = attribute.Name;
            }
            catch (Exception )
            {
                //
            }

            data.Add(
                new EnumCaptionResponse<TKey, TEnum>(
                     (TKey)System.Convert.ChangeType(Enum.Parse<TEnum>(field.Name), typeof(TKey)),
                     caption,
                     Enum.Parse<TEnum>(field.Name)
                )
            );
        }

        return data;
    }

    public IEnumerable<EnumCaptionResponse<TEnum>> Convert<TEnum>() 
        where TEnum : struct, Enum => 
        Convert<int, TEnum>()
            .Select(item => new EnumCaptionResponse<TEnum>(item.Id, item.Caption, item.Value));
}