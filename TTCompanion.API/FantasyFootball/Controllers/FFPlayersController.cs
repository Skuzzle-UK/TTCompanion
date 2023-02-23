using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TTCompanion.API.FantasyFootball.Models.FFPlayer;
using TTCompanion.API.FantasyFootball.Services;
using TTCompanion.API.FantasyFootball.Services.Player;
using TTCompanion.API.FantasyFootball.Services.Race;

namespace TTCompanion.API.FantasyFootball.Controllers
{
    [Route("ttcompanion.api/fantasyfootball")]
    [ApiController]
    public class FFPlayersController : ControllerBase
    {
        private readonly IFFRepository _FFRepository;
        private readonly IFFPlayerRepository _FFPlayerRepository;
        private readonly IFFRaceRepository _FFRaceRepository;
        private readonly IMapper _mapper;

        public FFPlayersController(IFFRepository fFRepository, IFFPlayerRepository fFPlayerRepository, IFFRaceRepository fFRaceRepository, IMapper mapper)
        {
            _FFRepository = fFRepository;
            _FFPlayerRepository = fFPlayerRepository;
            _FFRaceRepository = fFRaceRepository;
            _mapper = mapper;
        }

        [HttpGet("players", Name = "GetPlayers")]
        public async Task<ActionResult<IEnumerable<FFPlayerDto>>> GetPlayersAsync()
        {
            var players = await _FFPlayerRepository.GetPlayersAsync();
            if (players == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<FFPlayerWithoutSkillsDto>>(players));
        }

        [HttpGet("players/includeskills", Name = "GetPlayersWithSkills")]
        public async Task<ActionResult<IEnumerable<FFPlayerDto>>> GetPlayersWithSkillsAsync()
        {
            var players = await _FFPlayerRepository.GetPlayersAsync(true);
            if (players == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<FFPlayerDto>>(players));
        }

        [HttpGet("players/{raceId}", Name = "GetPlayersForRace")]
        public async Task<ActionResult<IEnumerable<FFPlayerDto>>> GetPlayersForRaceAsync(int raceId)
        {
            if(!await _FFRaceRepository.RaceExistsAsync(raceId))
            {
                return NotFound();
            }

            var players = await _FFPlayerRepository.GetPlayersForRaceAsync(raceId);
            if(players == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<FFPlayerWithoutSkillsDto>>(players));
        }

        [HttpGet("players/{raceId}/includeskills", Name = "GetPlayersWithSkillsForRace")]
        public async Task<ActionResult<IEnumerable<FFPlayerDto>>> GetPlayersWithSkillsForRaceAsync(int raceId)
        {
            if (!await _FFRaceRepository.RaceExistsAsync(raceId))
            {
                return NotFound();
            }

            var players = await _FFPlayerRepository.GetPlayersForRaceAsync(raceId, true);
            if (players == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<FFPlayerDto>>(players));
        }

        [HttpGet("players/{playerId}", Name = "GetPlayerById")]
        public async Task<ActionResult<FFPlayerDto>> GetPlayerByIdAsync(int playerId)
        {
            if (!await _FFPlayerRepository.PlayerExistsAsync(playerId))
            {
                return NotFound();
            }

            var player = await _FFPlayerRepository.GetPlayerByIdAsync(playerId);

            return Ok(_mapper.Map<FFPlayerWithoutSkillsDto>(player));
        }

        [HttpGet("players/{playerId}/includeskills", Name = "GetPlayerWithSkillsById")]
        public async Task<ActionResult<FFPlayerDto>> GetPlayerWithSkillsByIdAsync(int playerId)
        {
            if (!await _FFPlayerRepository.PlayerExistsAsync(playerId))
            {
                return NotFound();
            }

            var player = await _FFPlayerRepository.GetPlayerByIdAsync(playerId, true);

            return Ok(_mapper.Map<FFPlayerDto>(player));
        }

        [HttpPost("players", Name = "CreatePlayer")]
        public async Task<ActionResult<FFPlayerDto>> CreatePlayer(int raceId, FFPlayerDto player)
        {
            if (!await _FFRaceRepository.RaceExistsAsync(raceId))
            {
                return NotFound();
            }

            var finalPlayer = _mapper.Map<Entities.FFPlayer>(player);

            await _FFPlayerRepository.AddPlayerForRaceAsync(raceId, finalPlayer);

            await _FFRepository.SaveChangesAsync();

            var createdPlayerToReturn = _mapper.Map<FFPlayerWithoutSkillsDto>(finalPlayer);

            return CreatedAtRoute("GetPlayerById",
                new
                {
                    raceId = raceId,
                    playerId = createdPlayerToReturn.Id
                },
                createdPlayerToReturn);
        }

        [HttpPost("players/{playerId}", Name = "UpdatePlayer")]
        public async Task<ActionResult> UpdatePlayer(int playerId, FFPlayerForUpdateDto player)
        {
            var playerEntity = await _FFPlayerRepository.GetPlayerByIdAsync(playerId);
            if(playerEntity== null)
            {
                return NotFound();
            }

            _mapper.Map(player, playerEntity);
            await _FFRepository.SaveChangesAsync();
            
            return NoContent();
        }

        [HttpPatch("players/{playerId}", Name = "PartiallyUpdatePlayer")]
        public async Task<ActionResult> PartiallyUpdatePlayer(int playerId, JsonPatchDocument<FFPlayerForUpdateDto> patchDocument)
        {
            var playerEntity = await _FFPlayerRepository.GetPlayerByIdAsync(playerId);
            if (playerEntity == null)
            {
                return NotFound();
            }

            var playerToPatch = _mapper.Map<FFPlayerForUpdateDto>(playerEntity);
            
            patchDocument.ApplyTo(playerToPatch, ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!TryValidateModel(playerToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(playerToPatch, playerEntity);

            await _FFRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("players/{playerId}", Name = "DeletePlayer")]
        public async Task<ActionResult> DeletePlayer(int playerId)
        {

            var playerEntity = await _FFPlayerRepository.GetPlayerByIdAsync(playerId);
            if (playerEntity == null)
            {
                return NotFound();
            }

            _FFPlayerRepository.DeletePlayer(playerEntity);
            await _FFRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
