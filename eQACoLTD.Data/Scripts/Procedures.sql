--
use DB_eQACoLTD
---Lấy công nợ nhà cung cấp
go
drop function if exists dbo.fuGetSupplierDebt
go
create function fuGetSupplierDebt
(@supplier_id varchar(12))
returns decimal
as
begin
	declare @total_debt decimal;
	set @total_debt=(
	(select ISNULL(SUM(Quantity*UnitPrice),0) from PurchaseOrderDetails 
	inner join PurchaseOrders on PurchaseOrders.Id=PurchaseOrderDetails.PurchaseOrderId 
	where PurchaseOrders.SupplierId=@supplier_id)-
	(select ISNULL(SUM(Paid),0) from PaymentVouchers 
	inner join PurchaseOrders on PurchaseOrders.Id=PaymentVouchers.PurchaseOrderId 
	where PurchaseOrders.SupplierId=@supplier_id)
	+(select ISNULL(SUM(ReceiptVouchers.Received),0) from ReceiptVouchers where ReceiptVouchers.SupplierId=@supplier_id));
	return ISNULL(@total_debt,0);
end;
---Lấy danh sách nhà cung cấp phân trang
go
drop procedure if exists prGetSupplierPaging
go
create procedure prGetSupplierPaging
(@pageNumber int,@rowsOfPage int)
as
begin
	WITH Main_CE as(
		select Suppliers.Id,Suppliers.Name,
		Suppliers.Address as FullAddress,
		Suppliers.Email,
		Suppliers.PhoneNumber
		from Suppliers
		left join Employees on Employees.Id=Suppliers.EmployeeId
		where Suppliers.IsDelete=0
	),Count_CE as(
		select COUNT(*) as [TotalRecord] from Main_CE
	)
	select * from Main_CE,Count_CE
	order by Main_CE.Id
	offset (@pageNumber-1)*@rowsOfPage rows
	fetch next @rowsOfPage rows only
end;
---Lấy chi tiết nhà cung cấp theo Id
go
drop procedure if exists prGetSupplierById
go
create procedure prGetSupplierById
(@supplierId varchar(12))
as
begin
	select Suppliers.Id,Suppliers.Name, Suppliers.Address,
	Suppliers.PhoneNumber,Suppliers.Email,Suppliers.Fax,
	Employees.FullName as EmployeeName,
	Suppliers.Website,Suppliers.[Description]
	from Suppliers 
	left join Employees on Employees.Id=Suppliers.EmployeeId
	where Suppliers.Id=@supplierId and Suppliers.IsDelete=0
end;
---Lấy lịch sử nhập hàng nhà cung cấp phân trang
go
drop procedure if exists prGetSupplierGoodsReceiptHistories
go
create procedure prGetSupplierGoodsReceiptHistories
(@supplierId varchar(12),@pageNumber int,@rowsOfPage int)
as 
begin
	with Main_CE as(
		SELECT PurchaseOrderDetails.PurchaseOrderId,OrderStatuses.Name as OrderStatus,
		PaymentStatuses.Name as PaymentStatus,
		SUM(PurchaseOrderDetails.Quantity*PurchaseOrderDetails.UnitPrice) as TotalAmount,
		PurchaseOrders.DateCreated
		from PurchaseOrderDetails
		inner join PurchaseOrders on PurchaseOrders.Id=PurchaseOrderDetails.PurchaseOrderId
		left join PaymentVouchers on PaymentVouchers.PurchaseOrderId=PurchaseOrders.Id
		left join OrderStatuses on OrderStatuses.Id=PurchaseOrders.OrderStatusId
		left join PaymentStatuses on PaymentStatuses.Id=PurchaseOrders.PaymentStatusId
		where PurchaseOrders.SupplierId=@supplierId
		group by PurchaseOrderDetails.PurchaseOrderId,OrderStatuses.Name,PaymentStatuses.Name,PurchaseOrders.DateCreated,PurchaseOrders.Id
	),Count_CE as(
		select COUNT(*) as [TotalRecord] from Main_CE
	)
	select * from Main_CE,Count_CE
	order by Main_CE.PurchaseOrderId desc
	offset (@pageNumber-1)*@rowsOfPage rows
	fetch next @rowsOfPage rows only
