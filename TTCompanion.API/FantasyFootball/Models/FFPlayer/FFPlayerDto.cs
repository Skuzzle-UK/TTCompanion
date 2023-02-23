using TTCompanion.API.FantasyFootball.Models.FFSkill;

namespace TTCompanion.API.FantasyFootball.Models.FFPlayer
{
    public class FFPlayerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int MA { get; set; }
        public int ST { get; set; }
        public int AG { get; set; }
        public int? PA { get; set; }
        public int AV { get; set; }
        public int Cost { get; set; }
        public ICollection<FFSkillDto> Skills { get; set; } = new List<FFSkillDto>();
    }
}
