using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TTCompanion.API.FantasyFootball.Services.Player;
using TTCompanion.API.FantasyFootball.Services.Skill;
using TTCompanion.API.FantasyFootball.Services;
using TTCompanion.API.FantasyFootball.Models.Skill;
using TTCompanion.API.FantasyFootball.Models.SpecialRule;
using Microsoft.AspNetCore.JsonPatch;
using System.Text.Json;
using TTCompanion.API.FantasyFootball.Entities;
using Microsoft.AspNetCore.Authorization;

namespace TTCompanion.API.FantasyFootball.Controllers
{
    [Route("ttcompanion.api/fantasyfootball")]
    [Authorize]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly ISkillRepository _skillRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;
        const int maxSkillsPageSize = 100;

        public SkillsController(IRepository repository, ISkillRepository skillRepository, IPlayerRepository playerRepository, IMapper mapper)
        {
            _repository= repository;
            _skillRepository = skillRepository;
            _playerRepository = playerRepository;
            _mapper = mapper;
        }

        [HttpGet("skills", Name = "Get Skills")]
        public async Task<ActionResult<IEnumerable<SkillDto>>> GetSkillsAsync(int? playerId, string? name, string? searchQuery, int pageSize = 30, int pageNumber = 1)
        {
            if(pageSize > maxSkillsPageSize)
            {
                pageSize= maxSkillsPageSize;
            }

            if (playerId != null && !await _playerRepository.PlayerExistsAsync(playerId!.Value))
            {
                return NotFound($"playerId: {playerId} does not exist.");
            }

            var (skills, paginationMetadata) = await _skillRepository.GetSkillsAsync(playerId, name, searchQuery, pageNumber, pageSize);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<SkillDto>>(skills));
        }

        [HttpGet("skills/{skillId}")]
        public async Task<ActionResult<SkillDto>> GetSkillById(int skillId)
        {
            var skill = await _skillRepository.GetSkillByIdAsync(skillId);
            if (skill == null)
            {
                return NotFound($"skillId: {skillId} does not exist.");
            }

            return Ok(_mapper.Map<SkillDto>(skill));
        }

        [HttpPost("skills/{skillId}", Name = "Update Skill")]
        public async Task<ActionResult> UpdateSkill(int skillId, SkillForUpdateDto skill)
        {
            var skillEntity = await _skillRepository.GetSkillByIdAsync(skillId);
            if (skillEntity == null)
            {
                return NotFound($"skillId: {skillId} does not exist.");
            }

            if (!skillEntity.Modifiable)
            {
                return Unauthorized($"skillId: {skillId} can not be modified.");
            }

            _mapper.Map(skill, skillEntity);
            await _repository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("skills/{skillId}", Name = "Partially Update Skills")]
        public async Task<ActionResult> PartiallyUpdateSkills(int skillId, JsonPatchDocument<SkillForUpdateDto> patchDocument)
        {
            var skillEntity = await _skillRepository.GetSkillByIdAsync(skillId);
            if (skillEntity == null)
            {
                return NotFound($"skillId: {skillId} does not exist.");
            }

            if (!skillEntity.Modifiable)
            {
                return Unauthorized($"skillId: {skillId} can not be modified.");
            }

            var skillToPatch = _mapper.Map<SkillForUpdateDto>(skillEntity);

            patchDocument.ApplyTo(skillToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(skillToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(skillToPatch, skillEntity);

            await _repository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("skills/{skillId}", Name = "Delete Skill")]
        public async Task<ActionResult> DeleteSpecialRule(int skillId)
        {

            var skillEntity = await _skillRepository.GetSkillByIdAsync(skillId);
            if (skillEntity == null)
            {
                return NotFound($"skillId: {skillId} does not exist.");
            }

            if (!skillEntity.Modifiable)
            {
                return Unauthorized($"skillId: {skillId} can not be modified.");
            }

            _skillRepository.DeleteSkill(skillEntity);
            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
