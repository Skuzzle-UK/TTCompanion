using AutoMapper;
using TTCompanion.API.FantasyFootball.Models.Race;

namespace TTCompanion.API.FantasyFootball.Profiles
{
    public class RaceProfile : Profile
    {
        public RaceProfile()
        {
            CreateMap<Entities.Race, RaceDto>();
            CreateMap<Entities.Race, RaceOnlyDto>();
            CreateMap<Entities.Race, RaceWithSpecialRulesDto>();
            CreateMap<Entities.Race, RaceWithPlayersDto>();
            CreateMap<Entities.Race, RaceForUpdateDto>();
            CreateMap<RaceForUpdateDto, Entities.Race>();
        }
    }
}
