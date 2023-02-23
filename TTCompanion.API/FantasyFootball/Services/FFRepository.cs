using Microsoft.EntityFrameworkCore;
using TTCompanion.API.FantasyFootball.DBContexts;
using TTCompanion.API.FantasyFootball.Entities;

namespace TTCompanion.API.FantasyFootball.Services
{
    public class FFRepository : IFFRepository
    {
        private readonly FFContext _context;

        public FFRepository(FFContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #region Races
        public async Task<IEnumerable<FFRace>> GetRacesAsync(bool includeSpecialRules = false, bool includePlayers = false)
        {
            if (includeSpecialRules && !includePlayers)
            {
                return await _context.Races
                    .OrderBy(r => r.Name)
                    .Include(r => r.SpecialRules)
                    .ToListAsync();
            }
            if (includePlayers && !includeSpecialRules)
            {
                return await _context.Races
                    .OrderBy(r => r.Name)
                    .Include(r => r.Players)
                    .ToListAsync();
            }
            if (includeSpecialRules && includePlayers)
            {
                return await _context.Races
                    .OrderBy(r => r.Name)
                    .Include(r => r.SpecialRules)
                    .Include(r => r.Players)
                    .ToListAsync();
            }

            return await _context.Races
                .OrderBy(r => r.Name)
                .ToListAsync();
        }

        public async Task<FFRace?> GetRaceByIdAsync(int raceId, bool includeSpecialRules = false, bool includePlayers = false)
        {
            if (includeSpecialRules && !includePlayers)
            {
                return await _context.Races
                    .Include(r => r.SpecialRules)
                    .Where(r => r.Id == raceId)
                    .FirstOrDefaultAsync();
            }
            if (includePlayers && !includeSpecialRules)
            {
                return await _context.Races
                    .Include(r => r.Players)
                    .Where(r => r.Id == raceId)
                    .FirstOrDefaultAsync();
            }
            if (includeSpecialRules && includePlayers)
            {
                return await _context.Races
                    .Include(r => r.SpecialRules)
                    .Include(r => r.Players)
                    .Where(r => r.Id == raceId)
                    .FirstOrDefaultAsync();
            }

            return await _context.Races
                .Where(r => r.Id == raceId)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> RaceExistsAsync(int raceId)
        {
            return await _context.Races.AnyAsync(r => r.Id == raceId);
        }

        public void DeleteRace(FFRace race)
        {
            _context.Races.Remove(race);
        }
        #endregion

        #region Special Rules
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

        public async Task<FFSpecialRule?> GetSpecialRuleById(int specialRuleId)
        {
            return await _context.SpecialRules
                .Where(sr => sr.Id == specialRuleId)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> SpecialRuleExists(int specialRuleId)
        {
            return await _context.SpecialRules.AnyAsync(sr => sr.Id == specialRuleId);
        }

        public void DeleteSpecialRule(FFSpecialRule specialRule)
        {
            _context.SpecialRules.Remove(specialRule);
        }
        #endregion

        #region Players
        public async Task<IEnumerable<FFPlayer>> GetPlayersAsync(bool includeSkills)
        {
            if (includeSkills)
            {
                return await _context.Players
                    .OrderBy(p => p.Name)
                    .Include(p => p.Skills)
                    .ToListAsync();
            }

            return await _context.Players
                .OrderBy(r => r.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<FFPlayer>> GetPlayersForRaceAsync(int raceId, bool includeSkills)
        {
            if (includeSkills)
            {
                return await _context.Players
                    .OrderBy(p => p.Name)
                    .Include(p => p.Skills)
                    .Where(p => p.RaceId== raceId)
                    .ToListAsync();
            }

            return await _context.Players
                .OrderBy(p => p.Name)
                .Where(p => p.RaceId == raceId)
                .ToListAsync();
        }

        public async Task<FFPlayer?> GetPlayerByIdAsync(int playerId, bool includeSkills)
        {
            if (includeSkills)
            {
                return await _context.Players
                    .OrderBy(p => p.Name)
                    .Include(p => p.Skills)
                    .Where(p => p.Id == playerId)
                    .FirstOrDefaultAsync();
            }

            return await _context.Players
                .OrderBy(p => p.Name)
                .Where(p => p.Id == playerId)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> PlayerExists(int playerId)
        {
            return await _context.Players.AnyAsync(p => p.Id == playerId);
        }

        public void DeletePlayer(FFPlayer player)
        {
            _context.Players.Remove(player);
        }
        #endregion

        #region Skills
        public async Task<IEnumerable<FFSkill>> GetSkillsAsync()
        {
            return await _context.Skills
                .OrderBy(s => s.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<FFSkill>> GetSkillsForPlayerAsync(int playerId)
        {
            return await _context.Skills
                .OrderBy(s => s.Name)
                .Where(s => s.PlayerId == playerId)
                .ToListAsync();
        }

        public async Task<FFSkill?> GetSkillByIdAsync(int skillId)
        {
            return await _context.Skills
                .Where(s => s.Id == skillId)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> SkillExists(int skillId)
        {
            return await _context.Skills.AnyAsync(s => s.Id == skillId);
        }

        public void DeleteSkill(FFSkill skill)
        {
            _context.Skills.Remove(skill);
        }
        #endregion

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
