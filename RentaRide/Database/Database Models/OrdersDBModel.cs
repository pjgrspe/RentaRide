using RentaRide.Models.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentaRide.Database.Database_Models
{
    public class OrdersDBModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int orderID { get; set; }
        public int carID { get; set; }
        [ForeignKey("carID")]
        public CarsDBModel CarsDBModel { get; set; }
        public string userID { get; set; }
        [ForeignKey("userID")]
        public RentaRideAppUsers RentaRideAppUsers { get; set; }
        public int driverID { get; set; }
        [ForeignKey("driverID")]
        public DriversDBModel DriversDBModel { get; set; }
        //public int orderReceiptID <-- unsure what this is for yet
        [Required]
        public DateTime orderBookDate { get; set; }
        [Required]
        public DateTime orderPickupDate { get; set; }
        public DateTime? orderReturnDate { get; set; }
        public bool? orderStatus { get; set; }
        //Order Status
        //-n. Pending
        //-0. Cancelled
        //-1. Confirmed
        public decimal orderTotalCost { get; set; } = 0.00m;
        public decimal orderExtraFees { get; set; } = 0.00m;
        [Required]
        public string orderPickupLocation { get; set; }
        [Required]
        public string orderReturnLocation { get; set; }
        [Required]
        public int orderPaymentMethod { get; set; }
        [ForeignKey("orderPaymentMethod")]
        public PayTypesDBModel PayTypesDBModel { get; set; }
        public DateTime? orderPaymentDate { get; set; }
        public string? orderReservationID { get; set; }
        public int? orderRating { get; set; }
        [ForeignKey("orderRating")]
        public RatesDBModel RatesDBModel { get; set; }
        public int? orderReview { get; set; }
        public string? orderNotes { get; set; }
        public string? orderLocationLimit { get; set; }

    }
}
