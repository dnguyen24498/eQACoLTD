using AutoMapper;
using eQACoLTD.Data.Entities;
using eQACoLTD.ViewModel.System.Account.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eQACoLTD.IdentityServer.Configurations
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterRequest, AppUser>()
                .ForMember(des => des.NormalizedEmail, opt => opt.MapFrom(src => src.Email))
                .ForMember(des => des.NormalizedUserName, opt => opt.MapFrom(src => src.UserName));

        }
    }
}
