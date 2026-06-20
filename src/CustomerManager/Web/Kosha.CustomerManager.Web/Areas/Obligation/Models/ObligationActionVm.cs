namespace Kosha.CustomerManager.Web.Areas.Obligation.Models;

public record ObligationActionVm(
    string Comment, string? User, 
    string? Registered, bool? Mine = false
);