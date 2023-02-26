using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TTCompanion.API.FantasyFootball.Services.Player;
using TTCompanion.API.FantasyFootball.Services.Skill;
using TTCompanion.API.FantasyFootball.Services;
using TTCompanion.API.FantasyFootball.Models.Skill;
using TTCompanion.API.FantasyFootball.Models.SpecialRule;
using Microsoft.AspNetCore.JsonPatch;
using TTCompanion.API.FantasyFootball.Models.Player;

namespace TTCompanion.API.FantasyFootball.Controllers
{
    [Route("ttcompanion.api/fantasyfootball")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly ISkillRepository _skillRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;

        public SkillsController(IRepository repository, ISkillRepository skillRepository, IPlayerRepository playerRepository, IMapper mapper)
        {
            _repository= repository;
            _skillRepository = skillRepository;
            _playerRepository = playerRepository;
            _mapper = mapper;
        }

        [HttpGet("skills", Name = "Get Skills")]
        public async Task<ActionResult<IEnumerable<SkillDto>>> GetRacesAsync()
        {
            var skills = await _skillRepository.GetSkillsAsync();
            if (skills == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<SkillDto>>(skills));
        }

        [HttpGet("players/{playerId}/skills", Name = "Get Skills For Player")]
        public async Task<ActionResult<IEnumerable<SkillDto>>> GetSkillsForPlayer(int playerId)
        {
            var player = await _playerRepository.GetPlayerByIdAsync(playerId);
            if (player == null)
            {
                return NotFound();
            }

            var skills = await _skillRepository.GetSkillsForPlayerAsync(playerId);
            if (skills == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<SkillDto>>(skills));
        }

        [HttpGet("skills/{skillId}")]
        public async Task<ActionResult<SkillDto>> GetSkillById(int skillId)
        {
            var skill = await _skillRepository.GetSkillByIdAsync(skillId);
            if (skill == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<SkillDto>(skill));
        }

        [HttpPost("skills/{skillId}", Name = "Update Skill")]
        public async Task<ActionResult> UpdateSkill(int skillId, SkillForUpdateDto skill)
        {
            var skillEntity = await _skillRepository.GetSkillByIdAsync(skillId);
            if (skillEntity == null)
            {
                return NotFound();
            }

            if (!skillEntity.Modifiable)
            {
                return Unauthorized();
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
                return NotFound();
            }

            if (!skillEntity.Modifiable)
            {
                return Unauthorized();
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
                return NotFound();
            }

            if (!skillEntity.Modifiable)
            {
                return Unauthorized();
            }

            _skillRepository.DeleteSkill(skillEntity);
            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
