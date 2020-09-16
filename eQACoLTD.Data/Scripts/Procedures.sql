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






--Thêm dữ liệu
insert into Categories(Id,Name) VALUES('dabde5d3-3311-4836-8b37-18df01b9a371',N'Máy in')
insert into Categories(Id,Name) VALUES('45cb72fd-9ced-4de2-aae8-08dbb42fdbca',N'Bàn phím')
insert into Categories(Id,Name) VALUES('9a3f3ba5-76db-434a-acae-18fc90f1e1d6',N'Chuột')
insert into Categories(Id,Name) VALUES('2fac847b-9fa2-4e68-803f-9bc331fe5a02',N'Tai nghe')

insert into Brands(Id,Name) VALUES('7d6affea-fde6-4136-a532-0909d68bc802',N'Acer')
insert into Brands(Id,Name) VALUES('f1756f5a-bf11-4008-943a-9aa69f40e478',N'Asus')
insert into Brands(Id,Name) VALUES('01e4179b-1042-489c-8470-984badfd6433',N'Lenovo')
insert into Brands(Id,Name) VALUES('af7c139a-02d4-4f24-99c6-d49173de2b19',N'Canon')
insert into Brands(Id,Name) VALUES('992583af-900b-4318-93d5-008326458c4d',N'Razer')
insert into Brands(Id,Name) VALUES('8e64301e-0a6c-48f5-b004-c5c0ffa155f5',N'Logitech')


insert into Products(Id,Name,CategoryId,Views,RetailPrice,WholesalePrices,BrandId,StarScore,WarrantyPeriod)
VALUES('PRN0003',N'iMac 2017','94b4b0d2-d389-4ded-a61b-50249e14525a',2000,32500000,32000000,'8f968a61-69da-4581-8279-61516dc17639',1,36)

insert into Products(Id,Name,CategoryId,Views,RetailPrice,WholesalePrices,BrandId,StarScore,WarrantyPeriod)
VALUES('PRN0004',N'iMac 2020','94b4b0d2-d389-4ded-a61b-50249e14525a',1234,42000000,41500000,'8f968a61-69da-4581-8279-61516dc17639',1,36)

insert into Products(Id,Name,CategoryId,Views,RetailPrice,WholesalePrices,BrandId,StarScore,WarrantyPeriod)
VALUES('PRN0005',N'Nitro 5','94b4b0d2-d389-4ded-a61b-50249e14525a',2040,22500000,22000000,'7d6affea-fde6-4136-a532-0909d68bc802',1,36)

insert into Products(Id,Name,CategoryId,Views,RetailPrice,WholesalePrices,BrandId,StarScore,WarrantyPeriod)
VALUES('PRN0006',N'Predator Triton','94b4b0d2-d389-4ded-a61b-50249e14525a',2100,52500000,510000000,'7d6affea-fde6-4136-a532-0909d68bc802',1,36)

insert into Products(Id,Name,CategoryId,Views,RetailPrice,WholesalePrices,BrandId,StarScore,WarrantyPeriod)
VALUES('PRN0007',N'Aspire 3','94b4b0d2-d389-4ded-a61b-50249e14525a',2300,9350000,9000000,'7d6affea-fde6-4136-a532-0909d68bc802',1,36)

insert into Products(Id,Name,CategoryId,Views,RetailPrice,WholesalePrices,BrandId,StarScore,WarrantyPeriod)
VALUES('PRN0008',N'Swift 3','94b4b0d2-d389-4ded-a61b-50249e14525a',2300,12350000,12000000,'7d6affea-fde6-4136-a532-0909d68bc802',1,36)

insert into Products(Id,Name,CategoryId,Views,RetailPrice,WholesalePrices,BrandId,StarScore,WarrantyPeriod)
VALUES('PRN0009',N'Vivobook','94b4b0d2-d389-4ded-a61b-50249e14525a',2695,12500000,12000000,'f1756f5a-bf11-4008-943a-9aa69f40e478',1,36)

insert into Products(Id,Name,CategoryId,Views,RetailPrice,WholesalePrices,BrandId,StarScore,WarrantyPeriod)
VALUES('PRN0010',N'Zenbook','94b4b0d2-d389-4ded-a61b-50249e14525a',2700,22500000,22000000,'f1756f5a-bf11-4008-943a-9aa69f40e478',1,36)

