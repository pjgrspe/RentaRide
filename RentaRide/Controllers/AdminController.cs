using Microsoft.AspNetCore.Mvc;

namespace RentaRide.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
