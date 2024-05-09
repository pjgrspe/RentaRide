using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentaRide.Models.Accounts;
using RentaRide.Models.Identity;
using RentaRide.Utilities;

namespace RentaRide.Controllers
{
    public class RegistrationController(UserManager<RentaRideAppUsers> RentaRideAppUsers, RoleManager<IdentityRole> roleManager) : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly UserManager<RentaRideAppUsers> _userManager = RentaRideAppUsers;
        private RentaRideAppUsers CreaterUser()
        {
            try
            {
                return Activator.CreateInstance<RentaRideAppUsers>();
            }
            catch
            {
                throw new InvalidOperationException($"Cannot create an instance of '{nameof(RentaRideAppUsers)}' ");
            }
        }

        public IActionResult Index()
        {
            if (!_roleManager.RoleExistsAsync(RoleUtilities.RoleSuperAdmin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(RoleUtilities.RoleSuperAdmin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(RoleUtilities.RoleAdmin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(RoleUtilities.RoleFrontDesk)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(RoleUtilities.RoleUser)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(RoleUtilities.RoleUnapprovedUser)).GetAwaiter().GetResult();
            }

            return View();
        }
        [HttpPost]
        public IActionResult Index(RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                var userReg = CreaterUser();
                userReg.UserName = model.regmodelUsername;
                userReg.Email = model.regmodelEmail;
                userReg.userFirstName = model.regmodelFirstName;
                userReg.userMiddleName = model.regmodelMiddleName;
                userReg.userLastName = model.regmodelLastName;
                userReg.userisApproved = false;

                var userResult = _userManager.CreateAsync(userReg, model.regmodelPassword).GetAwaiter().GetResult();
                if (userResult.Succeeded)
                {
                    _userManager.AddToRoleAsync(userReg, RoleUtilities.RoleUser).GetAwaiter().GetResult();
                    ViewBag.SuccessMessage = "User registered";
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    List<IdentityError> errorList = userResult.Errors.ToList();
                    var errors = string.Join(", ", errorList.Select(e => e.Description));
                    ViewBag.ErrorMessage = errors;
                }

            }
            return View(model);
        }
    }
}
