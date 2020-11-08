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
                name: "AppRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Description = table.Column<string>(type: "nvarchar(250)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoles", x => x.Id);
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
                    Adrress = table.Column<string>(type: "nvarchar(300)", nullable: true)
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
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
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
                    Description = table.Column<string>(type: "nvarchar(250)", nullable: true)
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
                    Description = table.Column<string>(type: "nvarchar(250)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
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
                name: "TransactionStatuses",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transporters",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(30)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transporters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(12)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    OverView = table.Column<string>(type: "nvarchar(600)", nullable: true),
                    CategoryId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Views = table.Column<int>(nullable: false, defaultValue: 0),
                    RetailPrice = table.Column<decimal>(type: "decimal", nullable: false, defaultValue: 0m),
                    WholesalePrices = table.Column<decimal>(type: "decimal", nullable: false, defaultValue: 0m),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false),
                    BrandId = table.Column<string>(nullable: true),
                    Stars = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1),
                    WarrantyPeriod = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)0),
                    MinimumQuantity = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    MaximumQuantity = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
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
                name: "Promotions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    CategoryId = table.Column<string>(nullable: true),
                    DiscountValue = table.Column<decimal>(type: "decimal", nullable: false),
                    DiscountType = table.Column<string>(type: "char(1)", nullable: true),
                    FromDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Promotions_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(12)", nullable: false),
                    Dob = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    Gender = table.Column<bool>(nullable: true, defaultValue: false),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false),
                    AppuserId = table.Column<Guid>(nullable: true),
                    DepartmentId = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(30)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    BranchId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_AppUsers_AppuserId",
                        column: x => x.AppuserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    AppUserId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<string>(nullable: false),
                    Quantity = table.Column<int>(nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => new { x.AppUserId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_Carts_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Carts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductEvaluations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    ProductId = table.Column<string>(nullable: true),
                    AppUserId = table.Column<Guid>(nullable: true),
                    Title = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Stars = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductEvaluations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductEvaluations_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductEvaluations_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    ProductId = table.Column<string>(nullable: true),
                    Path = table.Column<string>(type: "nvarchar(1000)", nullable: true),
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
                name: "PromotionDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    PromotionId = table.Column<string>(nullable: true),
                    ProductId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromotionDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PromotionDetails_Promotions_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(12)", nullable: false),
                    Dob = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    Gender = table.Column<bool>(nullable: true, defaultValue: false),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false),
                    AppUserId = table.Column<Guid>(nullable: true),
                    CustomerTypeId = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(30)", nullable: true),
                    Email = table.Column<string>(type: "varchar(200)", nullable: true),
                    Fax = table.Column<string>(type: "varchar(50)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    EmployeeId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_CustomerTypes_CustomerTypeId",
                        column: x => x.CustomerTypeId,
                        principalTable: "CustomerTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
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
                    Description = table.Column<string>(type: "varchar(250)", nullable: true),
                    AppUserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suppliers_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Suppliers_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    EmployeeId = table.Column<string>(nullable: true),
                    BranchId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warehouses_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Warehouses_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductEvaluationReplies",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    ProductEvaluationId = table.Column<string>(nullable: true),
                    AppUserId = table.Column<Guid>(nullable: true),
                    Content = table.Column<string>(type: "nvarchar(1000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductEvaluationReplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductEvaluationReplies_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductEvaluationReplies_ProductEvaluations_ProductEvaluationId",
                        column: x => x.ProductEvaluationId,
                        principalTable: "ProductEvaluations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerPromotions",
                columns: table => new
                {
                    CustomerId = table.Column<string>(nullable: false),
                    PromotionId = table.Column<string>(nullable: false),
                    DiscountValue = table.Column<decimal>(type: "decimal", nullable: false),
                    DiscountType = table.Column<string>(type: "char(1)", nullable: true),
                    Code = table.Column<string>(type: "char(32)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPromotions", x => new { x.CustomerId, x.PromotionId });
                    table.ForeignKey(
                        name: "FK_CustomerPromotions_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerPromotions_Promotions_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(12)", nullable: false),
                    CustomerId = table.Column<string>(nullable: true),
                    TransactionStatusId = table.Column<string>(nullable: true),
                    PaymentStatusId = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2020, 11, 8, 9, 26, 24, 266, DateTimeKind.Local).AddTicks(3920)),
                    Description = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    EmployeeId = table.Column<string>(nullable: true),
                    DiscountValue = table.Column<decimal>(type: "decimal", nullable: false),
                    DiscountDescription = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    DiscountType = table.Column<string>(type: "char(1)", nullable: true),
                    BranchId = table.Column<string>(nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    CustomerAddress = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    CustomerPhone = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    OrderStatusId = table.Column<string>(nullable: true),
                    PromotionId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
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
                    table.ForeignKey(
                        name: "FK_Orders_Promotions_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_TransactionStatuses_TransactionStatusId",
                        column: x => x.TransactionStatusId,
                        principalTable: "TransactionStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RepairVouchers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(12)", nullable: false),
                    CustomerId = table.Column<string>(nullable: true),
                    CustomerName = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(30)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2020, 11, 8, 9, 26, 24, 279, DateTimeKind.Local).AddTicks(3361)),
                    AppointmentDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    BranchId = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairVouchers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepairVouchers_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RepairVouchers_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RepairVouchers_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrders",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(12)", nullable: false),
                    SupplierId = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2020, 11, 8, 9, 26, 24, 227, DateTimeKind.Local).AddTicks(1005)),
                    DeliveryDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2020, 11, 8, 9, 26, 24, 228, DateTimeKind.Local).AddTicks(9893)),
                    PaymentStatusId = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false),
                    DiscountValue = table.Column<decimal>(type: "decimal", nullable: false, defaultValue: 0m),
                    DiscountDescription = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    DiscountType = table.Column<string>(type: "char(1)", nullable: true),
                    BrandId = table.Column<string>(nullable: true),
                    TransactionStatusId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    EmployeeId = table.Column<string>(nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Branches_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
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
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_TransactionStatuses_TransactionStatusId",
                        column: x => x.TransactionStatusId,
                        principalTable: "TransactionStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventoryVouchers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(12)", nullable: false),
                    InventoryDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2020, 11, 8, 9, 26, 24, 284, DateTimeKind.Local).AddTicks(3385)),
                    EmployeeId = table.Column<string>(nullable: true),
                    WarehouseId = table.Column<string>(nullable: true),
                    IsConfirm = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryVouchers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryVouchers_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryVouchers_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LiquidationVouchers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(12)", nullable: false),
                    CustomerId = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2020, 11, 8, 9, 26, 24, 289, DateTimeKind.Local).AddTicks(821)),
                    Description = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    LiquidationDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2020, 11, 8, 9, 26, 24, 289, DateTimeKind.Local).AddTicks(1301)),
                    WarehouseId = table.Column<string>(nullable: true),
                    DiscountType = table.Column<string>(type: "char(1)", nullable: true),
                    DiscountValue = table.Column<decimal>(type: "decimal", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    CustomerAddress = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiquidationVouchers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LiquidationVouchers_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LiquidationVouchers_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    ProductId = table.Column<string>(nullable: false),
                    WarehouseId = table.Column<string>(nullable: true),
                    RealQuantity = table.Column<int>(type: "int", nullable: false),
                    AbleToSale = table.Column<int>(type: "int", nullable: false),
                    PlacedLocation = table.Column<string>(type: "nvarchar(500)", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_Stocks_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
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
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal", nullable: false),
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
                name: "Warranties",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(12)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2020, 11, 8, 9, 26, 24, 261, DateTimeKind.Local).AddTicks(930)),
                    PurchaseDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2020, 11, 8, 9, 26, 24, 261, DateTimeKind.Local).AddTicks(1291)),
                    OrderId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true, defaultValue: "nvarchar(300)"),
                    CustomerId = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warranties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warranties_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Warranties_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Warranties_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RepairVoucherDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    RepairVoucherId = table.Column<string>(nullable: true),
                    ProductId = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal", nullable: false),
                    RepairContent = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    ProductName = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    IsFixed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairVoucherDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepairVoucherDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RepairVoucherDetails_RepairVouchers_RepairVoucherId",
                        column: x => x.RepairVoucherId,
                        principalTable: "RepairVouchers",
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
                    UnitPrice = table.Column<decimal>(type: "decimal", nullable: false),
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
                name: "Returns",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(12)", nullable: false),
                    OrderId = table.Column<string>(nullable: true),
                    BranchId = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2020, 11, 8, 9, 26, 24, 272, DateTimeKind.Local).AddTicks(5734)),
                    IsImport = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Description = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    PurchaseOrderId = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Returns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Returns_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Returns_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Returns_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Returns_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventoryVoucherDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    InventoryVoucherId = table.Column<string>(nullable: true),
                    ProductId = table.Column<string>(nullable: true),
                    SystemQuantity = table.Column<int>(type: "int", nullable: false),
                    RealQuantity = table.Column<int>(type: "int", nullable: false),
                    BadQuantity = table.Column<int>(type: "int", nullable: false),
                    NormalQuantity = table.Column<int>(type: "int", nullable: false),
                    ExpiredQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryVoucherDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryVoucherDetails_InventoryVouchers_InventoryVoucherId",
                        column: x => x.InventoryVoucherId,
                        principalTable: "InventoryVouchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryVoucherDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LiquidationVoucherDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    ProductId = table.Column<string>(nullable: true),
                    LiquidationVoucherId = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiquidationVoucherDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LiquidationVoucherDetails_LiquidationVouchers_LiquidationVoucherId",
                        column: x => x.LiquidationVoucherId,
                        principalTable: "LiquidationVouchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LiquidationVoucherDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Shippings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    OrderId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    CustomerId = table.Column<string>(nullable: true),
                    CustomerName = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(30)", nullable: true),
                    Fee = table.Column<decimal>(type: "decimal", nullable: false, defaultValue: 0m),
                    TransporterId = table.Column<string>(nullable: true),
                    Address = table.Column<string>(type: "varchar(300)", nullable: true),
                    ShippingStatusId = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 11, 8, 9, 26, 24, 254, DateTimeKind.Local).AddTicks(8859)),
                    LiquidationVoucherId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shippings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shippings_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shippings_LiquidationVouchers_LiquidationVoucherId",
                        column: x => x.LiquidationVoucherId,
                        principalTable: "LiquidationVouchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shippings_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shippings_ShippingStatus_ShippingStatusId",
                        column: x => x.ShippingStatusId,
                        principalTable: "ShippingStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shippings_Transporters_TransporterId",
                        column: x => x.TransporterId,
                        principalTable: "Transporters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WarrantyDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    ProductId = table.Column<string>(nullable: true),
                    WarrantyId = table.Column<string>(nullable: true),
                    WarrantyPeriods = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarrantyDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarrantyDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WarrantyDetails_Warranties_WarrantyId",
                        column: x => x.WarrantyId,
                        principalTable: "Warranties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GoodsDeliveryNotes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(12)", nullable: false),
                    OrderId = table.Column<string>(nullable: true),
                    ExportDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2020, 11, 8, 9, 26, 24, 296, DateTimeKind.Local).AddTicks(6788)),
                    EmployeeId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    StockActionId = table.Column<string>(nullable: true),
                    WarehouseId = table.Column<string>(nullable: true),
                    ReturnId = table.Column<string>(nullable: true),
                    RepairVoucherId = table.Column<string>(nullable: true),
                    LiquidationVoucherId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsDeliveryNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsDeliveryNotes_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoodsDeliveryNotes_LiquidationVouchers_LiquidationVoucherId",
                        column: x => x.LiquidationVoucherId,
                        principalTable: "LiquidationVouchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoodsDeliveryNotes_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoodsDeliveryNotes_RepairVouchers_RepairVoucherId",
                        column: x => x.RepairVoucherId,
                        principalTable: "RepairVouchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoodsDeliveryNotes_Returns_ReturnId",
                        column: x => x.ReturnId,
                        principalTable: "Returns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoodsDeliveryNotes_StockActions_StockActionId",
                        column: x => x.StockActionId,
                        principalTable: "StockActions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoodsDeliveryNotes_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GoodsReceivedNotes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(12)", nullable: false),
                    PurchaseOrderId = table.Column<string>(nullable: true),
                    ImportDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2020, 11, 8, 9, 26, 24, 304, DateTimeKind.Local).AddTicks(7567)),
                    EmployeeId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    StockActionId = table.Column<string>(nullable: true),
                    WarehouseId = table.Column<string>(nullable: true),
                    RepairVoucherId = table.Column<string>(nullable: true),
                    PlacedLocation = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    ReturnId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsReceivedNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsReceivedNotes_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoodsReceivedNotes_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoodsReceivedNotes_RepairVouchers_RepairVoucherId",
                        column: x => x.RepairVoucherId,
                        principalTable: "RepairVouchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoodsReceivedNotes_Returns_ReturnId",
                        column: x => x.ReturnId,
                        principalTable: "Returns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoodsReceivedNotes_StockActions_StockActionId",
                        column: x => x.StockActionId,
                        principalTable: "StockActions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoodsReceivedNotes_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReturnDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    ReturnId = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<string>(nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReturnDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReturnDetails_Returns_ReturnId",
                        column: x => x.ReturnId,
                        principalTable: "Returns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentVouchers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(12)", nullable: false),
                    PurchaseOrderId = table.Column<string>(nullable: true),
                    Paid = table.Column<decimal>(type: "decimal", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2020, 11, 8, 9, 26, 24, 238, DateTimeKind.Local).AddTicks(5558)),
                    PaymentDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 11, 8, 9, 26, 24, 238, DateTimeKind.Local).AddTicks(3950)),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false),
                    PaymentMethodId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    CustomerId = table.Column<string>(nullable: true),
                    SupplierId = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<string>(nullable: true),
                    BranchId = table.Column<string>(nullable: true),
                    ReturnId = table.Column<string>(nullable: true),
                    ShippingId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentVouchers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentVouchers_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentVouchers_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentVouchers_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
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
                        name: "FK_PaymentVouchers_Returns_ReturnId",
                        column: x => x.ReturnId,
                        principalTable: "Returns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentVouchers_Shippings_ShippingId",
                        column: x => x.ShippingId,
                        principalTable: "Shippings",
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
                name: "ReceiptVouchers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(12)", nullable: false),
                    OrderId = table.Column<string>(nullable: true),
                    Received = table.Column<decimal>(type: "decimal", nullable: false, defaultValue: 0m),
                    ReceivedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 11, 8, 9, 26, 24, 248, DateTimeKind.Local).AddTicks(8781)),
                    PaymentMethodId = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false, defaultValue: false),
                    Description = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    SupplierId = table.Column<string>(nullable: true),
                    CustomerId = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2020, 11, 8, 9, 26, 24, 249, DateTimeKind.Local).AddTicks(402)),
                    EmployeeId = table.Column<string>(nullable: true),
                    BranchId = table.Column<string>(nullable: true),
                    RepairVoucherId = table.Column<string>(nullable: true),
                    LiquidationVoucherId = table.Column<string>(nullable: true),
                    ReturnId = table.Column<string>(nullable: true),
                    ShippingId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptVouchers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceiptVouchers_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptVouchers_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptVouchers_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptVouchers_LiquidationVouchers_LiquidationVoucherId",
                        column: x => x.LiquidationVoucherId,
                        principalTable: "LiquidationVouchers",
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
                        name: "FK_ReceiptVouchers_RepairVouchers_RepairVoucherId",
                        column: x => x.RepairVoucherId,
                        principalTable: "RepairVouchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptVouchers_Returns_ReturnId",
                        column: x => x.ReturnId,
                        principalTable: "Returns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptVouchers_Shippings_ShippingId",
                        column: x => x.ShippingId,
                        principalTable: "Shippings",
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
                name: "GoodsDeliveryNoteDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    GoodsDeliveryNoteId = table.Column<string>(nullable: true),
                    ProductId = table.Column<string>(nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsDeliveryNoteDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsDeliveryNoteDetails_GoodsDeliveryNotes_GoodsDeliveryNoteId",
                        column: x => x.GoodsDeliveryNoteId,
                        principalTable: "GoodsDeliveryNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoodsDeliveryNoteDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GoodsReceivedNoteDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    GoodsReceivedNoteId = table.Column<string>(nullable: true),
                    ProductId = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    UnitPrice = table.Column<decimal>(type: "decimal", nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsReceivedNoteDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsReceivedNoteDetails_GoodsReceivedNotes_GoodsReceivedNoteId",
                        column: x => x.GoodsReceivedNoteId,
                        principalTable: "GoodsReceivedNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoodsReceivedNoteDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockBook",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    GoodsReceivedNoteId = table.Column<string>(nullable: true),
                    GoodsDeliveryNoteId = table.Column<string>(nullable: true),
                    ImportQuantity = table.Column<int>(nullable: false),
                    ImportUnitPrice = table.Column<decimal>(nullable: false),
                    ExportQuantity = table.Column<int>(nullable: false),
                    ExportUnitPrice = table.Column<decimal>(nullable: false),
                    InventoryQuantity = table.Column<int>(nullable: false),
                    InventoryUnitPrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockBook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockBook_GoodsDeliveryNotes_GoodsDeliveryNoteId",
                        column: x => x.GoodsDeliveryNoteId,
                        principalTable: "GoodsDeliveryNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockBook_GoodsReceivedNotes_GoodsReceivedNoteId",
                        column: x => x.GoodsReceivedNoteId,
                        principalTable: "GoodsReceivedNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("dabaaa26-81a6-4137-8534-428fcfe8f692"), "1addfa84-d35f-4f83-9d9f-5854e66c9a6f", "Nhân viên thu ngân", "Cashier", "CASHIER" },
                    { new Guid("2c3047ca-da34-4a37-a640-d8b20bf0f21c"), "5ec7aba4-3d31-4f9f-a45f-d855b49c7bdc", "Quản lý chi nhánh", "Manager", "MANAGER" },
                    { new Guid("b6a7f49c-ed4a-41bf-b2b3-9fdaca763459"), "8dec27f5-7b95-486e-830d-fb08d2b5c99e", "Giám đốc", "SuperAdministrator", "SUPERADMINISTRATOR" },
                    { new Guid("0ae13bb5-43f5-404e-9100-046e7ff0bfc7"), "277edace-f69a-44fb-81d9-b058b610091c", "Kế toán", "Accountant", "ACCOUNTANT" },
                    { new Guid("68113af4-39b0-4926-b0fb-091d827fc6d9"), "bb473151-96fc-424d-86f1-bbb577ff9c6a", "Nhân viên kỹ thuật", "Technician", "TECHNICIAN" },
                    { new Guid("c4702302-748c-4b01-b0ad-8299d86896a4"), "2efaaaf0-6fee-409a-9705-76092a9fcaa9", "Nhân viên kinh doanh", "BusinessStaff", "BUSINESSSTAFF" },
                    { new Guid("e70b5bc1-102a-4ba5-a4e1-dd75b1fe5b1b"), "eea2b3c1-9b7b-4856-b330-63dd9210f6cf", "Thủ quỹ", "CashManager", "CASHMANAGER" },
                    { new Guid("1e76986e-fad7-42d9-a689-8a69d36273f9"), "39fa16ef-bed5-49dc-8598-20e2813baa66", "Quản trị viên", "Administrator", "ADMINISTRATOR" },
                    { new Guid("a7148fa4-5a7c-4144-bbfd-6d72c4f191c6"), "4b8927cc-5e96-43d7-8256-d4095d06e84a", "Nhân viên kho", "WarehouseStaff", "WAREHOUSESTAFF" },
                    { new Guid("3b46cfe9-6b65-4a91-bdd9-9ec9052c422a"), "89ed0c1b-ee49-4bfe-838d-5997572ce662", "Nhân viên bán hàng", "Salesman", "SALESMAN" },
                    { new Guid("ae9c2256-44e4-4d46-a297-4da29c7e1637"), "b51fc005-76d4-470b-ba99-b67a190395e8", "Thủ kho", "WarehouseManager", "WAREHOUSEMANAGER" }
                });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("8a4bde2a-b1f9-4498-be84-6d0282573bcf"), new Guid("b6a7f49c-ed4a-41bf-b2b3-9fdaca763459") },
                    { new Guid("ec3d3bc7-8141-4205-b068-4ca7d5fd1201"), new Guid("c4702302-748c-4b01-b0ad-8299d86896a4") },
                    { new Guid("408e3e40-3191-451c-a606-a1f565310e8a"), new Guid("e70b5bc1-102a-4ba5-a4e1-dd75b1fe5b1b") },
                    { new Guid("94a967b5-914b-43a5-b7f5-2cd42d994b92"), new Guid("dabaaa26-81a6-4137-8534-428fcfe8f692") },
                    { new Guid("fee378c8-8a38-4b42-a420-82ff5888e819"), new Guid("a7148fa4-5a7c-4144-bbfd-6d72c4f191c6") },
                    { new Guid("2ac747da-3752-488d-87dc-cb5d4a2e9432"), new Guid("3b46cfe9-6b65-4a91-bdd9-9ec9052c422a") },
                    { new Guid("1dcbb3b4-3bcd-4aaf-8b4d-e2339c5596f0"), new Guid("ae9c2256-44e4-4d46-a297-4da29c7e1637") }
                });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "Adrress", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("f502066f-7adc-4a5c-9d89-bb1015964cd9"), 0, null, "e2c65cc5-744e-4a17-afb6-8dc0428815f2", "duongnguyenadhp2@gmail.com", true, false, null, "DUONGNGUYENADHP2@GMAIL.COM", "CUS0002", "AQAAAAEAACcQAAAAENynIK+4txEe8z/8eU1O+Xrj7N67mYdM1d5HsoXycKxcMZSICGcObn0rq/jbi4ZWiw==", null, false, "", false, "cus0002" },
                    { new Guid("80efff0f-48cc-4e7a-8803-6782ce66960a"), 0, null, "c8d25d86-2812-426b-8a9e-ca1a86625f3f", "duongnguyenadhp1@gmail.com", true, false, null, "DUONGNGUYENADHP1@GMAIL.COM", "CUS0001", "AQAAAAEAACcQAAAAEIhhVUuJqVS4n8rD7b40JmQ2U58NjCpjciHn3f/KDjroL6Xbs5+XljzBtSxd1O8QRw==", null, false, "", false, "cus0001" },
                    { new Guid("ec3d3bc7-8141-4205-b068-4ca7d5fd1201"), 0, null, "08ea539f-8df7-4311-886c-2164920cbb11", "duongnguyenadhp6@gmail.com", true, false, null, "DUONGNGUYENADHP6@GMAIL.COM", "EPN0006", "AQAAAAEAACcQAAAAEJsfkfZ8tDYSgu4avMOm6VaNkszhuz56CW7Y0qvoN6FNeXY4muE6/dmor3S36CIvYA==", null, false, "", false, "epn0006" },
                    { new Guid("408e3e40-3191-451c-a606-a1f565310e8a"), 0, null, "dd5a0d91-b4d7-4ed1-94fb-309d15665827", "duongnguyenadhp5@gmail.com", true, false, null, "DUONGNGUYENADHP5@GMAIL.COM", "EPN0005", "AQAAAAEAACcQAAAAECDN8KtgAgV2sv29VhuK2zy4SYU+TuAc/3GD5bFTIn+wzs4ZvQGsINrnLpBTJg4XCw==", null, false, "", false, "epn0005" },
                    { new Guid("2ac747da-3752-488d-87dc-cb5d4a2e9432"), 0, null, "70a2ae39-0dcc-4fe4-9247-eb9e1e355d5f", "nguyen68973@st.vimaru.edu.vn", true, false, null, "NGUYEN68973@ST.VIMARU.EDU.VN", "EPN0002", "AQAAAAEAACcQAAAAENIg9UrvU/isU5sbkgrOqR52CZkFeo3rSQyTxnPMsLxp0YttTcOXaFmWWQqsrGwSUA==", null, false, "", false, "epn0002" },
                    { new Guid("fee378c8-8a38-4b42-a420-82ff5888e819"), 0, null, "a5792d5e-c82f-472f-8369-9252aec9b2fc", "duongnguyenadhp3@gmail.com", true, false, null, "DUONGNGUYENADHP3@GMAIL.COM", "EPN0003", "AQAAAAEAACcQAAAAELWtEmlidl2Jgjb5N/XL3UgeQUNdbAuFKaqac1ZOKfyVDPPr7ZjJQaukM9wT9aJ0SQ==", null, false, "", false, "epn0003" },
                    { new Guid("1dcbb3b4-3bcd-4aaf-8b4d-e2339c5596f0"), 0, null, "59b15fe1-0e71-48ca-8e35-b09c829b0446", "duongnguyenadhp@gmail.com", true, false, null, "DUONGNGUYENADHP@GMAIL.COM", "EPN0001", "AQAAAAEAACcQAAAAEI40apReaqhEx/cifKTYd5xJscirPqsJaoOKndLPMiW4xFOc6DvuZsCYPveLMtt8gQ==", null, false, "", false, "epn0001" },
                    { new Guid("8a4bde2a-b1f9-4498-be84-6d0282573bcf"), 0, null, "ce49c0fa-47c9-43ab-9b20-4037036fe732", "dnguyen24498@gmail.com", true, false, null, "DNGUYEN24498@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEAa9w4+bnHKJSTUq95EpxXBwpwvfue3sovz0+bUac5Ihoy51mIJeQq6CAfsqCKRd3Q==", null, false, "", false, "admin" },
                    { new Guid("94a967b5-914b-43a5-b7f5-2cd42d994b92"), 0, null, "cab79376-e982-4693-8179-14ee1ae1a44c", "duongnguyenadhp4@gmail.com", true, false, null, "DUONGNGUYENADHP4@GMAIL.COM", "EPN0004", "AQAAAAEAACcQAAAAEDWFzR/b1SR6gnnENn73W0bSCE/vylh2aNcsDWnp2cCDMpfz69nB6lQzDn9E3RnABQ==", null, false, "", false, "epn0004" }
                });

            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "Id", "Name" },
                values: new object[] { "ec4c314e-90b1-464c-aa52-2d34e555875e", "Chi nhánh mặc định" });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "fb5f1a2e-796a-4a38-9fb3-db9f4924c201", "Logitech" },
                    { "3d3db375-7436-4783-93d4-3f1777538b4e", "Samsung" },
                    { "ce382131-49b5-49ea-9732-9d33e49874de", "ThinkView" },
                    { "6c0d47b5-5b62-4c53-bd6e-680a3f08c1f0", "Canon" },
                    { "25a44ea6-ca33-494c-90d7-933e38ec3fb5", "Razer" },
                    { "82fab56a-dc26-4179-9624-d4eac7f43923", "Asus" },
                    { "bc240879-0f96-4752-9632-82141e4f23b3", "Lenovo" },
                    { "0f3d454d-a999-44de-af81-e918e117f5e5", "HP" },
                    { "df574e84-8df9-4da9-9686-52446cbd9a69", "Dell" },
                    { "85000231-9235-48a6-b852-c64bdcc3376b", "Apple" },
                    { "41f4bb29-5373-459a-8f6d-def64f15f747", "Acer" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { "f09b42c7-79f6-406e-8b32-d1bd714f93cf", null, "Chuột" },
                    { "610491a0-1824-4dd6-a8af-1f22f2a840a4", null, "Máy in" },
                    { "5f1d0891-37db-4d20-92e9-0a9da180ee62", null, "Bàn phím" },
                    { "c3eaeb51-38c5-4ac4-bbb1-9cbeb8881525", null, "Máy tính để bàn" },
                    { "e8a447d0-ab0c-4dbc-b11b-5bcb60af73e2", null, "Máy tính xách tay" },
                    { "a749d9d1-7621-4596-89f8-a6395778809b", null, "Tai nghe" }
                });

            migrationBuilder.InsertData(
                table: "CustomerTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { "c876bee4-019b-4b42-acbd-e728e9f545b6", null, "Bán buôn" },
                    { "a3bc8a51-9264-4590-af51-5fd20812695a", null, "Bán lẻ" }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "023536ea-f6c4-40a1-a610-2e95dd0e4f2a", "Bộ phận kho" },
                    { "2c33ac5e-c033-4eeb-b185-7f79007bbec3", "Bộ phận kinh doanh" },
                    { "15d805f8-32fb-43f3-8c95-31e87c05d3e7", "Bộ phận bán hàng" },
                    { "896571ab-344a-4258-b144-3f5c39b286e1", "Bộ phận thu ngân" },
                    { "fd730f29-f41c-4d7f-a4a7-66bb8b2ac6b0", "Bộ phận kế toán" },
                    { "aedb9def-b1ef-484a-ace0-d95471cf9421", "Bộ phận kỹ thuật" },
                    { "8567cb14-668c-45f9-9c9a-182ee3b99981", "Bộ phận quản lý cấp cao" }
                });

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "0e3c8901-759e-4df0-aed3-2f7e6b0425ed", "Chờ xác nhận" },
                    { "56c9e511-f469-4b29-a39f-c4bc5fa20428", "Đã xác nhận/Kiểm kho" },
                    { "39b20679-3c2a-4a4b-b73d-441443438308", "Đã tạo" },
                    { "384d0614-49bd-46c0-8db1-33fc6d0b34f2", "Hủy" }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "93f58e8a-7b32-4f80-a128-1c3dc5b50eda", "Chuyển khoản" },
                    { "f178a9b0-13fa-4221-90cc-7cede6995026", "Điểm tích lũy" },
                    { "7cd60e3f-c215-42b3-a98e-c4ac4fe71b63", "Tiền mặt" },
                    { "a196f0c3-c36a-4cb1-892c-3c72e1dd8b02", "Quẹt thẻ" }
                });

            migrationBuilder.InsertData(
                table: "PaymentStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "4d2a23f7-4d7b-4fcb-b69b-fa078487f9aa", "Chưa thanh toán" },
                    { "4cc5fe42-6e47-4d47-a205-96039474bdac", "Thanh toán một phần" },
                    { "03696c5b-71ad-4476-a0af-e52568d4b645", "Đã thanh toán" }
                });

            migrationBuilder.InsertData(
                table: "ShippingStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "cf4ff408-bdbc-4692-9b70-0e141269cfdb", "Đã đóng gói/Đợi giao hàng" },
                    { "e1bf07a3-4576-463d-940d-ecf284b80534", "Đang giao hàng" },
                    { "220966a7-8489-46dc-b621-97e834925554", "Đã giao hàng/Nhận tiền từ nhà vận chuyển" }
                });

            migrationBuilder.InsertData(
                table: "StockActions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "ec40371a-cd21-44f3-85a2-618ceb92a16f", "Nhập hàng nhà cung cấp" },
                    { "e27503bd-12c6-4d8e-a68e-6296892134e2", "Xuất kho giao hàng cho khách/shipper" },
                    { "066d0137-c819-4335-ab4a-b5659193b80b", "Nhập hàng khách trả" },
                    { "8170a4e6-42c4-48b6-a9a6-20ef490e75be", "Nhập hàng bảo hành" }
                });

            migrationBuilder.InsertData(
                table: "TransactionStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "38ed81d4-7606-458e-add1-bf4c03d035cb", "Hủy" },
                    { "4226c92d-694f-4948-afd4-04c636fd77a6", "Hoàn thành" },
                    { "7c2da6fd-28ab-496b-aa1b-bb343ec49d3a", "Đang chờ" },
                    { "cc0c7f54-de94-481c-b662-36584002fe41", "Đang kiểm kho" },
                    { "1fd31639-0fa6-4ac2-bbf2-f8dbd6e1f3c8", "Đang giao dịch" },
                    { "138193dc-86b4-4c9d-a961-b7c1b1f7bed1", "Đang giao hàng" }
                });

            migrationBuilder.InsertData(
                table: "Transporters",
                columns: new[] { "Id", "Address", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { "0ac9fb8f-c352-498d-9129-229fd5a080fa", "Số 647, Lạch Tray, Ngô Quyền, Hải Phòng", "Giao hàng nhanh", "0947325921" },
                    { "9d82a2e6-b9d9-4a89-977b-e94dd965e648", "Số 347, Lạch Tray, Ngô Quyền, Hải Phòng", "Giao hàng tiết kiệm", "0947685921" },
                    { "b6f71e5d-ea3b-4bbb-bbd4-b0f35acc052e", "Số 147, Lạch Tray, Ngô Quyền, Hải Phòng", "Viettel Post", "09476845921" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "AppUserId", "CustomerTypeId", "Description", "Email", "EmployeeId", "Fax", "Gender", "Name", "PhoneNumber", "Website" },
                values: new object[,]
                {
                    { "CUS0002", "Số 99, Đường Hải Triều, Phường Quán Toan, Quận Hồng Bàng, Thành phố Hải Phòng", new Guid("f502066f-7adc-4a5c-9d89-bb1015964cd9"), "a3bc8a51-9264-4590-af51-5fd20812695a", null, "duongnguyenadhp@gmail.com", null, null, false, "Bùi Thùy Dương", "0934349618", null },
                    { "CUS0001", "Số 88, Đường Hải Triều, Phường Quán Toan, Quận Hồng Bàng, Thành phố Hải Phòng", new Guid("80efff0f-48cc-4e7a-8803-6782ce66960a"), "a3bc8a51-9264-4590-af51-5fd20812695a", null, "duongnguyenadhp@gmail.com", null, null, true, "Nguyễn Dương Nguyên", "0934349618", null }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "AppuserId", "BranchId", "DepartmentId", "Description", "Dob", "Gender", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { "EPN0007", "Số 88-Hải Triều-Quán Toan-Hồng Bàng-Hải Phòng", new Guid("8a4bde2a-b1f9-4498-be84-6d0282573bcf"), "ec4c314e-90b1-464c-aa52-2d34e555875e", "8567cb14-668c-45f9-9c9a-182ee3b99981", null, new DateTime(1998, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Nguyễn Dương Nguyên", "0934347618" },
                    { "EPN0004", "Số 100, Trần Thành Ngọ, Kiến An, Hải Phòng", new Guid("94a967b5-914b-43a5-b7f5-2cd42d994b92"), "ec4c314e-90b1-464c-aa52-2d34e555875e", "896571ab-344a-4258-b144-3f5c39b286e1", null, new DateTime(1998, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Nguyễn Thành Nam", "0934347618" },
                    { "EPN0002", "Số 100, Trần Thành Ngọ, Kiến An, Hải Phòng", new Guid("2ac747da-3752-488d-87dc-cb5d4a2e9432"), "ec4c314e-90b1-464c-aa52-2d34e555875e", "15d805f8-32fb-43f3-8c95-31e87c05d3e7", null, new DateTime(1998, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Nguyễn Thanh Tú", "0934347618" },
                    { "EPN0006", "Số 100, Trần Thành Ngọ, Kiến An, Hải Phòng", new Guid("ec3d3bc7-8141-4205-b068-4ca7d5fd1201"), "ec4c314e-90b1-464c-aa52-2d34e555875e", "2c33ac5e-c033-4eeb-b185-7f79007bbec3", null, new DateTime(1998, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Trần Thị Thủy", "0934347618" },
                    { "EPN0003", "Số 100, Trần Thành Ngọ, Kiến An, Hải Phòng", new Guid("fee378c8-8a38-4b42-a420-82ff5888e819"), "ec4c314e-90b1-464c-aa52-2d34e555875e", "023536ea-f6c4-40a1-a610-2e95dd0e4f2a", null, new DateTime(1998, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Nguyễn Văn An", "0934347618" },
                    { "EPN0001", "Số 88, Trần Thành Ngọ, Kiến An, Hải Phòng", new Guid("1dcbb3b4-3bcd-4aaf-8b4d-e2339c5596f0"), "ec4c314e-90b1-464c-aa52-2d34e555875e", "023536ea-f6c4-40a1-a610-2e95dd0e4f2a", null, new DateTime(1998, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Nguyễn Thanh Tùng", "0934349618" },
                    { "EPN0005", "Số 100, Trần Thành Ngọ, Kiến An, Hải Phòng", new Guid("408e3e40-3191-451c-a606-a1f565310e8a"), "ec4c314e-90b1-464c-aa52-2d34e555875e", "896571ab-344a-4258-b144-3f5c39b286e1", null, new DateTime(1998, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Nguyễn Khánh Ly", "0934347618" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "CategoryId", "Description", "MaximumQuantity", "MinimumQuantity", "Name", "OverView", "RetailPrice", "Stars", "Views", "WarrantyPeriod", "WholesalePrices" },
                values: new object[,]
                {
                    { "PRN0009", "6c0d47b5-5b62-4c53-bd6e-680a3f08c1f0", "610491a0-1824-4dd6-a8af-1f22f2a840a4", null, 100, 5, "Canon LBP 2900 White", "Chức năng: Print, Khổ giấy: A4/A5, In đảo mặt: Không, Cổng giao tiếp: USB, Dùng mực: Canon EP303", 2990000m, (byte)1, 2232, (byte)36, 2700000m },
                    { "PRN0008", "6c0d47b5-5b62-4c53-bd6e-680a3f08c1f0", "610491a0-1824-4dd6-a8af-1f22f2a840a4", null, 100, 5, "Canon LBP 2900 Black", "Chức năng: Print, Khổ giấy: A4/A5, In đảo mặt: Không, Cổng giao tiếp: USB, Dùng mực: Canon EP303", 2990000m, (byte)1, 2100, (byte)36, 2700000m },
                    { "PRN0013", "25a44ea6-ca33-494c-90d7-933e38ec3fb5", "f09b42c7-79f6-406e-8b32-d1bd714f93cf", null, 100, 5, "Razer Kraken Pro V2", null, 2290000m, (byte)1, 2232, (byte)12, 2285000m },
                    { "PRN0012", "25a44ea6-ca33-494c-90d7-933e38ec3fb5", "f09b42c7-79f6-406e-8b32-d1bd714f93cf", null, 100, 5, "Razer Hammerhead Pro V2", null, 1290000m, (byte)1, 2232, (byte)12, 1285000m },
                    { "PRN0011", "fb5f1a2e-796a-4a38-9fb3-db9f4924c201", "f09b42c7-79f6-406e-8b32-d1bd714f93cf", null, 100, 5, "Logitech G102 White", "8000 DPI, LED RGB 16,8 triệu màu tùy chỉnh, Phù hợp với Gaming", 290000m, (byte)1, 2232, (byte)12, 270000m },
                    { "PRN0010", "fb5f1a2e-796a-4a38-9fb3-db9f4924c201", "f09b42c7-79f6-406e-8b32-d1bd714f93cf", null, 100, 5, "Logitech G102 Black", "8000 DPI, LED RGB 16,8 triệu màu tùy chỉnh, Phù hợp với Gaming", 290000m, (byte)1, 2232, (byte)12, 270000m },
                    { "PRN0004", "85000231-9235-48a6-b852-c64bdcc3376b", "c3eaeb51-38c5-4ac4-bbb1-9cbeb8881525", null, 100, 5, "Mac Mini (2020)", "I5-3.0GHz/8GB/512TB/Intel UHD Graphics 630/New-Fullbox", 27590000m, (byte)1, 2930, (byte)36, 27500000m },
                    { "PRN0019", "bc240879-0f96-4752-9632-82141e4f23b3", "e8a447d0-ab0c-4dbc-b11b-5bcb60af73e2", null, 100, 5, "Thinkpad X260", "I5-3.1GHz/8GB/128GB/New-Fullbox", 10990000m, (byte)1, 2100, (byte)36, 10900000m },
                    { "PRN0018", "bc240879-0f96-4752-9632-82141e4f23b3", "e8a447d0-ab0c-4dbc-b11b-5bcb60af73e2", null, 100, 5, "Thinkpad X250", "I5-2.6GHz/8GB/128GB/New-Fullbox", 9990000m, (byte)1, 2100, (byte)36, 9900000m },
                    { "PRN0017", "82fab56a-dc26-4179-9624-d4eac7f43923", "e8a447d0-ab0c-4dbc-b11b-5bcb60af73e2", null, 100, 5, "Asus Zenbook", "I5-3.6GHz/8GB/128GB/New-Fullbox", 13990000m, (byte)1, 2100, (byte)36, 13900000m },
                    { "PRN0016", "82fab56a-dc26-4179-9624-d4eac7f43923", "e8a447d0-ab0c-4dbc-b11b-5bcb60af73e2", null, 100, 5, "Asus ROG Strix", "I7-3.6GHz/8GB/512GB/New-Fullbox", 22990000m, (byte)1, 2100, (byte)36, 22900000m },
                    { "PRN0015", "82fab56a-dc26-4179-9624-d4eac7f43923", "e8a447d0-ab0c-4dbc-b11b-5bcb60af73e2", null, 100, 5, "Asus Predator", "I9-3.6GHz/16GB/1TB/New-Fullbox", 52990000m, (byte)1, 2100, (byte)36, 52900000m },
                    { "PRN0014", "82fab56a-dc26-4179-9624-d4eac7f43923", "e8a447d0-ab0c-4dbc-b11b-5bcb60af73e2", null, 100, 5, "Asus Nitro 5", "I5-2.6GHz/8GB/256GB/New-Fullbox", 12990000m, (byte)1, 2100, (byte)36, 12900000m },
                    { "PRN0007", "41f4bb29-5373-459a-8f6d-def64f15f747", "e8a447d0-ab0c-4dbc-b11b-5bcb60af73e2", null, 100, 5, "Acer Aspire 3 A315", "I3-2.3GHz/4GB/1TB/New-Fullbox", 9990000m, (byte)1, 2100, (byte)36, 9500000m },
                    { "PRN0006", "41f4bb29-5373-459a-8f6d-def64f15f747", "e8a447d0-ab0c-4dbc-b11b-5bcb60af73e2", null, 100, 5, "Acer Swift 3 SF314", "I3-2.1GHz/4GB/256GB/New-Fullbox", 13990000m, (byte)1, 2100, (byte)36, 13500000m },
                    { "PRN0005", "85000231-9235-48a6-b852-c64bdcc3376b", "e8a447d0-ab0c-4dbc-b11b-5bcb60af73e2", null, 100, 5, "Macbook Air 13.3\" (2020)", "Gray/I5-1.1GHz/8GB/256GB/New-Fullbox", 31990000m, (byte)1, 2100, (byte)36, 31500000m },
                    { "PRN0002", "85000231-9235-48a6-b852-c64bdcc3376b", "e8a447d0-ab0c-4dbc-b11b-5bcb60af73e2", null, 100, 5, "Macbook Pro 13.3\" (2019)", "Gray/I5-1.4GHz/8GB/256GB/TouchBar/New-Fullbox", 32000000m, (byte)1, 2500, (byte)36, 31500000m },
                    { "PRN0001", "85000231-9235-48a6-b852-c64bdcc3376b", "e8a447d0-ab0c-4dbc-b11b-5bcb60af73e2", null, 100, 5, "Macbook Pro 13.3\" (2020)", "Gray/I5-2.0GHz/16GB/512GB/TouchBar/New-Fullbox", 45000000m, (byte)1, 2100, (byte)36, 44500000m },
                    { "PRN0003", "85000231-9235-48a6-b852-c64bdcc3376b", "c3eaeb51-38c5-4ac4-bbb1-9cbeb8881525", null, 100, 5, "iMac 27\" (2020)", "I7-3.8GHz/8GB/1TB/Radeon Pro 5500 XT 8GB/New-Fullbox", 66800000m, (byte)1, 2930, (byte)36, 66500000m }
                });

            migrationBuilder.InsertData(
                table: "Warehouses",
                columns: new[] { "Id", "BranchId", "EmployeeId", "Name" },
                values: new object[,]
                {
                    { "3dea6b77-c95b-48ff-bf50-746ad1714bbd", "ec4c314e-90b1-464c-aa52-2d34e555875e", null, "Kho chứa sản phẩm thanh lý" },
                    { "ac386e1b-3647-4457-92b5-3e003e726290", "ec4c314e-90b1-464c-aa52-2d34e555875e", null, "Kho chứa sản phẩm bảo hành" },
                    { "d6bbee65-fe3d-4765-b569-202d9f3aa4f5", "ec4c314e-90b1-464c-aa52-2d34e555875e", null, "Kho chính" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "BranchId", "CustomerAddress", "CustomerId", "CustomerName", "CustomerPhone", "Description", "DiscountDescription", "DiscountType", "DiscountValue", "EmployeeId", "OrderStatusId", "PaymentStatusId", "PromotionId", "TotalAmount", "TransactionStatusId" },
                values: new object[] { "SRN0001", "ec4c314e-90b1-464c-aa52-2d34e555875e", null, "CUS0001", null, null, null, null, "$", 0m, "EPN0002", null, "03696c5b-71ad-4476-a0af-e52568d4b645", null, 45000000m, "4226c92d-694f-4948-afd4-04c636fd77a6" });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "IsThumbnail", "Path", "ProductId" },
                values: new object[,]
                {
                    { "6eb2d8f5-999e-4b3e-8280-0fb58c6ef35f", true, "6eb2d8f5-999e-4b3e-8280-0fb58c6ef35f.png", "PRN0001" },
                    { "9e45320a-3b5f-45c1-988b-9b54a917ce41", true, "9e45320a-3b5f-45c1-988b-9b54a917ce41.png", "PRN0009" },
                    { "126eb3fa-cd6c-4601-925b-c0e5754fe293", true, "126eb3fa-cd6c-4601-925b-c0e5754fe293.png", "PRN0008" },
                    { "8b361a2c-5a87-44ed-a327-934f7147c641", true, "8b361a2c-5a87-44ed-a327-934f7147c641.png", "PRN0013" },
                    { "9438d141-3011-4438-8ed3-85d7d375f255", true, "9438d141-3011-4438-8ed3-85d7d375f255.png", "PRN0012" },
                    { "9f8e48be-e769-4222-9fad-0ca1c60d7e57", true, "9f8e48be-e769-4222-9fad-0ca1c60d7e57.png", "PRN0011" },
                    { "648807b4-a97e-4e0c-bbef-c4c6198828b4", true, "648807b4-a97e-4e0c-bbef-c4c6198828b4.png", "PRN0010" },
                    { "9d2281d0-5bd9-4323-b79b-d65144af5338", true, "9d2281d0-5bd9-4323-b79b-d65144af5338.png", "PRN0004" },
                    { "b8a45f7e-7174-47cc-87da-17b18d084b99", true, "b8a45f7e-7174-47cc-87da-17b18d084b99.png", "PRN0003" },
                    { "dbe638f0-4ded-4bc4-b373-aac67c0698cb", true, "dbe638f0-4ded-4bc4-b373-aac67c0698cb.png", "PRN0018" },
                    { "4eb16e67-92f7-41cb-ab49-547e3ab4fb70", true, "4eb16e67-92f7-41cb-ab49-547e3ab4fb70.png", "PRN0019" },
                    { "4de34fba-6e86-42bf-ae6b-8a1d1aad8fc8", true, "4de34fba-6e86-42bf-ae6b-8a1d1aad8fc8.png", "PRN0016" },
                    { "1699bbbb-5490-43a0-9219-8ec31b65e958", true, "1699bbbb-5490-43a0-9219-8ec31b65e958.png", "PRN0015" },
                    { "53d1c2b4-6724-4ad4-b056-b6b26a830a8b", true, "53d1c2b4-6724-4ad4-b056-b6b26a830a8b.png", "PRN0014" },
                    { "0b9c9f29-f557-49e6-8753-7018576f1f11", true, "0b9c9f29-f557-49e6-8753-7018576f1f11.png", "PRN0007" },
                    { "bc091a22-5d49-4f0a-9d8b-1823e7df0b26", true, "bc091a22-5d49-4f0a-9d8b-1823e7df0b26.png", "PRN0006" },
                    { "d791b7d6-661a-43fd-a0e8-32589639a346", true, "d791b7d6-661a-43fd-a0e8-32589639a346.png", "PRN0005" },
                    { "bbe3a085-0b4e-4549-97b5-0ab47536c0db", true, "bbe3a085-0b4e-4549-97b5-0ab47536c0db.png", "PRN0002" },
                    { "86ef26e0-d2cd-48a7-8dd5-63b769731d55", true, "86ef26e0-d2cd-48a7-8dd5-63b769731d55.png", "PRN0017" }
                });

            migrationBuilder.InsertData(
                table: "Stocks",
                columns: new[] { "ProductId", "AbleToSale", "PlacedLocation", "RealQuantity", "WarehouseId" },
                values: new object[,]
                {
                    { "PRN0002", 10, null, 10, "d6bbee65-fe3d-4765-b569-202d9f3aa4f5" },
                    { "PRN0001", 9, null, 9, "d6bbee65-fe3d-4765-b569-202d9f3aa4f5" }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "Address", "AppUserId", "Description", "Email", "EmployeeId", "Fax", "Name", "PhoneNumber", "Website" },
                values: new object[,]
                {
                    { "SUN0002", "An Lão, Hải Phòng", null, null, "thienanconntact@gmail.com", "EPN0002", null, "Công ty TNHH Thiên An", "0959842342", null },
                    { "SUN0001", "Kinh Môn, Hải Dương", null, null, "minhkhangconntact@gmail.com", "EPN0001", null, "Công ty TNHH Minh Khang", "0959842342", null },
                    { "SUN0003", "Số 102,Nguyễn Bình, Đổng Quốc Bình, Ngô Quyền, Hải Phòng", null, null, "laptop247conntact@gmail.com", "EPN0001", null, "Cửa hàng máy tính Laptop247", "0959842342", null },
                    { "SUN0004", "Số 109,Nguyễn Bình, Đổng Quốc Bình, Ngô Quyền, Hải Phòng", null, null, "laptophaiphongconntact@gmail.com", "EPN0002", null, "Cửa hàng máy tính Hải Phòng", "0959842342", null }
                });

            migrationBuilder.InsertData(
                table: "GoodsDeliveryNotes",
                columns: new[] { "Id", "Description", "EmployeeId", "ExportDate", "LiquidationVoucherId", "OrderId", "RepairVoucherId", "ReturnId", "StockActionId", "WarehouseId" },
                values: new object[] { "GDN0001", null, "EPN0001", new DateTime(2020, 11, 8, 9, 26, 24, 298, DateTimeKind.Local).AddTicks(9862), null, "SRN0001", null, null, "e27503bd-12c6-4d8e-a68e-6296892134e2", "d6bbee65-fe3d-4765-b569-202d9f3aa4f5" });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "Id", "OrderId", "ProductId", "Quantity", "ServiceName", "UnitPrice" },
                values: new object[] { "ca9aa508-dc33-49b1-ae1c-8f76018dacdf", "SRN0001", "PRN0001", 1, null, 45000000m });

            migrationBuilder.InsertData(
                table: "PurchaseOrders",
                columns: new[] { "Id", "BrandId", "DateCreated", "DeliveryDate", "Description", "DiscountDescription", "DiscountType", "EmployeeId", "PaymentStatusId", "SupplierId", "TotalAmount", "TransactionStatusId" },
                values: new object[] { "PON0001", "ec4c314e-90b1-464c-aa52-2d34e555875e", new DateTime(2020, 11, 8, 9, 26, 24, 231, DateTimeKind.Local).AddTicks(4963), new DateTime(2020, 11, 8, 9, 26, 24, 231, DateTimeKind.Local).AddTicks(4517), null, null, "$", "EPN0001", "4cc5fe42-6e47-4d47-a205-96039474bdac", "SUN0001", 720000000m, "1fd31639-0fa6-4ac2-bbf2-f8dbd6e1f3c8" });

            migrationBuilder.InsertData(
                table: "ReceiptVouchers",
                columns: new[] { "Id", "BranchId", "CustomerId", "Description", "EmployeeId", "LiquidationVoucherId", "OrderId", "PaymentMethodId", "Received", "RepairVoucherId", "ReturnId", "ShippingId", "SupplierId" },
                values: new object[] { "RVN0001", "ec4c314e-90b1-464c-aa52-2d34e555875e", "CUS0001", null, "EPN0001", null, "SRN0001", "7cd60e3f-c215-42b3-a98e-c4ac4fe71b63", 45000000m, null, null, null, null });

            migrationBuilder.InsertData(
                table: "GoodsDeliveryNoteDetails",
                columns: new[] { "Id", "GoodsDeliveryNoteId", "ProductId", "Quantity", "UnitPrice" },
                values: new object[] { "d1013190-9ca9-4ef0-a0a3-1df43601fa9c", "GDN0001", "PRN0001", 1, 45000000m });

            migrationBuilder.InsertData(
                table: "GoodsReceivedNotes",
                columns: new[] { "Id", "Description", "EmployeeId", "ImportDate", "PlacedLocation", "PurchaseOrderId", "RepairVoucherId", "ReturnId", "StockActionId", "WarehouseId" },
                values: new object[] { "GRN0001", null, "EPN0001", new DateTime(2020, 11, 8, 9, 26, 24, 307, DateTimeKind.Local).AddTicks(3803), null, "PON0001", null, null, "ec40371a-cd21-44f3-85a2-618ceb92a16f", "d6bbee65-fe3d-4765-b569-202d9f3aa4f5" });

            migrationBuilder.InsertData(
                table: "PaymentVouchers",
                columns: new[] { "Id", "BranchId", "CustomerId", "DateCreated", "Description", "EmployeeId", "Paid", "PaymentDate", "PaymentMethodId", "PurchaseOrderId", "ReturnId", "ShippingId", "SupplierId" },
                values: new object[] { "PVN0001", "ec4c314e-90b1-464c-aa52-2d34e555875e", null, new DateTime(2020, 11, 8, 9, 26, 24, 241, DateTimeKind.Local).AddTicks(699), null, null, 320000000m, new DateTime(2020, 11, 8, 9, 26, 24, 241, DateTimeKind.Local).AddTicks(1077), "7cd60e3f-c215-42b3-a98e-c4ac4fe71b63", "PON0001", null, null, null });

            migrationBuilder.InsertData(
                table: "PurchaseOrderDetails",
                columns: new[] { "Id", "CostName", "ProductId", "PurchaseOrderId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { "73e88c7b-6a11-4c27-9045-308d2d1b553a", null, "PRN0001", "PON0001", 10, 42000000m },
                    { "e93c0a07-69a3-4736-9671-21ce451bc656", null, "PRN0002", "PON0001", 10, 30000000m }
                });

            migrationBuilder.InsertData(
                table: "GoodsReceivedNoteDetails",
                columns: new[] { "Id", "GoodsReceivedNoteId", "ProductId", "Quantity", "UnitPrice" },
                values: new object[] { "80f3c6fc-71e3-42a6-90a0-be91f3bd5234", "GRN0001", "PRN0001", 10, 42000000m });

            migrationBuilder.InsertData(
                table: "GoodsReceivedNoteDetails",
                columns: new[] { "Id", "GoodsReceivedNoteId", "ProductId", "Quantity", "UnitPrice" },
                values: new object[] { "1c5c2a8f-8c5f-4cba-94d4-f640242af728", "GRN0001", "PRN0002", 10, 30000000m });

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProductId",
                table: "Carts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPromotions_PromotionId",
                table: "CustomerPromotions",
                column: "PromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AppUserId",
                table: "Customers",
                column: "AppUserId",
                unique: true,
                filter: "[AppUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerTypeId",
                table: "Customers",
                column: "CustomerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_EmployeeId",
                table: "Customers",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_AppuserId",
                table: "Employees",
                column: "AppuserId",
                unique: true,
                filter: "[AppuserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BranchId",
                table: "Employees",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsDeliveryNoteDetails_GoodsDeliveryNoteId",
                table: "GoodsDeliveryNoteDetails",
                column: "GoodsDeliveryNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsDeliveryNoteDetails_ProductId",
                table: "GoodsDeliveryNoteDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsDeliveryNotes_EmployeeId",
                table: "GoodsDeliveryNotes",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsDeliveryNotes_LiquidationVoucherId",
                table: "GoodsDeliveryNotes",
                column: "LiquidationVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsDeliveryNotes_OrderId",
                table: "GoodsDeliveryNotes",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsDeliveryNotes_RepairVoucherId",
                table: "GoodsDeliveryNotes",
                column: "RepairVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsDeliveryNotes_ReturnId",
                table: "GoodsDeliveryNotes",
                column: "ReturnId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsDeliveryNotes_StockActionId",
                table: "GoodsDeliveryNotes",
                column: "StockActionId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsDeliveryNotes_WarehouseId",
                table: "GoodsDeliveryNotes",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivedNoteDetails_GoodsReceivedNoteId",
                table: "GoodsReceivedNoteDetails",
                column: "GoodsReceivedNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivedNoteDetails_ProductId",
                table: "GoodsReceivedNoteDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivedNotes_EmployeeId",
                table: "GoodsReceivedNotes",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivedNotes_PurchaseOrderId",
                table: "GoodsReceivedNotes",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivedNotes_RepairVoucherId",
                table: "GoodsReceivedNotes",
                column: "RepairVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivedNotes_ReturnId",
                table: "GoodsReceivedNotes",
                column: "ReturnId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivedNotes_StockActionId",
                table: "GoodsReceivedNotes",
                column: "StockActionId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivedNotes_WarehouseId",
                table: "GoodsReceivedNotes",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryVoucherDetails_InventoryVoucherId",
                table: "InventoryVoucherDetails",
                column: "InventoryVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryVoucherDetails_ProductId",
                table: "InventoryVoucherDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryVouchers_EmployeeId",
                table: "InventoryVouchers",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryVouchers_WarehouseId",
                table: "InventoryVouchers",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_LiquidationVoucherDetails_LiquidationVoucherId",
                table: "LiquidationVoucherDetails",
                column: "LiquidationVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_LiquidationVoucherDetails_ProductId",
                table: "LiquidationVoucherDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_LiquidationVouchers_CustomerId",
                table: "LiquidationVouchers",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_LiquidationVouchers_WarehouseId",
                table: "LiquidationVouchers",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BranchId",
                table: "Orders",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EmployeeId",
                table: "Orders",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderStatusId",
                table: "Orders",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentStatusId",
                table: "Orders",
                column: "PaymentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PromotionId",
                table: "Orders",
                column: "PromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TransactionStatusId",
                table: "Orders",
                column: "TransactionStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVouchers_BranchId",
                table: "PaymentVouchers",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVouchers_CustomerId",
                table: "PaymentVouchers",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVouchers_EmployeeId",
                table: "PaymentVouchers",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVouchers_PaymentMethodId",
                table: "PaymentVouchers",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVouchers_PurchaseOrderId",
                table: "PaymentVouchers",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVouchers_ReturnId",
                table: "PaymentVouchers",
                column: "ReturnId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVouchers_ShippingId",
                table: "PaymentVouchers",
                column: "ShippingId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVouchers_SupplierId",
                table: "PaymentVouchers",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductEvaluationReplies_AppUserId",
                table: "ProductEvaluationReplies",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductEvaluationReplies_ProductEvaluationId",
                table: "ProductEvaluationReplies",
                column: "ProductEvaluationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductEvaluations_AppUserId",
                table: "ProductEvaluations",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductEvaluations_ProductId",
                table: "ProductEvaluations",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionDetails_ProductId",
                table: "PromotionDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionDetails_PromotionId",
                table: "PromotionDetails",
                column: "PromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_Promotions_CategoryId",
                table: "Promotions",
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
                name: "IX_PurchaseOrders_BrandId",
                table: "PurchaseOrders",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_EmployeeId",
                table: "PurchaseOrders",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_PaymentStatusId",
                table: "PurchaseOrders",
                column: "PaymentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_SupplierId",
                table: "PurchaseOrders",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_TransactionStatusId",
                table: "PurchaseOrders",
                column: "TransactionStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptVouchers_BranchId",
                table: "ReceiptVouchers",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptVouchers_CustomerId",
                table: "ReceiptVouchers",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptVouchers_EmployeeId",
                table: "ReceiptVouchers",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptVouchers_LiquidationVoucherId",
                table: "ReceiptVouchers",
                column: "LiquidationVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptVouchers_OrderId",
                table: "ReceiptVouchers",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptVouchers_PaymentMethodId",
                table: "ReceiptVouchers",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptVouchers_RepairVoucherId",
                table: "ReceiptVouchers",
                column: "RepairVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptVouchers_ReturnId",
                table: "ReceiptVouchers",
                column: "ReturnId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptVouchers_ShippingId",
                table: "ReceiptVouchers",
                column: "ShippingId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptVouchers_SupplierId",
                table: "ReceiptVouchers",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairVoucherDetails_ProductId",
                table: "RepairVoucherDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairVoucherDetails_RepairVoucherId",
                table: "RepairVoucherDetails",
                column: "RepairVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairVouchers_BranchId",
                table: "RepairVouchers",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairVouchers_CustomerId",
                table: "RepairVouchers",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairVouchers_EmployeeId",
                table: "RepairVouchers",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnDetails_ProductId",
                table: "ReturnDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnDetails_ReturnId",
                table: "ReturnDetails",
                column: "ReturnId");

            migrationBuilder.CreateIndex(
                name: "IX_Returns_BranchId",
                table: "Returns",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Returns_EmployeeId",
                table: "Returns",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Returns_OrderId",
                table: "Returns",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Returns_PurchaseOrderId",
                table: "Returns",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Shippings_CustomerId",
                table: "Shippings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Shippings_LiquidationVoucherId",
                table: "Shippings",
                column: "LiquidationVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_Shippings_OrderId",
                table: "Shippings",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Shippings_ShippingStatusId",
                table: "Shippings",
                column: "ShippingStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Shippings_TransporterId",
                table: "Shippings",
                column: "TransporterId");

            migrationBuilder.CreateIndex(
                name: "IX_StockBook_GoodsDeliveryNoteId",
                table: "StockBook",
                column: "GoodsDeliveryNoteId",
                unique: true,
                filter: "[GoodsDeliveryNoteId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StockBook_GoodsReceivedNoteId",
                table: "StockBook",
                column: "GoodsReceivedNoteId",
                unique: true,
                filter: "[GoodsReceivedNoteId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_WarehouseId",
                table: "Stocks",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_AppUserId",
                table: "Suppliers",
                column: "AppUserId",
                unique: true,
                filter: "[AppUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_EmployeeId",
                table: "Suppliers",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_BranchId",
                table: "Warehouses",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_EmployeeId",
                table: "Warehouses",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Warranties_CustomerId",
                table: "Warranties",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Warranties_EmployeeId",
                table: "Warranties",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Warranties_OrderId",
                table: "Warranties",
                column: "OrderId",
                unique: true,
                filter: "[OrderId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WarrantyDetails_ProductId",
                table: "WarrantyDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WarrantyDetails_WarrantyId",
                table: "WarrantyDetails",
                column: "WarrantyId");
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
                name: "CustomerPromotions");

            migrationBuilder.DropTable(
                name: "GoodsDeliveryNoteDetails");

            migrationBuilder.DropTable(
                name: "GoodsReceivedNoteDetails");

            migrationBuilder.DropTable(
                name: "InventoryVoucherDetails");

            migrationBuilder.DropTable(
                name: "LiquidationVoucherDetails");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "PaymentVouchers");

            migrationBuilder.DropTable(
                name: "ProductEvaluationReplies");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "PromotionDetails");

            migrationBuilder.DropTable(
                name: "PurchaseOrderDetails");

            migrationBuilder.DropTable(
                name: "ReceiptVouchers");

            migrationBuilder.DropTable(
                name: "RepairVoucherDetails");

            migrationBuilder.DropTable(
                name: "ReturnDetails");

            migrationBuilder.DropTable(
                name: "StockBook");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "WarrantyDetails");

            migrationBuilder.DropTable(
                name: "InventoryVouchers");

            migrationBuilder.DropTable(
                name: "ProductEvaluations");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "Shippings");

            migrationBuilder.DropTable(
                name: "GoodsDeliveryNotes");

            migrationBuilder.DropTable(
                name: "GoodsReceivedNotes");

            migrationBuilder.DropTable(
                name: "Warranties");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ShippingStatus");

            migrationBuilder.DropTable(
                name: "Transporters");

            migrationBuilder.DropTable(
                name: "LiquidationVouchers");

            migrationBuilder.DropTable(
                name: "RepairVouchers");

            migrationBuilder.DropTable(
                name: "Returns");

            migrationBuilder.DropTable(
                name: "StockActions");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "OrderStatuses");

            migrationBuilder.DropTable(
                name: "Promotions");

            migrationBuilder.DropTable(
                name: "PaymentStatuses");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "TransactionStatuses");

            migrationBuilder.DropTable(
                name: "CustomerTypes");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
