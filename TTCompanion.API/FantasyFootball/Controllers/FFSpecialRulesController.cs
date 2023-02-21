using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TTCompanion.API.FantasyFootball.Models;

namespace TTCompanion.API.FantasyFootball.Controllers
{
    [Route("ttcompanion.api/fantasyfootball/teams/{teamId}/specialrules")]
    [ApiController]
    public class FFSpecialRulesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<FFPlayer>> GetTeamSpecialRules(int teamId)
        {
            var team = FFDataStore.Instance.Teams.FirstOrDefault(t => t.Id == teamId);

            if (team == null)
            {
                return NotFound();
            }

            return Ok(team.SpecialRules);
        }

        [HttpGet("{specialRuleId}")]
        public ActionResult<FFPlayer> GetSpecialRuleById(int teamId, int specialRuleId)
        {
            var team = FFDataStore.Instance.Teams.FirstOrDefault(t => t.Id == teamId);
            if (team == null)
            {
                return NotFound();
            }

            var specialRule = team.SpecialRules.FirstOrDefault(sr => sr.Id == specialRuleId);
            if (specialRule == null)
            {
                return NotFound();
            }

            return Ok(specialRule);
        }
    }
}
