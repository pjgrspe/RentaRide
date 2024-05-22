using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace RentaRide.Database.Database_Models
{
    public class ListingsDBModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int listingID { get; set; }
        public int carID { get; set; }
        [ForeignKey("carID")]
        public CarsDBModel CarsDBModel { get; set; }
        [Required]
        public string listingDetails { get; set; }
        [Required]
        public decimal listingPrice { get; set; } = 0.00m;
        [Required]
        public int listingRate { get; set; } = 1;
        //Listing Rates
        //-1. Hourly
        //-2. Daily
        //-3. Weekly
        //-4. Monthly
        //-5. Distance-Based
        [ForeignKey("listingRate")]
        public RatesDBModel listingRatesDBModel { get; set; }
        [Required]
        public bool listingIsRented { get; set; }
        public DateTime listingAvailabilityStart { get; set; }
        public DateTime  listingAvailabilityEnd { get; set; }

    }
}
