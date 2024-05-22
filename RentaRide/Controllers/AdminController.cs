using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RentaRide.Database;
using RentaRide.Database.Database_Models;
using RentaRide.Models.Accounts;
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
        private readonly IFileServices _fileServices;

        public AdminController(ILogger<AdminController> logger, RARdbContext rardbContext, IUserServices userServices, IFileServices fileServices)
        {
            _logger = logger;
            _rardbContext = rardbContext;
            _userServices = userServices;
            _fileServices = fileServices;
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
        public async Task<IActionResult> AddNewDriver(AddDriverModel model)
        {
            if (ModelState.IsValid)
            {
                var driverAdd = new DriversDBModel
                {
                    driverPicture = "Processing..",
                    driverPictureExt = "Processing..",
                    driverLicense = "Processing..",
                    driverLicenseExt = "Processing..",
                    driverLicenseBack = "Processing..",
                    driverLicenseBackExt = "Processing..",
                    driverFirstName = model.drivmodelFirstName,
                    driverMiddleName = model.drivmodelMiddleName,
                    driverLastName = model.drivmodelLastName,
                    driverContact = model.drivmodelContact,
                    driverEmail = model.drivmodelEmail,
                    driverRegisteredDate = DateTime.Now,
                    driverOnDuty = false,
                    driverIsActive = true
                };
                _rardbContext.TBL_Drivers.Add(driverAdd);
                _rardbContext.SaveChanges();
                var driverPictureImgUpload = _fileServices.ProcessUploadedFile(model.drivmodelImage, ImageCategories.imgProfile, driverAdd.driverID.ToString());
                var driverLicenseImgUpload = _fileServices.ProcessUploadedFile(model.drivmodelLicense, ImageCategories.imgLicense, driverAdd.driverID.ToString());
                var driverLicenseBackImgUpload = _fileServices.ProcessUploadedFile(model.drivmodelLicenseBack, ImageCategories.imgLicenseBack, driverAdd.driverID.ToString());

                var driverPictureFileExt = _fileServices.GetFileExtension(model.drivmodelImage);
                var driverLicenseFileExt = _fileServices.GetFileExtension(model.drivmodelLicense);
                var driverLicenseBackFileExt = _fileServices.GetFileExtension(model.drivmodelLicenseBack);
                
                var driverToUpdate = _rardbContext.TBL_Drivers.Find(driverAdd.driverID);
                driverToUpdate!.driverPicture = driverPictureImgUpload!;
                driverToUpdate!.driverPictureExt = driverPictureFileExt!;
                driverToUpdate!.driverLicense = driverLicenseImgUpload!;
                driverToUpdate!.driverLicenseExt = driverLicenseFileExt!;
                driverToUpdate!.driverLicenseBack = driverLicenseBackImgUpload!;
                driverToUpdate!.driverLicenseBackExt = driverLicenseBackFileExt!;
                
                await _rardbContext.SaveChangesAsync();
            }
            else
            {
                ViewBag.ErrorMessage = "An error occureed with adding driver";
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> LoadPartial(string menuName)
        {
            if (string.IsNullOrEmpty(menuName))
            {
                return BadRequest("Partial name is required.");
            }

            var adminPartialModel = new AdminPartialViewModel();
            try
            {
                //Model logic for Application menu name
                if (menuName == "Users")
                {
                    var users = await _rardbContext.TBL_UserDetails
                                      .Include(u => u.RentaRideAppUsers)
                                      .ToListAsync();


                    adminPartialModel.Users = users.Select(user => new UsersViewModel
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
                }else if (menuName == "Drivers")
                {
                    var drivers = await _rardbContext.TBL_Drivers.ToListAsync();
                    adminPartialModel.Drivers = drivers.Select(driver => new DriversViewModel
                    {
                        driverVMID = driver.driverID,
                        driverVMFirstName = driver.driverFirstName,
                        driverVMMiddleName = driver.driverMiddleName,
                        driverVMLastName = driver.driverLastName,
                        driverVMEmail = driver.driverEmail,
                        driverVMContact = driver.driverContact,
                        driverVMImage = driver.driverPicture,
                        driverVMLicense = driver.driverLicense,
                        driverVMLicenseBack = driver.driverLicenseBack,
                        driverVMDateCreated = driver.driverRegisteredDate,
                        driverVMDateLastDutyDate = driver.driverLastDutyDate,
                        driverVMOnDuty = driver.driverOnDuty,
                        driverVMIsActive = driver.driverIsActive
                    }).ToList();
                }

                return PartialView($"~/Views/Admin/Menu/{menuName}.cshtml", adminPartialModel);
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
