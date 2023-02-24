﻿using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TTCompanion.API.FantasyFootball.Models.Race;
using TTCompanion.API.FantasyFootball.Services;
using TTCompanion.API.FantasyFootball.Services.Race;

namespace TTCompanion.API.FantasyFootball.Controllers
{
    [ApiController]
    [Route("ttcompanion.api/fantasyfootball")]
    public class RacesController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IRaceRepository _raceRepository;
        private readonly IMapper _mapper;

        public RacesController(IRepository repository, IRaceRepository raceRepository, IMapper mapper)
        {
            _repository = repository;
            _raceRepository = raceRepository;
            _mapper = mapper;
        }

        [HttpGet("races", Name = "Get Races")]
        public async Task<ActionResult<IEnumerable<RaceOnlyDto>>> GetRacesAsync()
        {
            var races = await _raceRepository.GetRacesAsync();
            if(races == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<RaceOnlyDto>>(races));
        }

        [HttpGet("races/includeplayers", Name = "Get Races With Players")]
        public async Task<ActionResult<IEnumerable<RaceWithoutSpecialRulesDto>>> GetRacesWithPlayersAsync()
        {
            var races = await _raceRepository.GetRacesAsync(false, true);
            if (races == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<RaceWithoutSpecialRulesDto>>(races));
        }

        [HttpGet("races/includespecialrules", Name = "Get Races With Special Rules")]
        public async Task<ActionResult<IEnumerable<RaceWithoutPlayersDto>>> GetRacesWithSpecialRulesAsync()
        {
            var races = await _raceRepository.GetRacesAsync(true, false);
            if (races == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<RaceWithoutPlayersDto>>(races));
        }

        [HttpGet("races/includeall", Name = "Get Races With All")]
        public async Task<ActionResult<IEnumerable<RaceDto>>> GetRacesWithAllAsync()
        {
            var races = await _raceRepository.GetRacesAsync(true, true);
            if (races == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<RaceDto>>(races));
        }

        [HttpGet("races/{raceId}", Name = "Get Race By Id")]
        public async Task<ActionResult<RaceOnlyDto>> GetRaceByIdAsync(int raceId)
        {
            var race = await _raceRepository.GetRaceByIdAsync(raceId);
            if (race == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RaceOnlyDto>(race));
        }

        [HttpGet("races/{raceId}/includeplayers", Name = "Get Race With Players By Id")]
        public async Task<ActionResult<RaceWithoutSpecialRulesDto>> GetRaceWithPlayersByIdAsync(int raceId)
        {
            var race = await _raceRepository.GetRaceByIdAsync(raceId, false, true);
            if (race == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RaceWithoutSpecialRulesDto>(race));
        }

        [HttpGet("races/{raceId}/includespecialrules", Name = "Get Race With Special Rules By Id")]
        public async Task<ActionResult<RaceWithoutPlayersDto>> GetRaceWithSpecialRulesByIdAsync(int raceId)
        {
            var race = await _raceRepository.GetRaceByIdAsync(raceId, true, false);
            if (race == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RaceWithoutPlayersDto>(race));
        }

        [HttpGet("races/{raceId}/includeall", Name = "Get Race With All By Id")]
        public async Task<ActionResult<RaceDto>> GetRaceWithAllByIdAsync(int raceId)
        {
            var race = await _raceRepository.GetRaceByIdAsync(raceId, true, true);
            if (race == null)
            {
                return NotFound();
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
                return NotFound();
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
                return NotFound();
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
                return NotFound();
            }

            _raceRepository.DeleteRace(raceEntity);
            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}