using Microsoft.EntityFrameworkCore;
using TTCompanion.API.FantasyFootball.DBContexts;
using TTCompanion.API.FantasyFootball.Entities;

namespace TTCompanion.API.FantasyFootball.Services.SpecialRule
{
    public class FFSpecialRuleRepository : IFFSpecialRuleRepository
    {
        private readonly FFContext _context;

        public FFSpecialRuleRepository(FFContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<FFSpecialRule>> GetSpecialRulesAsync()
        {
            return await _context.SpecialRules
                .OrderBy(sr => sr.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<FFSpecialRule>> GetSpecialRulesForRaceAsync(int raceId)
        {
            return await _context.SpecialRules
                .OrderBy(sr => sr.Name)
                .Where(sr => sr.RaceId == raceId)
                .ToListAsync();
        }

        public async Task<FFSpecialRule?> GetSpecialRuleByIdAsync(int specialRuleId)
        {
            return await _context.SpecialRules
                .Where(sr => sr.Id == specialRuleId)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> SpecialRuleExistsAsync(int specialRuleId)
        {
            return await _context.SpecialRules.AnyAsync(sr => sr.Id == specialRuleId);
        }

        public Task AddSpecialRuleForRaceAsync(int raceId, FFSpecialRule specialRule)
        {
            throw new NotImplementedException();
        }

        public void DeleteSpecialRule(FFSpecialRule specialRule)
        {
            _context.SpecialRules.Remove(specialRule);
        }
    }
}
