using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Cervione.Core.Models.Identity;

using Microsoft.EntityFrameworkCore;

namespace Cervione.Core.Models.Groups;

[PrimaryKey(nameof(Id))]
public sealed class Member
{
    public Member()
    {
        
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    public string Id { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public string GroupId { get; set; }

    [Required]
    public string UserId { get; set; }

    [NotMapped]
    public Group Group { get; set; }

    public User User { get; set; }
}