using AutoMapper;
using TTCompanion.API.Models;

namespace TTCompanion.API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Entities.User, UserDto>();
            CreateMap<UserDto, Entities.User>();
        }
    }
}
