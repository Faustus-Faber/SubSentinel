using Microsoft.EntityFrameworkCore;
using SubSentinel.API.Models;

namespace SubSentinel.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Subscription> Subscriptions { get; set; }
    }
}