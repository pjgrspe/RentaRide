using RentaRide.Database.Database_Models;
using RentaRide.Models.Accounts;
using RentaRide.Models.Cars;
using RentaRide.Models.Listings;

namespace RentaRide.Models.ViewModels
{
    public class AdminPartialViewModel
    {
        public List<UsersViewModel> Users { get; set; } = new List<UsersViewModel>();
        public List<DriversViewModel> Drivers { get; set; } = new List<DriversViewModel>();
        public List<CarsViewModel> Cars { get; set; } = new List<CarsViewModel>();
        public List<ListingsViewModel> Listings { get; set; } = new List<ListingsViewModel>();
        public List<ListingCarListViewModel> ListingsCarList { get; set; } = new List<ListingCarListViewModel>();
        public List<CarTypesViewModel> CarTypes { get; set; } = new List<CarTypesViewModel>();
        public List<CarImagesViewModel> CarImages { get; set; } = new List<CarImagesViewModel>();
        public List<CarLogsViewModel> CarLogs { get; set; } = new List<CarLogsViewModel>();
        public CarLogsDetailsViewModel CarLogsDetails { get; set; } = new CarLogsDetailsViewModel();
        public CarDetailsViewModel CarDetails { get; set; } = new CarDetailsViewModel();
        public ListingDetailsViewModel ListingDetails { get; set; } = new ListingDetailsViewModel();
        public CarRatesViewModel CarRates { get; set; } = new CarRatesViewModel();
        public UserVerificationModel VerifyUser { get; set; }
        public DriverAddModel AddDriver { get; set; }
        public DriverEditModel EditDriver { get; set; }
        public DriverDelModel DelDriver { get; set; }
        public CarAddModel AddCar { get; set; } 
        public CarEditModel EditCar { get; set; }
        public CarDelModel DelCar { get; set; }
        public CarAddLogModel AddLog { get; set; }
        //public CarEditLogModel EditLog { get; set; }
        public CarDelLogModel DelLog { get; set; }
        public ListingsAdd AddListing { get; set; }
        public ListingsEdit EditListing { get; set; }

    }
}
