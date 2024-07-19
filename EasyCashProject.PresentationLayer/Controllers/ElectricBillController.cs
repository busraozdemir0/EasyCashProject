using Microsoft.AspNetCore.Mvc;

namespace EasyCashProject.PresentationLayer.Controllers
{
    public class ElectricBillController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