insert into Products(Id,Name,CategoryId,Views,RetailPrice,WholesalePrices,BrandId,StarScore,WarrantyPeriod)
VALUES('PRN0011',N'ROG Strix','94b4b0d2-d389-4ded-a61b-50249e14525a',2000,32500000,32000000,'f1756f5a-bf11-4008-943a-9aa69f40e478',1,36)

insert into Products(Id,Name,CategoryId,Views,RetailPrice,WholesalePrices,BrandId,StarScore,WarrantyPeriod)
VALUES('PRN00012',N'Thinkpad X250','94b4b0d2-d389-4ded-a61b-50249e14525a',2000,12500000,12000000,'01e4179b-1042-489c-8470-984badfd6433',1,36)

insert into Products(Id,Name,CategoryId,Views,RetailPrice,WholesalePrices,BrandId,StarScore,WarrantyPeriod)
VALUES('PRN00013',N'Thinkpad X260','94b4b0d2-d389-4ded-a61b-50249e14525a',2000,15500000,15000000,'01e4179b-1042-489c-8470-984badfd6433',1,36)

insert into Products(Id,Name,CategoryId,Views,RetailPrice,WholesalePrices,BrandId,StarScore,WarrantyPeriod)
VALUES('PRN00014',N'Thinkpad X270','94b4b0d2-d389-4ded-a61b-50249e14525a',2000,18500000,18000000,'01e4179b-1042-489c-8470-984badfd6433',1,36)

insert into Products(Id,Name,CategoryId,Views,RetailPrice,WholesalePrices,BrandId,StarScore,WarrantyPeriod)
VALUES('PRN00015',N'Thinkpad X280','94b4b0d2-d389-4ded-a61b-50249e14525a',2000,21500000,20500000,'01e4179b-1042-489c-8470-984badfd6433',1,36)

insert into Products(Id,Name,CategoryId,Views,RetailPrice,WholesalePrices,BrandId,StarScore,WarrantyPeriod)
VALUES('PRN00016',N'Thinkpad X290','94b4b0d2-d389-4ded-a61b-50249e14525a',2000,27500000,27000000,'01e4179b-1042-489c-8470-984badfd6433',1,36)

insert into Products(Id,Name,CategoryId,Views,RetailPrice,WholesalePrices,BrandId,StarScore,WarrantyPeriod)
VALUES('PRN00017',N'Canon LBP 290','dabde5d3-3311-4836-8b37-18df01b9a371',2000,2500000,2000000,'af7c139a-02d4-4f24-99c6-d49173de2b19',1,36)

insert into Products(Id,Name,CategoryId,Views,RetailPrice,WholesalePrices,BrandId,StarScore,WarrantyPeriod)
VALUES('PRN00018',N'Canon LBP 290','dabde5d3-3311-4836-8b37-18df01b9a371',2000,2500000,2000000,'af7c139a-02d4-4f24-99c6-d49173de2b19',1,36)

insert into Products(Id,Name,CategoryId,Views,RetailPrice,WholesalePrices,BrandId,StarScore,WarrantyPeriod)
VALUES('PRN00019',N'Canon LBP 310','dabde5d3-3311-4836-8b37-18df01b9a371',2000,2800000,2750000,'af7c139a-02d4-4f24-99c6-d49173de2b19',1,36)

insert into Products(Id,Name,CategoryId,Views,RetailPrice,WholesalePrices,BrandId,StarScore,WarrantyPeriod)
VALUES('PRN00020',N'Canon LBP X320','dabde5d3-3311-4836-8b37-18df01b9a371',2000,3500000,3000000,'af7c139a-02d4-4f24-99c6-d49173de2b19',1,36)

insert into Products(Id,Name,CategoryId,Views,RetailPrice,WholesalePrices,BrandId,StarScore,WarrantyPeriod)
VALUES('PRN00021',N'Canon LBP Z320','dabde5d3-3311-4836-8b37-18df01b9a371',2000,4500000,4250000,'af7c139a-02d4-4f24-99c6-d49173de2b19',1,36)

