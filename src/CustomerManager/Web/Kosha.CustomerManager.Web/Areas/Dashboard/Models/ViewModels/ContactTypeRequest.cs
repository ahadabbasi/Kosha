namespace Kosha.CustomerManager.Web.Areas.Dashboard.Models.ViewModels;

public record ContactTypeRequest(
    string Name, 
    string Title, 
    string Selected,
    bool Disabled = false
);