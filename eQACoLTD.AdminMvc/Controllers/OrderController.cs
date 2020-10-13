using Microsoft.AspNetCore.Mvc;

namespace eQACoLTD.AdminMvc.Controllers
{
    public class OrderController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}