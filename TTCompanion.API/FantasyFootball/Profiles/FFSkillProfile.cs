using AutoMapper;

namespace TTCompanion.API.FantasyFootball.Profiles
{
    public class FFSkillProfile : Profile
    {
        public FFSkillProfile()
        {
            CreateMap<Entities.FFSkill, Models.FFSkillDto>();
        }
    }
}
