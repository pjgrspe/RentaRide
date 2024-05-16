using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentaRide.Database;
using RentaRide.Models.Accounts;
using RentaRide.Models.Identity;
using RentaRide.Utilities;
using System.Net.Mail;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RentaRide.Controllers
{
    public class LoginController(SignInManager<RentaRideAppUsers> signinManager, RARdbContext rardbContext) : Controller
    {
        private readonly SignInManager<RentaRideAppUsers> _signInManager = signinManager;
        private readonly RARdbContext _rardbContext = rardbContext;
        public IActionResult Index()
        {
            return View();
        }
        public bool IsEmail(string emailadd) 
        {
            try 
            {
                MailAddress m = new MailAddress(emailadd);
                return true;
            }
            catch (FormatException) 
            { 
            return false;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var uName = model.loginmodelUsername;

                if (IsEmail(model.loginmodelUsername))
                {
                    var uData = _rardbContext.Users.FirstOrDefault(x => x.Email == model.loginmodelUsername);
                    if (uData != null)
                    {
                        uName = uData.UserName;
                    }

                }
                var res = _signInManager.PasswordSignInAsync(uName!, model.loginmodelPassword, model.loginmodelRememberMe, false).GetAwaiter().GetResult();

                if (res.Succeeded)
                {
                    var roleClaim = User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.Role);
                    if (roleClaim!.Value == RoleUtilities.RoleUser)
                    {
                        return RedirectToAction("Index", "Customer");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Incorrect Email/Username/Password";
                }


            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Logout()
        {
            try
            {
                _signInManager.SignOutAsync().GetAwaiter().GetResult();
                return RedirectToAction("Index", "Login");

            }
            catch(Exception ex)
            {

                return RedirectToAction("Index", "Login");
            }
        }
    }
}
