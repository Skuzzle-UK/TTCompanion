using AutoMapper;
using TTCompanion.API.FantasyFootball.Models.Player;

namespace TTCompanion.API.FantasyFootball.Profiles
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<Entities.Player, PlayerDto>();
            CreateMap<Entities.Player, PlayerForUpdateDto>();
            CreateMap<PlayerForUpdateDto, Entities.Player>();
        }
    }
}
