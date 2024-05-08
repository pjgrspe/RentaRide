using Microsoft.AspNetCore.Mvc;

namespace RentaRide.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
