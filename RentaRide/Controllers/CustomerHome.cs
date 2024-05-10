using Microsoft.AspNetCore.Mvc;

namespace RentaRide.Controllers
{
    public class CustomerHome : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
