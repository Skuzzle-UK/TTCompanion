using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TTCompanion.API.FantasyFootball.Models.Player;
using TTCompanion.API.FantasyFootball.Models.SpecialRule;
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
    public class SpecialRulesController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly ISpecialRuleRepository _specialRuleRepository;
        private readonly IRaceRepository _raceRepository;
        private readonly IMapper _mapper;

        public SpecialRulesController(IRepository repository, ISpecialRuleRepository specialRuleRepository, IRaceRepository raceRepository, IMapper mapper)
        {
            this._repository = repository;
            _specialRuleRepository = specialRuleRepository;
            _raceRepository = raceRepository;
            _mapper = mapper;
        }

        [HttpGet("specialrules", Name = "Get Special Rules")]
        public async Task<ActionResult<IEnumerable<SpecialRuleDto>>> GetSpecialRules(int? raceId)
        {
            var specialRules = await _specialRuleRepository.GetSpecialRulesAsync(raceId);
            if (specialRules == null || specialRules.Count() <= 0)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<SpecialRuleDto>>(specialRules));
        }

        [HttpGet("specialrules/{specialRuleId}")]
        public async Task<ActionResult<SpecialRuleDto>> GetSpecialRuleById(int specialRuleId)
        {
            var specialRule = await _specialRuleRepository.GetSpecialRuleByIdAsync(specialRuleId);
            if (specialRule == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<SpecialRuleDto>(specialRule));
        }

        [HttpPost("specialrules/{specialRuleId}", Name = "Update Special Rule")]
        public async Task<ActionResult> UpdateSpecialRule(int specialRuleId, SpecialRuleForUpdateDto specialRule)
        {
            var specialRuleEntity = await _specialRuleRepository.GetSpecialRuleByIdAsync(specialRuleId);
            if (specialRuleEntity == null)
            {
                return NotFound();
            }

            if (!specialRuleEntity.Modifiable)
            {
                return Unauthorized();
            }

            _mapper.Map(specialRule, specialRuleEntity);
            await _repository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("specialrules/{specialRuleId}", Name = "Partially Update Special Rule")]
        public async Task<ActionResult> PartiallyUpdateSpecialRule(int specialRuleId, JsonPatchDocument<SpecialRuleForUpdateDto> patchDocument)
        {
            var specialRuleEntity = await _specialRuleRepository.GetSpecialRuleByIdAsync(specialRuleId);
            if (specialRuleEntity == null)
            {
                return NotFound();
            }

            if (!specialRuleEntity.Modifiable)
            {
                return Unauthorized();
            }

            var specialRuleToPatch = _mapper.Map<SpecialRuleForUpdateDto>(specialRuleEntity);

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

            await _repository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("specialrules/{specialRuleId}", Name = "Delete Special Rule")]
        public async Task<ActionResult> DeleteSpecialRule(int specialRuleId)
        {
            var specialRuleEntity = await _specialRuleRepository.GetSpecialRuleByIdAsync(specialRuleId);
            if (specialRuleEntity == null)
            {
                return NotFound();
            }

            if (!specialRuleEntity.Modifiable)
            {
                return Unauthorized();
            }

            _specialRuleRepository.DeleteSpecialRule(specialRuleEntity);
            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
