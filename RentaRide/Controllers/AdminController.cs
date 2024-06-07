using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging;
using RentaRide.Database;
using RentaRide.Database.Database_Models;
using RentaRide.Migrations;
using RentaRide.Models.Accounts;
using RentaRide.Models.Cars;
using RentaRide.Models.Listings;
using RentaRide.Models.Orders;
using RentaRide.Models.ViewModels;
using RentaRide.Services;
using RentaRide.Utilities;
using System.Reflection;
using System.Text;

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

        public IActionResult map()
        {
            return View();
        }


        public async Task<IActionResult> LoadPartial(string tabName, int page = 1, int pageSize = 10)
        {
            if (string.IsNullOrEmpty(tabName))
            {
                return BadRequest("Partial name is required.");
            }

            var adminPartialModel = new AdminPartialViewModel();
            try
            {
                if (tabName == MenuTabNames.menuUsers)
                {
                    var users = await _rardbContext.TBL_UserDetails
                                      .Include(u => u.RentaRideAppUsers)
                                      .Skip((page - 1) * pageSize)
                                      .Take(pageSize)
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
                                          userVMLicenseIMG = _fileServices.imgNullCheck(user.userLicense, ImageCategories.imgLicense),
                                          userVMLicenseBackIMG = _fileServices.imgNullCheck(user.userLicenseBack, ImageCategories.imgLicenseBack),
                                          userVM2ndValidIDIMG = _fileServices.imgNullCheck(user.user2ndValidID, ImageCategories.img2ndID),
                                          userVMProofofBillingIMG = _fileServices.imgNullCheck(user.userProofofBilling, ImageCategories.imgPOB),
                                          userVMSelfieProofIMG = _fileServices.imgNullCheck(user.userSelfieProof, ImageCategories.imgSelfie)
                    }).ToList();
                }
                else if (tabName == MenuTabNames.menuDrivers)
                {
                    var drivers = await _rardbContext.TBL_Drivers
                                                     .Where(driver => driver.driverIsDeleted == false)
                                                     .Skip((page - 1) * pageSize)
                                                     .Take(pageSize)
                                                     .ToListAsync();

                    adminPartialModel.Drivers = drivers.Select(driver => new DriversViewModel
                                                     {
                                                         driverVMID = driver.driverID,
                                                         driverVMFirstName = driver.driverFirstName,
                                                         driverVMMiddleName = driver.driverMiddleName,
                                                         driverVMLastName = driver.driverLastName,
                                                         driverVMEmail = driver.driverEmail,
                                                         driverVMContact = driver.driverContact,
                                                         driverVMImageIMG = _fileServices.imgNullCheck(driver.driverPicture, ImageCategories.imgProfile),
                                                         driverVMLicenseIMG = _fileServices.imgNullCheck(driver.driverLicense, ImageCategories.imgLicense),
                                                         driverVMLicenseBackIMG = _fileServices.imgNullCheck(driver.driverLicenseBack, ImageCategories.imgLicenseBack),
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
                                                  .Skip((page - 1) * pageSize)
                                                  .Take(pageSize)
                                                  .ToListAsync();

                    adminPartialModel.Cars = cars.Select(car => new CarsViewModel
                                                  {
                                                      carVMID = car.carID,
                                                      carVMPictureIMG = _fileServices.imgNullCheck(car.carThumbnail, ImageCategories.imgCar),
                                                      carVMPictureExt = car.carThumbnailExt,
                                                      carVMMake = car.carMake,
                                                      carVMModel = car.carModel,
                                                      carVMYear = car.carYear,
                                                      carVMTransmission = car.carTransmission,
                                                      carVMColor = car.carColor,
                                                      carVMTypeID = car.carType,
                                                      carVMMileage = car.carMileage,
                                                      carVMFuelType = car.carFuelType,
                                                      carVMStatus = car.carStatus,
                                                      carVMLastChangeOilMileage = car.carLastChangeOilMileage,
                                                      carVMOilChangeInterval = car.carOilChangeInterval,
                        carVMPlateNumber = car.carLicensePlate,
                    }).ToList();
                }
                else if (tabName == MenuTabNames.menuListings)
                {
                    var listings = await _rardbContext.TBL_Listings
                                                      .Where(listing => listing.listingIsActive)
                                                      .Join(
                                                          _rardbContext.TBL_Cars,
                                                          listing => listing.carID,
                                                          car => car.carID,
                                                          (listing, car) => new ListingsViewModel
                                                          {
                                                              listingVMID = listing.listingID,
                                                              listingVMcarID = car.carID,
                                                              listingVMcarName = $"{car.carMake} {car.carModel} {car.carYear} - [{car.carLicensePlate}]",
                                                              listingVMcarNameNoLicense = $"{car.carMake} {car.carModel} ({car.carYear})",
                                                              listingVMcarIMG = _fileServices.imgNullCheck(car.carThumbnail, ImageCategories.imgCar),
                                                              listingVMcarIMGext = car.carThumbnailExt,
                                                              listingVMDetails = listing.listingDetails,
                                                              listingVMColor = car.carColor,
                                                              listingVMTransmission = car.carTransmission,
                                                              listingVMFuelType = car.carFuelType,
                                                              listingVMType = car.carType,
                                                              listingVMSeats = car.carSeats,
                                                              listingVMHourlyPrice = listing.listingHourlyPrice,
                                                              listingVMDailyPrice = listing.listingDailyPrice,
                                                              listingVMWeeklyPrice = listing.listingWeeklyPrice,
                                                              listingVMMonthlyPrice = listing.listingMonthlyPrice,
                                                              listingVMStatus = listing.listingStatus,
                                                              listingVMAvailabilityStart = listing.listingAvailabilityStart,
                                                              listingVMAvailabilityEnd = listing.listingAvailabilityEnd
                                                          }
                                                      )
                                                      .Skip((page - 1) * pageSize)
                                                      .Take(pageSize)
                                                      .ToListAsync();
                    adminPartialModel.Listings = listings;
                }
                else if (tabName == MenuTabNames.menuOrders)
                {
                    var orders = await _rardbContext.TBL_Orders
                                                    .Join(_rardbContext.TBL_Listings,
                                                          order => order.listingID,
                                                          listing => listing.listingID,
                                                          (order, listing) => new { order, listing })
                                                    .Join(_rardbContext.TBL_Cars,
                                                          orderListing => orderListing.listing.carID,
                                                          car => car.carID,
                                                          (orderListing, car) => new { orderListing.order, orderListing.listing, car })
                                                    .Join(_rardbContext.TBL_UserDetails,
                                                          orderListingCar => orderListingCar.order.userID,
                                                          user => user.UserID,
                                                          (orderListingCar, user) => new OrdersViewModel
                                                          {
                                                              ordersVMID = orderListingCar.order.orderID,
                                                              ordersVMreceipt = orderListingCar.order.orderReservationID,
                                                              ordersVMCustFName = user.RentaRideAppUsers.userFirstName,
                                                              ordersVMCustLName = user.RentaRideAppUsers.userLastName,
                                                              ordersVMCarName = orderListingCar.car.carMake + " " + orderListingCar.car.carModel + " " + "(" + orderListingCar.car.carYear + ")",
                                                              ordersVMPlateNumber = orderListingCar.car.carLicensePlate,
                                                              ordersVMStartDate = orderListingCar.order.orderPickupDate,
                                                              ordersVMEndDate = orderListingCar.order.orderReturnDate,
                                                              ordersVMTotalCost = orderListingCar.order.orderTotalCost,
                                                              ordersVMExtraFees = orderListingCar.order.orderExtraFees,
                                                              ordersVMStatusID = orderListingCar.order.orderStatus
                                                          })
                                                    .Skip((page - 1) * pageSize)
                                                    .Take(pageSize)
                                                    .ToListAsync();
                    adminPartialModel.Orders = orders;
                }

                return PartialView($"~/Views/Admin/Tabs/{tabName}.cshtml", adminPartialModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading partial view: {tabName}", tabName);
                return NotFound("Partial view not found.");
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

            var carImages = await _rardbContext.TBL_CarImages
                    .Where(carImg => carImg.carID == car.carID)
                    .Select(carImg => new CarImagesViewModel
                    {
                        carIMGVMID = carImg.carimgID,
                        carIMGVMCarIMG = _fileServices.imgNullCheck(carImg.carimgName, ImageCategories.imgCar),
                        carIMGVMCarExt = carImg.carimgExt
                    })
                    .ToListAsync();

            var carLogs = await _rardbContext.TBL_CarLogs
                    .Where(carLogs => carLogs.carID == car.carID && carLogs.LogIsDeleted == false)
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
                    cardeetsVMColor = car.carColor,
                    cardeetsVMLicense = car.carLicensePlate,
                    cardeetsVMMileage = car.carMileage,
                    cardeetsVMLastLog = car.carLastLogDate,
                    cardeetsVMStatusID = car.carStatus,
                    cardeetsVMORIMG = _fileServices.imgNullCheck(car.carORDoc, ImageCategories.imgCarDocs),
                    cardeetsVMCRIMG = _fileServices.imgNullCheck(car.carCRDoc, ImageCategories.imgCarDocs),
                    cardeetsVMORExt = car.carORDocExt,
                    cardeetsVMCRExt = car.carCRDocExt,
                    cardeetsVMSeats = car.carSeats,
                    cardeetsVMFuelType = car.carFuelType,
                    cardeetsVMOilChangeInterval = car.carOilChangeInterval,
                    cardeetsVMLastChangeOilMileage = car.carLastChangeOilMileage,
                    cardeetsVMLastMaintenance = car.carLastMaintenance

                },

                CarImages = carImages,
                CarLogs = carLogs
            };

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
        public async Task<IActionResult> GetOrderDetails(int orderId)
        {
            // Fetch the car details from the database using the carId
            var order = await _rardbContext.TBL_Orders.FindAsync(orderId);
            if (order == null)
            {
                return Json(new { success = false });
            }
            var listing = await _rardbContext.TBL_Listings.FindAsync(order.listingID);
            var car = await _rardbContext.TBL_Cars.FindAsync(listing.carID);
            var user = await _rardbContext.TBL_UserDetails
                                  .Include(u => u.RentaRideAppUsers)
                                  .FirstOrDefaultAsync(u => u.UserID == order.userID);
    
            // Create a ViewModel with the car details
            var viewModel = new AdminPartialViewModel
            {
                OrderDetails = new OrderDetailsViewModel
                {
                    orderdeetsVMID = order.orderID,
                    orderdeetsVMReceipt = order.orderReservationID,
                    orderdeetsVMCarName = $"{car.carMake} {car.carModel} ({car.carYear})",
                    orderdeetsVMCustFName = user.RentaRideAppUsers.userFirstName,
                    orderdeetsVMCustLName = user.RentaRideAppUsers.userLastName,
                    orderdeetsVMPlateNumber = car.carLicensePlate,
                    orderdeetsVMStartDate = order.orderPickupDate,
                    orderdeetsVMEndDate = order.orderReturnDate,
                    orderdeetsVMTotalCost = order.orderTotalCost,
                    orderdeetsVMExtraFees = order.orderExtraFees,
                    orderdeetsVMStatusID = order.orderStatus,
                    orderdeetsVMPOPIMG = _fileServices.imgNullCheck(order.orderPaymentIMG,ImageCategories.imgProofOP),
                    orderdeetsVMPOPIMGExt = order.orderPaymentExt
                }
            };

            return PartialView("~/Views/Admin/TabComponents/Orders/Modals.cshtml", viewModel);


        }
        public async Task<IActionResult> GetLogDetails(int logId)
        {
            // Fetch the car details from the database using the carId
            var carLog = await _rardbContext.TBL_CarLogs.FindAsync(logId);

            if (carLog == null)
            {
                return Json(new { success = false });
            }

            // Create a ViewModel with the car details
            var viewModel = new AdminPartialViewModel
            {
                CarLogsDetails = new CarLogsDetailsViewModel
                {
                    carlogDeetsVMID = carLog.logID,
                    carlogDeetsVMMileage = carLog.LogMileage,
                    carlogDeetsVMDate = carLog.LogDate,
                    carlogDeetsVMDetails = carLog.LogDetails,
                    carlogDeetsVMTypeID = carLog.LogType
                }
            };

            // Return the Details view with the ViewModel
            
            return PartialView("~/Views/Admin/TabComponents/Cars/Modals.cshtml", viewModel);
            //return Json(new { success = true, data = viewModel });

        }
        public async Task<IActionResult> GetListingDetails(int listingId)
        {
            // Fetch the car details from the database using the carId
            var listing = await _rardbContext.TBL_Listings
                                            .FirstOrDefaultAsync(l => l.listingID == listingId);

            if (listing == null)
            {
                return Json(new { success = false, message = "Invalid listing"});
            }

            return Json(new
            {
                success = true,
                hourlyRate = listing.listingHourlyPrice,
                dailyRate = listing.listingDailyPrice,
                weeklyRate = listing.listingWeeklyPrice,
                monthlyRate = listing.listingMonthlyPrice,
                startdate = listing.listingAvailabilityStart,
                enddate = listing.listingAvailabilityEnd,
            });

        }
        public async Task<IActionResult> GetListingRates(int listingId)
        {
            // Fetch the car details from the database using the carId
            var carlistingRates = await _rardbContext.TBL_Listings.FindAsync(listingId);

            if (carlistingRates == null)
            {
                return Json(new { success = false, message = "nvalid listing"});
            }

            // Create a ViewModel with the car details
            var viewModel = new AdminPartialViewModel
            {
                CarRates = new CarRatesViewModel
                {
                    HourlyRate = carlistingRates.listingHourlyPrice,
                    DailyRate = carlistingRates.listingDailyPrice,
                    WeeklyRate = carlistingRates.listingWeeklyPrice,
                    MonthlyRate = carlistingRates.listingMonthlyPrice
                }
            };

            // Return the Details view with the ViewModel
            
            //return PartialView("~/Views/Admin/TabComponents/Listings/Modals.cshtml", viewModel);
            return Json(new { success = true, data = viewModel });

        }
        public async Task<IActionResult> GetCarList()
        {
            var adminPartialModel = new AdminPartialViewModel();
            var cars = await _rardbContext.TBL_Cars.Where(car => car.carStatus == 1)
                                                     .ToListAsync();

            adminPartialModel.ListingsCarList = cars.Select(cars => new ListingCarListViewModel
            {
                listingcarID = cars.carID,
                listingcarName = $"{cars.carMake} {cars.carModel} {cars.carYear}",
            }).ToList();

            return PartialView("~/Views/Admin/TabComponents/Listings/Modals.cshtml", adminPartialModel);

        }
        public async Task<IActionResult> GetOrderChoicesList()
        {
            var adminPartialModel = new AdminPartialViewModel();
            var listings = await _rardbContext.TBL_Listings
                                                    .Where(listing => listing.listingIsActive == true)
                                                    .Join(
                                                        _rardbContext.TBL_Cars,
                                                        listing => listing.carID,
                                                        car => car.carID,
                                                        (listing, car) => new OrderListingListViewModel
                                                        {
                                                            orderlistingID = listing.listingID,
                                                            orderlistingName = $"{car.carMake} {car.carModel} {car.carYear}"
                                                        }
                                                    )
                                                    .ToListAsync();

            var users = await _rardbContext.TBL_UserDetails
                                        .Include(u => u.RentaRideAppUsers)
                                        .Where(u => u.RentaRideAppUsers.userisActive == true)
                                        .ToListAsync();
            var usersList = users.Select(user => new OrderUserListViewModel
            {
                orderuserID = user.RentaRideAppUsers.Id,
                orderuserName = $"{user.RentaRideAppUsers.userLastName}, {user.RentaRideAppUsers.userFirstName} "
            }).ToList();

            var drivers = await _rardbContext.TBL_Drivers
                                        .Where(d => d.driverIsActive == true)
                                        .ToListAsync();
            var driversList = drivers.Select(driver => new OrderDriverListViewModel
            {
                orderdriverID = driver.driverID,
                orderdriverName = $"{driver.driverLastName}, {driver.driverFirstName} "
            }).ToList();

            
            adminPartialModel.OrderListings = listings;
            adminPartialModel.OrderUsers = usersList;
            adminPartialModel.OrderDrivers = driversList;
            
            return PartialView("~/Views/Admin/TabComponents/Orders/Modals.cshtml", adminPartialModel);

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
        public async Task<IActionResult> AddNewCar([FromForm] IFormCollection form)
        {
            if (ModelState.IsValid)
            {
                DateTime.TryParse(form["caraddLastMaintenance"], out DateTime tempCarAddLastMaintenance);
                DateTime? caraddLastMaintenance = !string.IsNullOrEmpty(form["caraddLastMaintenance"]) ? tempCarAddLastMaintenance : (DateTime?)null;

                var files = form.Files;
                var caraddImages = files.GetFile("caraddImages");

                if (caraddImages == null || caraddImages.Length == 0)
                {
                    ViewBag.ErrorMessage = "No image uploaded";
                    return new JsonResult(new { success = false, message = "No image uploaded" });
                }

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
                        caraddFuelType = Int32.Parse(form["caraddFuelType"]),
                        caraddMileage = Int32.Parse(form["caraddMileage"]),
                        caraddLastChangeOilMileage = Int32.Parse(form["caraddLastChangeOilMileage"]),
                        caraddOilChangeInterval = Int32.Parse(form["caraddOilChangeInterval"]),
                        caraddSeats = Int32.Parse(form["caraddSeats"]),
                        caraddLastMaintenance = caraddLastMaintenance,
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
                    carStatus = 1, //
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
                
                var carToUpdate = _rardbContext.TBL_Cars.Find(carAdd.carID);
                int imgCounter = 0;
                foreach (var file in form.Files)
                {
                    imgCounter++;
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
                        
                        if (imgCounter == 1)
                        {
                            carToUpdate!.carThumbnail = carIMG!;
                            carToUpdate!.carThumbnailExt = carIMGext!;
                        }
                        _rardbContext.TBL_CarImages.Add(carImgAdd);
                        _rardbContext.SaveChanges();
                    }
                }
                var carORDocImgUpload = _fileServices.ProcessEncryptUploadedFile(model.AddCar.caraddORDoc, ImageCategories.imgCarDocs);
                var carCRDocImgUpload = _fileServices.ProcessEncryptUploadedFile(model.AddCar.caraddCRDoc, ImageCategories.imgCarDocs);

                var carORDocFileExt = _fileServices.GetFileExtension(model.AddCar.caraddORDoc);
                var carCRDocFileExt = _fileServices.GetFileExtension(model.AddCar.caraddCRDoc);

                
                carToUpdate!.carORDoc = carORDocImgUpload!;
                carToUpdate!.carORDocExt = carORDocFileExt!;
                carToUpdate!.carCRDoc = carCRDocImgUpload!;
                carToUpdate!.carCRDocExt = carCRDocFileExt!;

               
                

                var carLogs = new CarLogsDBModel
                {
                    carID = carAdd.carID,
                    LogDate = DateTime.Now,
                    LogMileage = carAdd.carMileage,
                    LogType = 0,
                    LogDetails = "Car added to the system"
                };
                _rardbContext.TBL_CarLogs.Add(carLogs);
                _rardbContext.SaveChanges();

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




                await _rardbContext.SaveChangesAsync();
                return new JsonResult(new { success = true });
            }
            else
            {
                ViewBag.ErrorMessage = "An error occureed with adding car";
                return new JsonResult(new { success = false, message = "An error occurred with adding car" });
            }
        }
        public async Task<IActionResult> EditCar([FromForm] IFormCollection form)
        {
            if (ModelState.IsValid)
            {
                var model = new AdminPartialViewModel{
                    EditCar = new CarEditModel
                    {
                        careditID = Int32.Parse(form["carEditID"]),
                        careditMake = form["carEditMake"],
                        careditModel = form["carEditModel"],
                        careditYear = Int32.Parse(form["carEditYear"]),
                        careditType = Int32.Parse(form["carEditType"]),
                        careditColor = form["carEditColor"],
                        careditPlateNumber = form["carEditPlateNumber"],
                        careditTrans = bool.Parse(form["carEditTrans"]),
                        careditFuelType = Int32.Parse(form["carEditFuelType"]),
                        careditOilChangeInterval = Int32.Parse(form["carEditOilChangeInterval"]),
                        careditSeats = Int32.Parse(form["carEditSeats"]),
                        careditORDoc = form.Files["carEditORDoc"],
                        careditCRDoc = form.Files["carEditCRDoc"],
                        careditImages = new List<IFormFile>()
                    }
                };

                var carToUpdate = _rardbContext.TBL_Cars.Find(model.EditCar.careditID);
                var editLogDetails = new StringBuilder("Car details edited: \n");

                if (carToUpdate.carMake != model.EditCar.careditMake)
                {
                    carToUpdate.carMake = model.EditCar.careditMake;
                    editLogDetails.AppendLine($"Make - {model.EditCar.careditMake}, \n");
                }

                if (carToUpdate.carModel != model.EditCar.careditModel)
                {
                    carToUpdate.carModel = model.EditCar.careditModel;
                    editLogDetails.AppendLine($"Model - {model.EditCar.careditModel}, \n");
                }

                if (carToUpdate.carYear != model.EditCar.careditYear)
                {
                    carToUpdate.carYear = model.EditCar.careditYear;
                    editLogDetails.AppendLine($"Year - {model.EditCar.careditYear}, \n");
                }

                if (carToUpdate.carType != model.EditCar.careditType)
                {
                    carToUpdate.carType = model.EditCar.careditType;
                    editLogDetails.AppendLine($"Type - {model.EditCar.careditType}, \n");
                }

                if (carToUpdate.carColor != model.EditCar.careditColor)
                {
                    carToUpdate.carColor = model.EditCar.careditColor;
                    editLogDetails.AppendLine($"Color - {model.EditCar.careditColor}, \n");
                }

                if (carToUpdate.carLicensePlate != model.EditCar.careditPlateNumber)
                {
                    carToUpdate.carLicensePlate = model.EditCar.careditPlateNumber;
                    editLogDetails.AppendLine($"Plate Number - {model.EditCar.careditPlateNumber}, \n");
                }

                if (carToUpdate.carTransmission != model.EditCar.careditTrans)
                {
                    carToUpdate.carTransmission = model.EditCar.careditTrans;
                    editLogDetails.AppendLine($"Transmission - {model.EditCar.careditTrans}, \n");
                }

                if (carToUpdate.carFuelType != model.EditCar.careditFuelType)
                {
                    carToUpdate.carFuelType = model.EditCar.careditFuelType;
                    editLogDetails.AppendLine($"Fuel Type - {model.EditCar.careditFuelType}, \n");
                }

                if (carToUpdate.carOilChangeInterval != model.EditCar.careditOilChangeInterval)
                {
                    carToUpdate.carOilChangeInterval = model.EditCar.careditOilChangeInterval;
                    editLogDetails.AppendLine($"Oil Change Interval - {model.EditCar.careditOilChangeInterval}, \n");
                }

                if (carToUpdate.carSeats != model.EditCar.careditSeats)
                {
                    carToUpdate.carSeats = model.EditCar.careditSeats;
                    editLogDetails.AppendLine($"Seats - {model.EditCar.careditSeats}, \n");
                }


                var files = form.Files;
                if (files.Any(f => f.Name == "careditImages"))
                {
                    // Delete the old images from the database
                    var oldImages = _rardbContext.TBL_CarImages.Where(i => i.carID == model.EditCar.careditID);

                    foreach(var oldImage in oldImages)
                    {
                        var oldFilePath = Path.Combine(_environment.WebRootPath, FileLoc.FileUploadFolder, ImageCategories.imgCar, oldImage.carimgName);
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }
                    _rardbContext.TBL_CarImages.RemoveRange(oldImages);
                    int imgCounter = 0;
                    foreach (var file in form.Files)
                    {
                        imgCounter++;
                        if (file.Name == "careditImages")
                        {
                            model.EditCar.careditImages.Add(file);
                            var carIMG = _fileServices.ProcessEncryptUploadedFile(file, ImageCategories.imgCar);
                            var carIMGext = _fileServices.GetFileExtension(file);
                            var carImgAdd = new CarImagesDBModel
                            {
                                carID = model.EditCar.careditID,
                                carimgName = carIMG!,
                                carimgExt = carIMGext!
                            };
                        
                        
                            if (imgCounter == 1)
                            {
                                carToUpdate!.carThumbnail = carIMG!;
                                carToUpdate!.carThumbnailExt = carIMGext!;
                            }
                            _rardbContext.TBL_CarImages.Add(carImgAdd);
                            _rardbContext.SaveChanges();
                        }
                    }

                    if (model.EditCar.careditORDoc != null)
                    {
                        var carORDocImgUpload = _fileServices.ProcessEncryptUploadedFile(model.EditCar.careditORDoc, ImageCategories.imgCarDocs);
                        var carORDocFileExt = _fileServices.GetFileExtension(model.EditCar.careditORDoc);
                        carToUpdate.carORDoc = carORDocImgUpload!;
                        carToUpdate.carORDocExt = carORDocFileExt!;
                        editLogDetails.AppendLine($"OR Doc - File changed, \n");
                    }

                    if (model.EditCar.careditCRDoc != null)
                    {
                        var carCRDocImgUpload = _fileServices.ProcessEncryptUploadedFile(model.EditCar.careditCRDoc, ImageCategories.imgCarDocs);
                        var carCRDocFileExt = _fileServices.GetFileExtension(model.EditCar.careditCRDoc);
                        carToUpdate.carCRDoc = carCRDocImgUpload!;
                        carToUpdate.carCRDocExt = carCRDocFileExt!;
                        editLogDetails.AppendLine($"CR Doc - File changed, \n");
                    }
                    
                    if (model.EditCar.careditImages.Count > 0)
                    {
                        editLogDetails.AppendLine($"Images - Files changed, \n");
                    }
                }




                var AddEditLog = new CarLogsDBModel
                {
                    carID = model.EditCar.careditID,
                    LogDetails = editLogDetails.ToString(),
                    LogType = 7,
                    LogMileage = carToUpdate.carMileage,
                    LogDate = DateTime.Now
                };

                _rardbContext.TBL_CarLogs.Add(AddEditLog);
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
        public async Task<IActionResult> DeleteCar([FromForm] IFormCollection form)
        {
            if (ModelState.IsValid)
            {
                var model = new AdminPartialViewModel{
                    DelCar = new CarDelModel
                    {
                        cardelID = Int32.Parse(form["cardelID"])
                    }
                };

                var carToDelete = _rardbContext.TBL_Cars.Find(model.DelCar.cardelID);
                carToDelete.carIsDeleted = true;

                 var AddEditLog = new CarLogsDBModel
                 {
                    carID = carToDelete.carID,
                    LogDetails = "Car deleted",
                    LogType = 8,
                    LogMileage = carToDelete.carMileage,
                    LogDate = DateTime.Now
                 };

                await _rardbContext.SaveChangesAsync();
                return new JsonResult(new { success = true });
            }
            else
            {
                ViewBag.ErrorMessage = "An error occureed with adding car";
                return new JsonResult(new { success = false, message = "An error occurred with adding car" });
            }
        }
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
                    Car.carStatus = 3;
                }
                else if(model.AddLog.addLogType == 3)
                {
                    Car.carLastChangeOilMileage = addLogMileage;
                }
                else if(model.AddLog.addLogType == 5)
                {
                    Car.carStatus = 4;
                }
                else
                {
                    Car.carStatus = 1;
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
        public async Task<IActionResult> DeleteLog([FromForm] IFormCollection form)
        {
            if (ModelState.IsValid)
            {
                var model = new AdminPartialViewModel{
                    DelLog = new CarDelLogModel
                    {
                        cardellogID = Int32.Parse(form["cardellogID"])
                    }
                };

                var logToDelete = _rardbContext.TBL_CarLogs.Find(model.DelLog.cardellogID);
                logToDelete.LogIsDeleted = true;
                _rardbContext.SaveChanges();


                var carToUpdate = _rardbContext.TBL_Cars.Find(logToDelete.carID);
                var latestLog = await _rardbContext.TBL_CarLogs
                        .Where(log => log.carID == carToUpdate.carID && !log.LogIsDeleted)
                        .OrderByDescending(log => log.LogDate)
                        .FirstOrDefaultAsync();
                carToUpdate.carMileage = latestLog.LogMileage;
                carToUpdate.carLastLogDate = latestLog.LogDate;

                var latestOilChangeLog = await _rardbContext.TBL_CarLogs
                        .Where(log => log.carID == carToUpdate.carID && log.LogType == 3 && !log.LogIsDeleted)
                        .OrderByDescending(log => log.LogDate)
                        .FirstOrDefaultAsync();

                if (latestOilChangeLog != null)
                {
                    carToUpdate.carLastChangeOilMileage = latestOilChangeLog.LogMileage;
                }
                else
                {
                    carToUpdate.carLastChangeOilMileage = 0;
                }

                var latestMaintenance = await _rardbContext.TBL_CarLogs
                        .Where(log => log.carID == carToUpdate.carID && log.LogType == 2 && !log.LogIsDeleted)
                        .OrderByDescending(log => log.LogDate)
                        .FirstOrDefaultAsync();
                if (latestMaintenance != null)
                {
                    carToUpdate.carLastMaintenance = latestMaintenance.LogDate;
                }
                else
                {
                    carToUpdate.carLastMaintenance = null;
                }


                await _rardbContext.SaveChangesAsync();
                return new JsonResult(new { success = true });
            }
            else
            {
                return new JsonResult(new { success = false, message = "An error occurred with deleting log" });
            }
        }
        public async Task<IActionResult> AddListing([FromForm] IFormCollection form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int addListingCarID = Int32.Parse(form["listingaddCarID"]);
                    var Car = _rardbContext.TBL_Cars.Find(addListingCarID);
                    DateTime.TryParse(form["listingsaddEndDate"], out DateTime tempaddListingEndDate);
                    DateTime? addListingEndDate = !string.IsNullOrEmpty(form["listingsaddEndDate"]) ? tempaddListingEndDate : (DateTime?)null;
                    var newListingStart = DateTime.Parse(form["listingaddStartDate"]);

                    var activeIndefiniteListing = await _rardbContext.TBL_Listings.FirstOrDefaultAsync(l => l.carID == addListingCarID && l.listingIsActive == true && l.listingAvailabilityEnd == null);

                    if (activeIndefiniteListing != null)
                    {
                        // There is an active indefinite listing, return an error
                        return new JsonResult(new { success = false, message = "There is an active indefinite listing for this car." });
                    }

                    // Check if the starting date is later than the end date
                    if (addListingEndDate.HasValue && newListingStart > addListingEndDate)
                    {
                        return new JsonResult(new { success = false, message = "The starting date cannot be later than the end date." });
                    }

                    // Check for overlapping listings
                    var overlappingListings = await _rardbContext.TBL_Listings
                        .Where(l => l.carID == addListingCarID && l.listingIsActive == true && l.listingAvailabilityStart < addListingEndDate && newListingStart < l.listingAvailabilityEnd)
                        .ToListAsync();

                    if (overlappingListings.Any())
                    {
                        // There are overlapping listings, return an error
                        return new JsonResult(new { success = false, message = "The new listing overlaps with an existing listing." });
                    }

                    var model = new AdminPartialViewModel
                    {
                        AddListing = new ListingsAddModel
                        {
                            listingaddCarID = addListingCarID,
                            listingaddHourlyPrice = Decimal.Parse(form["listingaddHourlyPrice"]),
                            listingaddDailyPrice = Decimal.Parse(form["listingaddDailyPrice"]),
                            listingaddWeeklyPrice = Decimal.Parse(form["listingaddWeeklyPrice"]),
                            listingaddMonthlyPrice = Decimal.Parse(form["listingaddMonthlyPrice"]),
                            listingaddStartDate = newListingStart, //
                            listingsaddEndDate = addListingEndDate,  //
                            listingaddDetails = form["listingaddDetails"]
                        }
                    };

                    var newlisting = new ListingsDBModel
                    {
                        carID = addListingCarID,
                        listingHourlyPrice = model.AddListing.listingaddHourlyPrice,
                        listingDailyPrice = model.AddListing.listingaddDailyPrice,
                        listingWeeklyPrice = model.AddListing.listingaddWeeklyPrice,
                        listingMonthlyPrice = model.AddListing.listingaddMonthlyPrice,
                        listingAvailabilityStart = model.AddListing.listingaddStartDate,
                        listingAvailabilityEnd = model.AddListing.listingsaddEndDate,
                        listingDetails = model.AddListing.listingaddDetails,
                        listingIsActive = true,
                        listingStatus = 1
                    };

                    _rardbContext.TBL_Listings.Add(newlisting);
                    _rardbContext.SaveChanges();

                    var logAdd = new CarLogsDBModel
                    {
                        carID = Car.carID,
                        LogDate = DateTime.Now,
                        LogMileage = Car.carMileage,
                        LogType = 9,
                        LogDetails = $"Listed: \n" +
                                     $"Hourly-{model.AddListing.listingaddHourlyPrice} \n " +
                                     $"Daily-{model.AddListing.listingaddDailyPrice} \n" +
                                     $"Weekly-{model.AddListing.listingaddWeeklyPrice} \n" +
                                     $"Monthly-{model.AddListing.listingaddMonthlyPrice} \n" +
                                     $"StartDate: {model.AddListing.listingaddStartDate} \n" +
                                     $"StartDate: {model.AddListing.listingsaddEndDate} \n"
                    };

                    _rardbContext.TBL_CarLogs.Add(logAdd);
                    _rardbContext.SaveChanges();

                    await _rardbContext.SaveChangesAsync();
                    return new JsonResult(new { success = true });
                }
                else
                {
                    ViewBag.ErrorMessage = "An error occurred with adding listing";
                    return new JsonResult(new { success = false, message = "An error occurred with adding listing" });
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message.ToString();
                return new JsonResult(new { success = false, message = errorMessage });
            }
        }
        public async Task<IActionResult> EditListing([FromForm] IFormCollection form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = new AdminPartialViewModel{
                        EditListing = new ListingsEditModel
                        {
                            listingeditID = Int32.Parse(form["listingeditID"]),
                            listingeditHourlyPrice = Decimal.Parse(form["listingeditHourlyPrice"]),
                            listingeditDailyPrice = Decimal.Parse(form["listingeditDailyPrice"]),
                            listingeditWeeklyPrice = Decimal.Parse(form["listingeditWeeklyPrice"]),
                            listingeditMonthlyPrice = Decimal.Parse(form["listingeditMonthlyPrice"]),
                            listingeditDetails = form["listingeditDetails"],
                            listingeditStatus = Int32.Parse(form["listingeditStatus"])
                        }
                    };

                    var ListingToEdit = _rardbContext.TBL_Listings.Find(model.EditListing.listingeditID);
                    ListingToEdit.listingHourlyPrice = model.EditListing.listingeditHourlyPrice;
                    ListingToEdit.listingDailyPrice = model.EditListing.listingeditDailyPrice;
                    ListingToEdit.listingWeeklyPrice = model.EditListing.listingeditWeeklyPrice;
                    ListingToEdit.listingMonthlyPrice = model.EditListing.listingeditMonthlyPrice;
                    ListingToEdit.listingDetails = model.EditListing.listingeditDetails;
                    ListingToEdit.listingStatus = model.EditListing.listingeditStatus;
                    _rardbContext.SaveChanges();

                    await _rardbContext.SaveChangesAsync();
                    return new JsonResult(new { success = true });
                }
                else
                {
                    return new JsonResult(new { success = false, message = "An error occurred with editing listing" });
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message.ToString();
                return new JsonResult(new { success = false, message = errorMessage });
            }
        }
        public async Task<IActionResult> DeleteListing(int listingId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var ListingToEdit = _rardbContext.TBL_Listings.Find(listingId);
                    ListingToEdit.listingIsActive = false;
                    _rardbContext.SaveChanges();

                    await _rardbContext.SaveChangesAsync();
                    return new JsonResult(new { success = true });
                }
                else
                {
                    return new JsonResult(new { success = false, message = "An error occurred with deleting listing" });
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message.ToString();
                return new JsonResult(new { success = false, message = errorMessage });
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddNewOrder([FromForm] IFormCollection form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = new AdminPartialViewModel
                    {
                        AddOrder = new OrderAddModel
                        {
                            orderaddFromAdmin = bool.Parse(form["orderaddFromAdmin"]),
                            orderaddListingID = Int32.Parse(form["orderaddListingID"]),
                            orderaddUserID = form["orderaddUserID"],
                            orderaddDriverID = Int32.Parse(form["orderaddDriverID"]),
                            orderaddStart = DateTime.Parse(form["orderaddStart"]),
                            orderaddEnd = DateTime.Parse(form["orderaddEnd"]),
                            orderaddPaymentID = Int32.Parse(form["orderaddPaymentID"]),
                            orderaddStatusID = Int32.Parse(form["orderaddStatusID"]),
                            orderaddPaymentIMG = form.Files["orderaddPaymentIMG"],
                            orderaddCost = decimal.Parse(form["orderaddCost"]),
                            orderaddExtraFee = decimal.Parse(form["orderaddExtraFee"]),
                            orderaddLocationLimit = form["orderaddLocationLimit"],
                            orderaddNotes = form["orderaddNotes"]
                        }
                    };
                    DateTime? PayDate = DateTime.Now;
                    if (!model.AddOrder.orderaddFromAdmin && model.AddOrder.orderaddPaymentID == 1)
                    {
                        PayDate = null;
                    }

                    var orderAdd = new OrdersDBModel
                    {
                        listingID = model.AddOrder.orderaddListingID,
                        userID = model.AddOrder.orderaddUserID,
                        driverID = model.AddOrder.orderaddDriverID,
                        orderBookDate = DateTime.Now,
                        orderPickupDate = model.AddOrder.orderaddStart,
                        orderReturnDate = model.AddOrder.orderaddEnd,
                        orderPaymentMethod = model.AddOrder.orderaddPaymentID,
                        orderPaymentDate = PayDate,
                        orderStatus = model.AddOrder.orderaddStatusID,
                        orderTotalCost = model.AddOrder.orderaddCost,
                        orderExtraFees = model.AddOrder.orderaddExtraFee,
                        orderReservationID = _userServices.GenerateReceiptNumber(),
                        orderLocationLimit = model.AddOrder.orderaddLocationLimit,
                        orderNotes = model.AddOrder.orderaddNotes,
                        orderReview = null,
                        orderPaymentIMG = "Pending...",
                        orderPaymentExt = "Pending..."

                    };
                    _rardbContext.TBL_Orders.Add(orderAdd);
                    _rardbContext.SaveChanges();

                    var orderPOPeImgUpload = _fileServices.ProcessEncryptUploadedFile(model.AddOrder.orderaddPaymentIMG, ImageCategories.imgProofOP);

                    var orderPOPFileExt = _fileServices.GetFileExtension(model.AddOrder.orderaddPaymentIMG);

                    var driverToUpdate = _rardbContext.TBL_Orders.Find(orderAdd.orderID);
                    driverToUpdate!.orderPaymentIMG = orderPOPeImgUpload!;
                    driverToUpdate!.orderPaymentExt = orderPOPFileExt!;

                    await _rardbContext.SaveChangesAsync();
                    return new JsonResult(new { success = true });
                }
                else
                {
                    return new JsonResult(new { success = false, message = "An error occurred with adding order" });

                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message.ToString();
                return new JsonResult(new { success = false, message = errorMessage });
            }
        }
        public async Task<IActionResult> OrderVerify(int orderId, int statusInt)
        {
            try
            {
                var order = _rardbContext.TBL_Orders.Find(orderId);
                if (order == null)
                {
                    return new JsonResult(new { success = false });
                }

                
                order!.orderStatus = statusInt;

                await _rardbContext.SaveChangesAsync();
                return new JsonResult(new { success = true });
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message.ToString();
                return new JsonResult(new { success = false, message = errorMessage});
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetListingDates(int listingId)
        {
            //var listing = await _rardbContext.TBL_Listings
            //    .Where(l => l.listingID == listingId)
            //    .Select(l => new { l.listingAvailabilityStart, l.listingAvailabilityEnd })
            //    .FirstOrDefaultAsync();

            //var orderDates = await _rardbContext.TBL_Orders
            //    .Where(o => o.listingID == listingId && (o.orderStatus == 2 || o.orderStatus == 4))
            //    .Select(o => new { o.orderPickupDate, o.orderReturnDate })
            //    .ToListAsync();

            //return Json(new { listing, orderDates });

                var listing = await _rardbContext.TBL_Listings.FindAsync(listingId);
                if (listing == null)
                {
                    return NotFound();
                }

                var orderDates = await _rardbContext.TBL_Orders
                    .Where(o => o.listingID == listingId && (o.orderStatus == 2 || o.orderStatus == 4))
                    .Select(o => new { o.orderPickupDate, o.orderReturnDate })
                    .ToListAsync();

                return Ok(new
                {
                    listingStartDate = listing.listingAvailabilityStart.ToString("yyyy-MM-ddTHH:mm:ss"),
                    listingEndDate = listing.listingAvailabilityEnd?.ToString("yyyy-MM-ddTHH:mm:ss"),
                    orderDates = orderDates.Select(d => new 
                    {
                        StartDate = d.orderPickupDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                        EndDate = d.orderReturnDate.ToString("yyyy-MM-ddTHH:mm:ss")
                    })
                });
        }
    }
}
