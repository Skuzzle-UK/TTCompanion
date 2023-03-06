using Microsoft.EntityFrameworkCore;
using TTCompanion.API.DBContexts;

namespace TTCompanion.API.FantasyFootball.Services.Player
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly DBContext _context;

        public PlayerRepository(DBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<(IEnumerable<Entities.Player>, PaginationMetadata)> GetPlayersAsync(int? raceId, string? name, string? searchQuery, bool includeSkills = false, int pageSize = 30, int pageNumber = 1)
        {
            var collection = _context.Players as IQueryable<Entities.Player>;

            if (!string.IsNullOrEmpty(name))
            {
                name = name.Trim();
                collection = collection.Where(p => p.Name == name);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                searchQuery = $"%{searchQuery}%";
                collection = collection
                    .Where(p => EF.Functions.Like(p.Name, searchQuery));
            }

            if (raceId != null)
            {
                collection = collection
                    .Where(p => p.Races.All(r => r.Id == raceId));  
            }

            if (includeSkills)
            {
                collection = collection
                    .Include(p => p.Skills);
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetaData = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            var collectionToReturn = await collection
                .OrderBy(p => p.Name)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetaData);
        }

        public async Task<Entities.Player?> GetPlayerByIdAsync(int playerId, bool includeSkills = false)
        {
            var collection = _context.Players as IQueryable<Entities.Player>;
            if (includeSkills)
            {
                collection = collection
                    .Include(p => p.Skills);
            }

            return await collection
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
