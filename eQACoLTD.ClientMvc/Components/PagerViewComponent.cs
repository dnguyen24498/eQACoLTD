using System.Threading.Tasks;
using eQACoLTD.ViewModel.Common;
using Microsoft.AspNetCore.Mvc;

namespace eQACoLTD.ClientMvc.Components
{
    public class PagerViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(PagedResultBase result)
        {
            result.LinkTemplate = Url.Action(RouteData.Values["action"].ToString(), new { page = "{0}" });
            return View("Default", result);
        }
    }
}