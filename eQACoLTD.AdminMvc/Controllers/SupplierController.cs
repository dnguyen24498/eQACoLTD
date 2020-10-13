using Microsoft.AspNetCore.Mvc;

namespace eQACoLTD.AdminMvc.Controllers
{
    public class SupplierController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}