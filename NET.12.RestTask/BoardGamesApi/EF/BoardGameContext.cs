using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BoardGamesApi.Models
{
    public class BoardGameContext : DbContext
    {
        public BoardGameContext(DbContextOptions<BoardGameContext> options)
            :base (options)
        {
            Database.EnsureCreated();
        }
        
        public DbSet<BoardGame> BoardGames { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BoardGame>().HasData(
                new BoardGame[] {
                new BoardGame()
                {
                    Id=1,
                    Name = "Wingspan",
                    Description = "The classic board games like Monopoly.",
                    Created = DateTime.Now
                },
                new BoardGame()
                {
                    Id=2,
                    Name = "Root",
                    Description = "The best asymmetric strategy board game of the decade.",
                    Created = DateTime.Now
                },
                new BoardGame()
                {
                    Id=3,
                    Name = "Hardback",
                    Description = "Exceptional deck-building word game.",
                    Created = DateTime.Now
                },
                 new BoardGame()
                 {
                     Id=4,
                     Name = "Azul",
                     Description = "The brilliant abstract game for two to five players.",
                     Created = DateTime.Now
                 },
                 new BoardGame()
                 {
                     Id=5,
                     Name = "Dinosaur Island",
                     Description = "This is basically Jurassic Park: The Game, in all its '80s glory.",
                     Created = DateTime.Now
                 }
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
