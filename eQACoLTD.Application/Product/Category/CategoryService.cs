using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using eQACoLTD.Application.Configurations;
using eQACoLTD.Application.Extensions;
using eQACoLTD.Data.DBContext;
using eQACoLTD.Utilities.Extensions;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.Category.Handlers;
using eQACoLTD.ViewModel.Product.Category.Queries;
using eQACoLTD.ViewModel.Product.ListProduct.Queries;
using Microsoft.EntityFrameworkCore;

namespace eQACoLTD.Application.Product.Category
{
    public class CategoryService:ICategoryService
    {
        private readonly AppIdentityDbContext _context;
        private ILoggerManager _logger;

        public CategoryService(AppIdentityDbContext context,ILoggerManager logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ApiResult<string>> CreateCategoryAsync(CategoryForCreationDto categoryDto)
        {
            try
            {
                if (categoryDto != null)
                {
                    var newId = Guid.NewGuid().ToString("D");
                    var newCategory = ObjectMapper.Mapper.Map<CategoryForCreationDto, eQACoLTD.Data.Entities.Category>(categoryDto);
                    newCategory.Id = newId;
                    await _context.Categories.AddAsync(newCategory);
                    await _context.SaveChangesAsync();
                    return new ApiResult<string>(HttpStatusCode.OK) { ResultObj=newId,Message = "Tạo danh mục thành công"};
                }
                return new ApiResult<string>(HttpStatusCode.BadRequest);
            }
            catch
            {
                return new ApiResult<string>(HttpStatusCode.InternalServerError, "Có lỗi khi thêm danh mục");
            }
        }

        public async Task<ApiResult<string>> DeleteCategoryAsync(string categoryId)
        {
            var checkCategory = await _context.Categories.FindAsync(categoryId);
            if (checkCategory == null) return new ApiResult<string>(HttpStatusCode.BadRequest, $"Không tìm thấy danh mục có mã: {categoryId}");
            _context.Categories.Remove(checkCategory);
            await _context.SaveChangesAsync();
            return new ApiResult<string>(HttpStatusCode.OK) {ResultObj=categoryId,Message = "Đã xóa danh mục"};
        }

        public async Task<ApiResult<PagedResult<ProductCardDto>>> GetProductsByCategoryPagingAsync(string categoryId,int pageIndex,int pageSize)
        {
            var products = await (from p in _context.Products
                join c in _context.Categories on p.CategoryId equals c.Id
                join brand in _context.Brands on p.BrandId equals brand.Id
                    into BrandGroup
                from b in BrandGroup.DefaultIfEmpty()
                join productImage in _context.ProductImages on p.Id equals productImage.ProductId
                    into ProductImageGroup
                from pi in ProductImageGroup.DefaultIfEmpty()
                orderby p.Id
                where c.Id.Contains(categoryId=="all"?"":categoryId) && pi.IsThumbnail == true
                select new ProductCardDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Stars = p.Stars,
                    Views = p.Views,
                    BrandName = b.Name,
                    CategoryName = c.Name,
                    ImagePath = pi.Path,
                    RetailPrice = p.RetailPrice
                }).GetPagedAsync(pageIndex, pageSize);
            return new ApiResult<PagedResult<ProductCardDto>>(HttpStatusCode.OK,products);
        }

        public async Task<ApiResult<IEnumerable<CategoryDto>>> GetCategoriesForHomePageAsync()
        {
            var categories = await (from c in _context.Categories
                             select new CategoryDto()
                             {
                                 Id = c.Id,
                                 Name = c.Name
                             }).ToListAsync();
            return new ApiResult<IEnumerable<CategoryDto>>(HttpStatusCode.OK,categories);
        }

        public async Task<ApiResult<PagedResult<CategoriesDto>>> GetCategoriesPagingAsync(int pageIndex, int pageSize)
        {
            var categories = await (from c in _context.Categories
                    let cCount=(from p in _context.Products
                                where p.CategoryId==c.Id select p).Count()
                    select new CategoriesDto(){
                        Id=c.Id,
                        Name=c.Name,
                        NumbProduct= cCount
                    }
                ).GetPagedAsync(pageIndex, pageSize);
            if(categories==null) return new ApiResult<PagedResult<CategoriesDto>>(HttpStatusCode.NoContent);
                return new ApiResult<PagedResult<CategoriesDto>>(HttpStatusCode.OK,categories);
            
        }

        public async Task<ApiResult<CategoryDto>> GetCategoryAsync(string categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null) 
                return new ApiResult<CategoryDto>(HttpStatusCode.BadRequest, $"Danh mục có mã: {categoryId} không tồn tại");
            return new ApiResult<CategoryDto>(HttpStatusCode.OK, ObjectMapper.Mapper.Map<eQACoLTD.Data.Entities.Category,CategoryDto>(category));
        }
        
    }
}