end;

---Lấy danh sách khách hàng phân trang
go
drop procedure if exists prGetCustomerPaging
go
create procedure prGetCustomerPaging
(@pageNumber int,@rowsOfPage int)
as
begin
	with Main_CE as(
		select Customers.Id,ISNULL(AppUsers.UserName,'') as UserName,
		Customers.FullName,
		ISNULL(AppUsers.PhoneNumber,Customers.DefaultPhoneNumber) as PhoneNumber,AppUsers.Email,Customers.Address as FullAddress,
		CustomerTypes.Name as CustomerTypeName
		from Customers
		left join AppUsers on Customers.UserId=AppUsers.Id
		left join CustomerTypes on CustomerTypes.Id=Customers.CustomerTypeId
	where Customers.IsDelete=0
	), Count_CE as(
		select COUNT(*) as [TotalRecord] from Main_CE
	)
	select * from Main_CE,Count_CE
	order by Main_CE.Id
	offset (@pageNumber-1)*@rowsOfPage rows
	fetch next @rowsOfPage rows only
end;

---Lấy chi tiết thông tin khách hàng theo Id
go
drop procedure if exists prGetCustomerById
go
create procedure prGetCustomerById
(@customerId varchar(12))
as
begin
	select Customers.Id,AppUsers.UserName,
	Customers.FullName,
	Customers.Dob, Customers.Address as FullAddress,
	Customers.Gender, Customers.AvatarPath,Customers.IsDelete,Customers.CustomerTypeId
	from Customers
	left join AppUsers on Customers.UserId=AppUsers.Id
	where Customers.Id=@customerId and Customers.IsDelete=0
end;
---Lấy công nợ khách hàng
go
drop function if exists dbo.fuGetCustomerDebt
go
create function fuGetCustomerDebt
(@customerId varchar(12))
returns decimal
as
begin
	declare @total_debt decimal;
	set @total_debt=(
		(select ISNULL(SUM(OrderDetails.Quantity*OrderDetails.UnitPrice),0) from OrderDetails 
		inner join Orders on Orders.Id=OrderDetails.OrderId where Orders.CustomerId=@customerId) - 
		(select ISNULL(SUM(ReceiptVouchers.Received),0) from ReceiptVouchers 
		inner join Orders on ReceiptVouchers.OrderId=Orders.Id where Orders.CustomerId=@customerId)+
		(select ISNULL(Sum(PaymentVouchers.Paid),0) from PaymentVouchers where PaymentVouchers.CustomerId=@customerId)
	);
	return ISNULL(@total_debt,0);
end;

---Lấy lịch sử mua hàng của khách hàng phân trang
go 
drop procedure if exists prGetCustomerHistoryPaging
go 
create procedure prGetCustomerHistoryPaging
(@customerId varchar(12),@pageNumber int,@rowsOfPage int)
as
begin
	with Main_CE as(
		select OrderDetails.OrderId, OrderStatuses.Name as OrderStatus,
		case
			when ShippingOrders.OrderId is null then N'Đã nhận hàng'
			else ShippingStatus.Name
		end as ShippingStatus, 
		PaymentStatuses.Name as PaymentStatus,
		SUM(OrderDetails.Quantity*OrderDetails.UnitPrice) as TotalAmount,Orders.DateCreated as OrderDate
		from OrderDetails
		inner join Orders on Orders.Id=OrderDetails.OrderId
		inner join OrderStatuses on OrderStatuses.Id=Orders.OrderStatusId
		inner join PaymentStatuses on PaymentStatuses.Id=Orders.PaymentStatusId
		left join ShippingOrders on ShippingOrders.OrderId=Orders.Id
		left join ShippingStatus on ShippingStatus.Id=ShippingOrders.ShippingStatusId
		where Orders.CustomerId=@customerId
		group by OrderDetails.OrderId,OrderStatuses.Name,ShippingOrders.OrderId,Orders.DateCreated,PaymentStatuses.Name,ShippingStatus.Name	
	),Count_CE as(
		select COUNT(*) as [TotalRecord] from Main_CE
	)
	select * from Main_CE,Count_CE
	order by Main_CE.OrderId desc
	offset (@pageNumber-1)*@rowsOfPage rows
	fetch next @rowsOfPage row only
