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
        public decimal listingHourlyPrice { get; set; } = 0.00m;
        [Required]
        public decimal listingDailyPrice { get; set; } = 0.00m;
        [Required]
        public decimal listingWeeklyPrice { get; set; } = 0.00m;
        [Required]
        public decimal listingMonthlyPrice { get; set; } = 0.00m;
        [Required]
        public int listingStatus { get; set; }
        //1 = Available,
        //2 = Hidden,
        //3 = Unavailable
        [Required]
        public bool listingIsActive { get; set; } = true;
        public DateTime listingAvailabilityStart { get; set; }
        public DateTime? listingAvailabilityEnd { get; set; }

    }
}
