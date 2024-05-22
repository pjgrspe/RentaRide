using RentaRide.Models.Accounts;

namespace RentaRide.Models.ViewModels
{
    public class AdminPartialViewModel
    {
        public List<UsersViewModel> Users { get; set; }
        public List<DriversViewModel> Drivers { get; set; }
        public AddDriverModel AddDriver { get; set; }
    }
}
