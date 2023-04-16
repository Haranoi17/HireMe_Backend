using HireMe_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace HireMe_Backend
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> users { get; set; }
        public DbSet<Offer> offers { get; set; }
    }
}
