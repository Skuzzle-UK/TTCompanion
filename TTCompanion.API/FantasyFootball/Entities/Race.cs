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
        public int CostOfReRolls { get; set; }
        public int MaxReRolls { get; set; }
        public int CostOfBribes { get; set; }
        public int MaxBribes { get; set; }
        public int CostOfCheerleader { get; set; }
        public int MaxCheerleaders { get; set; }
        public int CostOfAssistantCoach { get; set; }
        public int MaxAssistantCoachs { get; set; }
        public int CostOfDedicatedFan { get; set; }
        public int MaxDedicatedFans { get; set; }
        public bool ApothecaryAvailable { get; set; }
        public int? CostOfApothercary { get; set; }
        public int CostOfBloodweiserKeg { get; set; }
        public int MaxBloodweiserKegs { get; set; }
        public int CostOfMasterChef { get; set; }
        public int MaxMasterChefs { get; set; }
        public ICollection<Player> Players { get; set; } = new List<Player>();
        public ICollection<SpecialRule> SpecialRules { get; set; } = new List<SpecialRule>();

        public Race(string name)
        {
            Name = name;
        }
    }
}
