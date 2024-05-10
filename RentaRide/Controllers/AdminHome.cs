using Microsoft.AspNetCore.Mvc;

namespace RentaRide.Controllers
{
    public class AdminHome : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
