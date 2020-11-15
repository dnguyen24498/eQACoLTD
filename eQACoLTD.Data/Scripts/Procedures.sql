-- Lấy ngẫu nhiên 12 sản phẩm theo danh mục (Done)
go
drop procedure if exists prGetRandomProduct
go
create procedure prGetRandomProduct
as 
begin
	select top 6 Products.Id,Products.Name,
	Products.RetailPrice, 
	Products.Stars, Brands.Name as BrandName, Categories.Name as CategoryName,
	Products.Views,(select [Path] from ProductImages where ProductImages.ProductId=Products.Id and ProductImages.IsThumbnail=1) as ImagePath
	from Products
	left join Categories on Categories.Id=Products.CategoryId
	left join Brands on Brands.Id=Products.BrandId
	where Products.IsDelete=0
	order by NEWID()
end;
-- Lấy danh sách 16 sản phẩm bán chạy trong tháng theo danh mục (Done)
go
drop procedure if exists prGetBestSellingProductInMonth
go
create procedure prGetBestSellingProductInMonth
as
begin
	select top 12 OrderDetails.ProductId as Id,Products.Name,Categories.Name as CategoryName,
	Products.RetailPrice,
	Products.Stars, Brands.Name as BrandName,
	Products.Views,(select [Path] from ProductImages where ProductImages.ProductId=Products.Id and ProductImages.IsThumbnail=1) as ImagePath
	from OrderDetails 
	inner join Orders on Orders.Id=OrderDetails.OrderId
	left join Products on Products.Id=OrderDetails.ProductId
	left join Categories on Categories.Id=Products.CategoryId
	left join Brands on Brands.Id=Products.BrandId
	WHERE MONTH(Orders.DateCreated)=MONTH(GETDATE()) and YEAR(Orders.DateCreated)=YEAR(GETDATE()) and Products.IsDelete=0
	group by OrderDetails.ProductId,Products.Name,Products.RetailPrice,Products.Stars, Brands.Name,Products.Views,Products.Id,Categories.Name
	order by Sum(OrderDetails.Quantity) desc
end;

-- Lấy danh sách 20 sản phẩm mới về trong tháng (Done)
go
drop procedure if exists prGetNewArrivedProducts
go
create procedure prGetNewArrivedProducts
as
begin
	select top 20 PurchaseOrderDetails.ProductId as Id,Products.Name,
	Products.RetailPrice,
	Products.Stars, Brands.Name as BrandName,
	Products.Views,(select [Path] from ProductImages where ProductImages.ProductId=Products.Id and ProductImages.IsThumbnail=1) as ImagePath
	from PurchaseOrderDetails
	inner join PurchaseOrders on PurchaseOrders.Id=PurchaseOrderDetails.PurchaseOrderId
	left join Products on Products.Id=PurchaseOrderDetails.ProductId
	left join Brands on Brands.Id=Products.BrandId
	where MONTH(PurchaseOrders.DateCreated)=MONTH(GETDATE()) and YEAR(PurchaseOrders.DateCreated)=YEAR(GETDATE()) and Products.IsDelete=0 and PurchaseOrderDetails.ProductId is not null
	group by PurchaseOrderDetails.ProductId,Products.Name,Products.RetailPrice,Products.Stars,
	Brands.Name,Products.Views,Products.Id
end;

-- Lấy danh sách 16 sản phẩm được nhiều lượt xem(Done)
go
drop procedure if exists prGetProductsTopView
go
create procedure prGetProductsTopView
as
begin
	select top 16 Products.Id as Id, Products.Name,Products.RetailPrice,
	Products.Stars,Products.Views,Brands.Name as BrandName,
	(select [Path] from ProductImages where ProductImages.ProductId=Products.Id and ProductImages.IsThumbnail=1) as ImagePath
	from Products
	left join Brands on Brands.Id=Products.BrandId 
	where Products.Views >= 1000 and Products.IsDelete=0
	order by Products.Views desc
end;
--Lấy danh sách 16 sản phẩm được nhiều lượt bình chọn(Done)
go
drop procedure if exists prGetProductsTopRate
go
create procedure prGetProductsTopRate
as
begin
	select top 16 Products.Id, Products.Name,Products.RetailPrice,
	Products.Stars,Products.Views,Brands.Name as BrandName,
	(select [Path] from ProductImages where ProductImages.ProductId=Products.Id and ProductImages.IsThumbnail=1) as ImagePath
	from Products 
	left join Brands on Brands.Id=Products.BrandId
	WHERE Products.Stars >=1 and Products.IsDelete=0
	order by Products.Stars desc
end;
--Lấy danh sách 16 sản phẩm bán chạy nhất(Done)
go
drop procedure if exists prGetFeaturedProducts
go
create procedure prGetFeaturedProducts
as
begin
	select top 16 Products.Id,Products.Name,Products.RetailPrice,
	Products.Stars,Products.Views,Brands.Name as BrandName,
	(select [Path] from ProductImages where ProductImages.ProductId=Products.Id and ProductImages.IsThumbnail=1) as ImagePath
	from OrderDetails
	left join Products on Products.Id=OrderDetails.ProductId
	left join Brands on Brands.Id=Products.BrandId
	where OrderDetails.ProductId is not NULL
	group by OrderDetails.ProductId,Products.Id,Products.Name,Products.RetailPrice,Products.Stars,Products.Views,Brands.Name
	order by SUM(OrderDetails.Quantity) desc
