using AutoMapper;
using TTCompanion.API.FantasyFootball.Models.Skill;

namespace TTCompanion.API.FantasyFootball.Profiles
{
    public class SkillProfile : Profile
    {
        public SkillProfile()
        {
            CreateMap<Entities.Skill, SkillDto>();
        }
    }
}
