using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TTCompanion.API.FantasyFootball.Models;

namespace TTCompanion.API.FantasyFootball.Controllers
{
    [Route("ttcompanion.api/fantasyfootball/teams/{teamId}/players")]
    [ApiController]
    public class FFPlayersController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<FFPlayer>> GetTeamPlayers(int teamId)
        {
            var team = FFDataStore.Instance.Teams.FirstOrDefault(t => t.Id == teamId);

            if(team == null)
            {
                return NotFound();
            }

            return Ok(team.Players);
        }

        [HttpGet("{playerId}")]
        public ActionResult<FFPlayer> GetPlayerById(int teamId, int playerId)
        {
            var team = FFDataStore.Instance.Teams.FirstOrDefault(t => t.Id == teamId);
            if (team == null)
            {
                return NotFound();
            }

            var player = team.Players.FirstOrDefault(p => p.Id == playerId);
            if(player == null)
            {
                return NotFound();
            }

            return Ok(player);
        }
    }
}
