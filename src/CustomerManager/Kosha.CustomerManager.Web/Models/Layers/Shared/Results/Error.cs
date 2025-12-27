namespace Kosha.CustomerManager.Web.Models.Layers.Shared.Results;

public class Error
{
    public static readonly Error None = new("SomethingWrongHappened");

    public Error(string code, string? message = null)
    {
        Code = code;
        Message = message;
    }

    public string Code { get; }

    public string? Message { get; }
}