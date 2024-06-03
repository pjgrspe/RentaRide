using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging;
using RentaRide.Database;
using RentaRide.Database.Database_Models;
using RentaRide.Migrations;
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

        public async Task<IActionResult> LoadPartial(string tabName)
        {
            if (string.IsNullOrEmpty(tabName))
            {
                return BadRequest("Partial name is required.");
            }

            var adminPartialModel = new AdminPartialViewModel();
            try
            {
                //Model logic for Application menu name
                if (tabName == MenuTabNames.menuUsers)
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

                        userVMLicenseIMG = imgNullCheck(user.userLicense, ImageCategories.imgLicense, _configuration, _environment),
                        userVMLicenseBackIMG = imgNullCheck(user.userLicenseBack, ImageCategories.imgLicenseBack, _configuration, _environment),
                        userVM2ndValidIDIMG = imgNullCheck(user.user2ndValidID, ImageCategories.img2ndID, _configuration, _environment),
                        userVMProofofBillingIMG = imgNullCheck(user.userProofofBilling, ImageCategories.imgPOB, _configuration, _environment),
                        userVMSelfieProofIMG = imgNullCheck(user.userSelfieProof, ImageCategories.imgSelfie, _configuration, _environment)
                    }).ToList();

                }
                else if (tabName == MenuTabNames.menuDrivers)
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
                        driverVMImageIMG = imgNullCheck(driver.driverPicture, ImageCategories.imgProfile, _configuration, _environment),
                        driverVMLicenseIMG = imgNullCheck(driver.driverLicense, ImageCategories.imgLicense, _configuration, _environment),
                        driverVMLicenseBackIMG = imgNullCheck(driver.driverLicenseBack, ImageCategories.imgLicenseBack, _configuration, _environment),
                        driverVMImageExt = driver.driverPictureExt,
                        driverVMLicenseExt = driver.driverLicenseExt,
                        driverVMLicenseBackExt = driver.driverLicenseBackExt,
                        driverVMDateCreated = driver.driverRegisteredDate,
                        driverVMDateLastDutyDate = driver.driverLastDutyDate,
                        driverVMOnDuty = driver.driverOnDuty,
                        driverVMIsActive = driver.driverIsActive
                    }).ToList();
                }
                else if (tabName == MenuTabNames.menuCars)
                {
                    var cars = await _rardbContext.TBL_Cars
                                                    .Where(car => car.carIsDeleted == false)
                                                    .Join(
                                                        _rardbContext.TBL_CarTypes,
                                                        car => car.carType,
                                                        carType => carType.cartypeID,
                                                        (car, carType) => new CarsViewModel
                                                        {
                                                            carVMID = car.carID,
                                                            carVMPictureIMG = imgNullCheck(car.carThumbnail, ImageCategories.imgCar, _configuration, _environment),
                                                            carVMORDocIMG = imgNullCheck(car.carORDoc, ImageCategories.imgCarDocs, _configuration, _environment),
                                                            carVMCRDocIMG = imgNullCheck(car.carCRDoc, ImageCategories.imgCarDocs, _configuration, _environment),
                                                            carVMPictureExt = car.carThumbnailExt,
                                                            carVMORDocExt = car.carORDocExt,
                                                            carVMCRDocExt = car.carCRDocExt,
                                                            carVMMake = car.carMake,
                                                            carVMModel = car.carModel,
                                                            carVMYear = car.carYear,
                                                            carVMTransmission = car.carTransmission,
                                                            carVMColor = car.carColor,
                                                            carVMTypeID = car.carType,
                                                            carVMType = carType.cartypeName,
                                                            carVMMileage = car.carMileage,
                                                            carVMFuelType = car.carFuelType,
                                                            carVMStatus = car.carStatus,
                                                            carVMLastChangeOilMileage = car.carLastChangeOilMileage,
                                                            carVMOilChangeInterval = car.carOilChangeInterval,
                                                            carVMIsDeleted = car.carIsDeleted,
                                                            carVMPlateNumber = car.carLicensePlate,
                                                            carVMDateRegistered = car.carDateRegistered
                                                        }
                                                    )
                                                    .ToListAsync();

                    adminPartialModel.Cars = cars;

                    var carTypes = await _rardbContext.TBL_CarTypes.ToListAsync();
                    adminPartialModel.CarTypes = carTypes.Select(carType => new CarTypesViewModel
                    {
                        cartypeVMID = carType.cartypeID,
                        cartypeVMName = carType.cartypeName
                    }).ToList();
                }

                return PartialView($"~/Views/Admin/Tabs/{tabName}.cshtml", adminPartialModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading partial view: {tabName}", tabName);
                return NotFound("Partial view not found.");
            }
        }
        public async Task<IActionResult> AddNewCar([FromForm] IFormCollection form)
        {
            if (ModelState.IsValid)
            {
                var model = new AdminPartialViewModel{
                    AddCar = new CarAddModel
                    {
                        caraddImages = new List<IFormFile>(),
                        caraddMake = form["caraddMake"],
                        caraddModel = form["caraddModel"],
                        caraddYear = Int32.Parse(form["caraddYear"]),
                        caraddType = Int32.Parse(form["caraddType"]),
                        caraddColor = form["caraddColor"],
                        caraddPlateNumber = form["caraddPlateNumber"],
                        caraddTrans = bool.Parse(form["caraddTrans"]),
                        caraddFuelType = bool.Parse(form["caraddFuelType"]),
                        caraddMileage = Int32.Parse(form["caraddMileage"]),
                        caraddLastChangeOilMileage = Int32.Parse(form["caraddLastChangeOilMileage"]),
                        caraddOilChangeInterval = Int32.Parse(form["caraddOilChangeInterval"]),
                        caraddSeats = Int32.Parse(form["caraddSeats"]),
                        caraddLastMaintenance = DateTime.Parse(form["caraddLastMaintenance"]),
                        caraddORDoc = form.Files["caraddORDoc"],
                        caraddCRDoc = form.Files["caraddCRDoc"]

                    }
                };

                var carAdd = new CarsDBModel
                {
                    carThumbnail = "Pending....",
                    carThumbnailExt = "Pending....",
                    carORDoc = "Pending....",
                    carORDocExt = "Pending....",
                    carCRDoc = "Pending....",
                    carCRDocExt = "Pending....",
                    carMake = model.AddCar.caraddMake,
                    carModel = model.AddCar.caraddModel,
                    carYear = model.AddCar.caraddYear,
                    carTransmission = false,
                    carColor = model.AddCar.caraddColor,
                    carType = model.AddCar.caraddType,
                    carMileage = model.AddCar.caraddMileage,
                    carFuelType = model.AddCar.caraddFuelType,
                    carStatus = true, //
                    carLastMaintenance = model.AddCar.caraddLastMaintenance,
                    carLastChangeOilMileage = model.AddCar.caraddLastChangeOilMileage,
                    carOilChangeInterval = model.AddCar.caraddOilChangeInterval,
                    carLicensePlate = model.AddCar.caraddPlateNumber,
                    carIsDeleted = false,
                    carDateRegistered = DateTime.Now,
                    carSeats = model.AddCar.caraddSeats,
                    carLastLogDate = DateTime.Now
                };

                _rardbContext.TBL_Cars.Add(carAdd);
                _rardbContext.SaveChanges();

                foreach (var file in form.Files)
                {
                    if (file.Name == "caraddImages")
                    {
                        model.AddCar.caraddImages.Add(file);
                        var carIMG = _fileServices.ProcessEncryptUploadedFile(file, ImageCategories.imgCar);
                        var carIMGext = _fileServices.GetFileExtension(file);
                        var carImgAdd = new CarImagesDBModel
                        {
                            carID = carAdd.carID,
                            carimgName = carIMG!,
                            carimgExt = carIMGext!
                        };
                        
                        _rardbContext.TBL_CarImages.Add(carImgAdd);
                        _rardbContext.SaveChanges();
                    }
                }
                var carImageThumbnailImgUpload = _fileServices.ProcessEncryptUploadedFile(model.AddCar.caraddImages[0], ImageCategories.imgCar);
                var carORDocImgUpload = _fileServices.ProcessEncryptUploadedFile(model.AddCar.caraddORDoc, ImageCategories.imgCarDocs);
                var carCRDocImgUpload = _fileServices.ProcessEncryptUploadedFile(model.AddCar.caraddCRDoc, ImageCategories.imgCarDocs);

                var carImageThumbnailFileExt = _fileServices.GetFileExtension(model.AddCar.caraddImages[0]);
                var carORDocFileExt = _fileServices.GetFileExtension(model.AddCar.caraddORDoc);
                var carCRDocFileExt = _fileServices.GetFileExtension(model.AddCar.caraddCRDoc);

                
                var carToUpdate = _rardbContext.TBL_Cars.Find(carAdd.carID);
                carToUpdate!.carThumbnail = carImageThumbnailImgUpload!;
                carToUpdate!.carThumbnailExt = carImageThumbnailFileExt!;
                carToUpdate!.carORDoc = carORDocImgUpload!;
                carToUpdate!.carORDocExt = carORDocFileExt!;
                carToUpdate!.carCRDoc = carCRDocImgUpload!;
                carToUpdate!.carCRDocExt = carCRDocFileExt!;

               

                var carLastChangeOilLog = new CarLogsDBModel
                {
                    carID = carAdd.carID,
                    LogDate = DateTime.Now,
                    LogMileage = carAdd.carLastChangeOilMileage,
                    LogType = 3,
                    LogDetails = "Car last oil change"
                };
                _rardbContext.TBL_CarLogs.Add(carLastChangeOilLog);
                _rardbContext.SaveChanges();

                if (carAdd.carLastMaintenance != null)
                {
                    var carMaintenanceLog = new CarLogsDBModel
                    {
                        carID = carAdd.carID,
                        LogDate = (DateTime)carAdd.carLastMaintenance,
                        LogMileage = carAdd.carMileage,
                        LogType = 2,
                        LogDetails = "Car last maintenance"
                    };
                    _rardbContext.TBL_CarLogs.Add(carMaintenanceLog);
                    _rardbContext.SaveChanges();
                }

                var carLogs = new CarLogsDBModel
                {
                    carID = carAdd.carID,
                    LogDate = DateTime.Now,
                    LogMileage = carAdd.carMileage,
                    LogType = 1,
                    LogDetails = "Car added to the system"
                };
                _rardbContext.TBL_CarLogs.Add(carLogs);
                _rardbContext.SaveChanges();




                await _rardbContext.SaveChangesAsync();
                return new JsonResult(new { success = true });
            }
            else
            {
                ViewBag.ErrorMessage = "An error occureed with adding car";
                return new JsonResult(new { success = false, message = "An error occurred with adding car" });
            }
        }

        //Not implemented yet, just an initial setup
        public async Task<IActionResult> AddNewLog([FromForm] IFormCollection form)
        {
            if (ModelState.IsValid)
            {
                int addLogCarID = Int32.Parse(form["addLogCarID"]);
                int addLogMileage = Int32.Parse(form["addLogMileage"]);
                var Car = _rardbContext.TBL_Cars.Find(addLogCarID);

                if (Car!.carMileage > addLogMileage)
                {
                    ViewBag.ErrorMessage = "Mileage cannot be less than the current mileage";
                    return new JsonResult(new { success = false, message = "Mileage cannot be less than the current mileage" });
                }

                if (Car!.carLastLogDate > DateTime.Parse(form["addLogDate"]))
                {
                    ViewBag.ErrorMessage = "Date cannot be less than the last logged date";
                    return new JsonResult(new { success = false, message = "Date cannot be less than the last logged date" });
                }

                var model = new AdminPartialViewModel {
                    AddLog = new CarAddLogModel
                    {
                        addLogCarID = addLogCarID,
                        addLogDetails = form["addLogDetails"],
                        addLogType = Int32.Parse(form["addLogType"]),
                        addLogMileage = addLogMileage,
                        addLogDate = DateTime.Parse(form["addLogDate"])

                    }
                };

                var logAdd = new CarLogsDBModel
                {
                    carID = addLogCarID,
                    LogDate = model.AddLog.addLogDate,
                    LogMileage = model.AddLog.addLogMileage,
                    LogType = model.AddLog.addLogType,
                    LogDetails = model.AddLog.addLogDetails
                    
                };

                
                Car.carMileage = addLogMileage;
                Car.carLastLogDate = model.AddLog.addLogDate;
                if (model.AddLog.addLogType == 2)
                {
                    Car.carLastMaintenance = model.AddLog.addLogDate;
                }
                else if(model.AddLog.addLogType == 3)
                {
                    Car.carLastChangeOilMileage = addLogMileage;
                }
                _rardbContext.TBL_CarLogs.Add(logAdd);
                _rardbContext.SaveChanges();

                await _rardbContext.SaveChangesAsync();
                return new JsonResult(new { success = true });
            }
            else
            {
                ViewBag.ErrorMessage = "An error occureed with adding log";
                return new JsonResult(new { success = false, message = "An error occurred with adding log" });
            }
        }

        public async Task<IActionResult> GetCarDetails(int carId, bool forDetailsPage)
        {
            // Fetch the car details from the database using the carId
            var car = await _rardbContext.TBL_Cars.FindAsync(carId);

            if (car == null)
            {
                return Json(new { success = false });
            }

            var carType = await _rardbContext.TBL_CarTypes
                                                    .Where(ct => ct.cartypeID == car.carType)
                                                    .Select(ct => ct.cartypeName)
                                                    .FirstOrDefaultAsync();

            var carImages = await _rardbContext.TBL_CarImages
                    .Where(carImg => carImg.carID == car.carID)
                    .Select(carImg => new CarImagesViewModel
                    {
                        carIMGVMID = carImg.carimgID,
                        carIMGVMCarIMG = imgNullCheck(carImg.carimgName, ImageCategories.imgCar, _configuration, _environment),
                        carIMGVMCarExt = carImg.carimgExt
                    })
                    .ToListAsync();

            var carLogs = await _rardbContext.TBL_CarLogs
                    .Where(carLogs => carLogs.carID == car.carID)
                    .Select(carLogs => new CarLogsViewModel
                    {
                        carLogsVMID = carLogs.logID,
                        carLogsVMTypeID = carLogs.LogType,
                        carLogsVMMileage = carLogs.LogMileage,
                        carLogsVMDate = carLogs.LogDate,
                        carLogsVMDetails = carLogs.LogDetails
                    })
                    .ToListAsync();

            // Create a ViewModel with the car details
            var viewModel = new AdminPartialViewModel
            {
                CarDetails = new CarDetailsViewModel
                {
                    cardeetsVM = car.carID,
                    cardeetsVMMake = car.carMake,
                    cardeetsVMModel = car.carModel,
                    cardeetsVMYear = car.carYear,
                    cardeetsVMTransmission = car.carTransmission,
                    cardeetsVMTypeID = car.carType,
                    cardeetsVMCarType = carType,
                    cardeetsVMColor = car.carColor,
                    cardeetsVMLicense = car.carLicensePlate,
                    cardeetsVMMileage = car.carMileage,
                    cardeetsVMLastLog = car.carLastLogDate,
                    cardeetsVMStatusID = car.carStatus,
                    cardeetsVMORIMG = imgNullCheck(car.carORDoc, ImageCategories.imgCarDocs, _configuration, _environment),
                    cardeetsVMCRIMG = imgNullCheck(car.carCRDoc, ImageCategories.imgCarDocs, _configuration, _environment),
                    cardeetsVMORExt = car.carORDocExt,
                    cardeetsVMCRExt = car.carCRDocExt
                },

                CarImages = carImages,
                CarLogs = carLogs
            };

            var carTypes = await _rardbContext.TBL_CarTypes.ToListAsync();
            viewModel.CarTypes = carTypes.Select(carType => new CarTypesViewModel
            {
                cartypeVMID = carType.cartypeID,
                cartypeVMName = carType.cartypeName
            }).ToList();

            // Return the Details view with the ViewModel
            if (forDetailsPage)
            {
                return PartialView("~/Views/Admin/TabComponents/Cars/Details.cshtml", viewModel);
            }
            else
            {
                return PartialView("~/Views/Admin/TabComponents/Cars/Modals.cshtml", viewModel);
            }
            //return Json(new { success = true, data = viewModel });

        }

        [NonAction]
        private static string imgNullCheck(string? img, string imgCategory, IConfiguration configuration, IWebHostEnvironment environment)
        {
            var key = configuration["ImageEncryption:ImageKey"];
            var iv = configuration["ImageEncryption:ImageIV"];
            string UploadFolder = Path.Combine(FileLoc.FileUploadFolder, imgCategory);
            string path = Path.Combine(environment.WebRootPath, UploadFolder);
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
