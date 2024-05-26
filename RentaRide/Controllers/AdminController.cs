using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public AdminController(ILogger<AdminController> logger, RARdbContext rardbContext, IUserServices userServices, IFileServices fileServices, IConfiguration configuration, IWebHostEnvironment environment)
        {
            _logger = logger;
            _rardbContext = rardbContext;
            _userServices = userServices;
            _fileServices = fileServices;
            _configuration = configuration;
            _environment = environment;
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

        [HttpPost]
        public async Task<IActionResult> AddNewDriver([FromForm] IFormCollection form)
        {
            if (ModelState.IsValid)
            {
                var model = new AdminPartialViewModel
                {
                    AddDriver = new DriverAddModel
                    {
                        drivmodelFirstName = form["drivmodelFirstName"],
                        drivmodelMiddleName = form["drivmodelMiddleName"],
                        drivmodelLastName = form["drivmodelLastName"],
                        drivmodelEmail = form["drivmodelEmail"],
                        drivmodelContact = form["drivmodelContact"],
                        drivmodelImage = form.Files["drivmodelImage"],
                        drivmodelLicense = form.Files["drivmodelLicense"],
                        drivmodelLicenseBack = form.Files["drivmodelLicenseBack"]
                    }
                };

                var driverAdd = new DriversDBModel
                {
                    driverPicture = "Processing..",
                    driverPictureExt = "Processing..",
                    driverLicense = "Processing..",
                    driverLicenseExt = "Processing..",
                    driverLicenseBack = "Processing..",
                    driverLicenseBackExt = "Processing..",
                    driverFirstName = model.AddDriver.drivmodelFirstName,
                    driverMiddleName = model.AddDriver.drivmodelMiddleName,
                    driverLastName = model.AddDriver.drivmodelLastName,
                    driverContact = model.AddDriver.drivmodelContact,
                    driverEmail = model.AddDriver.drivmodelEmail,
                    driverRegisteredDate = DateTime.Now,
                    driverOnDuty = false,
                    driverIsActive = true
                };
                _rardbContext.TBL_Drivers.Add(driverAdd);
                _rardbContext.SaveChanges();
                var driverPictureImgUpload = _fileServices.ProcessEncryptUploadedFile(model.AddDriver.drivmodelImage, ImageCategories.imgProfile);
                var driverLicenseImgUpload = _fileServices.ProcessEncryptUploadedFile(model.AddDriver.drivmodelLicense, ImageCategories.imgLicense);
                var driverLicenseBackImgUpload = _fileServices.ProcessEncryptUploadedFile(model.AddDriver.drivmodelLicenseBack, ImageCategories.imgLicenseBack);

                var driverPictureFileExt = _fileServices.GetFileExtension(model.AddDriver.drivmodelImage);
                var driverLicenseFileExt = _fileServices.GetFileExtension(model.AddDriver.drivmodelLicense);
                var driverLicenseBackFileExt = _fileServices.GetFileExtension(model.AddDriver.drivmodelLicenseBack);

                var driverToUpdate = _rardbContext.TBL_Drivers.Find(driverAdd.driverID);
                driverToUpdate!.driverPicture = driverPictureImgUpload!;
                driverToUpdate!.driverPictureExt = driverPictureFileExt!;
                driverToUpdate!.driverLicense = driverLicenseImgUpload!;
                driverToUpdate!.driverLicenseExt = driverLicenseFileExt!;
                driverToUpdate!.driverLicenseBack = driverLicenseBackImgUpload!;
                driverToUpdate!.driverLicenseBackExt = driverLicenseBackFileExt!;

                await _rardbContext.SaveChangesAsync();
                return new JsonResult(new { success = true });
            }
            else
            {
                ViewBag.ErrorMessage = "An error occureed with adding driver";
                return new JsonResult(new { success = false, message = "An error occurred with adding driver" });

            }
        }

        [HttpPost]
        public async Task<IActionResult> EditDriver([FromForm] IFormCollection form)
        {
            if (ModelState.IsValid)
            {

                var model = new AdminPartialViewModel
                {
                    EditDriver = new DriverEditModel
                    {
                        driveditmodelID = Int32.Parse(form["driveditmodelID"]),
                        driveditmodelFirstName = form["driveditmodelFirstName"],
                        driveditmodelMiddleName = form["driveditmodelMiddleName"],
                        driveditmodelLastName = form["driveditmodelLastName"],
                        driveditmodelEmail = form["driveditmodelEmail"],
                        driveditmodelContact = form["driveditmodelContact"],
                        driveditmodelStatus = form["driveditmodelStatus"],
                        driveditmodelImage = form.Files["driveditmodelImage"],
                        driveditmodelLicense = form.Files["driveditmodelLicense"],
                        driveditmodelLicenseBack = form.Files["driveditmodelLicenseBack"]
                    }
                };

                var driveID = Int32.Parse(form["driveditmodelID"]);
                var driver = _rardbContext.TBL_Drivers.Find(driveID);
                driver!.driverFirstName = model.EditDriver.driveditmodelFirstName!;
                driver!.driverMiddleName = model.EditDriver.driveditmodelMiddleName!;
                driver!.driverLastName = model.EditDriver.driveditmodelLastName!;
                driver!.driverContact = model.EditDriver.driveditmodelEmail!;
                driver!.driverEmail = model.EditDriver.driveditmodelContact!;

                if (model.EditDriver.driveditmodelImage != null)
                {
                    var driverPictureImgUpload = _fileServices.ProcessEncryptUploadedFile(model.EditDriver.driveditmodelImage, ImageCategories.imgProfile);
                    var driverPictureFileExt = _fileServices.GetFileExtension(model.EditDriver.driveditmodelImage);
                    driver!.driverPicture = driverPictureImgUpload!;
                    driver!.driverPictureExt = driverPictureFileExt!;
                }
                if (model.EditDriver.driveditmodelLicense != null)
                {
                    var driverLicenseImgUpload = _fileServices.ProcessEncryptUploadedFile(model.EditDriver.driveditmodelLicense, ImageCategories.imgLicense);
                    var driverLicenseFileExt = _fileServices.GetFileExtension(model.EditDriver.driveditmodelLicense);
                    driver!.driverLicense = driverLicenseImgUpload!;
                    driver!.driverLicenseExt = driverLicenseFileExt!;
                }
                if (model.EditDriver.driveditmodelLicenseBack != null)
                {
                    var driverLicenseBackImgUpload = _fileServices.ProcessEncryptUploadedFile(model.EditDriver.driveditmodelLicenseBack, ImageCategories.imgLicenseBack);
                    var driverLicenseBackFileExt = _fileServices.GetFileExtension(model.EditDriver.driveditmodelLicenseBack);
                    driver!.driverLicenseBack = driverLicenseBackImgUpload!;
                    driver!.driverLicenseBackExt = driverLicenseBackFileExt!;
                }

                if (model.EditDriver.driveditmodelStatus == "active")
                {
                    driver!.driverIsActive = true;
                    driver!.driverOnDuty = false;
                }
                else if (model.EditDriver.driveditmodelStatus == "onduty")
                {
                    driver!.driverIsActive = true;
                    driver!.driverOnDuty = true;
                }
                else
                {
                    driver!.driverIsActive = false;
                    driver!.driverOnDuty = false;
                }

                await _rardbContext.SaveChangesAsync();
                return new JsonResult(new { success = true });
            }
            else
            {
                ViewBag.ErrorMessage = "An error occureed with editing driver";
                return new JsonResult(new { success = false, message = "An error occurred with editing driver" });

            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDriver([FromForm] IFormCollection form)
        { 
            if (ModelState.IsValid)
            {

                var driverID = form["drivdelmodelID"];
                var driveIDstring = Int32.Parse(driverID!);
                var driver = _rardbContext.TBL_Drivers.Find(driveIDstring);
                driver!.driverIsDeleted = true;

                await _rardbContext.SaveChangesAsync();
                return new JsonResult(new { success = true });
            }
            else
            {
                ViewBag.ErrorMessage = "An error occureed with deleting driver";
                return new JsonResult(new { success = false, message = "An error occurred with deleting driver" });

            }
        }
        [HttpPost]
        public async Task<IActionResult> UserVerify([FromForm] IFormCollection form)
        {
            if (ModelState.IsValid)
            {

                var userID = form["userverID"];
                var user = _rardbContext.Users.Find(userID);
                string? Verified = form["userverIsVerified"];
                bool? isVerified = null;

                if (Verified != null)
                {
                    bool temp;
                    if (bool.TryParse(Verified, out temp))
                    {
                        isVerified = temp;
                    }
                }
                user!.userisApproved = isVerified;

                await _rardbContext.SaveChangesAsync();
                return new JsonResult(new { success = true });
            }
            else
            {
                ViewBag.ErrorMessage = "An error occureed with deleting driver";
                return new JsonResult(new { success = false, message = "An error occurred with deleting driver" });

            }
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
                        userVMLicenseExt = user.userLicenseFileExt,
                        userVMLicenseBackExt = user.userLicenseBackFileExt,
                        userVM2ndValidIDExt = user.user2ndValidIDFileExt,
                        userVMProofofBillingExt = user.userProofofBillingFileExt,
                        userVMSelfieProofExt = user.userSelfieProofFileExt,

                        userVMLicenseIMG = imgNullCheck(user.userLicense, ImageCategories.imgLicense),
                        userVMLicenseBackIMG = imgNullCheck(user.userLicenseBack, ImageCategories.imgLicenseBack),
                        userVM2ndValidIDIMG = imgNullCheck(user.user2ndValidID, ImageCategories.img2ndID),
                        userVMProofofBillingIMG = imgNullCheck(user.userProofofBilling, ImageCategories.imgPOB),
                        userVMSelfieProofIMG = imgNullCheck(user.userSelfieProof, ImageCategories.imgSelfie)
                    }).ToList();

                }else if (menuName == "Drivers")
                {
                    var drivers = await _rardbContext.TBL_Drivers
                                                     .Where(driver => driver.driverIsDeleted == false)
                                                     .ToListAsync();
                    adminPartialModel.Drivers = drivers.Select(driver => new DriversViewModel
                    {
                        driverVMID = driver.driverID,
                        driverVMFirstName = driver.driverFirstName,
                        driverVMMiddleName = driver.driverMiddleName,
                        driverVMLastName = driver.driverLastName,
                        driverVMEmail = driver.driverEmail,
                        driverVMContact = driver.driverContact,
                        driverVMImageIMG = imgNullCheck(driver.driverPicture, ImageCategories.imgProfile),
                        driverVMLicenseIMG = imgNullCheck(driver.driverLicense, ImageCategories.imgLicense),
                        driverVMLicenseBackIMG = imgNullCheck(driver.driverLicenseBack, ImageCategories.imgLicenseBack),
                        driverVMImageExt = driver.driverPictureExt,
                        driverVMLicenseExt = driver.driverLicenseExt,
                        driverVMLicenseBackExt = driver.driverLicenseBackExt,
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
        [NonAction]
        private string imgNullCheck(string? img, string imgCategory)
        {
            var key = _configuration["ImageEncryption:ImageKey"];
            var iv = _configuration["ImageEncryption:ImageIV"];
            string UploadFolder = Path.Combine(FileLoc.FileUploadFolder, imgCategory);
            string path = Path.Combine(_environment.WebRootPath, UploadFolder);
            if (img == null)
            {
                img = "Default.png";
            }
            var imageBytes = ImageUtilities.ProcessDecodeImage(img,path,key!,iv!);
            var base64Image = Convert.ToBase64String(imageBytes);
            var filePath = base64Image;
            return filePath;
        }

    }
}
