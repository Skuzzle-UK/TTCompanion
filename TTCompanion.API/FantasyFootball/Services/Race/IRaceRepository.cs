﻿namespace TTCompanion.API.FantasyFootball.Services.Race
{
    public interface IRaceRepository
    {
        Task<(IEnumerable<Entities.Race>, PaginationMetadata)> GetRacesAsync(string? name, string? searchQuery, bool includeSpecialRules = false, bool includePlayers = false, int pageSize = 30, int pageNumber = 1);
        Task<Entities.Race?> GetRaceByIdAsync(int raceId, bool includeSpecialRules = false, bool includePlayers = false);
        Task<bool> RaceExistsAsync(int raceId);
        Task AddRaceAsync(Entities.Race race);
        void DeleteRace(Entities.Race race);
    }
}
