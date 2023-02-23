using AutoMapper;

namespace TTCompanion.API.FantasyFootball.Profiles
{
    public class FFSpecialRuleProfile : Profile
    {
        public FFSpecialRuleProfile()
        {
            CreateMap<Entities.FFSpecialRule, Models.FFSpecialRuleDto>();
        }
    }
}
