namespace TTCompanion.API.FantasyFootball.Services.SpecialRule
{
    public interface ISpecialRuleRepository
    {
        Task<IEnumerable<Entities.SpecialRule>> GetSpecialRulesAsync();
        Task<IEnumerable<Entities.SpecialRule>> GetSpecialRulesForRaceAsync(int raceId);
        Task<Entities.SpecialRule?> GetSpecialRuleByIdAsync(int specialRuleId);
        Task<bool> SpecialRuleExistsAsync(int specialRuleId);
        Task AddSpecialRuleForRaceAsync(int raceId, Entities.SpecialRule specialRule);
        void DeleteSpecialRule(Entities.SpecialRule specialRule);
    }
}
