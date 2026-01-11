using Microsoft.EntityFrameworkCore;
using ShiftManager.Api.Entities;

namespace ShiftManager.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) {}

        public DbSet<User> Users => Set<User>();
        public DbSet<Business> Businesses => Set<Business>();
    }
}