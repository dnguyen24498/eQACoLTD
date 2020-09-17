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
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 9, 17, 7, 6, 2, 714, DateTimeKind.Local).AddTicks(9179))
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
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 9, 17, 7, 6, 2, 724, DateTimeKind.Local).AddTicks(4460))
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
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 9, 17, 7, 6, 2, 771, DateTimeKind.Local).AddTicks(8124)),
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
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 9, 17, 7, 6, 2, 762, DateTimeKind.Local).AddTicks(867)),
                    PurchaseDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 9, 17, 7, 6, 2, 762, DateTimeKind.Local).AddTicks(1226)),
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
                    ReceivedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 9, 17, 7, 6, 2, 777, DateTimeKind.Local).AddTicks(581)),
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
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 9, 17, 7, 6, 2, 784, DateTimeKind.Local).AddTicks(4756))
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
                    PayDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 9, 17, 7, 6, 2, 767, DateTimeKind.Local).AddTicks(1490)),
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
                    RecordDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 9, 17, 7, 6, 2, 780, DateTimeKind.Local).AddTicks(3306)),
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
                    { new Guid("4c6d9e27-83db-496c-8a5f-16e4d3157428"), null, "8038e01c-976e-484a-94a1-31b2dc838990", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Quyền quản trị viên", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Administrator", "Administrator", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("04e6d6da-4f72-4b5b-88b9-2b919a9c7b9b"), null, "0b08ded1-0043-42b0-b451-1ea60cf988ab", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Quyền nhân viên kho", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "WarehouseStaff", "WarehouseStaff", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("8e06e692-467e-4003-a2de-ad4d3ca1dc6d"), null, "4601d24a-8e24-4630-8963-9571f190c898", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Quyền nhân viên lễ tân", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Receptionist", "Receptionist", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b430434f-5cf8-45bc-b25c-e3b123f9e880"), null, "42f52957-e4cd-4f34-964a-ca4e1e7b4ef1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Quyền nhân viên thu ngân", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cashier", "Cashier", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("8efe663c-677d-4bcf-bd59-3359d49bac87"), new Guid("4c6d9e27-83db-496c-8a5f-16e4d3157428") },
                    { new Guid("4715539a-9f13-4eb0-813b-74d9fda79ccb"), new Guid("b430434f-5cf8-45bc-b25c-e3b123f9e880") },
                    { new Guid("47a888f4-e5c8-4f07-bd00-fcc7bb531eb3"), new Guid("04e6d6da-4f72-4b5b-88b9-2b919a9c7b9b") }
                });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("0ee1852e-6396-4f71-86b6-5741def7398b"), 0, "1fe1a570-86ca-426d-9d1b-ce5082907919", "dophuongthao@gmail.com", true, false, null, "DOPHUONGTHAO@GMAIL.COM", "cus0002", "AQAAAAEAACcQAAAAEPYBVtRgC1wsTPk4i+7KjeJW/A5khZsiKQTjLfCuRQY9elRCPEcohsZNZtqOe7e1QA==", "1234567890", false, "", false, "cus0002" },
                    { new Guid("51098bea-63c8-4aba-8668-84562f5048a0"), 0, "85f1bd56-4b41-4636-89ae-639888042fb6", "duongnguyenadhp@gmail.com", true, false, null, "DUONGNGUYENADHP@GMAIL.COM", "cus0001", "AQAAAAEAACcQAAAAEGoeGZpSu10g/D66YuwHWkWq86cRQczioYXlGuDaJttAxbVOW74dimDebry+hVYVvQ==", "1234567890", false, "", false, "cus0001" },
                    { new Guid("4715539a-9f13-4eb0-813b-74d9fda79ccb"), 0, "8af7dd46-7e39-490e-81a5-6ac62076b9fc", "duongnguyenadhp1@gmail.com", true, false, null, "DUONGNGUYENADHP1@GMAIL.COM", "EPN0001", "AQAAAAEAACcQAAAAENjPaMADilW0yqJEzCawN3/iv1AX5TJMOwLjfgmwB7T+odHDJzRcKeDDjHgr0i2HBQ==", "1234567890", false, "", false, "epn0001" },
                    { new Guid("8efe663c-677d-4bcf-bd59-3359d49bac87"), 0, "d7a31986-2fbd-4f0a-a931-cba75d0bc593", "dnguyen24498@gmail.com", true, false, null, "DNGUYEN24498@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEDmf1wHwhoDBaLUEgbIpPcBNGfhLZF/lDOiuGBLnuWbDlLQU4VwaKow2LCUg0oxK8w==", null, false, "", false, "admin" },
                    { new Guid("47a888f4-e5c8-4f07-bd00-fcc7bb531eb3"), 0, "83cc0cb9-48d3-4510-ab7d-bcf5999eb241", "duongnguyenadhp2@gmail.com", true, false, null, "DUONGNGUYENADHP2@GMAIL.COM", "epn0002", "AQAAAAEAACcQAAAAEOuDN+GSMQXUhu5CXHW968LYhxsHGJMHQdannj+9Y5aQqpKHupFxONZPimGu63FpTA==", "1234567890", false, "", false, "epn0002" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Description", "ImagePath", "Name" },
                values: new object[,]
                {
                    { "bedf848d-f7d0-433f-98f1-2731067335af", null, null, "Apple" },
                    { "b693eb5a-e457-447c-9e29-48f438703780", null, null, "Dell" },
                    { "9d07f19f-afd8-408d-872e-d99331031c59", null, null, "HP" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name", "ThumbnailImagePath" },
                values: new object[,]
                {
                    { "3db32842-3ed5-458b-ad96-a113d73775ee", null, "Laptop", null },
                    { "58060083-6e76-4dea-ba20-0e332ec96122", null, "PC", null }
                });

            migrationBuilder.InsertData(
                table: "CustomerTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { "3daf45ee-395b-48c7-a790-247b372d1746", null, "Bán buôn" },
                    { "4f2b5d2b-99c0-45a7-abb8-2316e804da63", null, "Bán lẻ" }
                });

            migrationBuilder.InsertData(
                table: "DiscountTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "0d3c8b87-8010-491b-9867-84805888d937", "Phần trăm" },
                    { "35d3f8e6-4c28-4315-8e98-5ea957c847c2", "Tiền mặt" }
                });

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "75d76ab9-5e0e-4b42-8bdc-7e3c5d4904a1", "Đặt hàng" },
                    { "43972588-ea79-4bf0-8ed7-7e9ef42170a7", "Đang giao dịch" },
                    { "80b1a453-82a4-47ed-964e-1b69af87a1e1", "Hoàn thành" },
                    { "537d8259-b206-496a-8a85-29f76539301b", "Kết thúc" },
                    { "940195d2-885c-45aa-af98-17ad7549f90a", "Đã hủy" }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "2107adeb-7574-4f62-a898-22b79b67ee62", "Quẹt thẻ" },
                    { "a2b931dd-c5be-47c5-a134-f9f3474cf630", "Tiền mặt" },
                    { "f859d3ef-13e6-411b-b701-d5d643524653", "Chuyển khoản" }
                });

            migrationBuilder.InsertData(
                table: "PaymentStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "57fa4d1a-974a-4157-bd6c-287db184a51e", "Chưa thanh toán" },
                    { "6187b25a-957c-4690-ba99-f64499119f35", "Thanh toán một phần" },
                    { "646233ce-0eb0-4ed1-a5c6-8e16e1376393", "Đã thanh toán" }
                });

            migrationBuilder.InsertData(
                table: "StockActions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "0a12a679-0952-414f-a1e6-990b34f1be07", "Nhập hàng vào kho" },
                    { "a8ef7eae-3fad-4711-a4f0-8b5b7e5084af", "Cân bằng kho" },
                    { "d6f2e2be-d9d4-40bf-b872-1c5d747466c9", "Khởi tạo" },
                    { "1be88fef-0b15-4429-9012-9771a6e849f1", "Xuất kho giao hàng cho khách/shipper" }
                });

            migrationBuilder.InsertData(
                table: "Transporters",
                columns: new[] { "Id", "Name", "PhoneNumber" },
                values: new object[] { "be2ba488-7bf5-491d-ac72-15d3d4c50c05", "Giao hàng nhanh", "1234567890" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "AvatarPath", "CustomerTypeId", "DefaultPhoneNumber", "Dob", "FullName", "Gender", "UserId" },
                values: new object[,]
                {
                    { "CUS0001", "Số 99, Hải Triều, Số 99, Hải Triều, Quán Toan, Hồng Bàng, Hải Phòng", null, "4f2b5d2b-99c0-45a7-abb8-2316e804da63", null, new DateTime(1998, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bùi Thùy Dương", false, new Guid("51098bea-63c8-4aba-8668-84562f5048a0") },
                    { "CUS0002", "Số 99, Hải Triều, Số 99, Hải Triều, Quán Toan, Hồng Bàng, Hải Phòng", null, "4f2b5d2b-99c0-45a7-abb8-2316e804da63", null, new DateTime(1998, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đỗ Phương Thảo", false, new Guid("0ee1852e-6396-4f71-86b6-5741def7398b") }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "AvatarPath", "DefaultPhoneNumber", "Dob", "FullName", "Gender", "UserId" },
                values: new object[,]
                {
                    { "EPN0001", "Số 88, Hải Triều, Số 99, Hải Triều, Quán Toan, Hồng Bàng, Hải Phòng", null, "0123456789", new DateTime(1998, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nguyễn Dương Nguyên", true, new Guid("4715539a-9f13-4eb0-813b-74d9fda79ccb") },
                    { "EPN0002", "Số 99, Hải Triều, Số 99, Hải Triều, Quán Toan, Hồng Bàng, Hải Phòng", null, "0123456789", new DateTime(1998, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bùi Thùy Dương", false, new Guid("47a888f4-e5c8-4f07-bd00-fcc7bb531eb3") }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "CategoryId", "Description", "Information", "Name", "RetailPrice", "StarScore", "Views", "WarrantyPeriod", "WholesalePrices" },
                values: new object[,]
                {
                    { "PRN0001", "bedf848d-f7d0-433f-98f1-2731067335af", "3db32842-3ed5-458b-ad96-a113d73775ee", null, null, "Macbook Pro 2020", 22500000m, (byte)1, 1340, (byte)36, 21500000m },
                    { "PRN0002", "bedf848d-f7d0-433f-98f1-2731067335af", "3db32842-3ed5-458b-ad96-a113d73775ee", null, null, "Macbook Air 2020", 20500000m, (byte)1, 1340, (byte)36, 20000000m }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId", "DiscountDescription", "DiscountTypeId", "Note", "OrderStatusId", "PaymentStatusId" },
                values: new object[,]
                {
                    { "ODN0001", "CUS0001", null, null, null, "80b1a453-82a4-47ed-964e-1b69af87a1e1", "646233ce-0eb0-4ed1-a5c6-8e16e1376393" },
                    { "ODN0002", "CUS0002", null, null, null, "43972588-ea79-4bf0-8ed7-7e9ef42170a7", "6187b25a-957c-4690-ba99-f64499119f35" }
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
                    { "7c2139ae-ee0b-4e6a-9c48-32428fc4c032", "ODN0001", "PRN0001", 1, null, 22500000m },
                    { "27f6e5e9-a08c-4470-849b-20b9bc3cef39", "ODN0002", "PRN0002", 1, null, 22500000m }
                });

            migrationBuilder.InsertData(
                table: "PurchaseOrders",
                columns: new[] { "Id", "DeliveryDate", "DiscountDescription", "Note", "OrderStatusId", "PaymentStatusId", "SupplierId" },
                values: new object[,]
                {
                    { "PON0001", new DateTime(2020, 9, 17, 7, 6, 2, 811, DateTimeKind.Local).AddTicks(5419), null, null, "80b1a453-82a4-47ed-964e-1b69af87a1e1", "646233ce-0eb0-4ed1-a5c6-8e16e1376393", "SUN0001" },
                    { "PON0002", new DateTime(2020, 9, 17, 7, 6, 2, 811, DateTimeKind.Local).AddTicks(6224), null, null, "43972588-ea79-4bf0-8ed7-7e9ef42170a7", "6187b25a-957c-4690-ba99-f64499119f35", "SUN0002" }
                });

            migrationBuilder.InsertData(
                table: "ReceiptVouchers",
                columns: new[] { "Id", "CustomerId", "Description", "OrderId", "PaymentMethodId", "Received", "ReceivedDate", "SupplierId" },
                values: new object[,]
                {
                    { "RVN0001", null, null, "ODN0001", "a2b931dd-c5be-47c5-a134-f9f3474cf630", 22500000m, new DateTime(2020, 9, 17, 7, 6, 2, 812, DateTimeKind.Local).AddTicks(7456), null },
                    { "RVN0002", null, null, "ODN0002", "a2b931dd-c5be-47c5-a134-f9f3474cf630", 10500000m, new DateTime(2020, 9, 17, 7, 6, 2, 812, DateTimeKind.Local).AddTicks(8049), null }
                });

            migrationBuilder.InsertData(
                table: "PaymentVouchers",
                columns: new[] { "Id", "CustomerId", "Description", "Paid", "PayDate", "PaymentMethodId", "PurchaseOrderId", "SupplierId" },
                values: new object[,]
                {
                    { "PVN0001", null, null, 2250000000m, new DateTime(2020, 9, 17, 7, 6, 2, 812, DateTimeKind.Local).AddTicks(286), "a2b931dd-c5be-47c5-a134-f9f3474cf630", "PON0001", null },
                    { "PVN0002", null, null, 500000000m, new DateTime(2020, 9, 17, 7, 6, 2, 812, DateTimeKind.Local).AddTicks(1108), "a2b931dd-c5be-47c5-a134-f9f3474cf630", "PON0002", null }
                });

            migrationBuilder.InsertData(
                table: "PurchaseOrderDetails",
                columns: new[] { "Id", "CostName", "ProductId", "PurchaseOrderId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { "3d4684a8-dcfb-4a43-8fc9-5ea256134bd8", null, "PRN0001", "PON0001", 100, 22500000m },
                    { "33b88cf1-6ce0-4ffe-bc08-bf5d2caf97dc", null, "PRN0002", "PON0002", 50, 22500000m }
                });

            migrationBuilder.InsertData(
                table: "StockHistories",
                columns: new[] { "Id", "ChangeQuantity", "EmployeeId", "OrderDetailId", "ProductId", "PurchaseOrderDetailId", "RecordDate", "StockActionId" },
                values: new object[,]
                {
                    { "849765e2-31a4-486b-8da4-cb8521c3f370", -1, "EPN0001", "7c2139ae-ee0b-4e6a-9c48-32428fc4c032", "PRN0001", null, new DateTime(2020, 9, 17, 7, 6, 2, 813, DateTimeKind.Local).AddTicks(1076), "1be88fef-0b15-4429-9012-9771a6e849f1" },
                    { "332469da-fa27-4d84-8bf8-a1da22e68d18", -1, "EPN0001", "27f6e5e9-a08c-4470-849b-20b9bc3cef39", "PRN0002", null, new DateTime(2020, 9, 17, 7, 6, 2, 813, DateTimeKind.Local).AddTicks(1559), "1be88fef-0b15-4429-9012-9771a6e849f1" }
                });

            migrationBuilder.InsertData(
                table: "StockHistories",
                columns: new[] { "Id", "ChangeQuantity", "EmployeeId", "OrderDetailId", "ProductId", "PurchaseOrderDetailId", "RecordDate", "StockActionId" },
                values: new object[] { "b9b095eb-0974-4aac-bee4-5358c2401b99", 100, "EPN0001", null, "PRN0001", "3d4684a8-dcfb-4a43-8fc9-5ea256134bd8", new DateTime(2020, 9, 17, 7, 6, 2, 812, DateTimeKind.Local).AddTicks(9085), "d6f2e2be-d9d4-40bf-b872-1c5d747466c9" });

            migrationBuilder.InsertData(
                table: "StockHistories",
                columns: new[] { "Id", "ChangeQuantity", "EmployeeId", "OrderDetailId", "ProductId", "PurchaseOrderDetailId", "RecordDate", "StockActionId" },
                values: new object[] { "deef8666-a71c-4441-be9d-315e99353a8d", 50, "EPN0001", null, "PRN0002", "33b88cf1-6ce0-4ffe-bc08-bf5d2caf97dc", new DateTime(2020, 9, 17, 7, 6, 2, 813, DateTimeKind.Local).AddTicks(1025), "d6f2e2be-d9d4-40bf-b872-1c5d747466c9" });

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
