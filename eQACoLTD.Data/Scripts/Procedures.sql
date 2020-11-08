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