insert into Products(Id,Name,CategoryId,Views,RetailPrice,WholesalePrices,BrandId,StarScore,WarrantyPeriod)
VALUES('PRN00022',N'G102','9a3f3ba5-76db-434a-acae-18fc90f1e1d6',2000,500000,450000,'8e64301e-0a6c-48f5-b004-c5c0ffa155f5',1,36)

insert into Products(Id,Name,CategoryId,Views,RetailPrice,WholesalePrices,BrandId,StarScore,WarrantyPeriod)
VALUES('PRN00023',N'G103','9a3f3ba5-76db-434a-acae-18fc90f1e1d6',2000,540000,535000,'8e64301e-0a6c-48f5-b004-c5c0ffa155f5',1,36)

insert into Products(Id,Name,CategoryId,Views,RetailPrice,WholesalePrices,BrandId,StarScore,WarrantyPeriod)
VALUES('PRN00024',N'G104','9a3f3ba5-76db-434a-acae-18fc90f1e1d6',2000,580000,5500000,'8e64301e-0a6c-48f5-b004-c5c0ffa155f5',1,36)

insert into Products(Id,Name,CategoryId,Views,RetailPrice,WholesalePrices,BrandId,StarScore,WarrantyPeriod)
VALUES('PRN00025',N'G105','9a3f3ba5-76db-434a-acae-18fc90f1e1d6',2000,600000,580000,'8e64301e-0a6c-48f5-b004-c5c0ffa155f5',1,36)

insert into Products(Id,Name,CategoryId,Views,RetailPrice,WholesalePrices,BrandId,StarScore,WarrantyPeriod)
VALUES('PRN00026',N'G106','9a3f3ba5-76db-434a-acae-18fc90f1e1d6',2000,630000,600000,'8e64301e-0a6c-48f5-b004-c5c0ffa155f5',1,36)

insert into Products(Id,Name,CategoryId,Views,RetailPrice,WholesalePrices,BrandId,StarScore,WarrantyPeriod)
VALUES('PRN00027',N'Kraken Pro V1','2fac847b-9fa2-4e68-803f-9bc331fe5a02',2300,2500000,2450000,'992583af-900b-4318-93d5-008326458c4d',1,36)

insert into Products(Id,Name,CategoryId,Views,RetailPrice,WholesalePrices,BrandId,StarScore,WarrantyPeriod)
VALUES('PRN00028',N'Kraken Pro V2','2fac847b-9fa2-4e68-803f-9bc331fe5a02',2000,3500000,3450000,'992583af-900b-4318-93d5-008326458c4d',1,36)

insert into Products(Id,Name,CategoryId,Views,RetailPrice,WholesalePrices,BrandId,StarScore,WarrantyPeriod)
VALUES('PRN00029',N'Kraken Pro V3','2fac847b-9fa2-4e68-803f-9bc331fe5a02',2000,4500000,4450000,'992583af-900b-4318-93d5-008326458c4d',1,36)

insert into Products(Id,Name,CategoryId,Views,RetailPrice,WholesalePrices,BrandId,StarScore,WarrantyPeriod)
VALUES('PRN00030',N'Kraken Pro V4','2fac847b-9fa2-4e68-803f-9bc331fe5a02',2000,42500000,4150000,'992583af-900b-4318-93d5-008326458c4d',1,36)

insert into Products(Id,Name,CategoryId,Views,RetailPrice,WholesalePrices,BrandId,StarScore,WarrantyPeriod)
VALUES('PRN00031',N'Hammerhead','2fac847b-9fa2-4e68-803f-9bc331fe5a02',2000,6500000,6450000,'992583af-900b-4318-93d5-008326458c4d',1,36)


