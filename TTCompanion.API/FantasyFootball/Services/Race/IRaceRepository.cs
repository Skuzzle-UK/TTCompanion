namespace TTCompanion.API.FantasyFootball.Services.Race
{
    public interface IRaceRepository
    {
        Task<IEnumerable<Entities.Race>> GetRacesAsync(bool includeSpecialRules = false, bool includePlayers = false);
        Task<Entities.Race?> GetRaceByIdAsync(int raceId, bool includeSpecialRules = false, bool includePlayers = false);
        Task<bool> RaceExistsAsync(int raceId);
        Task AddRaceAsync(Entities.Race race);
        void DeleteRace(Entities.Race race);
    }
}
