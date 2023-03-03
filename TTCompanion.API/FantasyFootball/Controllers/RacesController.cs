using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TTCompanion.API.FantasyFootball.Models.Race;
using TTCompanion.API.FantasyFootball.Services;
using TTCompanion.API.FantasyFootball.Services.Race;

namespace TTCompanion.API.FantasyFootball.Controllers
{
    [ApiController]
    [Authorize]
    [Route("ttcompanion.api/fantasyfootball")]
    public class RacesController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IRaceRepository _raceRepository;
        private readonly IMapper _mapper;
        const int maxRacesPageSize = 100;

        public RacesController(IRepository repository, IRaceRepository raceRepository, IMapper mapper)
        {
            _repository = repository;
            _raceRepository = raceRepository;
            _mapper = mapper;
        }

        [HttpGet("races", Name = "Get Races")]
        public async Task<ActionResult<IEnumerable<RaceDto>>> GetRacesAsync(
            string? name, string? searchQuery,
            bool includeSpecialRules = false,
            bool includePlayers = false,
            int pageSize = 30,
            int pageNumber = 1
            )
        {
            if (pageSize > maxRacesPageSize)
            {
                pageSize = maxRacesPageSize;
            }

            var (races, paginationMetadata) = await _raceRepository.GetRacesAsync(name, searchQuery, includeSpecialRules, includePlayers, pageSize, pageNumber);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            if (!includeSpecialRules && !includePlayers)
            {
                return Ok(_mapper.Map<IEnumerable<RaceOnlyDto>>(races));
            }
            if (includeSpecialRules && !includePlayers)
            {
                return Ok(_mapper.Map<IEnumerable<RaceWithSpecialRulesDto>>(races));
            }
            if (!includeSpecialRules && includePlayers)
            {
                return Ok(_mapper.Map<IEnumerable<RaceWithPlayersDto>>(races));
            }

            return Ok(_mapper.Map<IEnumerable<RaceDto>>(races));
        }

        [HttpGet("races/{raceId}", Name = "Get Race By Id")]
        public async Task<ActionResult<RaceDto>> GetRaceByIdAsync(int raceId, bool includeSpecialRules = false, bool includePlayers = false)
        {
            var race = await _raceRepository.GetRaceByIdAsync(raceId, includeSpecialRules, includePlayers);
            if (race == null)
            {
                return NotFound($"raceId: {raceId} does not exist.");
            }

            return Ok(_mapper.Map<RaceDto>(race));
        }

        [HttpPost("races", Name = "Create Race")]
        public async Task<ActionResult<RaceDto>> CreateRace(RaceDto race)
        {
            var finalRace = _mapper.Map<Entities.Race>(race);
            await _raceRepository.AddRaceAsync(finalRace);
            await _repository.SaveChangesAsync();

            var createdRaceToReturn = _mapper.Map<RaceDto>(finalRace);

            return CreatedAtRoute("Get Race By Id",
                new
                {
                    raceId = createdRaceToReturn.Id
                },
                createdRaceToReturn);
        }

        [HttpPost("races/{raceId}", Name = "Update Race")]
        public async Task<ActionResult> UpdateRace(int raceId, RaceForUpdateDto race)
        {
            var raceEntity = await _raceRepository.GetRaceByIdAsync(raceId);
            if (raceEntity == null)
            {
                return NotFound($"raceId: {raceId} does not exist.");
            }

            if (!raceEntity.Modifiable)
            {
                return Unauthorized($"raceId: {raceId} can not be modified");
            }

            _mapper.Map(race, raceEntity);
            await _repository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("races/{raceId}", Name = "Partially Update Race")]
        public async Task<ActionResult> PartiallyUpdateRace(int raceId, JsonPatchDocument<RaceForUpdateDto> patchDocument)
        {
            var raceEntity = await _raceRepository.GetRaceByIdAsync(raceId);
            if (raceEntity == null)
            {
                return NotFound($"raceId: {raceId} does not exist.");
            }

            if (!raceEntity.Modifiable)
            {
                return Unauthorized($"raceId: {raceId} can not be modified");
            }

            var raceToPatch = _mapper.Map<RaceForUpdateDto>(raceEntity);

            patchDocument.ApplyTo(raceToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(raceToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(raceToPatch, raceEntity);

            await _repository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("races/{raceId}", Name = "Delete Race")]
        public async Task<ActionResult> DeleteRace(int raceId)
        {

            var raceEntity = await _raceRepository.GetRaceByIdAsync(raceId);
            if (raceEntity == null)
            {
                return NotFound($"raceId: {raceId} does not exist.");
            }

            if (!raceEntity.Modifiable)
            {
                return Unauthorized($"raceId: {raceId} can not be modified");
            }

            _raceRepository.DeleteRace(raceEntity);
            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
