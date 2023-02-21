using Microsoft.AspNetCore.Mvc;
using TTCompanion.API.FantasyFootball.Models;

namespace TTCompanion.API.FantasyFootball.Controllers
{
    [ApiController]
    [Route("ttcompanion.api/fantasyfootball/teams")]
    public class FFRacesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<FFRace>> GetAllTeams()
        {
            return Ok(FFDataStore.Instance.Teams);
        }

        [HttpGet("{id}")]
        public ActionResult<FFRace> GetTeamById(int id)
        {
            var teamToReturn = FFDataStore.Instance.Teams.FirstOrDefault(t => t.Id == id);

            if (teamToReturn == null)
            {
                return NotFound();

            }

            return Ok(teamToReturn);
        }
    }
}
