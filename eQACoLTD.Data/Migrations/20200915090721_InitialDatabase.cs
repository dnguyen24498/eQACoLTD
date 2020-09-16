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
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 9, 15, 16, 7, 19, 950, DateTimeKind.Local).AddTicks(4345))
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
                    ThumbnailImagePath = table.Column<string>(type: "nvarchar(1000)", nullable: true)
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
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 9, 15, 16, 7, 19, 958, DateTimeKind.Local).AddTicks(3264))
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
                    FullName = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    Gender = table.Column<bool>(nullable: true, defaultValue: false),
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
                    Information = table.Column<string>(type: "nvarchar(600)", nullable: true),
                    CategoryId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Views = table.Column<int>(nullable: false, defaultValue: 0),
                    RetailPrice = table.Column<decimal>(nullable: false, defaultValue: 0m),
                    WholesalePrices = table.Column<decimal>(nullable: false, defaultValue: 0m),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false),
                    BrandId = table.Column<string>(nullable: true),
                    StarScore = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1),
                    WarrantyPeriod = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)0)
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
                    FullName = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    Gender = table.Column<bool>(nullable: true, defaultValue: false),
                    AvatarPath = table.Column<string>(type: "nvarchar(1000)", nullable: true),
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
                    Address = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(30)", nullable: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false),
                    Email = table.Column<string>(type: "varchar(150)", nullable: true),
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
                    FullPath = table.Column<string>(type: "nvarchar(1000)", nullable: true),
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
                    StarScore = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1)
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
                    AbleToSale = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Inventory = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
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
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 9, 15, 16, 7, 20, 14, DateTimeKind.Local).AddTicks(1914)),
                    Note = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    OrderStatusId = table.Column<string>(nullable: true),
                    PaymentStatusId = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false),
                    DiscountTypeId = table.Column<string>(nullable: true),
                    DiscountValue = table.Column<decimal>(type: "decimal", nullable: false, defaultValue: 0m),
                    DiscountDescription = table.Column<string>(type: "nvarchar(500)", nullable: true)
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
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 9, 15, 16, 7, 20, 3, DateTimeKind.Local).AddTicks(9392)),
                    PurchaseDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 9, 15, 16, 7, 20, 3, DateTimeKind.Local).AddTicks(9785)),
                    OrderStatusId = table.Column<string>(nullable: true),
                    DeliveryDate = table.Column<DateTime>(nullable: false),
                    PaymentStatusId = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false),
                    DiscountTypeId = table.Column<string>(nullable: true),
                    DiscountValue = table.Column<decimal>(type: "decimal", nullable: false, defaultValue: 0m),
                    DiscountDescription = table.Column<string>(type: "nvarchar(500)", nullable: true)
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
                    Content = table.Column<string>(type: "nvarchar(1000)", nullable: false)
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
                    ReceivedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 9, 15, 16, 7, 20, 20, DateTimeKind.Local).AddTicks(3172)),
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
                    Description = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    CustomerId = table.Column<string>(nullable: true),
                    CustomerName = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(30)", nullable: true),
                    Fee = table.Column<decimal>(nullable: false, defaultValue: 0m),
                    TransporterId = table.Column<string>(nullable: true),
                    Address = table.Column<string>(type: "varchar(300)", nullable: true),
                    ShippingStatusId = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 9, 15, 16, 7, 20, 28, DateTimeKind.Local).AddTicks(437))
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
                    PayDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 9, 15, 16, 7, 20, 9, DateTimeKind.Local).AddTicks(2254)),
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
                    RecordDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 9, 15, 16, 7, 20, 23, DateTimeKind.Local).AddTicks(6751)),
                    EmployeeId = table.Column<string>(nullable: true),
                    StockActionId = table.Column<string>(nullable: true),
                    ChangeQuantity = table.Column<int>(nullable: false, defaultValue: 0),
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
                    { new Guid("55940568-a208-4b64-b4f3-68eba44d0613"), null, "2e28e684-7f73-4e07-b519-5147531577f5", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Quyền quản trị viên", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Administrator", "Administrator", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("fbda1a18-e054-4fe5-8a6a-c16250021a2a"), null, "f56d7daa-147c-48cc-889d-c651ef7dcdc5", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Quyền nhân viên kho", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "WarehouseStaff", "WarehouseStaff", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("825c118f-47db-49ff-b66e-83ba00824435"), null, "b96abfd0-392d-4ced-9cb8-c783ba61b97a", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Quyền nhân viên lễ tân", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receptionist", "Receptionist", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("2758c11e-6a38-4d52-a70a-a10704d68d77"), null, "fd2a92f2-c3a0-4941-ad3c-c28811f1d5d1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Quyền nhân viên thu ngân", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cashier", "Cashier", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("70865900-5fec-4335-9d27-423603ccef2a"), new Guid("55940568-a208-4b64-b4f3-68eba44d0613") },
                    { new Guid("33250e7f-d560-49f6-b106-719ed5e2424b"), new Guid("2758c11e-6a38-4d52-a70a-a10704d68d77") },
                    { new Guid("1398395b-ddf5-4afe-8e04-7e1da15b9a08"), new Guid("fbda1a18-e054-4fe5-8a6a-c16250021a2a") }
                });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("3fa33d4f-8850-418b-98c5-f01607424632"), 0, "144c2ff8-79df-47bb-ad67-6db2fb2bb1ff", "dophuongthao@gmail.com", true, false, null, "DOPHUONGTHAO@GMAIL.COM", "cus0002", "AQAAAAEAACcQAAAAEBwjFdjJ0fzNBGcPywNpxcA12lotGJDhL473nr/AJW5m9ElkB3M+TY5j5KC7Iaf0KQ==", "1234567890", false, "", false, "cus0002" },
                    { new Guid("995aed19-a0ce-4c11-b6c1-5417f74a4c30"), 0, "bcd8d2f6-a067-47fe-922a-a8c166627df1", "duongnguyenadhp@gmail.com", true, false, null, "DUONGNGUYENADHP@GMAIL.COM", "cus0001", "AQAAAAEAACcQAAAAEFbucXEeNrDU9d+48JXn042JwZACpc4m3NHuVbtrstd1gbFveRbRS7CKBjJnq8Z35A==", "1234567890", false, "", false, "cus0001" },
                    { new Guid("33250e7f-d560-49f6-b106-719ed5e2424b"), 0, "34a7668f-15a9-4628-b486-b5960fc236d5", "duongnguyenadhp1@gmail.com", true, false, null, "DUONGNGUYENADHP1@GMAIL.COM", "EPN0001", "AQAAAAEAACcQAAAAEGknTbpL5xZh1ZP8FJZUtiWOeAUem1ZqIsh+Jh/n+sXHL4tGWvRVyPBivYexOZ5Fog==", "1234567890", false, "", false, "epn0001" },
                    { new Guid("70865900-5fec-4335-9d27-423603ccef2a"), 0, "9e0c7824-ec50-46b8-907c-898ad1ef7ffa", "dnguyen24498@gmail.com", true, false, null, "DNGUYEN24498@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEEXhDrLxWl3nlmMDo6HhNZnJ9T+JvjMqk485sV1ca1pCwsfpH5ylcwzZpSiZDhvK4w==", null, false, "", false, "admin" },
                    { new Guid("1398395b-ddf5-4afe-8e04-7e1da15b9a08"), 0, "7001a433-469a-4d14-9cea-2f6b918d77e2", "duongnguyenadhp2@gmail.com", true, false, null, "DUONGNGUYENADHP2@GMAIL.COM", "epn0002", "AQAAAAEAACcQAAAAEGiLiPwRDuck3uOfluLBugdvihM+xmkzU17Z2XsMi9kwyuAZCnUQATd4xCYpMGrL2A==", "1234567890", false, "", false, "epn0002" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Description", "ImagePath", "Name" },
                values: new object[,]
                {
                    { "5e8cf27f-cfc6-437b-a01b-a96805f4e202", null, null, "Apple" },
                    { "277cff37-6614-454b-bda4-fe12848c54ec", null, null, "Dell" },
                    { "e015e8fa-5f72-4529-8a93-6ed57a6af4aa", null, null, "HP" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name", "ThumbnailImagePath" },
                values: new object[,]
                {
                    { "215cf0eb-e27d-455b-9db9-d11520dc76bb", null, "Laptop", null },
                    { "36050e28-44f2-4756-91c4-40d7ebe1036c", null, "PC", null }
                });

            migrationBuilder.InsertData(
                table: "CustomerTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { "5fc4a568-9472-4c12-bb0b-e9d4f9e66cf0", null, "Bán buôn" },
                    { "66da40a6-f289-4c86-aac4-72ddb9adae3d", null, "Bán lẻ" }
                });

            migrationBuilder.InsertData(
                table: "DiscountTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "4f6f5f81-c6ae-4ddd-b726-62a54f724502", "Phần trăm" },
                    { "f302e1da-00a5-454f-acf2-cef1bb44ed24", "Tiền mặt" }
                });

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "55162882-3bd7-49eb-a674-23963168a901", "Đặt hàng" },
                    { "e7a6ced0-c306-4099-9692-50518c490281", "Đang giao dịch" },
                    { "88b82ce5-273a-4fac-9a54-3ef3d686036a", "Hoàn thành" },
                    { "c5a77e70-df2d-4007-8dd1-c727c12ab8bc", "Kết thúc" },
                    { "2f0b8d31-5726-4475-a06e-18a26cc9d66b", "Đã hủy" }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "5e3b47e8-61d1-42b2-9bfa-a3242115677e", "Quẹt thẻ" },
                    { "ba8e558a-8f78-4045-b515-90d56c790b21", "Tiền mặt" },
                    { "52fb071c-ab8a-476b-8e89-71aa40fec838", "Chuyển khoản" }
                });

            migrationBuilder.InsertData(
                table: "PaymentStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "3a725c1e-255b-4609-aaec-312f58380799", "Chưa thanh toán" },
                    { "f917dc6c-6645-4090-8a7f-cd6e79e184e1", "Thanh toán một phần" },
                    { "e723be78-7769-420f-9c9b-4c5919a8e1d3", "Đã thanh toán" }
                });

            migrationBuilder.InsertData(
                table: "StockActions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "b3af0293-4dcd-4d5d-9fca-e2af586e42df", "Nhập hàng vào kho" },
                    { "d708921e-bad5-46a6-8b81-51b233d9f58c", "Cân bằng kho" },
                    { "edf29819-8c57-45dd-94c9-f3a90b7c3d68", "Khởi tạo" },
                    { "dc5c823e-f810-44e6-b6c9-6e22c33dd6f5", "Xuất kho giao hàng cho khách/shipper" }
                });

            migrationBuilder.InsertData(
                table: "Transporters",
                columns: new[] { "Id", "Name", "PhoneNumber" },
                values: new object[] { "71f27b74-f7e6-44d7-a4e7-9698ca304044", "Giao hàng nhanh", "1234567890" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "AvatarPath", "CustomerTypeId", "DefaultPhoneNumber", "Dob", "FullName", "Gender", "UserId" },
                values: new object[,]
                {
                    { "CUS0001", "Số 99, Hải Triều, Số 99, Hải Triều, Quán Toan, Hồng Bàng, Hải Phòng", null, "66da40a6-f289-4c86-aac4-72ddb9adae3d", null, new DateTime(1998, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bùi Thùy Dương", false, new Guid("995aed19-a0ce-4c11-b6c1-5417f74a4c30") },
                    { "CUS0002", "Số 99, Hải Triều, Số 99, Hải Triều, Quán Toan, Hồng Bàng, Hải Phòng", null, "66da40a6-f289-4c86-aac4-72ddb9adae3d", null, new DateTime(1998, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đỗ Phương Thảo", false, new Guid("3fa33d4f-8850-418b-98c5-f01607424632") }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "AvatarPath", "DefaultPhoneNumber", "Dob", "FullName", "Gender", "UserId" },
                values: new object[,]
                {
                    { "EPN0001", "Số 88, Hải Triều, Số 99, Hải Triều, Quán Toan, Hồng Bàng, Hải Phòng", null, "0123456789", new DateTime(1998, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nguyễn Dương Nguyên", true, new Guid("33250e7f-d560-49f6-b106-719ed5e2424b") },
                    { "EPN0002", "Số 99, Hải Triều, Số 99, Hải Triều, Quán Toan, Hồng Bàng, Hải Phòng", null, "0123456789", new DateTime(1998, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bùi Thùy Dương", false, new Guid("1398395b-ddf5-4afe-8e04-7e1da15b9a08") }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "CategoryId", "Description", "Information", "Name", "RetailPrice", "StarScore", "Views", "WarrantyPeriod", "WholesalePrices" },
                values: new object[,]
                {
                    { "PRN0001", "5e8cf27f-cfc6-437b-a01b-a96805f4e202", "215cf0eb-e27d-455b-9db9-d11520dc76bb", null, null, "Macbook Pro 2020", 22500000m, (byte)1, 1340, (byte)36, 21500000m },
                    { "PRN0002", "5e8cf27f-cfc6-437b-a01b-a96805f4e202", "215cf0eb-e27d-455b-9db9-d11520dc76bb", null, null, "Macbook Air 2020", 20500000m, (byte)1, 1340, (byte)36, 20000000m }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId", "DiscountDescription", "DiscountTypeId", "Note", "OrderStatusId", "PaymentStatusId" },
                values: new object[,]
                {
                    { "ODN0001", "CUS0001", null, null, null, "88b82ce5-273a-4fac-9a54-3ef3d686036a", "e723be78-7769-420f-9c9b-4c5919a8e1d3" },
                    { "ODN0002", "CUS0002", null, null, null, "e7a6ced0-c306-4099-9692-50518c490281", "f917dc6c-6645-4090-8a7f-cd6e79e184e1" }
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
                    { "4f84f1c8-9b8d-4f7b-92d2-f715540b7e1a", "ODN0001", "PRN0001", 1, null, 22500000m },
                    { "be117988-cd1c-45df-a5e8-69d8a1595d41", "ODN0002", "PRN0002", 1, null, 22500000m }
                });

            migrationBuilder.InsertData(
                table: "PurchaseOrders",
                columns: new[] { "Id", "DeliveryDate", "DiscountDescription", "Note", "OrderStatusId", "PaymentStatusId", "SupplierId" },
                values: new object[,]
                {
                    { "PON0001", new DateTime(2020, 9, 15, 16, 7, 20, 62, DateTimeKind.Local).AddTicks(752), null, null, "88b82ce5-273a-4fac-9a54-3ef3d686036a", "e723be78-7769-420f-9c9b-4c5919a8e1d3", "SUN0001" },
                    { "PON0002", new DateTime(2020, 9, 15, 16, 7, 20, 62, DateTimeKind.Local).AddTicks(1887), null, null, "e7a6ced0-c306-4099-9692-50518c490281", "f917dc6c-6645-4090-8a7f-cd6e79e184e1", "SUN0002" }
                });

            migrationBuilder.InsertData(
                table: "ReceiptVouchers",
                columns: new[] { "Id", "CustomerId", "Description", "OrderId", "PaymentMethodId", "Received", "ReceivedDate", "SupplierId" },
                values: new object[,]
                {
                    { "RVN0001", null, null, "ODN0001", "ba8e558a-8f78-4045-b515-90d56c790b21", 22500000m, new DateTime(2020, 9, 15, 16, 7, 20, 63, DateTimeKind.Local).AddTicks(7018), null },
                    { "RVN0002", null, null, "ODN0002", "ba8e558a-8f78-4045-b515-90d56c790b21", 10500000m, new DateTime(2020, 9, 15, 16, 7, 20, 63, DateTimeKind.Local).AddTicks(7901), null }
                });

            migrationBuilder.InsertData(
                table: "PaymentVouchers",
                columns: new[] { "Id", "CustomerId", "Description", "Paid", "PayDate", "PaymentMethodId", "PurchaseOrderId", "SupplierId" },
                values: new object[,]
                {
                    { "PVN0001", null, null, 2250000000m, new DateTime(2020, 9, 15, 16, 7, 20, 62, DateTimeKind.Local).AddTicks(7307), "ba8e558a-8f78-4045-b515-90d56c790b21", "PON0001", null },
                    { "PVN0002", null, null, 500000000m, new DateTime(2020, 9, 15, 16, 7, 20, 62, DateTimeKind.Local).AddTicks(8621), "ba8e558a-8f78-4045-b515-90d56c790b21", "PON0002", null }
                });

            migrationBuilder.InsertData(
                table: "PurchaseOrderDetails",
                columns: new[] { "Id", "CostName", "ProductId", "PurchaseOrderId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { "9b9d7c98-7f42-4456-a22f-a081947912f0", null, "PRN0001", "PON0001", 100, 22500000m },
                    { "a3f939b1-f01d-4d06-a4aa-9ce8be174cfe", null, "PRN0002", "PON0002", 50, 22500000m }
                });

            migrationBuilder.InsertData(
                table: "StockHistories",
                columns: new[] { "Id", "ChangeQuantity", "EmployeeId", "OrderDetailId", "ProductId", "PurchaseOrderDetailId", "RecordDate", "StockActionId" },
                values: new object[,]
                {
                    { "abbfb2d1-d0f4-48c2-9edd-925d0eb3cb1c", -1, "EPN0001", "4f84f1c8-9b8d-4f7b-92d2-f715540b7e1a", "PRN0001", null, new DateTime(2020, 9, 15, 16, 7, 20, 64, DateTimeKind.Local).AddTicks(1223), "dc5c823e-f810-44e6-b6c9-6e22c33dd6f5" },
                    { "92df7133-8a34-494e-b625-8f1b93c91218", -1, "EPN0001", "be117988-cd1c-45df-a5e8-69d8a1595d41", "PRN0002", null, new DateTime(2020, 9, 15, 16, 7, 20, 64, DateTimeKind.Local).AddTicks(1749), "dc5c823e-f810-44e6-b6c9-6e22c33dd6f5" }
                });

            migrationBuilder.InsertData(
                table: "StockHistories",
                columns: new[] { "Id", "ChangeQuantity", "EmployeeId", "OrderDetailId", "ProductId", "PurchaseOrderDetailId", "RecordDate", "StockActionId" },
                values: new object[] { "6a5b995b-6162-4eeb-b6c4-e69e06f05215", 100, "EPN0001", null, "PRN0001", "9b9d7c98-7f42-4456-a22f-a081947912f0", new DateTime(2020, 9, 15, 16, 7, 20, 63, DateTimeKind.Local).AddTicks(9042), "edf29819-8c57-45dd-94c9-f3a90b7c3d68" });

            migrationBuilder.InsertData(
                table: "StockHistories",
                columns: new[] { "Id", "ChangeQuantity", "EmployeeId", "OrderDetailId", "ProductId", "PurchaseOrderDetailId", "RecordDate", "StockActionId" },
                values: new object[] { "c6a1627c-1ea3-41f2-af57-91fd7a61fd4b", 50, "EPN0001", null, "PRN0002", "a3f939b1-f01d-4d06-a4aa-9ce8be174cfe", new DateTime(2020, 9, 15, 16, 7, 20, 64, DateTimeKind.Local).AddTicks(1166), "edf29819-8c57-45dd-94c9-f3a90b7c3d68" });

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
