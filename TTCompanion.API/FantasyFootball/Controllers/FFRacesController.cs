using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TTCompanion.API.FantasyFootball.Models.FFRace;
using TTCompanion.API.FantasyFootball.Services;
using TTCompanion.API.FantasyFootball.Services.Race;

namespace TTCompanion.API.FantasyFootball.Controllers
{
    [ApiController]
    [Route("ttcompanion.api/fantasyfootball")]
    public class FFRacesController : ControllerBase
    {
        private readonly IFFRepository _FFRepository;
        private readonly IFFRaceRepository _FFRaceRepository;
        private readonly IMapper _mapper;

        public FFRacesController(IFFRepository fFRepository, IFFRaceRepository fFRaceRepository, IMapper mapper)
        {
            _FFRepository = fFRepository;
            _FFRaceRepository = fFRaceRepository;
            _mapper = mapper;
        }

        [HttpGet("races", Name = "Get Races")]
        public async Task<ActionResult<IEnumerable<FFRaceOnlyDto>>> GetRacesAsync()
        {
            var races = await _FFRaceRepository.GetRacesAsync();
            if(races == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<FFRaceOnlyDto>>(races));
        }

        [HttpGet("races/includeplayers", Name = "Get Races With Players")]
        public async Task<ActionResult<IEnumerable<FFRaceWithoutSpecialRulesDto>>> GetRacesWithPlayersAsync()
        {
            var races = await _FFRaceRepository.GetRacesAsync(false, true);
            if (races == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<FFRaceWithoutSpecialRulesDto>>(races));
        }

        [HttpGet("races/includespecialrules", Name = "Get Races With Special Rules")]
        public async Task<ActionResult<IEnumerable<FFRaceWithoutPlayersDto>>> GetRacesWithSpecialRulesAsync()
        {
            var races = await _FFRaceRepository.GetRacesAsync(true, false);
            if (races == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<FFRaceWithoutPlayersDto>>(races));
        }

        [HttpGet("races/includeall", Name = "Get Races With All")]
        public async Task<ActionResult<IEnumerable<FFRaceDto>>> GetRacesWithAllAsync()
        {
            var races = await _FFRaceRepository.GetRacesAsync(true, true);
            if (races == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<FFRaceDto>>(races));
        }

        [HttpGet("races/{raceId}", Name = "Get Race By Id")]
        public async Task<ActionResult<FFRaceOnlyDto>> GetRaceByIdAsync(int raceId)
        {
            var race = await _FFRaceRepository.GetRaceByIdAsync(raceId);
            if (race == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<FFRaceOnlyDto>(race));
        }

        [HttpGet("races/{raceId}/includeplayers", Name = "Get Race With Players By Id")]
        public async Task<ActionResult<FFRaceWithoutSpecialRulesDto>> GetRaceWithPlayersByIdAsync(int raceId)
        {
            var race = await _FFRaceRepository.GetRaceByIdAsync(raceId, false, true);
            if (race == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<FFRaceWithoutSpecialRulesDto>(race));
        }

        [HttpGet("races/{raceId}/includespecialrules", Name = "Get Race With Special Rules By Id")]
        public async Task<ActionResult<FFRaceWithoutPlayersDto>> GetRaceWithSpecialRulesByIdAsync(int raceId)
        {
            var race = await _FFRaceRepository.GetRaceByIdAsync(raceId, true, false);
            if (race == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<FFRaceWithoutPlayersDto>(race));
        }

        [HttpGet("races/{raceId}/includeall", Name = "Get Race With All By Id")]
        public async Task<ActionResult<FFRaceDto>> GetRaceWithAllByIdAsync(int raceId)
        {
            var race = await _FFRaceRepository.GetRaceByIdAsync(raceId, true, true);
            if (race == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<FFRaceDto>(race));
        }

        [HttpPost("races", Name = "Create Race")]
        public async Task<ActionResult<FFRaceDto>> CreateRace(FFRaceDto race)
        {
            var finalRace = _mapper.Map<Entities.FFRace>(race);
            await _FFRaceRepository.AddRaceAsync(finalRace);
            await _FFRepository.SaveChangesAsync();

            var createdRaceToReturn = _mapper.Map<FFRaceDto>(finalRace);

            return CreatedAtRoute("Get Race By Id",
                new
                {
                    raceId = createdRaceToReturn.Id
                },
                createdRaceToReturn);
        }

        [HttpPost("races/{raceId}", Name = "Update Race")]
        public async Task<ActionResult> UpdateRace(int raceId, FFRaceForUpdateDto race)
        {
            var raceEntity = await _FFRaceRepository.GetRaceByIdAsync(raceId);
            if (raceEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(race, raceEntity);
            await _FFRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("races/{raceId}", Name = "Partially Update Race")]
        public async Task<ActionResult> PartiallyUpdateRace(int raceId, JsonPatchDocument<FFRaceForUpdateDto> patchDocument)
        {
            var raceEntity = await _FFRaceRepository.GetRaceByIdAsync(raceId);
            if (raceEntity == null)
            {
                return NotFound();
            }

            var raceToPatch = _mapper.Map<FFRaceForUpdateDto>(raceEntity);

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

            await _FFRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("races/{raceId}", Name = "Delete Race")]
        public async Task<ActionResult> DeleteRace(int raceId)
        {

            var raceEntity = await _FFRaceRepository.GetRaceByIdAsync(raceId);
            if (raceEntity == null)
            {
                return NotFound();
            }

            _FFRaceRepository.DeleteRace(raceEntity);
            await _FFRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
