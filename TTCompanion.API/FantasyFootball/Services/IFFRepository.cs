using TTCompanion.API.FantasyFootball.Entities;

namespace TTCompanion.API.FantasyFootball.Services
{
    public interface IFFRepository
    {
        Task<IEnumerable<FFRace>> GetRacesAsync(bool includeSpecialRules = false, bool includePlayers = false);
        Task<FFRace?> GetRaceByIdAsync(int raceId, bool includeSpecialRules = false, bool includePlayers = false);
        Task<bool> RaceExistsAsync(int raceId);
        Task AddRaceAsync(FFRace race);
        void DeleteRace(FFRace race);

        Task<IEnumerable<FFSpecialRule>> GetSpecialRulesAsync();
        Task<IEnumerable<FFSpecialRule>> GetSpecialRulesForRaceAsync(int raceId);
        Task<FFSpecialRule?> GetSpecialRuleByIdAsync(int specialRuleId);
        Task<bool> SpecialRuleExistsAsync(int specialRuleId);
        Task AddSpecialRuleForRaceAsync(int raceId, FFSpecialRule specialRule);
        void DeleteSpecialRule(FFSpecialRule specialRule);


        Task<IEnumerable<FFPlayer>> GetPlayersAsync(bool includeSkills = false);
        Task<IEnumerable<FFPlayer>> GetPlayersForRaceAsync(int raceId, bool includeSkills = false);
        Task<FFPlayer?> GetPlayerByIdAsync(int playerId, bool includeSkills = false);
        Task<bool> PlayerExistsAsync(int playerId);
        Task AddPlayerForRaceAsync(int raceId, FFPlayer player);
        void DeletePlayer(FFPlayer player);

        Task<IEnumerable<FFSkill>> GetSkillsAsync();
        Task<IEnumerable<FFSkill>> GetSkillsForPlayerAsync(int playerId);
        Task<FFSkill?> GetSkillByIdAsync(int skillId);
        Task<bool> SkillExistsAsync(int skillId);
        Task AddSkillForPlayerAsync(int playerId, FFSkill skill);
        void DeleteSkill(FFSkill skill);

        Task<bool> SaveChangesAsync();
    }
}
