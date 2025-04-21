using CatAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CatAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet <CatImage> CatImages { get; set; }
        public AppDbContext( DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
    }
}
