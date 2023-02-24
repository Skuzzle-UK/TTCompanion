using AutoMapper;
using TTCompanion.API.FantasyFootball.Models.SpecialRule;

namespace TTCompanion.API.FantasyFootball.Profiles
{
    public class SpecialRuleProfile : Profile
    {
        public SpecialRuleProfile()
        {
            CreateMap<Entities.SpecialRule, SpecialRuleDto>();
            
            CreateMap<Entities.SpecialRule, SpecialRuleForUpdateDto>();
            CreateMap<SpecialRuleForUpdateDto, Entities.SpecialRule>();
        }
    }
}
