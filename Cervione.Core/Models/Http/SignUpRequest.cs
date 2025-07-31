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
    public string Email { get; set; }
    
    [DataType(DataType.Password)]
    [Required]
    public string Password { get; set; }
    
    [Compare(nameof(Password))]
    [DataType(DataType.Password)]
    [Required]
    public string ConfirmPassword { get; set; }
}