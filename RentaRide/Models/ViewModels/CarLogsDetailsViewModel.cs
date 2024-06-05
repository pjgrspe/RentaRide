using RentaRide.Database.Database_Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RentaRide.Utilities;

namespace RentaRide.Models.ViewModels
{
    public class CarLogsDetailsViewModel
    {
        public int carlogDeetsVMID { get; set; }
        public DateTime carlogDeetsVMDate { get; set; }
        public int carlogDeetsVMMileage { get; set; }
        public int carlogDeetsVMTypeID { get; set; }
        public string carlogDeetsVMType
        {
            get
            {
                return LogTypes.logTypeNames[carlogDeetsVMTypeID];
            }
        }
        public string carlogDeetsVMDetails { get; set; }
    }
}
