using TTCompanion.API.FantasyFootball.Entities;

namespace TTCompanion.API.FantasyFootball.Services.Skill
{
    public interface IFFSkillRepository
    {
        Task<IEnumerable<FFSkill>> GetSkillsAsync();
        Task<IEnumerable<FFSkill>> GetSkillsForPlayerAsync(int playerId);
        Task<FFSkill?> GetSkillByIdAsync(int skillId);
        Task<bool> SkillExistsAsync(int skillId);
        Task AddSkillForPlayerAsync(int playerId, FFSkill skill);
        void DeleteSkill(FFSkill skill);
    }
}