end;

-- Lấy danh sách phiếu thu - báo cáo sổ quỹ
go
drop procedure if exists prGetReceiptVoucherForCashBookReport
go
create procedure prGetReceiptVoucherForCashBookReport
(@fromDate as Date, @ToDate as Date, @branchId as char(36))
as 
begin
	select 
	ReceiptVouchers.Received,
	ReceiptVouchers.DateCreated,
	ReceiptVouchers.ReceivedDate as RecordDate,
	ReceiptVouchers.Id,
	ISNULL(ReceiptVouchers.OrderId,(ISNULL(ReceiptVouchers.RepairVoucherId,ISNULL(ReceiptVouchers.LiquidationVoucherId,ISNULL(ReceiptVouchers.ReturnId,ShippingId))))) as OriginalDocumentId,
	ISNULL(Customers.Name,Suppliers.Name) as TargetPerson,
	PaymentMethods.Name as PaymentMethodName
from ReceiptVouchers
left join Customers on ReceiptVouchers.CustomerId=Customers.Id
left join Suppliers on ReceiptVouchers.SupplierId=Suppliers.Id
left join PaymentMethods on ReceiptVouchers.PaymentMethodId=PaymentMethods.Id
where ReceiptVouchers.ReceivedDate >= @fromDate and ReceiptVouchers.ReceivedDate <= @ToDate and ReceiptVouchers.BranchId=@branchId
end;

--Lấy danh sách phiếu chi - báo cáo sổ quỹ
go
drop procedure if exists prGetPaymentVoucherForCashBookReport
go
create procedure prGetPaymentVoucherForCashBookReport
(@fromDate as Date, @ToDate as Date,@branchId as char(36))
as 
begin
	select 
	PaymentVouchers.Paid,
	PaymentVouchers.DateCreated,
	PaymentVouchers.PaymentDate as RecordDate,
	PaymentVouchers.Id,
	ISNULL(PaymentVouchers.PurchaseOrderId,(ISNULL(PaymentVouchers.ReturnId,PaymentVouchers.ShippingId))) as OriginalDocumentId,
	ISNULL(Customers.Name,Suppliers.Name) as TargetPerson,
	PaymentMethods.Name as PaymentMethodName
from PaymentVouchers
left join Customers on PaymentVouchers.CustomerId=Customers.Id
left join Suppliers on PaymentVouchers.SupplierId=Suppliers.Id
left join PaymentMethods on PaymentVouchers.PaymentMethodId=PaymentMethods.Id
where PaymentVouchers.PaymentDate >= @fromDate and PaymentVouchers.PaymentDate <= @ToDate and PaymentVouchers.BranchId=@branchId
end;
go
-- Lấy danh sách tồn kho - báo cáo sổ kho
go
drop procedure if exists prGetStockBookReports
go
create procedure prGetStockBookReports
(@warehouseId char(36),@dateTime date) 
as 
begin 
	select 
	Products.Id,
	Products.Name as ProductName,
	Stocks.RealQuantity as RealInventoryQuantity,
	Stocks.AbleToSale as SystemInventoryQuantity,
	(select SUM(GoodsReceivedNoteDetails.Quantity*GoodsReceivedNoteDetails.UnitPrice)
    from GoodsReceivedNoteDetails join GoodsReceivedNotes on GoodsReceivedNotes.Id=GoodsReceivedNoteDetails.GoodsReceivedNoteId
	where GoodsReceivedNoteDetails.ProductId=Stocks.ProductId and GoodsReceivedNotes.WarehouseId=@warehouseId and GoodsReceivedNotes.ImportDate<=@dateTime)/
	(select SUM(GoodsReceivedNoteDetails.Quantity)
    from GoodsReceivedNoteDetails join GoodsReceivedNotes on GoodsReceivedNotes.Id=GoodsReceivedNoteDetails.GoodsReceivedNoteId
	where GoodsReceivedNoteDetails.ProductId=Stocks.ProductId and GoodsReceivedNotes.WarehouseId=@warehouseId and GoodsReceivedNotes.ImportDate<=@dateTime) as AveragePrice
	from Stocks
	left join Products on Stocks.ProductId=Products.Id
	left join GoodsReceivedNoteDetails on GoodsReceivedNoteDetails.ProductId=Stocks.ProductId
	left join GoodsReceivedNotes on GoodsReceivedNotes.Id=GoodsReceivedNoteDetails.GoodsReceivedNoteId
	where Stocks.RealQuantity>0 and Stocks.WarehouseId=@warehouseId and GoodsReceivedNotes.ImportDate<=@dateTime
end;

exec prGetStockBookReports @warehouseId='d6bbee65-fe3d-4765-b569-202d9f3aa4f5',@dateTime='2020/11/12'