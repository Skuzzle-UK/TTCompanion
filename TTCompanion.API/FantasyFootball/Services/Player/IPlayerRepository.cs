namespace TTCompanion.API.FantasyFootball.Services.Player
{
    public interface IPlayerRepository
    {
        Task<IEnumerable<Entities.Player>> GetPlayersAsync(int? raceId, string? name, string? searchQuery, bool includeSkills = false);
        Task<Entities.Player?> GetPlayerByIdAsync(int playerId, bool includeSkills = false);
        Task<bool> PlayerExistsAsync(int playerId);
        Task AddPlayerForRaceAsync(int raceId, Entities.Player player);
        void DeletePlayer(Entities.Player player);
    }
}
