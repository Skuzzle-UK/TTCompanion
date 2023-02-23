using TTCompanion.API.FantasyFootball.Entities;

namespace TTCompanion.API.FantasyFootball.Services.Race
{
    public interface IFFRaceRepository
    {
        Task<IEnumerable<FFRace>> GetRacesAsync(bool includeSpecialRules = false, bool includePlayers = false);
        Task<FFRace?> GetRaceByIdAsync(int raceId, bool includeSpecialRules = false, bool includePlayers = false);
        Task<bool> RaceExistsAsync(int raceId);
        Task AddRaceAsync(FFRace race);
        void DeleteRace(FFRace race);
    }
}
