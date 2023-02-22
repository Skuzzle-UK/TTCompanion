using Microsoft.EntityFrameworkCore;
using TTCompanion.API.FantasyFootball.Entities;

namespace TTCompanion.API.FantasyFootball.DBContexts
{
    public class FFContext : DbContext
    {
        public DbSet<FFRace> Races { get; set; } = null!;
        public DbSet<FFPlayer> Players { get; set; } = null!;
        public DbSet<FFSpecialRule> SpecialRules { get; set; } = null!;
        public DbSet<FFSkill> Skills { get; set; } = null!;

        public FFContext(DbContextOptions<FFContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FFRace>().HasData(
                new FFRace("Shambling Undead")
                {
                    Id = 1,
                    CostOfReRolls = 70000
                },
                new FFRace("Snotling")
                {
                    Id = 2,
                    CostOfReRolls = 60000
                }
                );

            modelBuilder.Entity<FFPlayer>().HasData(
                new FFPlayer("Ghoul Runner") {
                    Id = 1,
                    RaceId = 1,
                    MA = 7,
                    ST = 3,
                    AG = 3,
                    PA = 4,
                    AV = 8,
                    Cost = 75000
                },
                new FFPlayer("Mummy")
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
