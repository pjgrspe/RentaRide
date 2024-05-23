using RentaRide.Models.Accounts;

namespace RentaRide.Models.ViewModels
{
    public class AdminPartialViewModel
    {
        public List<UsersViewModel> Users { get; set; } = new List<UsersViewModel>();
        public List<DriversViewModel> Drivers { get; set; } = new List<DriversViewModel>();
        public AddDriverModel AddDriver { get; set; }
    }
}
