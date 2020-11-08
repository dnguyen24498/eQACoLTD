﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Dapper;
using eQACoLTD.Application.Common;
using eQACoLTD.Application.Configurations;
using eQACoLTD.Application.Extensions;
using eQACoLTD.Data.DBContext;
using eQACoLTD.Data.Entities;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.ListProduct.Handlers;
using eQACoLTD.ViewModel.Product.ListProduct.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NotImplementedException = System.NotImplementedException;

namespace eQACoLTD.Application.Product.ListProduct
{
    public class ListProductService:IListProductService
    {
        private readonly AppIdentityDbContext _context;
        private readonly IStorageService _storageService;
        private readonly IConfiguration _configuration;
        public ListProductService(AppIdentityDbContext context,IStorageService storageService,IConfiguration configuration)
        {
            _context = context;
            _storageService = storageService;
            _configuration = configuration;
        }
        public async Task<ApiResult<PagedResult<ProductsDto>>> GetProductsPagingAsync(int pageNumber, int pageSize)
        {
            var products = await(from p in _context.Products
                join pi in _context.ProductImages on p.Id equals pi.ProductId
                join c in _context.Categories on p.CategoryId equals c.Id
                join b in _context.Brands on p.BrandId equals b.Id
                orderby p.Id
                where pi.IsThumbnail == true && p.IsDelete==false
                select new ProductsDto()
                {
                    Id = p.Id,
                    BrandName = b.Name,
                    CategoryName = c.Name,
                    ProductName = p.Name,
                    ThumbnailPath = pi.Path,
                    AbleToSale = (from s in _context.Stocks
                        where s.ProductId == p.Id
                        select s.AbleToSale).Sum(),
                    RealQuantity = (from s in _context.Stocks
                        where s.ProductId == p.Id
                        select s.RealQuantity).Sum()
                }).GetPagedAsync(pageNumber,pageSize);
            if(products==null) return new ApiResult<PagedResult<ProductsDto>>(HttpStatusCode.NoContent);
            return new ApiResult<PagedResult<ProductsDto>>(HttpStatusCode.OK,products);
        }

        public async Task<ApiResult<ProductDto>> GetProductAsync(string productId)
        {
            var product = await (from p in _context.Products
                join b in _context.Brands on p.BrandId equals b.Id
                join c in _context.Categories on p.CategoryId equals c.Id
                join productimages in _context.ProductImages on p.Id equals productimages.ProductId
                    into ProductImageGroup
                    from pi in ProductImageGroup.DefaultIfEmpty()
                where p.Id==productId && p.IsDelete==false && pi.IsThumbnail==true
                select new ProductDto()
                {
                    Id = p.Id,
                    Description = p.Description,
                    OverView = p.OverView,
                    Name = p.Name,
                    Views = p.Views,
                    BrandName = b.Name,
                    CategoryName = c.Name,
                    RetailPrice = p.RetailPrice,
                    Stars = p.Stars,
                    WarrantyPeriod = p.WarrantyPeriod,
                    WholesalePrices = p.WholesalePrices,
                    Path = pi.Path,
                    AbleToSale = (from s in _context.Stocks 
                        where s.ProductId==p.Id && s.WarehouseId==GlobalProperties.MainWarehouseId 
                        select s.AbleToSale).SingleOrDefault(),
                    ListImage = (from pi in _context.ProductImages where pi.ProductId==p.Id
                        select new ProductImagesDto()
                        {
                            Id = pi.Id,
                            ImagePath = pi.Path,
                            IsThumbnail = pi.IsThumbnail
                        })
                }).SingleOrDefaultAsync();
            if(product==null) return new ApiResult<ProductDto>(HttpStatusCode.NotFound,$"Không tìm thấy sản phẩm có mã: {productId}");
            return new ApiResult<ProductDto>(HttpStatusCode.OK,product);
        }

