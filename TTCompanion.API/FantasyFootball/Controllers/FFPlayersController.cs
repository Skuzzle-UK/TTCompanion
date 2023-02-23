using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TTCompanion.API.FantasyFootball.Models;
using TTCompanion.API.FantasyFootball.Services;

namespace TTCompanion.API.FantasyFootball.Controllers
{
    [Route("ttcompanion.api/fantasyfootball")]
    [ApiController]
    public class FFPlayersController : ControllerBase
    {
        private readonly IFFRepository _FFRepository;
        private readonly IMapper _mapper;

        public FFPlayersController(IFFRepository fFRepository, IMapper mapper)
        {
            _FFRepository = fFRepository;
            _mapper = mapper;
        }

        [HttpGet("players", Name = "GetPlayers")]
        public async Task<ActionResult<IEnumerable<FFPlayerDto>>> GetPlayersAsync()
        {
            var players = await _FFRepository.GetPlayersAsync();
            if (players == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<Models.FFPlayerWithoutSkillsDto>>(players));
        }

        [HttpGet("players/includeskills", Name = "GetPlayersWithSkills")]
        public async Task<ActionResult<IEnumerable<FFPlayerDto>>> GetPlayersWithSkillsAsync()
        {
            var players = await _FFRepository.GetPlayersAsync(true);
            if (players == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<Models.FFPlayerDto>>(players));
        }

        [HttpGet("players/{raceId}", Name = "GetPlayersForRace")]
        public async Task<ActionResult<IEnumerable<FFPlayerDto>>> GetPlayersForRaceAsync(int raceId)
        {
            if(!await _FFRepository.RaceExistsAsync(raceId))
            {
                return NotFound();
            }

            var players = await _FFRepository.GetPlayersForRaceAsync(raceId);
            if(players == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<Models.FFPlayerWithoutSkillsDto>>(players));
        }

        [HttpGet("players/{raceId}/includeskills", Name = "GetPlayersWithSkillsForRace")]
        public async Task<ActionResult<IEnumerable<FFPlayerDto>>> GetPlayersWithSkillsForRaceAsync(int raceId)
        {
            if (!await _FFRepository.RaceExistsAsync(raceId))
            {
                return NotFound();
            }

            var players = await _FFRepository.GetPlayersForRaceAsync(raceId, true);
            if (players == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<Models.FFPlayerDto>>(players));
        }

        [HttpGet("players/{playerId}", Name = "GetPlayerById")]
        public async Task<ActionResult<FFPlayerDto>> GetPlayerByIdAsync(int playerId)
        {
            if (!await _FFRepository.PlayerExistsAsync(playerId))
            {
                return NotFound();
            }

            var player = await _FFRepository.GetPlayerByIdAsync(playerId);

            return Ok(_mapper.Map<Models.FFPlayerWithoutSkillsDto>(player));
        }

        [HttpGet("players/{playerId}/includeskills", Name = "GetPlayerWithSkillsById")]
        public async Task<ActionResult<FFPlayerDto>> GetPlayerWithSkillsByIdAsync(int playerId)
        {
            if (!await _FFRepository.PlayerExistsAsync(playerId))
            {
                return NotFound();
            }

            var player = await _FFRepository.GetPlayerByIdAsync(playerId, true);

            return Ok(_mapper.Map<Models.FFPlayerDto>(player));
        }

        [HttpPost("players", Name = "CreatePlayer")]
        public async Task<ActionResult<FFPlayerDto>> CreatePlayer(int raceId, FFPlayerDto player)
        {
            if (!await _FFRepository.RaceExistsAsync(raceId))
            {
                return NotFound();
            }

            var finalPlayer = _mapper.Map<Entities.FFPlayer>(player);

            await _FFRepository.AddPlayerForRaceAsync(raceId, finalPlayer);

            await _FFRepository.SaveChangesAsync();

            var createdPlayerToReturn = _mapper.Map<Models.FFPlayerWithoutSkillsDto>(finalPlayer);

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
            var playerEntity = await _FFRepository.GetPlayerByIdAsync(playerId);
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
            var playerEntity = await _FFRepository.GetPlayerByIdAsync(playerId);
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

            var playerEntity = await _FFRepository.GetPlayerByIdAsync(playerId);
            if (playerEntity == null)
            {
                return NotFound();
            }

            _FFRepository.DeletePlayer(playerEntity);
            await _FFRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
