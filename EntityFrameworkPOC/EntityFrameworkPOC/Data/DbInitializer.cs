using EntityFrameworkPOC.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkPOC.Data
{
    public class DbInitializer
    {
        private readonly ModelBuilder _builder;

        public DbInitializer(ModelBuilder builder)
        {
            _builder = builder;
        }

        public void Seed()
        {
            _builder.Entity<Artist>(a =>
            {
                a.HasData(new Artist
                {
                    Id = new Guid("90d10994-3bdd-4ca2-a178-6a35fd653c59"),
                    Name = "Taylor Swift",
                    DateOfBirth = new DateTime(1989, 12, 13),
                });
                a.HasData(new Artist
                {
                    Id = new Guid("6ebc3dbe-2e7b-4132-8c33-e089d47694cd"),
                    Name = "Selena Gomez",
                    DateOfBirth = new DateTime(1992, 07, 22),
                });
            });

            _builder.Entity<Song>(s =>
            {
                s.HasData(new Song
                {
                    Id = new Guid("98474b8e-d713-401e-8aee-acb7353f97bb"),
                    Title = "Daylight",
                    Genre = Genre.Pop,
                    Rating = 5,
                    ArtistId = new Guid("90d10994-3bdd-4ca2-a178-6a35fd653c59")
                });
                s.HasData(new Song
                {
                    Id = new Guid("bfe902af-3cf0-4a1c-8a83-66be60b028ba"),
                    Title = "Afterglow",
                    Genre = Genre.Electronic,
                    Rating = 5,
                    ArtistId = new Guid("90d10994-3bdd-4ca2-a178-6a35fd653c59")
                });
                s.HasData(new Song
                {
                    Id = new Guid("150c81c6-2458-466e-907a-2df11325e2b8"),
                    Title = "Rare",
                    Genre = Genre.Rock,
                    Rating = 4.5,
                    ArtistId = new Guid("6ebc3dbe-2e7b-4132-8c33-e089d47694cd")
                });
            });
        }
    }
}
