using Microsoft.EntityFrameworkCore;
using TTCompanion.API.FantasyFootball.DBContexts;

namespace TTCompanion.API.FantasyFootball.Services.Player
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly DBContext _context;

        public PlayerRepository(DBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Entities.Player>> GetPlayersAsync(bool includeSkills)
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

        public async Task<IEnumerable<Entities.Player>> GetPlayersForRaceAsync(int raceId, bool includeSkills)
        {
            if (includeSkills)
            {
                return await _context.Players
                    .OrderBy(p => p.Name)
                    .Include(p => p.Skills)
                    .Where(p => p.RaceId == raceId)
                    .ToListAsync();
            }

            return await _context.Players
                .OrderBy(p => p.Name)
                .Where(p => p.RaceId == raceId)
                .ToListAsync();
        }

        public async Task<Entities.Player?> GetPlayerByIdAsync(int playerId, bool includeSkills)
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

        public async Task<bool> PlayerExistsAsync(int playerId)
        {
            return await _context.Players.AnyAsync(p => p.Id == playerId);
        }

        public Task AddPlayerForRaceAsync(int raceId, Entities.Player player)
        {
            throw new NotImplementedException();
        }

        public void DeletePlayer(Entities.Player player)
        {
            _context.Players.Remove(player);
        }
    }
}
