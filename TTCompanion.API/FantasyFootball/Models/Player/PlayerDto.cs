using TTCompanion.API.FantasyFootball.Models.Skill;

namespace TTCompanion.API.FantasyFootball.Models.Player
{
    public class PlayerDto : PlayerOnlyDto
    {
        public ICollection<SkillDto> Skills { get; set; } = new List<SkillDto>();
    }
}
