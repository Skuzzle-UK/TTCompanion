using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TTCompanion.API.FantasyFootball.Models.FFPlayer;
using TTCompanion.API.FantasyFootball.Models.FFSpecialRule;
using TTCompanion.API.FantasyFootball.Services.Player;
using TTCompanion.API.FantasyFootball.Services.Race;
using TTCompanion.API.FantasyFootball.Services;
using TTCompanion.API.FantasyFootball.Services.SpecialRule;
using System.Diagnostics;
using Microsoft.AspNetCore.JsonPatch;

namespace TTCompanion.API.FantasyFootball.Controllers
{
    [Route("ttcompanion.api/fantasyfootball")]
    [ApiController]
    public class FFSpecialRulesController : ControllerBase
    {
        private readonly IFFRepository _FFRepository;
        private readonly IFFSpecialRuleRepository _FFSpecialRuleRepository;
        private readonly IFFRaceRepository _FFRaceRepository;
        private readonly IMapper _mapper;

        public FFSpecialRulesController(IFFRepository fFRepository, IFFSpecialRuleRepository fFSpecialRuleRepository, IFFRaceRepository fFRaceRepository, IMapper mapper)
        {
            _FFRepository = fFRepository;
            _FFSpecialRuleRepository = fFSpecialRuleRepository;
            _FFRaceRepository = fFRaceRepository;
            _mapper = mapper;
        }

        [HttpGet("specialrules", Name = "Get Special Rules")]
        public async Task<ActionResult<IEnumerable<FFSpecialRuleDto>>> GetSpecialRules()
        {
            var specialRules = await _FFSpecialRuleRepository.GetSpecialRulesAsync();
            if (specialRules == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<FFSpecialRuleDto>>(specialRules));
        }

        [HttpGet("races/{raceId}/specialrules", Name = "Get Special Rules For Race")]
        public async Task<ActionResult<IEnumerable<FFSpecialRuleDto>>> GetSpecialRulesForRace(int raceId)
        {
            var race = await _FFRaceRepository.GetRaceByIdAsync(raceId);
            if (race == null)
            {
                return NotFound();
            }

            var specialRules = await _FFSpecialRuleRepository.GetSpecialRulesForRaceAsync(raceId);
            if (specialRules == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<FFSpecialRuleDto>>(specialRules));
        }

        [HttpGet("specialrules/{specialRuleId}")]
        public async Task<ActionResult<FFPlayerDto>> GetSpecialRuleById(int specialRuleId)
        {
            var race = await _FFRaceRepository.GetRaceByIdAsync(specialRuleId);
            if (race == null)
            {
                return NotFound();
            }

            var specialRule = await _FFSpecialRuleRepository.GetSpecialRuleByIdAsync(specialRuleId);
            if (specialRule == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<FFSpecialRuleDto>(specialRule));
        }

        [HttpPost("specialrules/{specialRuleId}", Name = "Update Special Rule")]
        public async Task<ActionResult> UpdateSpecialRule(int specialRuleId, FFSpecialRuleForUpdateDto specialRule)
        {
            var specialRuleEntity = await _FFSpecialRuleRepository.GetSpecialRuleByIdAsync(specialRuleId);
            if (specialRuleEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(specialRule, specialRuleEntity);
            await _FFRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("specialrules/{specialRuleId}", Name = "Partially Update Special Rule")]
        public async Task<ActionResult> PartiallyUpdateSpecialRule(int specialRuleId, JsonPatchDocument<FFSpecialRuleForUpdateDto> patchDocument)
        {
            var specialRuleEntity = await _FFSpecialRuleRepository.GetSpecialRuleByIdAsync(specialRuleId);
            if (specialRuleEntity == null)
            {
                return NotFound();
            }

            var specialRuleToPatch = _mapper.Map<FFSpecialRuleForUpdateDto>(specialRuleEntity);

            patchDocument.ApplyTo(specialRuleToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(specialRuleToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(specialRuleToPatch, specialRuleEntity);

            await _FFRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("specialrules/{specialRuleId}", Name = "Delete Special Rule")]
        public async Task<ActionResult> DeleteSpecialRule(int specialRuleId)
        {

            var specialRuleEntity = await _FFSpecialRuleRepository.GetSpecialRuleByIdAsync(specialRuleId);
            if (specialRuleEntity == null)
            {
                return NotFound();
            }

            _FFSpecialRuleRepository.DeleteSpecialRule(specialRuleEntity);
            await _FFRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
