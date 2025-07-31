using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Cervione.Core.Models.Identity;

using Microsoft.EntityFrameworkCore;

namespace Cervione.Core.Models.Groups;

[PrimaryKey(nameof(Id))]
public sealed class Group
{
    public Group()
    {
        
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    public string Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Code { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }
    
    [Required]
    public List<Member> Members { get; set; } = [];

    [Required]
    public string UserId { get; set; }

    [NotMapped]
    public User User { get; set; }
}