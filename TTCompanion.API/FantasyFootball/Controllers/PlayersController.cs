using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TTCompanion.API.FantasyFootball.Models.Player;
using TTCompanion.API.FantasyFootball.Services;
using TTCompanion.API.FantasyFootball.Services.Player;
using TTCompanion.API.FantasyFootball.Services.Race;

namespace TTCompanion.API.FantasyFootball.Controllers
{
    [ApiController]
    [Route("ttcompanion.api/fantasyfootball")]
    public class PlayersController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IRaceRepository _raceRepository;
        private readonly IMapper _mapper;

        public PlayersController(IRepository repository, IPlayerRepository playerRepository, IRaceRepository raceRepository, IMapper mapper)
        {
            _repository = repository;
            _playerRepository = playerRepository;
            _raceRepository = raceRepository;
            _mapper = mapper;
        }

        [HttpGet("players", Name = "Get Players")]
        public async Task<ActionResult<IEnumerable<PlayerDto>>> GetPlayersAsync()
        {
            var players = await _playerRepository.GetPlayersAsync();
            if (players == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<PlayerWithoutSkillsDto>>(players));
        }

        [HttpGet("players/includeskills", Name = "Get Players With Skills")]
        public async Task<ActionResult<IEnumerable<PlayerDto>>> GetPlayersWithSkillsAsync()
        {
            var players = await _playerRepository.GetPlayersAsync(true);
            if (players == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<PlayerDto>>(players));
        }

        [HttpGet("race/{raceId}/players", Name = "Get Players For Race")]
        public async Task<ActionResult<IEnumerable<PlayerDto>>> GetPlayersForRaceAsync(int raceId)
        {
            if(!await _raceRepository.RaceExistsAsync(raceId))
            {
                return NotFound();
            }

            var players = await _playerRepository.GetPlayersForRaceAsync(raceId);
            if(players == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<PlayerWithoutSkillsDto>>(players));
        }

        [HttpGet("race/{raceId}/players/includeskills", Name = "Get Players With Skills For Race")]
        public async Task<ActionResult<IEnumerable<PlayerDto>>> GetPlayersWithSkillsForRaceAsync(int raceId)
        {
            if (!await _raceRepository.RaceExistsAsync(raceId))
            {
                return NotFound();
            }

            var players = await _playerRepository.GetPlayersForRaceAsync(raceId, true);
            if (players == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<PlayerDto>>(players));
        }

        [HttpGet("players/{playerId}", Name = "Get Player By Id")]
        public async Task<ActionResult<PlayerDto>> GetPlayerByIdAsync(int playerId)
        {
            if (!await _playerRepository.PlayerExistsAsync(playerId))
            {
                return NotFound();
            }

            var player = await _playerRepository.GetPlayerByIdAsync(playerId);

            return Ok(_mapper.Map<PlayerWithoutSkillsDto>(player));
        }

        [HttpGet("players/{playerId}/includeskills", Name = "Get Player With Skills By Id")]
        public async Task<ActionResult<PlayerDto>> GetPlayerWithSkillsByIdAsync(int playerId)
        {
            if (!await _playerRepository.PlayerExistsAsync(playerId))
            {
                return NotFound();
            }

            var player = await _playerRepository.GetPlayerByIdAsync(playerId, true);

            return Ok(_mapper.Map<PlayerDto>(player));
        }

        [HttpPost("players", Name = "Create Player")]
        public async Task<ActionResult<PlayerDto>> CreatePlayer(int raceId, PlayerDto player)
        {
            if (!await _raceRepository.RaceExistsAsync(raceId))
            {
                return NotFound();
            }

            var finalPlayer = _mapper.Map<Entities.Player>(player);

            await _playerRepository.AddPlayerForRaceAsync(raceId, finalPlayer);

            await _repository.SaveChangesAsync();

            var createdPlayerToReturn = _mapper.Map<PlayerDto>(finalPlayer);

            return CreatedAtRoute("Get Player By Id",
                new
                {
                    raceId = raceId,
                    playerId = createdPlayerToReturn.Id
                },
                createdPlayerToReturn);
        }

        [HttpPost("players/{playerId}", Name = "Update Player")]
        public async Task<ActionResult> UpdatePlayer(int playerId, PlayerForUpdateDto player)
        {
            var playerEntity = await _playerRepository.GetPlayerByIdAsync(playerId);
            if(playerEntity== null)
            {
                return NotFound();
            }

            _mapper.Map(player, playerEntity);
            await _repository.SaveChangesAsync();
            
            return NoContent();
        }

        [HttpPatch("players/{playerId}", Name = "Partially Update Player")]
        public async Task<ActionResult> PartiallyUpdatePlayer(int playerId, JsonPatchDocument<PlayerForUpdateDto> patchDocument)
        {
            var playerEntity = await _playerRepository.GetPlayerByIdAsync(playerId);
            if (playerEntity == null)
            {
                return NotFound();
            }

            var playerToPatch = _mapper.Map<PlayerForUpdateDto>(playerEntity);
            
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

            await _repository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("players/{playerId}", Name = "Delete Player")]
        public async Task<ActionResult> DeletePlayer(int playerId)
        {

            var playerEntity = await _playerRepository.GetPlayerByIdAsync(playerId);
            if (playerEntity == null)
            {
                return NotFound();
            }

            _playerRepository.DeletePlayer(playerEntity);
            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
