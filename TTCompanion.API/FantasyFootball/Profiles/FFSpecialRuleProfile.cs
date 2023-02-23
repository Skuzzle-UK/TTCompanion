using AutoMapper;
using TTCompanion.API.FantasyFootball.Models.FFSpecialRule;

namespace TTCompanion.API.FantasyFootball.Profiles
{
    public class FFSpecialRuleProfile : Profile
    {
        public FFSpecialRuleProfile()
        {
            CreateMap<Entities.FFSpecialRule, FFSpecialRuleDto>();
        }
    }
}
