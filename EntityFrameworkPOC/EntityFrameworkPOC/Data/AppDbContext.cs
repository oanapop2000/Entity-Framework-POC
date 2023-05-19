using EntityFrameworkPOC.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkPOC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song> Songs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Song>()
                .HasOne(x => x.Artist)
                .WithMany(x => x.Songs);

            new DbInitializer(builder).Seed();
        }
    }
}
