using TTCompanion.API.FantasyFootball.Entities;

namespace TTCompanion.API.FantasyFootball.Services.Player
{
    public interface IPlayerRepository
    {
        Task<IEnumerable<Entities.Player>> GetPlayersAsync(bool includeSkills = false);
        Task<IEnumerable<Entities.Player>> GetPlayersForRaceAsync(int raceId, bool includeSkills = false);
        Task<Entities.Player?> GetPlayerByIdAsync(int playerId, bool includeSkills = false);
        Task<bool> PlayerExistsAsync(int playerId);
        Task AddPlayerForRaceAsync(int raceId, Entities.Player player);
        void DeletePlayer(Entities.Player player);
    }
}
