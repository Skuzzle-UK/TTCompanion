using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TTCompanion.API.FantasyFootball.Models.SpecialRule;
using TTCompanion.API.FantasyFootball.Services;
using TTCompanion.API.FantasyFootball.Services.Race;
using TTCompanion.API.FantasyFootball.Services.SpecialRule;

namespace TTCompanion.API.FantasyFootball.Controllers
{
    [Route("ttcompanion.api/fantasyfootball")]
    [Authorize]
    [ApiController]
    public class SpecialRulesController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly ISpecialRuleRepository _specialRuleRepository;
        private readonly IRaceRepository _raceRepository;
        private readonly IMapper _mapper;
        const int maxSpecialRulesPageSize = 100;

        public SpecialRulesController(IRepository repository, ISpecialRuleRepository specialRuleRepository, IRaceRepository raceRepository, IMapper mapper)
        {
            this._repository = repository;
            _specialRuleRepository = specialRuleRepository;
            _raceRepository = raceRepository;
            _mapper = mapper;
        }

        [HttpGet("specialrules", Name = "Get Special Rules")]
        public async Task<ActionResult<IEnumerable<SpecialRuleDto>>> GetSpecialRules(int? raceId, string? name, string? searchQuery, int pageSize = 30, int pageNumber = 1)
        {
            if(pageSize > maxSpecialRulesPageSize)
            {
                pageSize = maxSpecialRulesPageSize;
            }

            if (raceId != null && !await _raceRepository.RaceExistsAsync(raceId!.Value))
            {
                return NotFound($"raceId: {raceId} does not exist");
            }

            var (specialRules, paginationMetadata) = await _specialRuleRepository.GetSpecialRulesAsync(raceId, name, searchQuery, pageSize, pageNumber);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<SpecialRuleDto>>(specialRules));
        }

        [HttpGet("specialrules/{specialRuleId}")]
        public async Task<ActionResult<SpecialRuleDto>> GetSpecialRuleById(int specialRuleId)
        {
            var specialRule = await _specialRuleRepository.GetSpecialRuleByIdAsync(specialRuleId);
            if (specialRule == null)
            {
                return NotFound($"specialRuleId: {specialRuleId} does not exist");
            }

            return Ok(_mapper.Map<SpecialRuleDto>(specialRule));
        }

        [HttpPost("specialrules/{specialRuleId}", Name = "Update Special Rule")]
        public async Task<ActionResult> UpdateSpecialRule(int specialRuleId, SpecialRuleForUpdateDto specialRule)
        {
            var specialRuleEntity = await _specialRuleRepository.GetSpecialRuleByIdAsync(specialRuleId);
            if (specialRuleEntity == null)
            {
                return NotFound($"specialRuleId: {specialRuleId} does not exist");
            }

            if (!specialRuleEntity.Modifiable)
            {
                return Unauthorized($"specialRuleId: {specialRuleId} can not be modified");
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
                return NotFound($"specialRuleId: {specialRuleId} does not exist");
            }

            if (!specialRuleEntity.Modifiable)
            {
                return Unauthorized($"specialRuleId: {specialRuleId} can not be modified");
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
                return NotFound($"specialRuleId: {specialRuleId} does not exist");
            }

            if (!specialRuleEntity.Modifiable)
            {
                return Unauthorized($"specialRuleId: {specialRuleId} can not be modified");
            }

            _specialRuleRepository.DeleteSpecialRule(specialRuleEntity);
            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
