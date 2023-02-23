using AutoMapper;
using TTCompanion.API.FantasyFootball.Models.FFRace;

namespace TTCompanion.API.FantasyFootball.Profiles
{
    public class FFRaceProfile : Profile
    {
        public FFRaceProfile()
        {
            CreateMap<Entities.FFRace, FFRaceDto>();
            CreateMap<Entities.FFRace, FFRaceOnlyDto>();
            CreateMap<Entities.FFRace, FFRaceWithoutPlayersDto>();
            CreateMap<Entities.FFRace, FFRaceWithoutSpecialRulesDto>();
        }
    }
}
