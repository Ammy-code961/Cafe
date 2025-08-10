using Microsoft.EntityFrameworkCore;
using CafeApp.Models;

namespace CafeApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Add your tables/models here
        public DbSet<User> Users { get; set; }
    }
}


