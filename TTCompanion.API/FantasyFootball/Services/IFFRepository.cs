using TTCompanion.API.FantasyFootball.Entities;

namespace TTCompanion.API.FantasyFootball.Services
{
    public interface IFFRepository
    {
        Task<IEnumerable<FFRace>> GetRacesAsync(bool includeSpecialRules = false, bool includePlayers = false);
        Task<FFRace?> GetRaceByIdAsync(int raceId, bool includeSpecialRules = false, bool includePlayers = false);
        Task<bool> RaceExistsAsync(int raceId);
        void DeleteRace(FFRace race);

        Task<IEnumerable<FFSpecialRule>> GetSpecialRulesAsync();
        Task<IEnumerable<FFSpecialRule>> GetSpecialRulesForRaceAsync(int raceId);
        Task<FFSpecialRule?> GetSpecialRuleById(int specialRuleId);
        Task<bool> SpecialRuleExists(int specialRuleId);
        void DeleteSpecialRule(FFSpecialRule specialRule);


        Task<IEnumerable<FFPlayer>> GetPlayersAsync(bool includeSkills);
        Task<IEnumerable<FFPlayer>> GetPlayersForRaceAsync(int raceId, bool includeSkills = false);
        Task<FFPlayer?> GetPlayerByIdAsync(int playerId, bool includeSkills = false);
        Task<bool> PlayerExists(int playerId);
        void DeletePlayer(FFPlayer player);

        Task<IEnumerable<FFSkill>> GetSkillsAsync();
        Task<IEnumerable<FFSkill>> GetSkillsForPlayerAsync(int playerId);
        Task<FFSkill?> GetSkillByIdAsync(int skillId);
        Task<bool> SkillExists(int skillId);
        void DeleteSkill(FFSkill skill);

        Task<bool> SaveChangesAsync();
    }
}
