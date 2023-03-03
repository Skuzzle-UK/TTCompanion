using TTCompanion.API.FantasyFootball.Models.Player;

namespace TTCompanion.API.FantasyFootball.Models.Race
{
    public class RaceWithPlayersDto : RaceOnlyDto
    {
        public ICollection<PlayerDto> Players { get; set; } = new List<PlayerDto>();
    }
}
