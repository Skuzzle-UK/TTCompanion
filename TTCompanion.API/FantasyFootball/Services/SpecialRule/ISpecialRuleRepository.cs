namespace TTCompanion.API.FantasyFootball.Services.SpecialRule
{
    public interface ISpecialRuleRepository
    {
        Task<IEnumerable<Entities.SpecialRule>> GetSpecialRulesAsync(int? raceId, int pageNumber = 1, int pageSize = 30);
        Task<Entities.SpecialRule?> GetSpecialRuleByIdAsync(int specialRuleId);
        Task<bool> SpecialRuleExistsAsync(int specialRuleId);
        Task AddSpecialRuleForRaceAsync(int raceId, Entities.SpecialRule specialRule);
        void DeleteSpecialRule(Entities.SpecialRule specialRule);
    }
}
