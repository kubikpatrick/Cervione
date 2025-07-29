using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Cervione.Core.Models.Devices;
using Cervione.Core.Models.Groups;

using Microsoft.AspNetCore.Identity;

namespace Cervione.Core.Models.Identity;

public sealed class User : IdentityUser, IPositionable
{
    public User()
    {
        
    }
    
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    public string Avatar { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; }
    
    public Position? Position { get; set; }
    
    [NotMapped]
    public List<Device> Devices { get; set; } = [];

    [NotMapped]
    public List<Member> Members { get; set; } = [];
    
    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";
}