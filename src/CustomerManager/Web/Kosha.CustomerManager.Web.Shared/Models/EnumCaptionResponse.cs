namespace Kosha.CustomerManager.Web.Shared.Models;

public record EnumCaptionResponse<TKey, TEnum>(
    TKey Id, 
    string Caption, 
    TEnum Value
) : CaptionResponse<TKey>(Id, Caption);


public record EnumCaptionResponse<TEnum>(
    int Id,
    string Caption,
    TEnum Value
) : EnumCaptionResponse<int, TEnum>(Id, Caption, Value);