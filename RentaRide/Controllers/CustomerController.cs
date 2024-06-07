using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentaRide.Database;
using RentaRide.Database.Database_Models;
using RentaRide.Models.Identity;
using RentaRide.Models.Orders;
using RentaRide.Models.ViewModels;
using RentaRide.Services;
using RentaRide.Utilities;
using System.Drawing.Printing;

namespace RentaRide.Controllers
{
    public class CustomerController : Controller
    {

        private readonly ILogger<CustomerController> _logger;
        private readonly RARdbContext _rardbContext;
        private readonly IUserServices _userServices;
        private readonly IFileServices _fileServices;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;
        private readonly UserManager<RentaRideAppUsers> _userManager;
        public CustomerController(ILogger<CustomerController> logger, RARdbContext rardbContext, IUserServices userServices, UserManager<RentaRideAppUsers> userManager, IFileServices fileServices, IConfiguration configuration, IWebHostEnvironment environment)
        {
            _logger = logger;
            _rardbContext = rardbContext;
            _userServices = userServices;
            _fileServices = fileServices;
            _configuration = configuration;
            _environment = environment;
            _userManager = userManager;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var customerViewModel = new CustomerPartialViewModel();
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
            var userInfo = new CustomerInfoViewModel
            {
                UserId = user.Id,
                UserName = user.userFirstName,
                UserEmail = user.Email!,
                UserIsApproved = user.userisApproved,

            };
            customerViewModel.CustomerInfo = userInfo;
            customerViewModel.Listings = listings;



            return View(customerViewModel);
            //return PartialView($"~/Views/Customer/Components/Content.cshtml", customerViewModel);

        }

        public async Task<IActionResult> GetListingDates(int listingId)
        {
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
        public async Task<IActionResult> GetListingDetails(int listingId)
        {
            // Fetch the car details from the database using the carId
            var listing = await _rardbContext.TBL_Listings
                                            .FirstOrDefaultAsync(l => l.listingID == listingId);

            if (listing == null)
            {
                return Json(new { success = false, message = "invalid listing" });
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
        public async Task<IActionResult> AddNewOrder([FromForm] IFormCollection form)
        {
            try
            {
                if (ModelState.IsValid)
                {


                    var model = new CustomerPartialViewModel
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
                    int? orderaddDriverID = null;
                    if (!string.IsNullOrEmpty(form["orderaddDriverID"]))
                    {
                        orderaddDriverID = Int32.Parse(form["orderaddDriverID"]);
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

    }
}
