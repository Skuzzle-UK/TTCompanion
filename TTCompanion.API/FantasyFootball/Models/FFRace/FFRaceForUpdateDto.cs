using System.ComponentModel.DataAnnotations;

namespace TTCompanion.API.FantasyFootball.Models.FFRace
{
    public class FFRaceForUpdateDto
    {
        [Required(ErrorMessage = "You should provide a name value.")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
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
        public bool ApothecaryAvailable { get; set; } = false;
        public int? CostOfApothercary { get; set; } = null;
        public int CostOfBloodweiserKeg { get; set; } = 50000;
        public int MaxBloodweiserKegs { get; set; } = 2;
        public int CostOfMasterChef { get; set; } = 300000;
        public int MaxMasterChefs { get; set; } = 1;
    }
}
