using Microsoft.AspNetCore.Mvc;

namespace RentaRide.Controllers
{
    public class AccountStatusController : Controller
    {
        public IActionResult Denied()
        {
            return View();
        }

        public IActionResult UnderReview()
        {
            return View();
        }
    }
}
