using ClubsBack.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClubsBack.Repository
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Club> Clubs { get; set; } = null!;
        public DbSet<ClubUser> ClubsUsers { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Club>()
                .HasMany(c => c.Users)
                .WithMany(u => u.Clubs)
                .UsingEntity<ClubUser>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
