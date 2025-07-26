using System.ComponentModel.DataAnnotations;

namespace Cervione.Core.Models.Http;

public sealed class SignUpRequest
{
    public SignUpRequest()
    {
        
    }

    public SignUpRequest(string email, string password, string confirmPassword)
    {
        Email = email;
        Password = password;
        ConfirmPassword = confirmPassword;
    }
    
    [EmailAddress]
    [Required]
    public required string Email { get; init; }
    
    [DataType(DataType.Password)]
    [Required]
    public required string Password { get; init; }
    
    [Compare(nameof(Password))]
    [DataType(DataType.Password)]
    [Required]
    public required string ConfirmPassword { get; init; }
}