using AutoMapper;
using eQACoLTD.Data.Entities;
using eQACoLTD.Utilities.Extensions;
using eQACoLTD.ViewModel.System.Role.Handlers;
using eQACoLTD.ViewModel.System.Role.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Application.Configurations
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateRoleRequest, AppRole>();
            CreateMap<AppRole, RoleResponse>();
            CreateMap<UpdateRoleRequest, AppRole>()
                .ForMember(des => des.NormalizedName, opt => opt.MapFrom(src => src.Name));
        }
    }
}
