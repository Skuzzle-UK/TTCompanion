using Microsoft.EntityFrameworkCore;
using TTCompanion.API.DBContexts;

namespace TTCompanion.API.FantasyFootball.Services.SpecialRule
{
    public class SpecialRuleRepository : ISpecialRuleRepository
    {
        private readonly DBContext _context;

        public SpecialRuleRepository(DBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<(IEnumerable<Entities.SpecialRule>, PaginationMetadata)> GetSpecialRulesAsync(int? raceId, string? name, string? searchQuery, int pageSize = 30, int pageNumber = 1)
        {
            var collection = _context.SpecialRules as IQueryable<Entities.SpecialRule>;

            if (!string.IsNullOrEmpty(name))
            {
                name = name.Trim();
                collection = collection.Where(sr => sr.Name == name);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                searchQuery = $"%{searchQuery}%";
                collection = collection
                    .Where(sr => EF.Functions.Like(sr.Name, searchQuery));
            }

            if (raceId != null)
            {
                collection = collection
                .Where(s => s.Races.Any(p => p.Id == raceId));
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetaData = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            var collectionToReturn =  await collection
                .OrderBy(s => s.Name)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetaData);
        }

        public async Task<Entities.SpecialRule?> GetSpecialRuleByIdAsync(int specialRuleId)
        {
            return await _context.SpecialRules
                .Where(sr => sr.Id == specialRuleId)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> SpecialRuleExistsAsync(int specialRuleId)
        {
            return await _context.SpecialRules.AnyAsync(sr => sr.Id == specialRuleId);
        }

        public Task AddSpecialRuleForRaceAsync(int raceId, Entities.SpecialRule specialRule)
        {
            throw new NotImplementedException();
        }

        public void DeleteSpecialRule(Entities.SpecialRule specialRule)
        {
            _context.SpecialRules.Remove(specialRule);
        }
    }
}
