using System.Threading.Tasks;
using eQACoLTD.AdminMvc.Services;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Customer.Queries;
using Microsoft.AspNetCore.Mvc;
namespace eQACoLTD.AdminMvc.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerAPIService _customerApiService;

        public CustomerController(ICustomerAPIService customerApiService)
        {
            _customerApiService = customerApiService;
        }
        // GET
        public async Task<IActionResult> Index(int page = 1,int size=15)
        {
            var result = await _customerApiService.GetCustomersPagingAsync(page,size);
            if (result.Code!=System.Net.HttpStatusCode.OK) return View(new PagedResult<CustomersDto>());
            return View(result.ResultObj);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}