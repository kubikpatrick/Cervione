using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

namespace Cervione.Core.Models;

[Owned]
public sealed class Position
{
    public Position()
    {
        
    }

    public Position(double longitude, double latitude)
    {
        Longitude = longitude;
        Latitude = latitude;
        Timestamp = DateTime.UtcNow;
    }
    
    [Required]
    public double Longitude { get; set; }
    
    [Required]
    public double Latitude { get; set; }
    
    [Required]
    public DateTime Timestamp { get; set; }
}