end;
---Lấy danh sách nhân viên phân trang
go
drop procedure if exists prGetEmployeePaging
go
create procedure prGetEmployeePaging
(@pageNumber int,@rowsOfPage int)
as
begin
	with Main_CE as(
		select Employees.Id,AppUsers.UserName,
		Employees.FullName,
		AppUsers.Email, AppUsers.PhoneNumber,Employees.IsDelete
		from Employees
		left join AppUsers on AppUsers.Id=Employees.UserId
		where Employees.IsDelete=0
	),Count_CE as(
		select COUNT(*) as [TotalRecord] from Main_CE
	)
	select * from Main_CE,Count_CE
	order by Main_CE.Id
	offset (@pageNumber-1)*@rowsOfPage rows
	fetch next @rowsOfPage row only
end;
---Lấy chi tiết nhân viên theo Id
go
drop procedure if exists prGetEmployeeById
go
create procedure prGetEmployeeById
(@employeeId varchar(12))
as 
begin
	select Employees.Id,AppUsers.UserName,
    Employees.FullName,Employees.Dob,Employees.Address,Employees.Gender,
	Employees.AvatarPath
    from Employees
	left join AppUsers on AppUsers.Id=Employees.UserId
	where Employees.Id=@employeeId and Employees.IsDelete=0
end;
---Lấy danh sách danh mục phân trang
go
drop procedure if exists prGetCategoryPaging
go
create procedure prGetCategoryPaging
(@pageNumber int,@rowsOfPage int)
as
begin
	with Main_CE as(
		select Categories.Id,Categories.ThumbnailImagePath,Categories.Name,(select COUNT(*) from Products where Products.CategoryId=Categories.Id) as NumbProduct,
		Categories.[Description]
		from Categories
	),Count_CE as(
		select COUNT(*) as [TotalRecord] from Main_CE
	)
	select * from Main_CE,Count_CE
	order by Main_CE.Id
	offset (@pageNumber-1)*@rowsOfPage rows
	fetch next @rowsOfPage row only
end;
---Lấy danh sách sản phẩm phân trang
go 
drop procedure if exists prGetProductPaging
go
create procedure prGetProductPaging
(@pageNumber int,@rowsOfPage int)
as
begin
	with Main_CE as(
		select (select ProductImages.FullPath from ProductImages where ProductImages.ProductId=Products.Id and ProductImages.IsThumbnail=1) as ThumbnailPath,
		Products.Id,
		Products.Name as ProductName,
		Categories.Name as CategoryName,
		Brands.Name as BrandName,
		Stocks.AbleToSale
		from Products
		left join Stocks on Stocks.ProductId=Products.Id
		left join Categories on Categories.Id=Products.CategoryId
		left join Brands on Brands.Id=Products.BrandId
		where IsDelete=0 and Stocks.AbleToSale > 0
	),Count_CE as(
		select COUNT(*) as [TotalRecord] from Main_CE
	)
	select * from Main_CE,Count_CE
	order by Main_CE.Id desc
	offset (@pageNumber-1)*@rowsOfPage rows
	fetch next @rowsOfPage row only
end;
---Lấy chi tiết sản phẩm
go
drop procedure if exists prGetProductDetail
go
create procedure prGetProductDetail
(@productId varchar(12))
as
begin
	select Products.Id,Products.Name,
	Products.Information,Categories.Name as CategoryName,
	Products.[Description],Products.Views ,StarScore,
	Products.RetailPrice,Products.WholesalePrices,
	WarrantyPeriod,Brands.Name as BrandName
	from Products
	left join Categories on Categories.Id=Products.CategoryId
	left join Brands on Brands.Id=Products.BrandId
	where Products.Id=@productId
end;

