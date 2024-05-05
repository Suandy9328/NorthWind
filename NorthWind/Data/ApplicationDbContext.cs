using Microsoft.EntityFrameworkCore;
using NorthWind.Models;

namespace NorthWind.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
 
        public DbSet<Product> Products { get; set; }
    }
}
