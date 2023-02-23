using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TTCompanion.API.FantasyFootball.Models;
using TTCompanion.API.FantasyFootball.Services;

namespace TTCompanion.API.FantasyFootball.Controllers
{
    [Route("ttcompanion.api/fantasyfootball/teams/{teamId}/players")]
    [ApiController]
    public class FFPlayersController : ControllerBase
    {
        private readonly IFFRepository _FFReposity;
        private readonly IMapper _mapper;

        public FFPlayersController(IFFRepository fFRepository, IMapper mapper)
        {
            _FFReposity = fFRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FFPlayerDto>>> GetPlayersForRaceAsync(int teamId)
        {
            if(!await _FFReposity.RaceExistsAsync(teamId))
            {
                return NotFound();
            }

            var players = await _FFReposity.GetPlayersForRaceAsync(teamId);
            if(players == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<FFPlayerDto>>(players));
        }
        //@TODO Continue from here using repository and mapper

        [HttpGet("{playerId}")]
        public ActionResult<FFPlayerDto> GetPlayerById(int teamId, int playerId)
        {
            var team = FFDataStore.Instance.Races.FirstOrDefault(t => t.Id == teamId);
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
