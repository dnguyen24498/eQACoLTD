using AutoMapper;
using eQACoLTD.Data.Entities;
using eQACoLTD.Utilities.Extensions;
using eQACoLTD.ViewModel.System.Role.Handlers;
using eQACoLTD.ViewModel.System.Role.Queries;
using eQACoLTD.ViewModel.System.User.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Application.Configurations
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateRoleRequest, AppRole>();
            CreateMap<AppRole, RoleResponse>();
            CreateMap<RoleResponse, AppRole>();
            CreateMap<UpdateRoleRequest, AppRole>()
                .ForMember(des => des.NormalizedName, opt => opt.MapFrom(src => src.Name));

            CreateMap<AppUser, UserProfileResponse>();
            CreateMap<UserProfileResponse, AppUser>()
                 .ForMember(destination => destination.DOB, options =>
                    options.Condition(source => source.DOB != null))
            .ForMember(destination => destination.City, options =>
                    options.Condition(source => source.City != null))
            .ForMember(destination => destination.District, options =>
                    options.Condition(source => source.District != null))
            .ForMember(destination => destination.SubDistrict, options =>
                    options.Condition(source => source.SubDistrict != null))
            .ForMember(destination => destination.Street, options =>
                    options.Condition(source => source.Street != null))
            .ForMember(destination => destination.Address, options =>
                    options.Condition(source => source.Address != null))
            .ForMember(destination => destination.DOB, options =>
                    options.Condition(source =>DateTime.Compare(source.DOB,DateTime.Now)<0 
                    && DateTime.Compare(source.DOB,new DateTime(1900,1,1))>0))
            .ForMember(destination => destination.FirstName, options =>
                    options.Condition(source => source.FirstName != null))
            .ForMember(destination => destination.MiddleName, options =>
                    options.Condition(source => source.MiddleName != null))
            .ForMember(destination => destination.LastName, options =>
                    options.Condition(source => source.LastName != null))
            .ForMember(destination => destination.PhoneNumber, options =>
                    options.Condition(source => source.PhoneNumber != null))
            .Ignore(record=>record.UserName);
        }
    }
}
