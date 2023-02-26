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

        /// <summary>
        /// Seed by creating a var of entity type and then adding var to .HasData of modeBuilder.Entity<Type>
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Skills
            var dodge = new Skill(1, "Dodge");
            var defensive = new Skill(2, "Defensive");

            modelBuilder.Entity<Skill>()
                .HasMany(s => s.Players)
                .WithMany(p => p.Skills)
                .UsingEntity(j => j
                    .ToTable("PlayerSkill"))
                .HasData(
                    dodge,
                    defensive
                );
            #endregion

            #region Players
            var ghoulRunner = new Player(1, "Ghoul Runner")
            {
                MA = 7,
                ST = 3,
                AG = 3,
                PA = 4,
                AV = 8,
                Cost = 75000,
            };

            var mummy = new Player(2, "Mummy")
            {
                MA = 3,
                ST = 5,
                AG = 5,
                PA = null,
                AV = 10,
                Cost = 125000,
            };

            modelBuilder.Entity<Player>()
                .HasMany(p => p.Skills)
                .WithMany(s => s.Players)
                .UsingEntity(j => j
                    .ToTable("PlayerSkill")
                    //Add skills to players here
                    .HasData(
                        new { PlayersId = ghoulRunner.Id, SkillsId = dodge.Id }
                    ))
                .HasData(
                    ghoulRunner,
                    mummy
                );
            #endregion

            #region Special Rules
            var mastersOfUndeath = new SpecialRule(1, "Masters of Undeath");
            modelBuilder.Entity<SpecialRule>()
                .HasData(
                    mastersOfUndeath
                );
            #endregion

            #region Races
            var shamblingUndead = new Race(1, "Shambling Undead")
            {
                CostOfReRolls = 70000,
                MaxApothecarys = 0,
            };

            var snotling = new Race(2, "Snotling")
            {
                CostOfBribes = 50000,
                MaxRiotousRookies = 1,
            };

            var amazon = new Race(3, "Amazon");
            
            var blackOrc = new Race(4, "Black Orc")
            {
                CostOfBribes = 50000
            };

            var chaosChosen = new Race(5, "Choas Chosen");
            
            var chaosDwarf = new Race(6, "Chaos Dwarf")
            {
                CostOfReRolls = 70000
            };

            var chaosRenegade = new Race(7, "Chaos Renegade")
            {
                CostOfReRolls = 70000
            };

            var daemonsOfKhorne = new Race(8, "Daemons of Khorne")
            {
                CostOfReRolls = 70000
            };

            var darkElf = new Race(9, "Dark Elf")
            {
                CostOfReRolls = 50000
            };

            var dwarf = new Race(10, "Dwarf")
            {
                CostOfReRolls = 50000
            };

            var elvenUnion = new Race(11, "Elven Union")
            {
                CostOfReRolls = 50000
            };

            var goblin = new Race(12, "Goblin")
            {
                CostOfBribes = 50000
            };

            var halfling = new Race(13, "Halfling")
            {
                CostOfMasterChef = 100000
            };

            var highElf = new Race(14, "High Elf")
            {
                CostOfReRolls = 50000
            };

            var human = new Race(15, "Human")
            {
                CostOfReRolls = 50000
            };

            var imperialNobility = new Race(16, "Imperial Nobility")
            {
                CostOfReRolls = 70000
            };

            var khorne = new Race(17, "Khorne");
            
            var lizardmen = new Race(18, "Lizardmen")
            {
                CostOfReRolls = 70000
            };

            var necromanticHorror = new Race(19, "Necromantic Horror")
            {
                CostOfReRolls = 70000,
                MaxApothecarys = 0
            };

            var norse = new Race(20, "Norse");

            var nurgle = new Race(21, "Nurgle")
            {
                CostOfReRolls = 70000,
                MaxApothecarys = 0
            };

            var ogre = new Race(22, "Ogre")
            {
                CostOfReRolls = 70000,
                MaxRiotousRookies = 1
            };
            var oldWorldAlliance = new Race(23, "Old World Alliance")
            {
                CostOfReRolls = 70000
            };

            var orc = new Race(24, "Orc");
            
            var skaven = new Race(25, "Skaven")
            {
                CostOfReRolls = 50000
            };

            var slann = new Race(26, "Slann")
            {
                CostOfReRolls = 50000
            };

            var tombKings = new Race(27, "Tomb Kings")
            {
                CostOfReRolls = 70000,
                MaxApothecarys = 0
            };

            var underworldDenizens = new Race(28, "Underworld Denizens")
            {
                CostOfReRolls = 70000,
                CostOfBribes = 50000
            };
            
            var vampire = new Race(29, "Vampire")
            {
                CostOfReRolls = 70000
            };
            
            var woodElf = new Race(30, "Wood Elf");

            modelBuilder.Entity<Race>()
                .HasMany(r => r.Players)
                .WithMany(p => p.Races)
                .UsingEntity(j => j
                    .ToTable("PlayerRace")
                    //Add players to race here
                    .HasData(
                        new { PlayersId = ghoulRunner.Id, RacesId = shamblingUndead.Id },
                        new { PlayersId = mummy.Id, RacesId = shamblingUndead.Id }
                    ))
                .HasMany(r => r.SpecialRules)
                .WithMany(sr => sr.Races)
                .UsingEntity(j => j
                    .ToTable("RaceSpecialRule")
                    //Add special rule to race here
                    .HasData(
                        new { RacesId = shamblingUndead.Id, SpecialRulesId = mastersOfUndeath.Id }
                    ))
                .HasData(
                    amazon,
                    blackOrc,
                    chaosChosen,
                    chaosDwarf,
                    chaosRenegade,
                    daemonsOfKhorne,
                    darkElf,
                    dwarf,
                    elvenUnion,
                    goblin,
                    halfling,
                    highElf,
                    human,
                    imperialNobility,
                    khorne,
                    lizardmen,
                    necromanticHorror,
                    norse,
                    nurgle,
                    ogre,
                    oldWorldAlliance,
                    orc,
                    shamblingUndead,
                    skaven,
                    slann,
                    snotling,
                    tombKings,
                    underworldDenizens,
                    vampire,
                    woodElf
                );
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
