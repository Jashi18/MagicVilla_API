﻿using System.ComponentModel.DataAnnotations;

namespace MagicVillaAPI.Models.DTO
{
    public class VillaDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Details { get; set; }
        public string Name { get; set; }
        public int Occupancy { get; set; }
        public int Sqft { get; set; }
        public int Rate { get; set; }
    }
}
