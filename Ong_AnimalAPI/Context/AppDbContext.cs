using Microsoft.EntityFrameworkCore;
using Ong_AnimalAPI.Models;

namespace Ong_AnimalAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        DbSet<Animal> Animals { get; set; } 
    }
}
