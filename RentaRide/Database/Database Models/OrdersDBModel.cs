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
        public int listingID { get; set; }
        [ForeignKey("listingID")]
        public ListingsDBModel ListingsDBModel { get; set; }
        public string userID { get; set; }
        [ForeignKey("userID")]
        public RentaRideAppUsers RentaRideAppUsers { get; set; }
        public int driverID { get; set; }
        [ForeignKey("driverID")]
        public DriversDBModel DriversDBModel { get; set; }
        [Required]
        public DateTime orderBookDate { get; set; }
        [Required]
        public DateTime orderPickupDate { get; set; }
        public DateTime? orderReturnDate { get; set; }
        public int orderStatus { get; set; }
        public decimal orderTotalCost { get; set; } = 0.00m;
        public decimal orderExtraFees { get; set; } = 0.00m;
        [Required]
        public int orderPaymentMethod { get; set; }
        public DateTime? orderPaymentDate { get; set; }
        public string orderReservationID { get; set; }
        public int? orderReview { get; set; }
        public string? orderNotes { get; set; }
        public string orderLocationLimit { get; set; }
        public string? orderPaymentIMG { get; set; }
        public string? orderPaymentExt { get; set; }

    }
}
