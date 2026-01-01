using System.ComponentModel.DataAnnotations;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Authentication;

public record AuthenticationRequest(
    [Required, Display(Name = "نام کاربری")]
    string Username
);