namespace RentaRide.Models.Listings
{
    public class ListingsAdd
    {
        public int listingaddCarID { get; set; }
        public DateTime listingaddStartDate { get; set; }
        public DateTime? listingsaddEndDate { get; set; }
        public decimal listingaddHourlyPrice { get; set; }
        public decimal listingaddDailyPrice { get; set; }
        public decimal listingaddWeeklyPrice { get; set; }
        public decimal listingaddMonthlyPrice { get; set; }
        public string listingaddDetails { get; set; }
    }
}
