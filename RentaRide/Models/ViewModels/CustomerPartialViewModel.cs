using RentaRide.Models.Orders;

namespace RentaRide.Models.ViewModels
{
    public class CustomerPartialViewModel
    {
        public List<ListingsViewModel> Listings { get; set; } = new List<ListingsViewModel>();
        public CustomerInfoViewModel CustomerInfo { get; set; }
        public OrderAddModel AddOrder { get; set; }
    }
}
