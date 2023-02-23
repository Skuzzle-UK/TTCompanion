using AutoMapper;

namespace TTCompanion.API.FantasyFootball.Profiles
{
    public class FFRaceProfile : Profile
    {
        public FFRaceProfile()
        {
            CreateMap<Entities.FFRace, Models.FFRaceDto>();
            CreateMap<Entities.FFRace, Models.FFRaceOnlyDto>();
            CreateMap<Entities.FFRace, Models.FFRaceWithoutPlayersDto>();
            CreateMap<Entities.FFRace, Models.FFRaceWithoutSpecialRulesDto>();
        }
    }
}
