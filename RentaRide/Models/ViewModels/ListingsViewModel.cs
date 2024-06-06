namespace RentaRide.Models.ViewModels
{
    public class ListingsViewModel
    {
        public int listingVMID { get; set; }
        public int carID { get; set; }
        public string carName { get; set; }
        public string carIMG { get; set; }
        public string carIMGext { get; set; }
        public string listingVMDetails { get; set; }
        public decimal listingVMHourlyPrice { get; set; }
        public decimal listingVMDailyPrice { get; set; }
        public decimal listingVMWeeklyPrice { get; set; }
        public decimal listingVMMonthlyPrice { get; set; }
        public int listingVMStatus { get; set; }
        public DateTime listingVMAvailabilityStart { get; set; }
        public DateTime? listingVMAvailabilityEnd { get; set; }

    }
}