insert into Suppliers(Id,Name,Address) VALUES('SUN0003',N'Công ty TNHH A',N'Số 99, Hải Triều, Quán Toan, Hồng Bàng, Hải Phòng')
insert into Suppliers(Id,Name,Address) VALUES('SUN0004',N'Công ty TNHH B',N'Số 98, Hải Triều, Quán Toan, Hồng Bàng, Hải Phòng')
insert into Suppliers(Id,Name,Address) VALUES('SUN0005',N'Công ty TNHH C',N'Số 97, Hải Triều, Quán Toan, Hồng Bàng, Hải Phòng')
insert into Suppliers(Id,Name,Address) VALUES('SUN0006',N'Công ty TNHH D',N'Số 96, Hải Triều, Quán Toan, Hồng Bàng, Hải Phòng')
insert into Suppliers(Id,Name,Address) VALUES('SUN0007',N'Công ty TNHH E',N'Số 95, Hải Triều, Quán Toan, Hồng Bàng, Hải Phòng')
insert into Suppliers(Id,Name,Address) VALUES('SUN0008',N'Công ty TNHH F',N'Số 94, Hải Triều, Quán Toan, Hồng Bàng, Hải Phòng')
insert into Suppliers(Id,Name,Address) VALUES('SUN0009',N'Công ty TNHH G',N'Số 93, Hải Triều, Quán Toan, Hồng Bàng, Hải Phòng')
insert into Suppliers(Id,Name,Address) VALUES('SUN0010',N'Công ty TNHH H',N'Số 92, Hải Triều, Quán Toan, Hồng Bàng, Hải Phòng')
insert into Suppliers(Id,Name,Address) VALUES('SUN0011',N'Công ty TNHH J',N'Số 91, Hải Triều, Quán Toan, Hồng Bàng, Hải Phòng')
insert into Suppliers(Id,Name,Address) VALUES('SUN0012',N'Công ty TNHH K',N'Số 90, Hải Triều, Quán Toan, Hồng Bàng, Hải Phòng')
insert into Suppliers(Id,Name,Address) VALUES('SUN0013',N'Công ty TNHH L',N'Số 89, Hải Triều, Quán Toan, Hồng Bàng, Hải Phòng')


insert into PurchaseOrders(Id,SupplierId,DateCreated,PurchaseDate,OrderStatusId,DeliveryDate,PaymentStatusId,DiscountValue)
VALUES ('PON0003','SUN0003',GETDATE(),GETDATE(),'0b243072-2e23-4397-81b5-cbf887b1d8d3',GETDATE(),'0b710f3a-6426-4dff-89ec-afc9f154b668',0.00)
insert into PurchaseOrderDetails(Id,PurchaseOrderId,ProductId,Quantity,UnitPrice) 
VALUES('03d117de-96a2-4f65-8d46-ef7e25e10a69','PON0003','PRN0003',100,30000000)
insert into PaymentVouchers(Id,PurchaseOrderId,Paid,PayDate,PaymentMethodId)
VALUES('PVN0003','PON0003',2500000000,GETDATE(),'28021394-ea05-45aa-ab43-30cbe1cd53fc')

insert into PurchaseOrders(Id,SupplierId,DateCreated,PurchaseDate,OrderStatusId,DeliveryDate,PaymentStatusId,DiscountValue)
VALUES ('PON0004','SUN0004',GETDATE(),GETDATE(),'f1e884de-b62d-43ad-9ec1-6bfe13e9aadf',GETDATE(),'f3288f36-e405-413f-baa3-5c232ea7bd3f',0.00)
insert into PurchaseOrderDetails(Id,PurchaseOrderId,ProductId,Quantity,UnitPrice) 
VALUES('ab179c4e-d5d1-45d6-bbcf-d36403cdfd7b','PON0004','PRN0004',10,40000000)
insert into PaymentVouchers(Id,PurchaseOrderId,Paid,PayDate,PaymentMethodId)
VALUES('PVN0004','PON0004',400000000,GETDATE(),'28021394-ea05-45aa-ab43-30cbe1cd53fc')

insert into PurchaseOrders(Id,SupplierId,DateCreated,PurchaseDate,OrderStatusId,DeliveryDate,PaymentStatusId,DiscountValue)
VALUES ('PON0005','SUN0005',GETDATE(),GETDATE(),'f1e884de-b62d-43ad-9ec1-6bfe13e9aadf',GETDATE(),'f3288f36-e405-413f-baa3-5c232ea7bd3f',0.00)
insert into PurchaseOrderDetails(Id,PurchaseOrderId,ProductId,Quantity,UnitPrice) 
VALUES('d2a0a5c3-bbdf-43b4-9f61-867c6a426427','PON0005','PRN0005',100,22000000)
insert into PaymentVouchers(Id,PurchaseOrderId,Paid,PayDate,PaymentMethodId)
VALUES('PVN0005','PON0005',2200000000,GETDATE(),'28021394-ea05-45aa-ab43-30cbe1cd53fc')

