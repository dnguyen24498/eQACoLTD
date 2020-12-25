using AutoMapper;
using eQACoLTD.Data.Entities;
using eQACoLTD.ViewModel.Customer.Handlers;
using eQACoLTD.ViewModel.Order.Handlers;
using eQACoLTD.ViewModel.Product.Category.Handlers;
using eQACoLTD.ViewModel.Product.Category.Queries;
using eQACoLTD.ViewModel.Product.ListProduct.Handlers;
using eQACoLTD.ViewModel.Product.Payment.Handlers;
using eQACoLTD.ViewModel.Product.PurchaseOrder.Handlers;
using eQACoLTD.ViewModel.Product.PurchaseOrder.Queries;
using eQACoLTD.ViewModel.Product.Stock.Handlers;
using eQACoLTD.ViewModel.Product.Supplier.Handlers;
using eQACoLTD.ViewModel.System.Account.Queries;
using eQACoLTD.ViewModel.System.Employee.Handlers;

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
            CreateMap<ExportOrderDto, GoodsDeliveryNote>();
            CreateMap<OrderPaymenForCreationDto, ReceiptVoucher>();
            CreateMap<PurchaseOrderDetail, PurchaseOrderDetailsDto>();
            CreateMap<PurchaseOrderForCreationDto, PurchaseOrder>();
            CreateMap<PurchaseOrderDetailsForCreation, PurchaseOrderDetail>();
            CreateMap<ImportPurchaseOrderDto,GoodsReceivedNote>();
            CreateMap<ImportPurchaseOrderDetailsDto, GoodsReceivedNoteDetail>();
            CreateMap<PurchaseOrderPaymentForCreationDto, PaymentVoucher>();
        }
    }
}
