using System.ComponentModel.DataAnnotations;

namespace Application.Models.Auth;

public class RegistrationRequest
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [MinLength(6)]
    public required string UserName { get; set; }

    [Required]
    [MinLength(6)]
    public required string Password { get; set; }

    [Required]
    public required string RoleName { get; set; }
}
