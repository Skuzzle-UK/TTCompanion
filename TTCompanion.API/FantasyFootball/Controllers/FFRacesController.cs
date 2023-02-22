using Microsoft.AspNetCore.Mvc;
using TTCompanion.API.FantasyFootball.Models;

namespace TTCompanion.API.FantasyFootball.Controllers
{
    [ApiController]
    [Route("ttcompanion.api/fantasyfootball/teams")]
    public class FFRacesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<FFRaceDto>> GetAllTeams()
        {
            return Ok(FFDataStore.Instance.Races);
        }

        [HttpGet("{id}")]
        public ActionResult<FFRaceDto> GetTeamById(int id)
        {
            var teamToReturn = FFDataStore.Instance.Races.FirstOrDefault(t => t.Id == id);

            if (teamToReturn == null)
            {
                return NotFound();

            }

            return Ok(teamToReturn);
        }
    }
}
