namespace TTCompanion.API.FantasyFootball.Services.Skill
{
    public interface ISkillRepository
    {
        Task<IEnumerable<Entities.Skill>> GetSkillsAsync(int? playerId, string? name, string? searchQuery, int pageNumber = 1, int pageSize = 30);
        Task<Entities.Skill?> GetSkillByIdAsync(int skillId);
        Task<bool> SkillExistsAsync(int skillId);
        Task AddSkillForPlayerAsync(int playerId, Entities.Skill skill);
        void DeleteSkill(Entities.Skill skill);
    }
}
