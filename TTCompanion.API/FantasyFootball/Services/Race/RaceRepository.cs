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

        public async Task<IEnumerable<Entities.Race>> GetRacesAsync(string? name, string? searchQuery, bool includeSpecialRules = false, bool includePlayers = false, int pageNumber = 1, int pageSize = 30)
        {
            var collection = _context.Races as IQueryable<Entities.Race>;

            if(!string.IsNullOrEmpty(name))
            {
                name = name.Trim();
                collection = collection.Where(r => r.Name == name);
            }

            if(!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                searchQuery = $"%{searchQuery}%";
                collection = collection
                    .Where(r => EF.Functions.Like(r.Name, searchQuery));
            }

            if (includeSpecialRules)
            {
                collection = collection
                    .Include(r => r.SpecialRules);
            }

            if (includePlayers)
            {
                collection = collection
                    .Include(r => r.Players)
                    .ThenInclude(p => p.Skills);
            }

            return await collection
                .OrderBy(r => r.Name)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Entities.Race?> GetRaceByIdAsync(int raceId, bool includeSpecialRules = false, bool includePlayers = false)
        {
            var collection = _context.Races as IQueryable<Entities.Race>;
            
            collection = collection.Where(r => r.Id == raceId);

            if (includeSpecialRules)
            {
                collection = collection
                    .Include(r => r.SpecialRules);
            }

            if (includePlayers)
            {
                collection = collection
                    .Include(r => r.Players)
                    .ThenInclude(p => p.Skills);
            }

            return await collection
                .OrderBy(r => r.Name)
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
