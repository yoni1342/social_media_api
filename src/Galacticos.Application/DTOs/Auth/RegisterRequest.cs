using System.ComponentModel.DataAnnotations;

namespace Galacticos.Application.DTOs.Auth;

public class RegisterRequest
{
    [Required]
    public string FirstName { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    [Required]
    public string UserName { get; set; } = null!;
    [EmailAddress]
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    [MinLength(8)]
    public string Password { get; set; } = null!;
    [Required]
    [MinLength(8)]
    public string ConfirmPassword { get; set; } = null!;
}