---Lấy thông tin sản phẩm trong kho phân trang
go
drop procedure if exists prGetProductInStockPaging
go
create procedure prGetProductInStockPaging
(@pageNumber int,@rowsOfPage int)
as
begin
	with Main_CE as(
		select (select ImagePath from ProductImages where ProductImages.ProductId=Products.Id and ProductImages.IsThumbnail=1) as ThumbnailPath,
		Products.Name,Products.Id,Categories.Name as CategoryName, Brands.Name as BrandName,Stocks.AbleToSale,Stocks.Inventory
		from Stocks
		inner join Products on Products.Id=Stocks.ProductId
		left join Categories on Categories.Id=Products.CategoryId
		left join Brands on Brands.Id=Products.BrandId
	),Count_CE as(
		select COUNT(*) as [TotalRecord] from Main_CE
	)
	select * from Main_CE,Count_CE
	order by Main_CE.Id
	offset (@pageNumber-1)*@rowsOfPage rows
	fetch next @rowsOfPage row only
end;
--
select * from Suppliers 
-- Lấy ngẫu nhiên 12 sản phẩm theo danh mục
go
drop procedure if exists prGetRandomProduct
go
create procedure prGetRandomProduct
as 
begin
	select top 6 Products.Id,Products.Name,
	Products.RetailPrice, 
	Products.StarScore, Brands.Name as BrandName, Categories.Name as CategoryName,
	Products.Views,(select FullPath from ProductImages where ProductImages.ProductId=Products.Id and ProductImages.IsThumbnail=1) as ImagePath
	from Products
	left join Categories on Categories.Id=Products.CategoryId
	left join Brands on Brands.Id=Products.BrandId
	where Products.IsDelete=0
	order by NEWID()
end;
-- Lấy danh sách 16 sản phẩm bán chạy trong tháng theo danh mục
go
drop procedure if exists prGetBestSellProducts
go
create procedure prGetBestSellProducts
as
begin
	select top 12 OrderDetails.ProductId as Id,Products.Name,Categories.Name as CategoryName,
	Products.RetailPrice,
	Products.StarScore, Brands.Name as BrandName,
	Products.Views,(select FullPath from ProductImages where ProductImages.ProductId=Products.Id and ProductImages.IsThumbnail=1) as ImagePath
	from OrderDetails 
	inner join Orders on Orders.Id=OrderDetails.OrderId
	left join Products on Products.Id=OrderDetails.ProductId
	left join Categories on Categories.Id=Products.CategoryId
	left join Brands on Brands.Id=Products.BrandId
	WHERE MONTH(Orders.DateCreated)=MONTH(GETDATE()) and YEAR(Orders.DateCreated)=YEAR(GETDATE()) and Products.IsDelete=0
	group by OrderDetails.ProductId,Products.Name,Products.RetailPrice,Products.StarScore, Brands.Name,Products.Views,Products.Id,Categories.Name
	order by Sum(OrderDetails.Quantity) desc
end;

-- Lấy danh sách 20 sản phẩm mới về trong tháng
go
drop procedure if exists prGetNewArrivedProducts
go
create procedure prGetNewArrivedProducts
as
begin
	select top 20 PurchaseOrderDetails.ProductId as Id,Products.Name,
	Products.RetailPrice,
	Products.StarScore, Brands.Name as BrandName,
	Products.Views,(select FullPath from ProductImages where ProductImages.ProductId=Products.Id and ProductImages.IsThumbnail=1) as ImagePath
	from PurchaseOrderDetails
	inner join PurchaseOrders on PurchaseOrders.Id=PurchaseOrderDetails.PurchaseOrderId
	left join Products on Products.Id=PurchaseOrderDetails.ProductId
	left join Brands on Brands.Id=Products.BrandId
	where MONTH(PurchaseOrders.PurchaseDate)=MONTH(GETDATE()) and YEAR(PurchaseOrders.PurchaseDate)=YEAR(GETDATE()) and Products.IsDelete=0
	group by PurchaseOrderDetails.ProductId,Products.Name,Products.RetailPrice,Products.StarScore,
	Brands.Name,Products.Views,Products.Id
end;
-- Lấy danh sách 16 sản phẩm được nhiều lượt xem
go
drop procedure if exists prGetProductsTopViewed
go
create procedure prGetProductsTopViewed
as
begin
	select top 16 Products.Id as Id, Products.Name,Products.RetailPrice,
	Products.StarScore,Products.Views,Brands.Name as BrandName,
	(select FullPath from ProductImages where ProductImages.ProductId=Products.Id and ProductImages.IsThumbnail=1) as ImagePath
	from Products
	left join Brands on Brands.Id=Products.BrandId 
	where Products.Views >= 1000 and Products.IsDelete=0
	order by Products.Views desc