        public async Task<ApiResult<string>> AddImageToProductAsync(string productId, IList<IFormFile> files)
        {
            try
            {
                var checkProduct = await _context.Products.FindAsync(productId);
                if(checkProduct==null) return new ApiResult<string>(HttpStatusCode.NotFound,$"Không tìm thấy sản phẩm có mã: {productId}");
                var checkThumbnailImg = await _context.ProductImages
                    .Where(x => x.ProductId == checkProduct.Id && x.IsThumbnail == true).SingleOrDefaultAsync();
                Guid imageId = Guid.Empty;
                string imagePath = "";
                for (int i = 0; i < files.Count; i++)
                {
                    imageId = Guid.NewGuid();
                    imagePath = await this.SaveFile(files[i], imageId);
                
                    await _context.ProductImages.AddAsync(new ProductImage()
                    {
                        Id = imageId.ToString("D"),
                        Path = imagePath,
                        IsThumbnail = checkThumbnailImg!=null?false:(i==0?true:false),
                        ProductId = productId
                    });
                    await _context.SaveChangesAsync();
                }
                return new ApiResult<string>(HttpStatusCode.OK){ResultObj = productId};
            }
            catch
            {
                return new ApiResult<string>(HttpStatusCode.InternalServerError,$"Có lỗi khi thêm ảnh sản phẩm");
            }
        }

        public async Task<ApiResult<string>> CreateProductAsync(ProductForCreationDto creationDto)
        {
            try
            {
                var sequenceNumber = await _context.Products.CountAsync();
                var productId = IdentifyGenerator.GenerateProductId(sequenceNumber + 1);
                var product = ObjectMapper.Mapper.Map<Data.Entities.Product>(creationDto);
                product.Id = productId;
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                return new ApiResult<string>(HttpStatusCode.OK){ResultObj = productId,Message = "Tạo mới sản phẩm thành công"};
            }
            catch
            {
                return new ApiResult<string>(HttpStatusCode.InternalServerError,$"Có lỗi khi thêm sản phẩm");
            }
        }

