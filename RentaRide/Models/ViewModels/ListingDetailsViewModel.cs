using RentaRide.Utilities;

namespace RentaRide.Models.ViewModels
{
    public class ListingDetailsViewModel
    {
        public int listingdeetsVMID { get; set; }
        public decimal? listingdeetsVMHourlyRate { get; set; }
        public decimal listingdeetsVMDailyRate { get; set; }
        public decimal listingdeetsVMWeeklyRate { get; set; }
        public decimal listingdeetsVMMonthlyRate { get; set; }
        public string? listingdeetsVMDetails { get; set; }
        public int listingdeetsVMStatus { get; set; }

        public string listingdeetsVMStatusName
        {
            get
            {
                if (listingdeetsVMStatus <= TypeNamesUtilities.ListingStatusNames.Length)
                {
                    return TypeNamesUtilities.ListingStatusNames[listingdeetsVMStatus];
                }
                else
                {
                    return "Invalid Type";
                }
            }
        }

    }
}
