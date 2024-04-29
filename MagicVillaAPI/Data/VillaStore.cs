using MagicVillaAPI.Models.DTO;

namespace MagicVillaAPI.Data
{
    public class VillaStore
    {
        public static readonly List<VillaDTO> VillaList = new List<VillaDTO>()
            {
                new() {Id = 1, Name = "Pool View", Occupancy = 4, Sqft = 100},
                new() {Id = 2,Name = "Beach View", Occupancy = 2, Sqft = 60}
            };
    }
}
