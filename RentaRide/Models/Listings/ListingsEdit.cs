namespace RentaRide.Models.Listings
{
    public class ListingsEdit
    {
        public int listingeditID { get; set; }
        public decimal listingeditHourlyPrice { get; set; }
        public decimal listingeditDailyPrice { get; set; }
        public decimal listingeditWeeklyPrice { get; set; }
        public decimal listingeditMonthlyPrice { get; set; }
        public int listingeditStatus { get; set; }
        public string? listingeditDetails { get; set; }
    }
}
