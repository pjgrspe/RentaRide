using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentaRide.Database;
using RentaRide.Database.Database_Models;
using RentaRide.Models.Accounts;
using RentaRide.Models.Identity;
using RentaRide.Services;
using RentaRide.Utilities;
using System;
using System.IO;

namespace RentaRide.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly SignInManager<RentaRideAppUsers> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<RentaRideAppUsers> _userManager;
        private readonly RARdbContext _rardbContext;
        private readonly IWebHostEnvironment _environment;
        private readonly IFileServices _fileServices;

        public RegistrationController(UserManager<RentaRideAppUsers> userManager, RoleManager<IdentityRole> roleManager, SignInManager<RentaRideAppUsers> signInManager, RARdbContext rardbContext, IWebHostEnvironment environment, IFileServices fileServices)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _rardbContext = rardbContext;
            _environment = environment;
            _fileServices = fileServices;
        }

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
                userReg.userisActive = true;

                var userResult = _userManager.CreateAsync(userReg, model.regmodelPassword).GetAwaiter().GetResult();
                if (userResult.Succeeded)
                {
                    _userManager.AddToRoleAsync(userReg, RoleUtilities.RoleUser).GetAwaiter().GetResult();

                    var licenseImgUpload = _fileServices.ProcessUploadedFile(model.regmodelLicense, ImageCategories.imgLicense,userReg.Id);
                    var licenseBackImgUpload = _fileServices.ProcessUploadedFile(model.regmodelLicenseBack, ImageCategories.imgLicenseBack,userReg.Id);
                    var secValidIDImgUpload = _fileServices.ProcessUploadedFile(model.regmodel2ndValidID, ImageCategories.img2ndID, userReg.Id);
                    var POBImgUpload = _fileServices.ProcessUploadedFile(model.regmodelPOB, ImageCategories.imgPOB, userReg.Id);
                    var SelfieImgUpload = _fileServices.ProcessUploadedFile(model.regmodelSelfieProof, ImageCategories.imgSelfie, userReg.Id);

                    var licenseFileExt = _fileServices.GetFileExtension(model.regmodelLicense);
                    var licenseBackFileExt = _fileServices.GetFileExtension(model.regmodelLicenseBack);
                    var secValidIDFileExt = _fileServices.GetFileExtension(model.regmodel2ndValidID);
                    var POBFileExt = _fileServices.GetFileExtension(model.regmodelPOB);
                    var SelfieFileExt = _fileServices.GetFileExtension(model.regmodelSelfieProof);
                    var userDetailsReg = new UserDetailsDBModel
                    {
                        userDateCreated = DateTime.Now,
                        userDateLastModified = DateTime.Now,
                        userDateModified = DateTime.Now,
                        UserID = userReg.Id,
                        userDOB = model.regmodelDOB,
                        userStreetAdd = model.regmodelStreetAdd,
                        userCityAdd = model.regmodelCityAdd,
                        userProvinceAdd = model.regmodelProvinceAdd,
                        userContact = model.regmodelContact,
                        userLicense = licenseImgUpload,
                        userLicenseFileExt = licenseFileExt,
                        userLicenseBack = licenseBackImgUpload,
                        userLicenseBackFileExt = licenseBackFileExt,
                        user2ndValidID = secValidIDImgUpload,
                        user2ndValidIDFileExt = secValidIDFileExt,
                        userProofofBilling = POBImgUpload,
                        userProofofBillingFileExt = POBFileExt,
                        userSelfieProof = SelfieImgUpload,
                        userSelfieProofFileExt = SelfieFileExt,
                    };
                    _rardbContext.TBL_UserDetails.Add(userDetailsReg);
                    _rardbContext.SaveChangesAsync().GetAwaiter().GetResult();
                    ViewBag.SuccessMessage = "User registered";


                    var res = _signInManager.PasswordSignInAsync(userReg.UserName, model.regmodelPassword, false, false).GetAwaiter().GetResult();

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
                        ViewBag.ErrorMessage = "An error occureed. Please log-in again.";
                    }
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


