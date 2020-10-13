using Microsoft.AspNetCore.Mvc;

namespace eQACoLTD.AdminMvc.Controllers
{
    public class CustomerController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}