insert into PurchaseOrders(Id,SupplierId,DateCreated,PurchaseDate,OrderStatusId,DeliveryDate,PaymentStatusId,DiscountValue)
VALUES ('PON0006','SUN0006',GETDATE(),GETDATE(),'f1e884de-b62d-43ad-9ec1-6bfe13e9aadf',GETDATE(),'f3288f36-e405-413f-baa3-5c232ea7bd3f',0.00)
insert into PurchaseOrderDetails(Id,PurchaseOrderId,ProductId,Quantity,UnitPrice) 
VALUES('9b9b8b56-1f31-488f-b9e4-881828edc4e3','PON0006','PRN0006',10,52000000)
insert into PaymentVouchers(Id,PurchaseOrderId,Paid,PayDate,PaymentMethodId)
VALUES('PVN0006','PON0006',520000000,GETDATE(),'28021394-ea05-45aa-ab43-30cbe1cd53fc')

insert into PurchaseOrders(Id,SupplierId,DateCreated,PurchaseDate,OrderStatusId,DeliveryDate,PaymentStatusId,DiscountValue)
VALUES ('PON0007','SUN0007',GETDATE(),GETDATE(),'0b243072-2e23-4397-81b5-cbf887b1d8d3',GETDATE(),'73e1a911-ff43-4573-981b-3c363d4146e4',0.00)
insert into PurchaseOrderDetails(Id,PurchaseOrderId,ProductId,Quantity,UnitPrice) 
VALUES('2435d63d-73a7-4f14-80af-e282386982c6','PON0007','PRN0007',100,9000000)
insert into PaymentVouchers(Id,PurchaseOrderId,Paid,PayDate,PaymentMethodId)
VALUES('PVN0007','PON0007',0,GETDATE(),'28021394-ea05-45aa-ab43-30cbe1cd53fc')

insert into PurchaseOrders(Id,SupplierId,DateCreated,PurchaseDate,OrderStatusId,DeliveryDate,PaymentStatusId,DiscountValue)
VALUES ('PON0008','SUN0008',GETDATE(),GETDATE(),'0b243072-2e23-4397-81b5-cbf887b1d8d3',GETDATE(),'73e1a911-ff43-4573-981b-3c363d4146e4',0.00)
insert into PurchaseOrderDetails(Id,PurchaseOrderId,ProductId,Quantity,UnitPrice) 
VALUES('e3ff3913-0c7c-405a-8dbc-8f0b780fd25d','PON0008','PRN0008',10,12000000)
insert into PaymentVouchers(Id,PurchaseOrderId,Paid,PayDate,PaymentMethodId)
VALUES('PVN0008','PON0008',0,GETDATE(),'28021394-ea05-45aa-ab43-30cbe1cd53fc')

insert into PurchaseOrders(Id,SupplierId,DateCreated,PurchaseDate,OrderStatusId,DeliveryDate,PaymentStatusId,DiscountValue)
VALUES ('PON0009','SUN0009',GETDATE(),GETDATE(),'f1e884de-b62d-43ad-9ec1-6bfe13e9aadf',GETDATE(),'f3288f36-e405-413f-baa3-5c232ea7bd3f',0.00)
insert into PurchaseOrderDetails(Id,PurchaseOrderId,ProductId,Quantity,UnitPrice) 
VALUES('a86650db-bd73-4e0e-84ff-90d1ace73627','PON0009','PRN0009',10,12000000)
insert into PaymentVouchers(Id,PurchaseOrderId,Paid,PayDate,PaymentMethodId)
VALUES('PVN0009','PON0009',120000000,GETDATE(),'28021394-ea05-45aa-ab43-30cbe1cd53fc')

insert into PurchaseOrders(Id,SupplierId,DateCreated,PurchaseDate,OrderStatusId,DeliveryDate,PaymentStatusId,DiscountValue)
VALUES ('PON0010','SUN0010',GETDATE(),GETDATE(),'f1e884de-b62d-43ad-9ec1-6bfe13e9aadf',GETDATE(),'f3288f36-e405-413f-baa3-5c232ea7bd3f',0.00)
insert into PurchaseOrderDetails(Id,PurchaseOrderId,ProductId,Quantity,UnitPrice) 
VALUES('9cb9cd0f-bd17-427d-88d4-dcf2b8f3b9b9','PON0010','PRN0010',10,22000000)
insert into PaymentVouchers(Id,PurchaseOrderId,Paid,PayDate,PaymentMethodId)
VALUES('PVN0010','PON0010',220000000,GETDATE(),'28021394-ea05-45aa-ab43-30cbe1cd53fc')

