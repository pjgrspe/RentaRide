using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RentaRide.Database;
using RentaRide.Models.ViewModels;

namespace RentaRide.Controllers
{
    public class AdminController(ILogger<AdminController> logger, RARdbContext rardbContext) : Controller
    {
        private readonly ILogger<AdminController> _logger = logger;
        private readonly RARdbContext _rardbContext = rardbContext;

        //public AdminController(ILogger<AdminController> logger)
        //{
        //    _logger = logger;
        //}

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> LoadPartial(string menuName)
        {
            if (string.IsNullOrEmpty(menuName))
            {
                return BadRequest("Partial name is required.");
            }

            List<UsersViewModel> userViewModels = null;

            try
            {
                //Model logic for Application menu name
                if (menuName == "Applications")
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
                        userVMLicense = imgNullCheck(user.userLicense) /*+ user.userLicenseFileExt*/,
                        userVM2ndValidID = imgNullCheck(user.user2ndValidID) /*+ user.user2ndValidIDFileExt*/,
                        userVMProofofBilling = imgNullCheck(user.userProofofBilling)/* + user.userProofofBillingFileExt*/,
                        userVMSelfieProof = imgNullCheck(user.userSelfieProof) /*+ user.userSelfieProofFileExt*/
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
