using System.ComponentModel.DataAnnotations;
using CityInfo.API.Models;

namespace CityInfo.API.Entities
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        
        public string? Address { get; set; }

        public ICollection<PointOfInterest> PointOfInterests { get; set; }
            = new List<PointOfInterest>();
    }
}