insert into PurchaseOrders(Id,SupplierId,DateCreated,PurchaseDate,OrderStatusId,DeliveryDate,PaymentStatusId,DiscountValue)
VALUES ('PON0011','SUN0011',GETDATE(),GETDATE(),'f1e884de-b62d-43ad-9ec1-6bfe13e9aadf',GETDATE(),'f3288f36-e405-413f-baa3-5c232ea7bd3f',0.00)
insert into PurchaseOrderDetails(Id,PurchaseOrderId,ProductId,Quantity,UnitPrice) 
VALUES('e6ab6df8-cca2-4317-aefe-4a23b88586a3','PON0011','PRN0011',10,32000000)
insert into PaymentVouchers(Id,PurchaseOrderId,Paid,PayDate,PaymentMethodId)
VALUES('PVN0011','PON0011',320000000,GETDATE(),'28021394-ea05-45aa-ab43-30cbe1cd53fc')


insert into Orders(Id,CustomerId,DateCreated,OrderStatusId,PaymentStatusId,DiscountValue)
VALUES('ODN0003','CUS0001',GETDATE(),'f1e884de-b62d-43ad-9ec1-6bfe13e9aadf','f3288f36-e405-413f-baa3-5c232ea7bd3f',0.00)
insert into OrderDetails(Id,OrderId,ProductId,Quantity,UnitPrice)
VALUES('e6f23913-f4b1-4908-9020-230ff2eee264','ODN0003','PRN0003',2,32500000)
insert into ReceiptVouchers(Id,OrderId,Received,ReceivedDate,PaymentMethodId)
VALUES('RVN0003','ODN0003',65000000,GETDATE(),'8eb1249d-277d-450c-a023-92f80fc31f7f')

insert into Orders(Id,CustomerId,DateCreated,OrderStatusId,PaymentStatusId,DiscountValue)
VALUES('ODN0004','CUS0001',GETDATE(),'f1e884de-b62d-43ad-9ec1-6bfe13e9aadf','f3288f36-e405-413f-baa3-5c232ea7bd3f',0.00)
insert into OrderDetails(Id,OrderId,ProductId,Quantity,UnitPrice)
VALUES('bf5a1cca-3dd8-41a7-89b0-644cd2efc93f','ODN0004','PRN0004',1,42000000)
insert into ReceiptVouchers(Id,OrderId,Received,ReceivedDate,PaymentMethodId)
VALUES('RVN0004','ODN0004',42000000,GETDATE(),'8eb1249d-277d-450c-a023-92f80fc31f7f')

insert into Orders(Id,CustomerId,DateCreated,OrderStatusId,PaymentStatusId,DiscountValue)
VALUES('ODN0005','CUS0001',GETDATE(),'f1e884de-b62d-43ad-9ec1-6bfe13e9aadf','f3288f36-e405-413f-baa3-5c232ea7bd3f',0.00)
insert into OrderDetails(Id,OrderId,ProductId,Quantity,UnitPrice)
VALUES('d0663a4c-52a6-49e6-85f8-462fe06321ac','ODN0005','PRN0005',1,22500000)
insert into ReceiptVouchers(Id,OrderId,Received,ReceivedDate,PaymentMethodId)
VALUES('RVN0005','ODN0005',22500000,GETDATE(),'8eb1249d-277d-450c-a023-92f80fc31f7f')

insert into Orders(Id,CustomerId,DateCreated,OrderStatusId,PaymentStatusId,DiscountValue)
VALUES('ODN0006','CUS0001',GETDATE(),'f1e884de-b62d-43ad-9ec1-6bfe13e9aadf','f3288f36-e405-413f-baa3-5c232ea7bd3f',0.00)
insert into OrderDetails(Id,OrderId,ProductId,Quantity,UnitPrice)
VALUES('6006ec40-abb1-4563-aaa5-db40fc8da52c','ODN0006','PRN0006',1,52500000)
insert into ReceiptVouchers(Id,OrderId,Received,ReceivedDate,PaymentMethodId)
VALUES('RVN0006','ODN0006',52500000,GETDATE(),'8eb1249d-277d-450c-a023-92f80fc31f7f')

