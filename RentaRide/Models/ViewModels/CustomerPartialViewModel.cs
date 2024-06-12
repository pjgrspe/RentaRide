using Microsoft.AspNetCore.Mvc.Rendering;
using RentaRide.Models.Orders;

namespace RentaRide.Models.ViewModels
{
    public class CustomerPartialViewModel
    {    
        public List<ListingsViewModel> Listings { get; set; } = new List<ListingsViewModel>();
        public CustomerInfoViewModel CustomerInfo { get; set; }
        public OrderAddModel AddOrder { get; set; } = new OrderAddModel();
        public SelectList WithDriverSelectList { get; set; }
    }
}