end;
--Lấy danh sách 16 sản phẩm được nhiều lượt bình chọn 
go
drop procedure if exists prGetProductsTopRated
go
create procedure prGetProductsTopRated
as
begin
	select top 16 Products.Id, Products.Name,Products.RetailPrice,
	Products.StarScore,Products.Views,Brands.Name as BrandName,
	(select FullPath from ProductImages where ProductImages.ProductId=Products.Id and ProductImages.IsThumbnail=1) as ImagePath
	from Products 
	left join Brands on Brands.Id=Products.BrandId
	WHERE Products.StarScore >=1 and Products.IsDelete=0
	order by Products.StarScore desc
end;
--Lấy danh sách 16 sản phẩm bán chạy nhất tháng
go
drop procedure if exists prGetFeaturedProducts
go
create procedure prGetFeaturedProducts
as
begin
	select top 16 Products.Id,Products.Name,Products.RetailPrice,
	Products.StarScore,Products.Views,Brands.Name as BrandName,
	(select FullPath from ProductImages where ProductImages.ProductId=Products.Id and ProductImages.IsThumbnail=1) as ImagePath
	from OrderDetails
	left join Products on Products.Id=OrderDetails.ProductId
	left join Brands on Brands.Id=Products.BrandId
	group by OrderDetails.ProductId,Products.Id,Products.Name,Products.RetailPrice,Products.StarScore,Products.Views,Brands.Name
	order by SUM(OrderDetails.Quantity) desc
end;
--Lấy danh sách tài khoản trong hệ thống phân trang
go
drop procedure if exists prGetAccountsPaging
go
create procedure prGetAccountsPaging 
(@pageNumber int,@rowsOfPage int)
as
begin
	with Main_CTE as(
		select AppUsers.Id,UserName,Email,AppUsers.PhoneNumber,
		Customers.FullName as CustomerName,Employees.FullName as EmployeeName,AppUsers.DateCreated
		from AppUsers
		left join Employees on Employees.UserId=AppUsers.Id
		left join Customers on Customers.UserId=AppUsers.Id
	),Count_CTE as(
		select COUNT(*) as [TotalRecord] from Main_CTE
	)
	select * from Main_CTE, Count_CTE
	order by Main_CTE.UserName asc
	offset (@pageNumber-1)*@rowsOfPage rows
	fetch next @rowsOfPage row only
end;
-- Lấy thông tin chi tiết tài khoản
go
drop procedure if exists prGetAccountDetail
go
create procedure prGetAccountDetail
(@userId uniqueidentifier)
as
begin
	select AppUsers.Id,UserName,Email,PhoneNumber,ISNULL(Customers.Address,Employees.Address) as [Address],
	ISNULL(Customers.Gender,Employees.Gender) as Gender,
	ISNULL(Customers.FullName,Employees.FullName) as FullName from AppUsers
	left join Customers on Customers.UserId=AppUsers.Id
	left join Employees on Employees.UserId=AppUsers.Id
	where AppUsers.Id=@userId
end;

--Lấy quyền mà tài khoản không thuộc về
go
drop procedure if exists prGetAccountNotInRoles
go
create procedure prGetAccountNotInRoles
(@userId uniqueidentifier)
as
begin
	select AppRoles.Id,AppRoles.Description as Name
	from AppRoles
	cross join AppUserRoles 
	cross join AppUsers
	where AppUsers.Id=@userId and AppRoles.Id not in 
	(select RoleId from AppUserRoles where UserId=@userId)
	group by AppRoles.Id,AppRoles.Description
end;

--Lấy quyền mà tài khoản thuộc về
go
drop procedure if exists prGetAccountRoles
go
create procedure prGetAccountRoles
(@userId uniqueidentifier)
as
begin
	select AppRoles.Id,AppRoles.Description as Name
	from AppUsers
	inner join AppUserRoles on AppUserRoles.UserId=AppUsers.Id
	inner join AppRoles on AppRoles.Id=AppUserRoles.RoleId
	where AppUsers.Id=@userId
end;
