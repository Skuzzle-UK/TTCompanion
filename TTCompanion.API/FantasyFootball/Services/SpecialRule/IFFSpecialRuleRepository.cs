using TTCompanion.API.FantasyFootball.Entities;

namespace TTCompanion.API.FantasyFootball.Services.SpecialRule
{
    public interface IFFSpecialRuleRepository
    {
        Task<IEnumerable<FFSpecialRule>> GetSpecialRulesAsync();
        Task<IEnumerable<FFSpecialRule>> GetSpecialRulesForRaceAsync(int raceId);
        Task<FFSpecialRule?> GetSpecialRuleByIdAsync(int specialRuleId);
        Task<bool> SpecialRuleExistsAsync(int specialRuleId);
        Task AddSpecialRuleForRaceAsync(int raceId, FFSpecialRule specialRule);
        void DeleteSpecialRule(FFSpecialRule specialRule);
    }
}
