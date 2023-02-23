using AutoMapper;
using TTCompanion.API.FantasyFootball.Models.FFPlayer;

namespace TTCompanion.API.FantasyFootball.Profiles
{
    public class FFPlayerProfile : Profile
    {
        public FFPlayerProfile()
        {
            CreateMap<Entities.FFPlayer, FFPlayerDto>();
            CreateMap<Entities.FFPlayer, FFPlayerWithoutSkillsDto>();

            CreateMap<Entities.FFPlayer, FFPlayerForUpdateDto>();
            CreateMap<FFPlayerForUpdateDto, Entities.FFPlayer>();
        }
    }
}
