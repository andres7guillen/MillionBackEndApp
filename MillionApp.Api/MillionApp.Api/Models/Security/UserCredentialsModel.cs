using System.ComponentModel.DataAnnotations;

namespace MillionApp.Api.Models.Security;

public class UserCredentialsModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    [StringLength(200)]
    public string? Name { get; set; }

    [StringLength(200)]
    public string? SurName { get; set; }

}
