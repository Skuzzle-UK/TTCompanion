using Microsoft.EntityFrameworkCore;
using TTCompanion.API.FantasyFootball.DBContexts;
using TTCompanion.API.FantasyFootball.Entities;

namespace TTCompanion.API.FantasyFootball.Services.Race
{
    public class FFRaceRepository : IFFRaceRepository
    {
        private readonly FFContext _context;

        public FFRaceRepository(FFContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

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

        public Task AddRaceAsync(FFRace race)
        {
            throw new NotImplementedException();
        }

        public void DeleteRace(FFRace race)
        {
            _context.Races.Remove(race);
        }
    }
}
