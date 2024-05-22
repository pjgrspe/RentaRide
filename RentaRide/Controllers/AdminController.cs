using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RentaRide.Database;
using RentaRide.Models.ViewModels;
using RentaRide.Services;
using RentaRide.Utilities;

namespace RentaRide.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly RARdbContext _rardbContext; 
        private readonly IUserServices _userServices;

        public AdminController(ILogger<AdminController> logger, RARdbContext rardbContext, IUserServices userServices)
        {
            _logger = logger;
            _rardbContext = rardbContext;
            _userServices = userServices;
        }

        public IActionResult Index()
        {
            //Uncomment when frontend is ready
            //if (_userServices.IsUserLoggedIn(User))
            //{
            //    var roleClaim = User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.Role);
            //    if (roleClaim!.Value == RoleUtilities.RoleUser)
            //    {
            //        return RedirectToAction("Index", "Customer");
            //    }
            //    else
            //    {
            //        return View();
            //    }
            //}
            //return RedirectToAction("Index", "Login");
            return View();

        }

        public async Task<IActionResult> LoadPartial(string menuName)
        {
            if (string.IsNullOrEmpty(menuName))
            {
                return BadRequest("Partial name is required.");
            }

            List<UsersViewModel>? userViewModels = null;

            try
            {
                //Model logic for Application menu name
                if (menuName == "Users")
                {
                    var users = await _rardbContext.TBL_UserDetails
                                      .Include(u => u.RentaRideAppUsers)
                                      .ToListAsync();


                    userViewModels = users.Select(user => new UsersViewModel
                    {
                        userVMID = user.RentaRideAppUsers.Id,
                        userVMFirstName = user.RentaRideAppUsers.userFirstName,
                        userVMMiddleName = user.RentaRideAppUsers.userMiddleName,
                        userVMLastName = user.RentaRideAppUsers.userLastName,
                        userVMEmail = user.RentaRideAppUsers.Email,
                        userVMisApproved = user.RentaRideAppUsers.userisApproved,
                        userVMisActive = user.RentaRideAppUsers.userisActive,
                        userVMDetailID = user.userDetailID,
                        userVMDateCreated = user.userDateCreated,
                        userVMDateLastModified = user.userDateLastModified,
                        userVMDateModified = user.userDateModified,
                        userVMDOB = user.userDOB,
                        userVMstreetAdd = user.userStreetAdd,
                        userVMCityAdd = user.userCityAdd,
                        userVMProvinceAdd = user.userProvinceAdd,
                        userVMContact = user.userContact,
                        userVMLicense = imgNullCheck(user.userLicense),
                        userVMLicenseBack = imgNullCheck(user.userLicenseBack),
                        userVM2ndValidID = imgNullCheck(user.user2ndValidID),
                        userVMProofofBilling = imgNullCheck(user.userProofofBilling),
                        userVMSelfieProof = imgNullCheck(user.userSelfieProof)
                    }).ToList();
                }

                return PartialView($"~/Views/Admin/Menu/{menuName}.cshtml", userViewModels);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading partial view: {menuName}", menuName);
                return NotFound("Partial view not found.");
            }
        }
        [HttpPost]
        public IActionResult UpdateUserValidation(string userId, bool isApproved)
        {
            var user = _rardbContext.Users.Find(userId);
            if (user != null)
            {
                user.userisApproved = isApproved;
                _rardbContext.SaveChanges();
            }

            return RedirectToAction("Index");
            //return View();
        }

        [NonAction]
        private static string imgNullCheck(string? img)
        {
            string filePath = "Default.png";
            if (img != null)
            {

                 filePath = img;
            }

            return filePath;
        }

    }
}
