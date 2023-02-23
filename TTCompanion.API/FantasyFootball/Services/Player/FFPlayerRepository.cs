using Microsoft.EntityFrameworkCore;
using TTCompanion.API.FantasyFootball.DBContexts;
using TTCompanion.API.FantasyFootball.Entities;

namespace TTCompanion.API.FantasyFootball.Services.Player
{
    public class FFPlayerRepository : IFFPlayerRepository
    {
        private readonly FFContext _context;

        public FFPlayerRepository(FFContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

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
                    .Where(p => p.RaceId == raceId)
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

        public async Task<bool> PlayerExistsAsync(int playerId)
        {
            return await _context.Players.AnyAsync(p => p.Id == playerId);
        }

        public Task AddPlayerForRaceAsync(int raceId, FFPlayer player)
        {
            throw new NotImplementedException();
        }

        public void DeletePlayer(FFPlayer player)
        {
            _context.Players.Remove(player);
        }
    }
}
