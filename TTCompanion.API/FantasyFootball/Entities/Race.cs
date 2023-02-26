using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TTCompanion.API.FantasyFootball.Entities
{
    public class Race
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public int CostOfReRolls { get; set; } = 60000;
        public int MaxReRolls { get; set; } = 8;
        public int CostOfBribes { get; set; } = 100000;
        public int MaxBribes { get; set; } = 3;
        public int CostOfCheerleader { get; set; } = 10000;
        public int MaxCheerleaders { get; set; } = 12;
        public int CostOfAssistantCoach { get; set; } = 10000;
        public int MaxAssistantCoachs { get; set; } = 6;
        public int CostOfDedicatedFan { get; set; } = 10000;
        public int MaxDedicatedFans { get; set; } = 6;
        public int CostOfApothecary { get; set; } = 50000;
        public int MaxApothecarys { get; set; } = 1;
        public int CostOfBloodweiserKeg { get; set; } = 50000;
        public int MaxBloodweiserKegs { get; set; } = 2;
        public int CostOfMasterChef { get; set; } = 300000;
        public int MaxMasterChefs { get; set; } = 1;
        public int CostOfRiotousRookies { get; set; } = 100000;
        public int MaxRiotousRookies { get; set; } = 0;
        public ICollection<Player> Players { get; set; } = new List<Player>();
        public ICollection<SpecialRule> SpecialRules { get; set; } = new List<SpecialRule>();
        //public ICollection<RaceSpecialRule> SpecialRules { get; set; } = new List<RaceSpecialRule>();

        public bool Modifiable { get; } = true;
        internal Race(int id, string name)
        {
            Id = id;
            Name = name;
            Modifiable = false;
        }
        internal Race(string name)
        {
            Name = name;
        }
    }
}
