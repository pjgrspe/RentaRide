using Microsoft.AspNetCore.Mvc;

namespace RentaRide.Controllers
{
    public class RegistrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
