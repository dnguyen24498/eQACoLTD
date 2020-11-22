using System.Net;
using System.Threading.Tasks;
using eQACoLTD.AdminMvc.Services;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.Category.Queries;
using Microsoft.AspNetCore.Mvc;

namespace eQACoLTD.AdminMvc.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryAPIService _categoryService;

        public CategoryController(ICategoryAPIService categoryService)
        {
            _categoryService = categoryService;
        }
        // GET
        public async Task<IActionResult> Index(int page = 1,int size=15)
        {
            var result = await _categoryService.GetCategoryPagingAsync(page,size);
            if (result.Code == HttpStatusCode.Forbidden) return View("403");
            if (result.Code!=System.Net.HttpStatusCode.OK) return View(new PagedResult<CategoriesDto>());
            else return View(result.ResultObj);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}