using TTCompanion.API.FantasyFootball.Entities;

namespace TTCompanion.API.FantasyFootball.Services.Player
{
    public interface IFFPlayerRepository
    {
        Task<IEnumerable<FFPlayer>> GetPlayersAsync(bool includeSkills = false);
        Task<IEnumerable<FFPlayer>> GetPlayersForRaceAsync(int raceId, bool includeSkills = false);
        Task<FFPlayer?> GetPlayerByIdAsync(int playerId, bool includeSkills = false);
        Task<bool> PlayerExistsAsync(int playerId);
        Task AddPlayerForRaceAsync(int raceId, FFPlayer player);
        void DeletePlayer(FFPlayer player);
    }
}
