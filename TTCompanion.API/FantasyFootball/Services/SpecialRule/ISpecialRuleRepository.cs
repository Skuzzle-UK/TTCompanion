namespace TTCompanion.API.FantasyFootball.Services.SpecialRule
{
    public interface ISpecialRuleRepository
    {
        Task<(IEnumerable<Entities.SpecialRule>, PaginationMetadata)> GetSpecialRulesAsync(int? raceId, string? name, string? searchQuery, int pageSize = 30, int pageNumber = 1);
        Task<Entities.SpecialRule?> GetSpecialRuleByIdAsync(int specialRuleId);
        Task<bool> SpecialRuleExistsAsync(int specialRuleId);
        Task AddSpecialRuleForRaceAsync(int raceId, Entities.SpecialRule specialRule);
        void DeleteSpecialRule(Entities.SpecialRule specialRule);
    }
}
