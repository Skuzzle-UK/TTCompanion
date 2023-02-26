using Microsoft.EntityFrameworkCore;
using TTCompanion.API.FantasyFootball.DBContexts;

namespace TTCompanion.API.FantasyFootball.Services.Race
{
    public class RaceRepository : IRaceRepository
    {
        private readonly DBContext _context;

        public RaceRepository(DBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Entities.Race>> GetRacesAsync(bool includeSpecialRules = false, bool includePlayers = false)
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
                        .ThenInclude(s => s.Skills)
                    .ToListAsync();
            }
            if (includeSpecialRules && includePlayers)
            {
                return await _context.Races
                    .OrderBy(r => r.Name)
                    .Include(r => r.SpecialRules)
                    .Include(r => r.Players)
                        .ThenInclude(s => s.Skills)
                    .ToListAsync();
            }

            return await _context.Races
                .OrderBy(r => r.Name)
                .ToListAsync();
        }

        public async Task<Entities.Race?> GetRaceByIdAsync(int raceId, bool includeSpecialRules = false, bool includePlayers = false)
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
                        .ThenInclude(s => s.Skills)
                    .Where(r => r.Id == raceId)
                    .FirstOrDefaultAsync();
            }
            if (includeSpecialRules && includePlayers)
            {
                return await _context.Races
                    .Include(r => r.SpecialRules)
                    .Include(r => r.Players)
                        .ThenInclude(s => s.Skills)
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

        public Task AddRaceAsync(Entities.Race race)
        {
            throw new NotImplementedException();
        }

        public void DeleteRace(Entities.Race race)
        {
            _context.Races.Remove(race);
        }
    }
}
