using AutoMapper;
using TTCompanion.API.FantasyFootball.Models.FFSkill;

namespace TTCompanion.API.FantasyFootball.Profiles
{
    public class FFSkillProfile : Profile
    {
        public FFSkillProfile()
        {
            CreateMap<Entities.FFSkill, FFSkillDto>();
        }
    }
}
