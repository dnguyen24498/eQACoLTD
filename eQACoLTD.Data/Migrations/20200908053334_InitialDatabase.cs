using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eQACoLTD.Data.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserLogins",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: true),
                    ProviderKey = table.Column<string>(nullable: true),
                    ProviderDisplayName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserLogins", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AppUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserRoles", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(type: "varchar(14)", nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(type: "varchar(150)", nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<string>(type: "varchar(150)", nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(30)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 9, 8, 12, 33, 33, 368, DateTimeKind.Local).AddTicks(1666))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserTokens", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    ImagePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    ThumbnailImagePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 9, 8, 12, 33, 33, 375, DateTimeKind.Local).AddTicks(2209))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiscountTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatuses",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentStatuses",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShippingStatus",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockActions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockActions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transporters",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transporters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Description = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    FromDate = table.Column<DateTime>(nullable: false),
                    ToDate = table.Column<DateTime>(nullable: false),
                    AppUserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppRoles_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(12)", nullable: false),
                    Dob = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    FullName = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    Gender = table.Column<bool>(nullable: true),
                    AvatarPath = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false),
                    UserId = table.Column<Guid>(nullable: true),
                    DefaultPhoneNumber = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(12)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Information = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    CategoryId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(type: "nvarchar(4000)", nullable: true),
                    Views = table.Column<int>(nullable: false, defaultValue: 0),
                    RetailPrice = table.Column<decimal>(nullable: false, defaultValue: 0m),
                    WholesalePrices = table.Column<decimal>(nullable: false, defaultValue: 0m),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false),
                    BrandId = table.Column<string>(nullable: true),
                    StarScore = table.Column<int>(nullable: false, defaultValue: 0),
                    WarrantyPeriod = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(12)", nullable: false),
                    Dob = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    FullName = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    Gender = table.Column<bool>(nullable: true),
                    AvatarPath = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false),
                    UserId = table.Column<Guid>(nullable: true),
                    CustomerTypeId = table.Column<string>(nullable: true),
                    DefaultPhoneNumber = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_CustomerTypes_CustomerTypeId",
                        column: x => x.CustomerTypeId,
                        principalTable: "CustomerTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(12)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(300)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(30)", nullable: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false),
                    Email = table.Column<string>(type: "varchar(100)", nullable: true),
                    Fax = table.Column<string>(type: "varchar(30)", nullable: true),
                    EmployeeId = table.Column<string>(nullable: true),
                    Website = table.Column<string>(type: "varchar(150)", nullable: true),
                    Description = table.Column<string>(type: "varchar(250)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suppliers_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<string>(nullable: false),
                    Quantity = table.Column<int>(nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => new { x.UserId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_Carts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Carts_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    ProductId = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    FullPath = table.Column<string>(nullable: true),
                    IsThumbnail = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductReviews",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    ProductId = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: true),
                    Title = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    StarScore = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductReviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductReviews_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    ProductId = table.Column<string>(nullable: false),
                    AbleToSale = table.Column<int>(nullable: false, defaultValue: 0),
                    Inventory = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Stocks_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(12)", nullable: false),
                    CustomerId = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 9, 8, 12, 33, 33, 426, DateTimeKind.Local).AddTicks(5878)),
                    Note = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    OrderStatusId = table.Column<string>(nullable: true),
                    PaymentStatusId = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false),
                    DiscountTypeId = table.Column<string>(nullable: true),
                    DiscountValue = table.Column<decimal>(nullable: false),
                    DiscountDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_DiscountTypes_DiscountTypeId",
                        column: x => x.DiscountTypeId,
                        principalTable: "DiscountTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_OrderStatuses_OrderStatusId",
                        column: x => x.OrderStatusId,
                        principalTable: "OrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_PaymentStatuses_PaymentStatusId",
                        column: x => x.PaymentStatusId,
                        principalTable: "PaymentStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrders",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(12)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    SupplierId = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 9, 8, 12, 33, 33, 415, DateTimeKind.Local).AddTicks(7693)),
                    PurchaseDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 9, 8, 12, 33, 33, 415, DateTimeKind.Local).AddTicks(8141)),
                    OrderStatusId = table.Column<string>(nullable: true),
                    DeliveryDate = table.Column<DateTime>(nullable: false),
                    PaymentStatusId = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false),
                    DiscountTypeId = table.Column<string>(nullable: true),
                    DiscountValue = table.Column<decimal>(nullable: false),
                    DiscountDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_DiscountTypes_DiscountTypeId",
                        column: x => x.DiscountTypeId,
                        principalTable: "DiscountTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_OrderStatuses_OrderStatusId",
                        column: x => x.OrderStatusId,
                        principalTable: "OrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_PaymentStatuses_PaymentStatusId",
                        column: x => x.PaymentStatusId,
                        principalTable: "PaymentStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductReviewReplies",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    ProductReviewId = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: true),
                    Content = table.Column<string>(type: "nvarchar(500)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReviewReplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductReviewReplies_ProductReviews_ProductReviewId",
                        column: x => x.ProductReviewId,
                        principalTable: "ProductReviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductReviewReplies_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    OrderId = table.Column<string>(nullable: true),
                    ProductId = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false, defaultValue: 1),
                    UnitPrice = table.Column<decimal>(nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(300)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptVouchers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(12)", nullable: false),
                    OrderId = table.Column<string>(nullable: true),
                    Received = table.Column<decimal>(nullable: false, defaultValue: 0m),
                    ReceivedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 9, 8, 12, 33, 33, 432, DateTimeKind.Local).AddTicks(1867)),
                    PaymentMethodId = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false),
                    Description = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    SupplierId = table.Column<string>(nullable: true),
                    CustomerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptVouchers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceiptVouchers_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptVouchers_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptVouchers_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptVouchers_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShippingOrders",
                columns: table => new
                {
                    OrderId = table.Column<string>(nullable: false),
                    ShippingId = table.Column<string>(type: "varchar(20)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    CustomerId = table.Column<string>(nullable: true),
                    CustomerName = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(30)", nullable: true),
                    Fee = table.Column<decimal>(nullable: false, defaultValue: 0m),
                    TransporterId = table.Column<string>(nullable: true),
                    Address = table.Column<string>(type: "varchar(300)", nullable: true),
                    ShippingStatusId = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 9, 8, 12, 33, 33, 440, DateTimeKind.Local).AddTicks(7869))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingOrders", x => new { x.OrderId, x.ShippingId });
                    table.ForeignKey(
                        name: "FK_ShippingOrders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShippingOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShippingOrders_ShippingStatus_ShippingStatusId",
                        column: x => x.ShippingStatusId,
                        principalTable: "ShippingStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShippingOrders_Transporters_TransporterId",
                        column: x => x.TransporterId,
                        principalTable: "Transporters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentVouchers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(12)", nullable: false),
                    PurchaseOrderId = table.Column<string>(nullable: true),
                    Paid = table.Column<decimal>(nullable: false),
                    PayDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 9, 8, 12, 33, 33, 421, DateTimeKind.Local).AddTicks(4561)),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false),
                    PaymentMethodId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    CustomerId = table.Column<string>(nullable: true),
                    SupplierId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentVouchers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentVouchers_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentVouchers_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentVouchers_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentVouchers_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    PurchaseOrderId = table.Column<string>(nullable: true),
                    ProductId = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false, defaultValue: 1),
                    UnitPrice = table.Column<decimal>(nullable: false),
                    CostName = table.Column<string>(type: "nvarchar(400)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderDetails_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockHistories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    ProductId = table.Column<string>(nullable: true),
                    RecordDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 9, 8, 12, 33, 33, 436, DateTimeKind.Local).AddTicks(1279)),
                    EmployeeId = table.Column<string>(nullable: true),
                    StockActionId = table.Column<string>(nullable: true),
                    ChangeQuantity = table.Column<int>(nullable: false),
                    PurchaseOrderDetailId = table.Column<string>(nullable: true),
                    OrderDetailId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockHistories_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockHistories_OrderDetails_OrderDetailId",
                        column: x => x.OrderDetailId,
                        principalTable: "OrderDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockHistories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockHistories_PurchaseOrderDetails_PurchaseOrderDetailId",
                        column: x => x.PurchaseOrderDetailId,
                        principalTable: "PurchaseOrderDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockHistories_StockActions_StockActionId",
                        column: x => x.StockActionId,
                        principalTable: "StockActions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "AppUserId", "ConcurrencyStamp", "DateCreated", "Description", "FromDate", "Name", "NormalizedName", "ToDate" },
                values: new object[,]
                {
                    { new Guid("4f12c9bd-597f-41dc-9e75-602a094018ee"), null, "0c4245b9-f15f-4000-b711-e0aa4736d016", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Quyền quản trị viên", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Administrator", "Administrator", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("2c3d4339-4b5d-4a51-8657-ed0097edefc2"), null, "72b623e6-2792-43a6-b55b-aa413707eac0", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Quyền nhân viên kho", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "WarehouseStaff", "WarehouseStaff", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("d834062e-2a02-4e49-bf93-664ff8d6cfc9"), null, "e5fda3ca-e5ff-44fb-a006-4e26c4f9244a", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Quyền nhân viên lễ tân", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receptionist", "Receptionist", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("8e869c4f-c2e5-4b99-9af2-6d103f0fc8b7"), null, "8bcdc8a5-2069-438d-b010-dd680083026e", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Quyền nhân viên thu ngân", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cashier", "Cashier", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("8917bf80-34a9-4524-b6dc-a08fdb9c93e0"), new Guid("4f12c9bd-597f-41dc-9e75-602a094018ee") },
                    { new Guid("30aca85c-3f9b-4e55-a6f3-59b629ed0fac"), new Guid("8e869c4f-c2e5-4b99-9af2-6d103f0fc8b7") },
                    { new Guid("7bdac961-0b05-4494-89fa-60d08b98687b"), new Guid("2c3d4339-4b5d-4a51-8657-ed0097edefc2") }
                });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("a0a054bf-12c6-4b54-90e9-351fed0629a9"), 0, "d179c0be-51d4-4172-aab2-71c2cfa133e3", "dophuongthao@gmail.com", "1", false, null, "DOPHUONGTHAO@GMAIL.COM", "cus0002", "AQAAAAEAACcQAAAAEAYYBAIKaa1T2uzAHwF9dt0u6i5fSiaEjdgC+wcKbwp180juqocgT4rdNOLgKDGW3Q==", "1234567890", false, "", false, "cus0002" },
                    { new Guid("cde674e1-6c47-4b00-ac56-558e0d85beff"), 0, "4b51faef-b238-417e-977b-0f91cf14b4c8", "duongnguyenadhp@gmail.com", "1", false, null, "DUONGNGUYENADHP@GMAIL.COM", "cus0001", "AQAAAAEAACcQAAAAEKVI7hUgKzukM9Mf3kucxFHXQPIRjmADywderRDKQ0mzpKro/YDEtzURSPBXESdAxQ==", "1234567890", false, "", false, "cus0001" },
                    { new Guid("30aca85c-3f9b-4e55-a6f3-59b629ed0fac"), 0, "aad4d7a8-0283-4412-89fa-45103caaf1eb", "duongnguyenadhp1@gmail.com", "1", false, null, "DUONGNGUYENADHP1@GMAIL.COM", "EPN0001", "AQAAAAEAACcQAAAAENggjC8Qcnr7nVj9wWtOFEXKlrTBRr/1KVG0liecr+pjql9USz6AvXah6mVbnv7RtQ==", "1234567890", false, "", false, "epn0001" },
                    { new Guid("8917bf80-34a9-4524-b6dc-a08fdb9c93e0"), 0, "dc83e988-1d27-4e5a-8c58-6019713165b3", "dnguyen24498@gmail.com", "1", false, null, "DNGUYEN24498@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEO5CY+HAsiRPbRM2zIZNJBvXfDQy2Eur5xuIKwJpHo5EUd0ndID3HhqxgdQtln57Xg==", null, false, "", false, "admin" },
                    { new Guid("7bdac961-0b05-4494-89fa-60d08b98687b"), 0, "3bc80606-8e14-4697-a679-7d53c3ba7886", "duongnguyenadhp2@gmail.com", "1", false, null, "DUONGNGUYENADHP2@GMAIL.COM", "epn0002", "AQAAAAEAACcQAAAAEBcK7crSQ/foPah+lW+RLyG4bamHkJ2lEOF0RIDDIaabQr7HTnWaYyNksTifOPorrw==", "1234567890", false, "", false, "epn0002" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Description", "ImagePath", "Name" },
                values: new object[,]
                {
                    { "8f968a61-69da-4581-8279-61516dc17639", null, null, "Apple" },
                    { "fd9bf3ed-6e72-4751-bef8-c207b74bc7d2", null, null, "Dell" },
                    { "742f5bf2-9476-46ea-b3ab-bfe9feb523fc", null, null, "HP" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name", "ThumbnailImagePath" },
                values: new object[,]
                {
                    { "94b4b0d2-d389-4ded-a61b-50249e14525a", null, "Laptop", null },
                    { "8c92bd25-48e6-4c45-9b27-8dd655d86ba1", null, "PC", null }
                });

            migrationBuilder.InsertData(
                table: "CustomerTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { "0cacdb29-9fd7-4b10-b040-230545e7452c", null, "Bán buôn" },
                    { "14b57c23-2953-45a5-9b6a-227758278c7e", null, "Bán lẻ" }
                });

            migrationBuilder.InsertData(
                table: "DiscountTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "a2bf4601-83a4-413e-8f9b-c6457151dc44", "Phần trăm" },
                    { "736b454c-6b06-45c1-93fd-ee426ba100d8", "Tiền mặt" }
                });

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "592b1f98-6c7a-45b8-a972-a00571226f99", "Đặt hàng" },
                    { "0b243072-2e23-4397-81b5-cbf887b1d8d3", "Đang giao dịch" },
                    { "f1e884de-b62d-43ad-9ec1-6bfe13e9aadf", "Hoàn thành" },
                    { "2d749b3d-9744-4daf-9dd4-a3916d987ec8", "Kết thúc" },
                    { "f4273e3b-fe73-4daa-834f-4e19f8d36bd1", "Đã hủy" }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "6e362134-8060-40bd-90ba-faa161bbd7e0", "Quẹt thẻ" },
                    { "8eb1249d-277d-450c-a023-92f80fc31f7f", "Tiền mặt" },
                    { "28021394-ea05-45aa-ab43-30cbe1cd53fc", "Chuyển khoản" }
                });

            migrationBuilder.InsertData(
                table: "PaymentStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "73e1a911-ff43-4573-981b-3c363d4146e4", "Chưa thanh toán" },
                    { "0b710f3a-6426-4dff-89ec-afc9f154b668", "Thanh toán một phần" },
                    { "f3288f36-e405-413f-baa3-5c232ea7bd3f", "Đã thanh toán" }
                });

            migrationBuilder.InsertData(
                table: "StockActions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "020cc73e-35b2-4012-8af9-2927fb5d04cf", "Nhập hàng vào kho" },
                    { "1f4c4fb9-d6e5-4d8d-bfc2-f4627f8334cd", "Cân bằng kho" },
                    { "d3a28127-b271-4280-b6d7-632b71248926", "Khởi tạo" },
                    { "22a9ea7b-ecb4-4d3f-86a0-eb7129ceeb04", "Xuất kho giao hàng cho khách/shipper" }
                });

            migrationBuilder.InsertData(
                table: "Transporters",
                columns: new[] { "Id", "Name", "PhoneNumber" },
                values: new object[] { "9567c16b-bd65-4c45-8200-fc96c578c050", "Giao hàng nhanh", "1234567890" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "AvatarPath", "CustomerTypeId", "Dob", "FullName", "Gender", "UserId" },
                values: new object[,]
                {
                    { "CUS0001", "Số 99, Hải Triều, Số 99, Hải Triều, Quán Toan, Hồng Bàng, Hải Phòng", null, "14b57c23-2953-45a5-9b6a-227758278c7e", new DateTime(1998, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bùi Thùy Dương", false, new Guid("cde674e1-6c47-4b00-ac56-558e0d85beff") },
                    { "CUS0002", "Số 99, Hải Triều, Số 99, Hải Triều, Quán Toan, Hồng Bàng, Hải Phòng", null, "14b57c23-2953-45a5-9b6a-227758278c7e", new DateTime(1998, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đỗ Phương Thảo", false, new Guid("a0a054bf-12c6-4b54-90e9-351fed0629a9") }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "AvatarPath", "DefaultPhoneNumber", "Dob", "FullName", "Gender", "UserId" },
                values: new object[,]
                {
                    { "EPN0001", "Số 88, Hải Triều, Số 99, Hải Triều, Quán Toan, Hồng Bàng, Hải Phòng", null, "0123456789", new DateTime(1998, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nguyễn Dương Nguyên", true, new Guid("30aca85c-3f9b-4e55-a6f3-59b629ed0fac") },
                    { "EPN0002", "Số 99, Hải Triều, Số 99, Hải Triều, Quán Toan, Hồng Bàng, Hải Phòng", null, "0123456789", new DateTime(1998, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bùi Thùy Dương", false, new Guid("7bdac961-0b05-4494-89fa-60d08b98687b") }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "CategoryId", "Description", "Information", "Name", "RetailPrice", "StarScore", "Views", "WarrantyPeriod", "WholesalePrices" },
                values: new object[,]
                {
                    { "PRN0001", "8f968a61-69da-4581-8279-61516dc17639", "94b4b0d2-d389-4ded-a61b-50249e14525a", null, null, "Macbook Pro 2020", 22500000m, 1, 1340, 36, 21500000m },
                    { "PRN0002", "8f968a61-69da-4581-8279-61516dc17639", "94b4b0d2-d389-4ded-a61b-50249e14525a", null, null, "Macbook Air 2020", 20500000m, 1, 1340, 36, 20000000m }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId", "DiscountDescription", "DiscountValue", "Note", "OrderStatusId", "PaymentStatusId" },
                values: new object[,]
                {
                    { "ODN0001", "CUS0001", null, 0m, null, "f1e884de-b62d-43ad-9ec1-6bfe13e9aadf", "f3288f36-e405-413f-baa3-5c232ea7bd3f" },
                    { "ODN0002", "CUS0002", null, 0m, null, "0b243072-2e23-4397-81b5-cbf887b1d8d3", "0b710f3a-6426-4dff-89ec-afc9f154b668" }
                });

            migrationBuilder.InsertData(
                table: "Stocks",
                columns: new[] { "ProductId", "AbleToSale", "Inventory" },
                values: new object[,]
                {
                    { "PRN0001", 99, 99 },
                    { "PRN0002", 49, 49 }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "Address", "Description", "Email", "EmployeeId", "Fax", "Name", "PhoneNumber", "Website" },
                values: new object[,]
                {
                    { "SUN0001", "Số 99, Hải Triều, Quán Toan, Hồng Bàng, Hải Phòng", null, "contyabc@gmail.com", "EPN0001", "1234567890", "Công ty TNHH ABC", "1234567890", "abc.com.vn" },
                    { "SUN0002", "Số 99, Hải Triều, Quán Toan, Hồng Bàng, Hải Phòng", null, "contyabc@gmail.com", "EPN0001", "1234567890", "Công ty TNHH DEF", "1234567890", "abc.com.vn" }
                });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "Id", "OrderId", "ProductId", "Quantity", "ServiceName", "UnitPrice" },
                values: new object[,]
                {
                    { "6e5c27aa-30b4-479e-8686-175094ed6c5b", "ODN0001", "PRN0001", 1, null, 22500000m },
                    { "906a00ad-0fa9-48ef-ba51-e326d3f77ea1", "ODN0002", "PRN0002", 1, null, 22500000m }
                });

            migrationBuilder.InsertData(
                table: "PurchaseOrders",
                columns: new[] { "Id", "DeliveryDate", "DiscountDescription", "DiscountValue", "Note", "OrderStatusId", "PaymentStatusId", "SupplierId" },
                values: new object[,]
                {
                    { "PON0001", new DateTime(2020, 9, 8, 12, 33, 33, 477, DateTimeKind.Local).AddTicks(6378), null, 0m, null, "f1e884de-b62d-43ad-9ec1-6bfe13e9aadf", "f3288f36-e405-413f-baa3-5c232ea7bd3f", "SUN0001" },
                    { "PON0002", new DateTime(2020, 9, 8, 12, 33, 33, 477, DateTimeKind.Local).AddTicks(9630), null, 0m, null, "0b243072-2e23-4397-81b5-cbf887b1d8d3", "0b710f3a-6426-4dff-89ec-afc9f154b668", "SUN0002" }
                });

            migrationBuilder.InsertData(
                table: "ReceiptVouchers",
                columns: new[] { "Id", "Description", "OrderId", "PaymentMethodId", "Received", "ReceivedDate" },
                values: new object[,]
                {
                    { "RVN0001", null, "ODN0001", "8eb1249d-277d-450c-a023-92f80fc31f7f", 22500000m, new DateTime(2020, 9, 8, 12, 33, 33, 480, DateTimeKind.Local).AddTicks(205) },
                    { "RVN0002", null, "ODN0002", "8eb1249d-277d-450c-a023-92f80fc31f7f", 10500000m, new DateTime(2020, 9, 8, 12, 33, 33, 480, DateTimeKind.Local).AddTicks(1824) }
                });

            migrationBuilder.InsertData(
                table: "PaymentVouchers",
                columns: new[] { "Id", "CustomerId", "Description", "Paid", "PayDate", "PaymentMethodId", "PurchaseOrderId", "SupplierId" },
                values: new object[,]
                {
                    { "PVN0001", null, null, 2250000000m, new DateTime(2020, 9, 8, 12, 33, 33, 478, DateTimeKind.Local).AddTicks(6612), "8eb1249d-277d-450c-a023-92f80fc31f7f", "PON0001", null },
                    { "PVN0002", null, null, 500000000m, new DateTime(2020, 9, 8, 12, 33, 33, 478, DateTimeKind.Local).AddTicks(8235), "8eb1249d-277d-450c-a023-92f80fc31f7f", "PON0002", null }
                });

            migrationBuilder.InsertData(
                table: "PurchaseOrderDetails",
                columns: new[] { "Id", "CostName", "ProductId", "PurchaseOrderId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { "500b1862-90f3-48e4-a393-7d8d3a80d852", null, "PRN0001", "PON0001", 100, 22500000m },
                    { "d95faaa5-a7db-4182-be3e-705ea383761c", null, "PRN0002", "PON0002", 50, 22500000m }
                });

            migrationBuilder.InsertData(
                table: "StockHistories",
                columns: new[] { "Id", "ChangeQuantity", "EmployeeId", "OrderDetailId", "ProductId", "RecordDate", "StockActionId" },
                values: new object[,]
                {
                    { "6bb4f9d8-7df8-4b20-ad1f-f996be3f53ec", -1, "EPN0001", "6e5c27aa-30b4-479e-8686-175094ed6c5b", "PRN0001", new DateTime(2020, 9, 8, 12, 33, 33, 480, DateTimeKind.Local).AddTicks(7153), "22a9ea7b-ecb4-4d3f-86a0-eb7129ceeb04" },
                    { "948447ee-5eb3-4a08-afc6-cddc1fa7aa46", -1, "EPN0001", "906a00ad-0fa9-48ef-ba51-e326d3f77ea1", "PRN0002", new DateTime(2020, 9, 8, 12, 33, 33, 480, DateTimeKind.Local).AddTicks(8121), "22a9ea7b-ecb4-4d3f-86a0-eb7129ceeb04" }
                });

            migrationBuilder.InsertData(
                table: "StockHistories",
                columns: new[] { "Id", "ChangeQuantity", "EmployeeId", "ProductId", "PurchaseOrderDetailId", "RecordDate", "StockActionId" },
                values: new object[] { "84ed9ad6-3a1e-4eba-95b9-ba99009301b0", 100, "EPN0001", "PRN0001", "500b1862-90f3-48e4-a393-7d8d3a80d852", new DateTime(2020, 9, 8, 12, 33, 33, 480, DateTimeKind.Local).AddTicks(3736), "d3a28127-b271-4280-b6d7-632b71248926" });

            migrationBuilder.InsertData(
                table: "StockHistories",
                columns: new[] { "Id", "ChangeQuantity", "EmployeeId", "ProductId", "PurchaseOrderDetailId", "RecordDate", "StockActionId" },
                values: new object[] { "9570d184-8b16-4ed0-8939-2a6a428ea18c", 50, "EPN0001", "PRN0002", "d95faaa5-a7db-4182-be3e-705ea383761c", new DateTime(2020, 9, 8, 12, 33, 33, 480, DateTimeKind.Local).AddTicks(7041), "d3a28127-b271-4280-b6d7-632b71248926" });

            migrationBuilder.CreateIndex(
                name: "IX_AppRoles_AppUserId",
                table: "AppRoles",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProductId",
                table: "Carts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerTypeId",
                table: "Customers",
                column: "CustomerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId",
                table: "Customers",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DiscountTypeId",
                table: "Orders",
                column: "DiscountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderStatusId",
                table: "Orders",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentStatusId",
                table: "Orders",
                column: "PaymentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVouchers_CustomerId",
                table: "PaymentVouchers",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVouchers_PaymentMethodId",
                table: "PaymentVouchers",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVouchers_PurchaseOrderId",
                table: "PaymentVouchers",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVouchers_SupplierId",
                table: "PaymentVouchers",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviewReplies_ProductReviewId",
                table: "ProductReviewReplies",
                column: "ProductReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviewReplies_UserId",
                table: "ProductReviewReplies",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_ProductId",
                table: "ProductReviews",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_UserId",
                table: "ProductReviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderDetails_ProductId",
                table: "PurchaseOrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderDetails_PurchaseOrderId",
                table: "PurchaseOrderDetails",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_DiscountTypeId",
                table: "PurchaseOrders",
                column: "DiscountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_OrderStatusId",
                table: "PurchaseOrders",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_PaymentStatusId",
                table: "PurchaseOrders",
                column: "PaymentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_SupplierId",
                table: "PurchaseOrders",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptVouchers_CustomerId",
                table: "ReceiptVouchers",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptVouchers_OrderId",
                table: "ReceiptVouchers",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptVouchers_PaymentMethodId",
                table: "ReceiptVouchers",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptVouchers_SupplierId",
                table: "ReceiptVouchers",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingOrders_CustomerId",
                table: "ShippingOrders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingOrders_ShippingStatusId",
                table: "ShippingOrders",
                column: "ShippingStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingOrders_TransporterId",
                table: "ShippingOrders",
                column: "TransporterId");

            migrationBuilder.CreateIndex(
                name: "IX_StockHistories_EmployeeId",
                table: "StockHistories",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_StockHistories_OrderDetailId",
                table: "StockHistories",
                column: "OrderDetailId",
                unique: true,
                filter: "[OrderDetailId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StockHistories_ProductId",
                table: "StockHistories",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StockHistories_PurchaseOrderDetailId",
                table: "StockHistories",
                column: "PurchaseOrderDetailId",
                unique: true,
                filter: "[PurchaseOrderDetailId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StockHistories_StockActionId",
                table: "StockHistories",
                column: "StockActionId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_EmployeeId",
                table: "Suppliers",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppRoleClaims");

            migrationBuilder.DropTable(
                name: "AppRoles");

            migrationBuilder.DropTable(
                name: "AppUserClaims");

            migrationBuilder.DropTable(
                name: "AppUserLogins");

            migrationBuilder.DropTable(
                name: "AppUserRoles");

            migrationBuilder.DropTable(
                name: "AppUserTokens");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "PaymentVouchers");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "ProductReviewReplies");

            migrationBuilder.DropTable(
                name: "ReceiptVouchers");

            migrationBuilder.DropTable(
                name: "ShippingOrders");

            migrationBuilder.DropTable(
                name: "StockHistories");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "ProductReviews");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "ShippingStatus");

            migrationBuilder.DropTable(
                name: "Transporters");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "PurchaseOrderDetails");

            migrationBuilder.DropTable(
                name: "StockActions");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "DiscountTypes");

            migrationBuilder.DropTable(
                name: "OrderStatuses");

            migrationBuilder.DropTable(
                name: "PaymentStatuses");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "CustomerTypes");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "AppUsers");
        }
    }
}
