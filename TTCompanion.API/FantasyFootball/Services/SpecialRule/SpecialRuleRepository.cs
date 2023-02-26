using Microsoft.EntityFrameworkCore;
using TTCompanion.API.FantasyFootball.DBContexts;

namespace TTCompanion.API.FantasyFootball.Services.SpecialRule
{
    public class SpecialRuleRepository : ISpecialRuleRepository
    {
        private readonly DBContext _context;

        public SpecialRuleRepository(DBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Entities.SpecialRule>> GetSpecialRulesAsync()
        {
            return await _context.SpecialRules
                .OrderBy(sr => sr.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Entities.SpecialRule>> GetSpecialRulesForRaceAsync(int raceId)
        {
            //@TODO work out how to do this
            return await _context.SpecialRules
                .OrderBy(sr => sr.Name)
                //.Where(sr => sr.Races.Contains(race))
                .ToListAsync();
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
