using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TTCompanion.API.FantasyFootball.Entities;

namespace TTCompanion.API.FantasyFootball.DBContexts
{
    public class DBContext : DbContext
    {
        public DbSet<Race> Races { get; set; } = null!;
        public DbSet<SpecialRule> SpecialRules { get; set; } = null!;
        public DbSet<Player> Players { get; set; } = null!;
        public DbSet<Skill> Skills { get; set; } = null!;

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>()
                .HasMany(s => s.Players)
                .WithMany(p => p.Skills)
                .UsingEntity(j => j
                    .ToTable("PlayerSkill"))
                .HasData(
                    new Skill("Dodge")
                    {
                        Id = 1
                    }
                );

            modelBuilder.Entity<Player>()
                .HasMany(p => p.Skills)
                .WithMany(s => s.Players)
                .UsingEntity(j => j
                    .ToTable("PlayerSkill")
                    .HasData(
                    new { SkillsId = 1, PlayersId = 1 }
                    ))
                .HasData(
            new Player("Ghoul Runner")
            {
                Id = 1,
                MA = 7,
                ST = 3,
                AG = 3,
                PA = 4,
                AV = 8,
                Cost = 75000,
                CanDelete = false
            },
            new Player("Mummy")
            {
                Id = 2,
                MA = 3,
                ST = 5,
                AG = 5,
                PA = null,
                AV = 10,
                Cost = 125000,
                CanDelete = false
            });

            modelBuilder.Entity<SpecialRule>()
                .HasData(
                new SpecialRule("Masters of Undeath")
                {
                    Id = 1
                });

            modelBuilder.Entity<Race>()
                .HasMany(r => r.Players)
                .WithMany(p => p.Races)
                .UsingEntity(j => j
                    .ToTable("PlayerRace")
                    .HasData(
                    new { PlayersId = 1, RacesId = 1 },
                    new { PlayersId = 2, RacesId = 1 }
                    ))
                .HasMany(r => r.SpecialRules)
                .WithMany(sr => sr.Races)
                .UsingEntity(j => j
                    .ToTable("RaceSpecialRule")
                    .HasData(
                    new { RacesId = 1, SpecialRulesId = 1 }
                    ))
                .HasData(
                new Race("Shambling Undead")
                {
                    Id = 1,
                    CostOfReRolls = 70000,
                    MaxApothecarys = 0,
                    CanDelete = false,
                },
                new Race("Snotling")
                {
                    Id = 27,
                    CostOfBribes = 50000,
                    MaxRiotousRookies = 1,
                    CanDelete = false
                },
                new Race("Amazon")
                {
                    Id = 3,
                    CanDelete = false
                },
                new Race("Black Orc")
                {
                    Id = 4,
                    CostOfBribes = 50000,
                    CanDelete = false
                },
                new Race("Choas Chosen")
                {
                    Id = 5,
                    CanDelete = false
                },
                new Race("Chaos Dwarf")
                {
                    Id = 6,
                    CostOfReRolls = 70000,
                    CanDelete = false
                },
                new Race("Chaos Renegade")
                {
                    Id = 7,
                    CostOfReRolls = 70000,
                    CanDelete = false
                },
                new Race("Daemons of Khorne")
                {
                    Id = 8,
                    CostOfReRolls = 70000,
                    CanDelete = false
                },
                new Race("Dark Elf")
                {
                    Id = 9,
                    CostOfReRolls = 50000,
                    CanDelete = false
                },
                new Race("Dwarf")
                {
                    Id = 10,
                    CostOfReRolls = 50000,
                    CanDelete = false
                },
                new Race("Elven Union")
                {
                    Id = 11,
                    CostOfReRolls = 50000,
                    CanDelete = false
                },
                new Race("Goblin")
                {
                    Id = 12,
                    CostOfBribes = 50000,
                    CanDelete = false
                },
                new Race("Halfling")
                {
                    Id = 13,
                    CostOfMasterChef = 100000,
                    CanDelete = false
                },
                new Race("High Elf")
                {
                    Id = 14,
                    CostOfReRolls = 50000,
                    CanDelete = false
                },
                new Race("Human")
                {
                    Id = 15,
                    CostOfReRolls = 50000,
                    CanDelete = false
                },
                new Race("Imperial Nobility")
                {
                    Id = 16,
                    CostOfReRolls = 70000,
                    CanDelete = false
                },
                new Race("Khorne")
                {
                    Id = 17,
                    CanDelete = false
                },
                new Race("Lizardmen")
                {
                    Id = 18,
                    CostOfReRolls = 70000,
                    CanDelete = false
                },
                new Race("Necromantic Horror")
                {
                    Id = 19,
                    CostOfReRolls = 70000,
                    MaxApothecarys = 0,
                    CanDelete = false
                },
                new Race("Norse")
                {
                    Id = 20,
                    CanDelete = false
                },
                new Race("Nurgle")
                {
                    Id = 21,
                    CostOfReRolls = 70000,
                    MaxApothecarys = 0,
                    CanDelete = false
                },
                new Race("Ogre")
                {
                    Id = 22,
                    CostOfReRolls = 70000,
                    MaxRiotousRookies = 1,
                    CanDelete = false
                },
                new Race("Old World Alliance")
                {
                    Id = 23,
                    CostOfReRolls = 70000,
                    CanDelete = false
                },
                new Race("Orc")
                {
                    Id = 24,
                    CanDelete = false
                },
                new Race("Skaven")
                {
                    Id = 25,
                    CostOfReRolls = 50000,
                    CanDelete = false
                },
                new Race("Slann")
                {
                    Id = 26,
                    CostOfReRolls = 50000,
                    CanDelete = false
                },
                new Race("Tomb Kings")
                {
                    Id = 29,
                    CostOfReRolls = 70000,
                    MaxApothecarys = 0,
                    CanDelete = false
                },
                new Race("Underworld Denizens")
                {
                    Id = 30,
                    CostOfReRolls = 70000,
                    CostOfBribes = 50000,
                    CanDelete = false
                },
                new Race("Vampire")
                {
                    Id = 31,
                    CostOfReRolls = 70000,
                    CanDelete = false
                },
                new Race("Wood Elf")
                {
                    Id = 32,
                    CanDelete = false
                }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
