using MagicVillaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVillaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { }

        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData
                (
                new Villa()
                {
                    Id = 1,
                    Name = "Royal Villa",
                    Details = "With personal Pool and Beach View",
                    Sqft = 200,
                    Occupancy = 5,
                    Rate = 200,
                },
                new Villa()
                {
                    Id = 2,
                    Name = "Lux Villa",
                    Details = "With 2 Floor, Jungle view and personal Pool",
                    Sqft = 250,
                    Occupancy = 8,
                    Rate = 350,
                },
                new Villa()
                {
                    Id = 3,
                    Name = "Diamond Villa",
                    Details = "With Pool view and terrace",
                    Sqft = 150,
                    Occupancy = 4,
                    Rate = 150,
                }
                );
        }
    }
}
