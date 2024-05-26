using RentaRide.Models.Accounts;

namespace RentaRide.Models.ViewModels
{
    public class AdminPartialViewModel
    {
        public List<UsersViewModel> Users { get; set; } = new List<UsersViewModel>();
        public List<DriversViewModel> Drivers { get; set; } = new List<DriversViewModel>();
        public DriverAddModel AddDriver { get; set; }
        public DriverEditModel EditDriver { get; set; }
        public DriverDelModel DelDriver { get; set; }
        public UserVerificationModel VerifyUser { get; set; }
    }
}
