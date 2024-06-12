using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentaRide.Database;
using RentaRide.Database.Database_Models;
using RentaRide.Models;
using RentaRide.Models.Orders;
using RentaRide.Models.ViewModels;
using RentaRide.Services;
using RentaRide.Utilities;

namespace RentaRide.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly RARdbContext _rardbContext;
        private readonly IUserServices _userServices;
        private readonly IFileServices _fileServices;

        public APIController(RARdbContext rardbContext, IUserServices userServices, IFileServices fileServices)
        {
            _rardbContext = rardbContext;
            _userServices = userServices;
            _fileServices = fileServices;
        }

        [HttpGet]
        [Route("{action}/{listingId}")]
        public async Task<IActionResult> GetListingDetails(int listingId)
        {
             var listing = await _rardbContext.TBL_Listings
                                                    .FirstOrDefaultAsync(l => l.listingID == listingId);

            if (listing == null)
            {
                return NotFound(new { success = false, message = "invalid listing" });
            }

            return Ok(new
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

        [HttpGet]
        [Route("{action}/{listingId}")]
        public async Task<IActionResult> GetListingDates(int listingId)
        {
            var listing = await _rardbContext.TBL_Listings.FindAsync(listingId);
            if (listing == null)
            {
                return NotFound(new { success = false, message = "invalid listing" });
            }

            var orderDates = await _rardbContext.TBL_Orders
                .Where(o => o.listingID == listingId && (o.orderStatus == 2 || o.orderStatus == 4))
                .Select(o => new { o.orderPickupDate, o.orderReturnDate })
                .ToListAsync();

            var result = new
            {
                listingStartDate = listing.listingAvailabilityStart.ToString("yyyy-MM-ddTHH:mm:ss"),
                listingEndDate = listing.listingAvailabilityEnd?.ToString("yyyy-MM-ddTHH:mm:ss"),
                orderDates = orderDates.Select(d => new
                {
                    startDate = d.orderPickupDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                    endDate = d.orderReturnDate.ToString("yyyy-MM-ddTHH:mm:ss")
                })
            };

            return Ok(result);
        }

        [HttpPost]
        [Route("{action}")]
        public async Task<IActionResult> AddNewOrder([FromForm] IFormCollection form)
        {
            try
            {
                int? orderDriver = null;
                bool isAdmin = bool.Parse(form["orderaddFromAdmin"]);
                if (isAdmin)
                {
                    orderDriver = Int32.Parse(form["orderaddDriverID"]);
                }
                var model = new OrderAddModel();
                    {
                        model.orderaddFromAdmin = bool.Parse(form["orderaddFromAdmin"]);
                        model.orderaddListingID = Int32.Parse(form["orderaddListingID"]);
                        model.orderaddUserID = form["orderaddUserID"];
                        model.orderaddDriverID = orderDriver;
                        model.orderaddStart = DateTime.Parse(form["orderaddStart"]);
                        model.orderaddEnd = DateTime.Parse(form["orderaddEnd"]);
                        model.orderaddPaymentID = Int32.Parse(form["orderaddPaymentID"]);
                        model.orderaddStatusID = Int32.Parse(form["orderaddStatusID"]);
                        model.orderaddPaymentIMG = form.Files["orderaddPaymentIMG"];
                        model.orderaddCost = decimal.Parse(form["orderaddCost"]);
                        model.orderaddExtraFee = decimal.Parse(form["orderaddExtraFee"]);
                        model.orderaddLocationLimit = form["orderaddLocationLimit"];
                        model.orderaddNotes = form["orderaddNotes"];
                        model.orderHasDriver = Boolean.Parse(form["orderHasDriver"]);
                    }

                if (ModelState.IsValid)
                {
                    
                    
                    DateTime? PayDate = DateTime.Now;
                    var orderPOPeImgUpload = _fileServices.ProcessEncryptUploadedFile(model.orderaddPaymentIMG, ImageCategories.imgProofOP);
                    var orderPOPFileExt = _fileServices.GetFileExtension(model.orderaddPaymentIMG);
                    if (!model.orderaddFromAdmin && model.orderaddPaymentID == 1)
                    {
                        PayDate = null;
                    }

                    var orderAdd = new OrdersDBModel
                    {
                        listingID = model.orderaddListingID,
                        userID = model.orderaddUserID,
                        driverID = orderDriver,
                        orderBookDate = DateTime.Now,
                        orderPickupDate = model.orderaddStart,
                        orderReturnDate = model.orderaddEnd,
                        orderPaymentMethod = model.orderaddPaymentID,
                        orderPaymentDate = PayDate,
                        orderStatus = model.orderaddStatusID,
                        orderTotalCost = model.orderaddCost,
                        orderExtraFees = model.orderaddExtraFee,
                        orderReservationID = _userServices.GenerateReceiptNumber(),
                        orderLocationLimit = model.orderaddLocationLimit,
                        orderNotes = model.orderaddNotes,
                        orderReview = null,
                        orderPaymentIMG = orderPOPeImgUpload!,
                        orderPaymentExt = orderPOPFileExt!,

                    };
                    _rardbContext.TBL_Orders.Add(orderAdd);
                    _rardbContext.SaveChanges();


                    await _rardbContext.SaveChangesAsync();
                    return Ok(new ResponseModel { Status = "Success", Message = "Order Added" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = "User login error" });
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message.ToString();
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Message = errorMessage });
            }
        }
    }
}
