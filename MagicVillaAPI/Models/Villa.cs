using System.ComponentModel.DataAnnotations;

namespace MagicVillaAPI.Models
{
    public class Villa
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public int Occupancy { get; set; }
        public int Sqft { get; set; }
        public int Rate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