        public async Task<ApiResult<IEnumerable<ProductCardDto>>> GetRandom()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var products = await connection.QueryAsync<ProductCardDto>(
                    @"EXEC prGetRandomProduct");
                if(products==null) return new ApiResult<IEnumerable<ProductCardDto>>(HttpStatusCode.NoContent);
                return new ApiResult<IEnumerable<ProductCardDto>>(HttpStatusCode.OK,products);
            }
        }

        public async Task<ApiResult<IEnumerable<ProductCardDto>>> GetBestSellingInMonth()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var products = await connection.QueryAsync<ProductCardDto>(
                    @"EXEC prGetBestSellingProductInMonth");
                if(products==null) return new ApiResult<IEnumerable<ProductCardDto>>(HttpStatusCode.NoContent);
                return new ApiResult<IEnumerable<ProductCardDto>>(HttpStatusCode.OK,products);
            }
        }

        public async Task<ApiResult<IEnumerable<ProductCardDto>>> GetBestSelling()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var products = await connection.QueryAsync<ProductCardDto>(
                    @"EXEC prGetFeaturedProducts");
                if(products==null) return new ApiResult<IEnumerable<ProductCardDto>>(HttpStatusCode.NoContent);
                return new ApiResult<IEnumerable<ProductCardDto>>(HttpStatusCode.OK,products);
            }
        }

        public async Task<ApiResult<IEnumerable<ProductCardDto>>> GetNewArrived()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var products = await connection.QueryAsync<ProductCardDto>(
                    @"EXEC prGetNewArrivedProducts");
                if(products==null) return new ApiResult<IEnumerable<ProductCardDto>>(HttpStatusCode.NoContent);
                return new ApiResult<IEnumerable<ProductCardDto>>(HttpStatusCode.OK,products);
            }
        }

        public async Task<ApiResult<IEnumerable<ProductCardDto>>> TopView()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var products = await connection.QueryAsync<ProductCardDto>(
                    @"EXEC prGetProductsTopView");
                if(products==null) return new ApiResult<IEnumerable<ProductCardDto>>(HttpStatusCode.NoContent);
                return new ApiResult<IEnumerable<ProductCardDto>>(HttpStatusCode.OK,products);
            }
        }

        public async Task<ApiResult<IEnumerable<ProductCardDto>>> TopRate()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var products = await connection.QueryAsync<ProductCardDto>(
                    @"EXEC prGetProductsTopRate");
                if(products==null) return new ApiResult<IEnumerable<ProductCardDto>>(HttpStatusCode.NoContent);
                return new ApiResult<IEnumerable<ProductCardDto>>(HttpStatusCode.OK,products);
            }
        }

        public async Task<ApiResult<PagedResult<ProductCardDto>>> SearchProductsByCategory(string categoryId, string searchValue,
            int pageNumber,int pageSize)
        {
            var products = await (from p in _context.Products
                join category in _context.Categories on p.CategoryId equals category.Id
                    into CategoryGroup
                from c in CategoryGroup.DefaultIfEmpty()
                join brand in _context.Brands on p.BrandId equals brand.Id
                    into BrandGroup
                from b in BrandGroup.DefaultIfEmpty()
                join productImage in _context.ProductImages on p.Id equals productImage.ProductId
                    into ProductImageGroup
                from pi in ProductImageGroup.DefaultIfEmpty()
                where p.CategoryId.Contains(!string.IsNullOrEmpty(categoryId)? categoryId:"") 
                    && p.IsDelete == false && pi.IsThumbnail == true && p.Name.ToLower().Contains(
                    !string.IsNullOrEmpty(searchValue)? searchValue.ToLower():"")
                select new ProductCardDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Stars = p.Stars,
                    Views = p.Views,
                    BrandName = b.Name,
                    CategoryName = c.Name,
                    ImagePath = pi.Path,
                    RetailPrice = p.RetailPrice,
                    AbleToSale=_context.Stocks.Where(x=>x.ProductId==p.Id && 
                        x.WarehouseId==GlobalProperties.MainWarehouseId).SingleOrDefault().AbleToSale
                }).GetPagedAsync(pageNumber, pageSize);
            return new ApiResult<PagedResult<ProductCardDto>>(HttpStatusCode.OK,products);
        }

        public async Task<ApiResult<PagedResult<ProductCardDto>>> FilterProductsByCategoryAsync(string categoryId, string brandId, decimal minimumPrice, 
            decimal maximumPrice,int pageNumber, int pageSize)
        {
            if(minimumPrice>maximumPrice) return new ApiResult<PagedResult<ProductCardDto>>
                    (HttpStatusCode.BadRequest,"Giá tối thiểu không được lớn hơn giá tối đa.");
            var products = await (from p in _context.Products
                    join brand in _context.Brands on p.BrandId equals brand.Id
                        into BrandGroup
                    from b in BrandGroup.DefaultIfEmpty()
                    join category in _context.Categories on p.CategoryId equals category.Id
                        into CategoryGroup
                    from c in CategoryGroup.DefaultIfEmpty()
                    join productImage in _context.ProductImages on p.Id equals productImage.ProductId
                        into ProductImageGroup
                    from pi in ProductImageGroup.DefaultIfEmpty()
                    where p.CategoryId.ToLower().Contains(!string.IsNullOrEmpty(categoryId) ? categoryId : "")
                          && p.RetailPrice >= minimumPrice && p.RetailPrice <= maximumPrice && b.Id.ToLower()
                              .Contains(!string.IsNullOrEmpty(brandId) ? brandId : "")
                          && pi.IsThumbnail == true
                    select new ProductCardDto()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Stars = p.Stars,
                        Views = p.Views,
                        BrandName = b.Name,
                        CategoryName = c.Name,
                        ImagePath = pi.Path,
                        RetailPrice = p.RetailPrice,
                        AbleToSale = _context.Stocks.Where(x => x.ProductId == p.Id &&
                          x.WarehouseId == GlobalProperties.MainWarehouseId).SingleOrDefault().AbleToSale
                    }
                ).GetPagedAsync(pageNumber, pageSize);
            return new ApiResult<PagedResult<ProductCardDto>>(HttpStatusCode.OK,products);
        }

        private async Task<string> SaveFile(IFormFile file,Guid guid)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{guid}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}