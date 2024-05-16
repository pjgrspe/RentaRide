using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentaRide.Database;
using RentaRide.Database.Database_Models;
using RentaRide.Models.Accounts;
using RentaRide.Models.Identity;
using RentaRide.Utilities;
using System;
using System.IO;

namespace RentaRide.Controllers
{
    public class RegistrationController(UserManager<RentaRideAppUsers> RentaRideAppUsers, RoleManager<IdentityRole> roleManager, RARdbContext rardbContext, IWebHostEnvironment environment) : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly UserManager<RentaRideAppUsers> _userManager = RentaRideAppUsers;
        private readonly RARdbContext _rardbContext = rardbContext;
        private readonly IWebHostEnvironment _environment = environment;
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

                    var licenseImgUpload = ProcessUploadedFile(model.regmodelLicense, ImageCategories.imgLicense,userReg.Id);
                    var secValidIDImgUpload = ProcessUploadedFile(model.regmodel2ndValidID, ImageCategories.img2ndID, userReg.Id);
                    var POBImgUpload = ProcessUploadedFile(model.regmodelPOB, ImageCategories.imgPOB, userReg.Id);
                    var SelfieImgUpload = ProcessUploadedFile(model.regmodelSelfieProof, ImageCategories.imgSelfie, userReg.Id);

                    var licenseFileExt = GetFileExtension(model.regmodelLicense);
                    var secValidIDFileExt = GetFileExtension(model.regmodel2ndValidID);
                    var POBFileExt = GetFileExtension(model.regmodelPOB);
                    var SelfieFileExt = GetFileExtension(model.regmodelSelfieProof);
                    var userDetailsReg = new UserDetailsModel
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
                    List<IdentityError> errorList = userResult.Errors.ToList();
                    var errors = string.Join(", ", errorList.Select(e => e.Description));
                    ViewBag.ErrorMessage = errors;
                }

            }
            return View(model);
        }

        public string ProcessUploadedFile(IFormFile img, string imgCategory, string UID)
        {
            string uniqueFileName = null;
            string UploadFolder = Path.Combine(FileLoc.FileUploadFolder, imgCategory);
            string path = Path.Combine(_environment.WebRootPath, UploadFolder);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (img != null)
            {
                uniqueFileName = UID;
                string filePath = Path.Combine(path, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    img.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        public string GetFileExtension(IFormFile img)
        {
            string fileExtension = null;
            if (img != null)
            {
                fileExtension = Path.GetExtension(img.FileName);
            }

            return fileExtension;
        }

    }
}


