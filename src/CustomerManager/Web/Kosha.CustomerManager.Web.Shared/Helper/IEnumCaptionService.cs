using System;
using System.Collections.Generic;
using Kosha.CustomerManager.Web.Shared.Models;

namespace Kosha.CustomerManager.Web.Shared.Helper;

public interface IEnumCaptionService
{
    IEnumerable<EnumCaptionResponse<TKey, TEnum>> Convert<TKey, TEnum>()
        where TEnum : struct, Enum;

    IEnumerable<EnumCaptionResponse<TEnum>> Convert<TEnum>()
        where TEnum : struct, Enum;
}