﻿using Microsoft.EntityFrameworkCore;
using TTCompanion.API.FantasyFootball.Entities;

namespace TTCompanion.API.FantasyFootball.DBContexts
{
    public class DBContext : DbContext
    {
        public DbSet<Race> Races { get; set; } = null!;
        public DbSet<Player> Players { get; set; } = null!;
        public DbSet<SpecialRule> SpecialRules { get; set; } = null!;
        public DbSet<Skill> Skills { get; set; } = null!;

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Race>().HasData(
                new Race("Shambling Undead")
                {
                    Id = 1,
                    CostOfReRolls = 70000
                },
                new Race("Snotling")
                {
                    Id = 2,
                    CostOfReRolls = 60000
                }
                );

            modelBuilder.Entity<Player>().HasData(
                new Player("Ghoul Runner") {
                    Id = 1,
                    RaceId = 1,
                    MA = 7,
                    ST = 3,
                    AG = 3,
                    PA = 4,
                    AV = 8,
                    Cost = 75000
                },
                new Player("Mummy")
                {
                    Id = 2,
                    RaceId = 1,
                    MA = 3,
                    ST = 5,
                    AG = 5,
                    PA = null,
                    AV = 10,
                    Cost = 125000
                });
            
            base.OnModelCreating(modelBuilder);
        }
    }
}