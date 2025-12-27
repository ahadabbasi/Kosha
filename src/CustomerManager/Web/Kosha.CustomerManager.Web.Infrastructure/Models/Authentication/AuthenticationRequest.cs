using System.ComponentModel.DataAnnotations;

namespace Kosha.CustomerManager.Web.Infrastructure.Models.Authentication;

public record AuthenticationRequest(
    [Required]
    string Username
);