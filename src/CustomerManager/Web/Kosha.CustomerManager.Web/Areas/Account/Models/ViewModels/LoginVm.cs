using Kosha.CustomerManager.Web.Infrastructure.Models.Authentication;
using System.ComponentModel.DataAnnotations;

namespace Kosha.CustomerManager.Web.Areas.Account.Models.ViewModels;

public record LoginVm(
    [Required] string Username,
    [Required] string Password,
    bool RememberMe,
    string? ReturnUrl = null
) : AuthenticationRequest(Username);