insert into Orders(Id,CustomerId,DateCreated,OrderStatusId,PaymentStatusId,DiscountValue)
VALUES('ODN0007','CUS0001',GETDATE(),'f1e884de-b62d-43ad-9ec1-6bfe13e9aadf','f3288f36-e405-413f-baa3-5c232ea7bd3f',0.00)
insert into OrderDetails(Id,OrderId,ProductId,Quantity,UnitPrice)
VALUES('2ae60321-773f-48f0-9c66-25179ca216a1','ODN0007','PRN0007',1,9350000)
insert into ReceiptVouchers(Id,OrderId,Received,ReceivedDate,PaymentMethodId)
VALUES('RVN0007','ODN0007',9350000,GETDATE(),'8eb1249d-277d-450c-a023-92f80fc31f7f')

insert into Orders(Id,CustomerId,DateCreated,OrderStatusId,PaymentStatusId,DiscountValue)
VALUES('ODN0008','CUS0002',GETDATE(),'f1e884de-b62d-43ad-9ec1-6bfe13e9aadf','f3288f36-e405-413f-baa3-5c232ea7bd3f',0.00)
insert into OrderDetails(Id,OrderId,ProductId,Quantity,UnitPrice)
VALUES('825050ee-defb-4cd2-b57f-bdb0c4a53d2a','ODN0008','PRN0008',1,12350000)
insert into ReceiptVouchers(Id,OrderId,Received,ReceivedDate,PaymentMethodId)
VALUES('RVN0008','ODN0008',12350000,GETDATE(),'8eb1249d-277d-450c-a023-92f80fc31f7f')

insert into Orders(Id,CustomerId,DateCreated,OrderStatusId,PaymentStatusId,DiscountValue)
VALUES('ODN0009','CUS0002',GETDATE(),'f1e884de-b62d-43ad-9ec1-6bfe13e9aadf','f3288f36-e405-413f-baa3-5c232ea7bd3f',0.00)
insert into OrderDetails(Id,OrderId,ProductId,Quantity,UnitPrice)
VALUES('bf690d30-9ec7-4e23-a9ce-57b3598e5354','ODN0009','PRN0009',1,12500000)
insert into ReceiptVouchers(Id,OrderId,Received,ReceivedDate,PaymentMethodId)
VALUES('RVN0009','ODN0009',12500000,GETDATE(),'8eb1249d-277d-450c-a023-92f80fc31f7f')

insert into Orders(Id,CustomerId,DateCreated,OrderStatusId,PaymentStatusId,DiscountValue)
VALUES('ODN0010','CUS0002',GETDATE(),'f1e884de-b62d-43ad-9ec1-6bfe13e9aadf','f3288f36-e405-413f-baa3-5c232ea7bd3f',0.00)
insert into OrderDetails(Id,OrderId,ProductId,Quantity,UnitPrice)
VALUES('c18ea094-de2d-4934-b970-2f3d20e24ee5','ODN0010','PRN0010',1,22500000)
insert into ReceiptVouchers(Id,OrderId,Received,ReceivedDate,PaymentMethodId)
VALUES('RVN0010','ODN0010',22500000,GETDATE(),'8eb1249d-277d-450c-a023-92f80fc31f7f')

insert into Orders(Id,CustomerId,DateCreated,OrderStatusId,PaymentStatusId,DiscountValue)
VALUES('ODN0011','CUS0002',GETDATE(),'f1e884de-b62d-43ad-9ec1-6bfe13e9aadf','f3288f36-e405-413f-baa3-5c232ea7bd3f',0.00)
insert into OrderDetails(Id,OrderId,ProductId,Quantity,UnitPrice)
VALUES('bb2da4f7-ff64-4b90-93b9-4cd77f7b7924','ODN0011','PRN0011',1,32500000)
insert into ReceiptVouchers(Id,OrderId,Received,ReceivedDate,PaymentMethodId)
VALUES('RVN0011','ODN0011',32500000,GETDATE(),'8eb1249d-277d-450c-a023-92f80fc31f7f')

