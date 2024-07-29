using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyCashProject.PresentationLayer.Controllers
{
    [Authorize]
    public class ElectricBillController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
