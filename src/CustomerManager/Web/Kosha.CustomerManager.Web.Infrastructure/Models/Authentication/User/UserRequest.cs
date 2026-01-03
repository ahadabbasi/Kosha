using System.ComponentModel.DataAnnotations;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Authentication.User;

public record UserRequest(
    [Required, Display(Name = "نام کاربری")]
    string Username
);