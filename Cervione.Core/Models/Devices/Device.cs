using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cervione.Core.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace Cervione.Core.Models.Devices;

[PrimaryKey(nameof(Id))]
public sealed class Device : IMarkable
{
    public Device()
    {
        
    }
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    public string Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Hash { get; set; }
    
    [Required]
    public bool IsPrincipal { get; set; }
    
    [Required]
    public DeviceType Type { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; }
    
    [Required]
    public Position Position { get; set; }
    
    [Required]
    public string UserId { get; set; }
    
    [NotMapped]
    public User User { get; set; }
}