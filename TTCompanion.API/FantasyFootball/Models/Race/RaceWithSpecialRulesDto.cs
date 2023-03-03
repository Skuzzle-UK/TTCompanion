using TTCompanion.API.FantasyFootball.Models.SpecialRule;

namespace TTCompanion.API.FantasyFootball.Models.Race
{
    public class RaceWithSpecialRulesDto : RaceOnlyDto
    {
        public ICollection<SpecialRuleDto> SpecialRules { get; set; } = new List<SpecialRuleDto>();
    }
}
