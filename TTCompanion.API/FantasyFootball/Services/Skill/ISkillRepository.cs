namespace TTCompanion.API.FantasyFootball.Services.Skill
{
    public interface ISkillRepository
    {
        Task<IEnumerable<Entities.Skill>> GetSkillsAsync(int? playerId);
        Task<Entities.Skill?> GetSkillByIdAsync(int skillId);
        Task<bool> SkillExistsAsync(int skillId);
        Task AddSkillForPlayerAsync(int playerId, Entities.Skill skill);
        void DeleteSkill(Entities.Skill skill);
    }
}
