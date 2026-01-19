using Microsoft.EntityFrameworkCore;
using ShiftManager.Api.Entities;

namespace ShiftManager.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Business> Businesses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<Business>(
                entity =>
                {
                    entity.Property(b => b.Id).ValueGeneratedOnAdd();

                    entity.Property(b => b.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");
                }
            ); 

            base.OnModelCreating(modelBuilder);
        }
    }
}