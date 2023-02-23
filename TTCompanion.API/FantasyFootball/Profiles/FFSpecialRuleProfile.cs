using AutoMapper;
using TTCompanion.API.FantasyFootball.Models.FFSpecialRule;

namespace TTCompanion.API.FantasyFootball.Profiles
{
    public class FFSpecialRuleProfile : Profile
    {
        public FFSpecialRuleProfile()
        {
            CreateMap<Entities.FFSpecialRule, FFSpecialRuleDto>();
            
            CreateMap<Entities.FFSpecialRule, FFSpecialRuleForUpdateDto>();
            CreateMap<FFSpecialRuleForUpdateDto, Entities.FFSpecialRule>();
        }
    }
}
