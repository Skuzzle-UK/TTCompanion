using TTCompanion.API.FantasyFootball.Models.Player;
using TTCompanion.API.FantasyFootball.Models.SpecialRule;

namespace TTCompanion.API.FantasyFootball.Models.Race
{
    public class RaceDto : RaceOnlyDto
    {
        public ICollection<PlayerDto> Players { get; set; } = new List<PlayerDto>();
        public ICollection<SpecialRuleDto> SpecialRules { get; set; } = new List<SpecialRuleDto>();
    }
}
