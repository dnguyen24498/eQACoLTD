using AutoMapper;
using eQACoLTD.Data.Entities;
using eQACoLTD.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using eQACoLTD.ViewModel.Customer.Handlers;
using eQACoLTD.ViewModel.Customer.Queries;
using eQACoLTD.ViewModel.System.Account.Queries;
using eQACoLTD.ViewModel.System.Employee.Handlers;
using eQACoLTD.ViewModel.Product.Category.Queries;
using eQACoLTD.ViewModel.Product.Category.Handlers;
using eQACoLTD.ViewModel.Product.ListProduct.Handlers;
using eQACoLTD.ViewModel.Product.Supplier.Handlers;
using eQACoLTD.ViewModel.Order.Handlers;

namespace eQACoLTD.Application.Configurations
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppRole, AccountRolesDto>();
            CreateMap<EmployeeForCreationDto,Employee>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryForCreationDto, Category>();
            CreateMap<SupplierForCreationDto,Supplier>();
            CreateMap<ProductForCreationDto,Data.Entities.Product>();
            CreateMap<CustomerForCreationDto,Data.Entities.Customer>();
            CreateMap<OrderDetailsForCreationDto, OrderDetail>();
            CreateMap<OrderForCreationDto, Data.Entities.Order>();
            CreateMap<OrderGoodsDeliveryNoteForCreationDto, GoodsDeliveryNote>();
            CreateMap<OrderReceiptVoucherForCreationDto, ReceiptVoucher>();
        }
    }
}
