﻿using AutoMapper;

namespace TTCompanion.API.FantasyFootball.Profiles
{
    public class FFPlayerProfile : Profile
    {
        public FFPlayerProfile()
        {
            CreateMap<Entities.FFPlayer, Models.FFPlayerDto>();
            CreateMap<Entities.FFPlayer, Models.FFPlayerWithoutSkillsDto>();
        }
    }
}
