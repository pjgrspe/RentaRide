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

                if(carlogDeetsVMTypeID <= TypeNamesUtilities.logTypeNames.Length)
                {
                    return TypeNamesUtilities.logTypeNames[carlogDeetsVMTypeID];
                }
                else
                {
                    return "Invalid Type";
                }
            }
        }
        public string carlogDeetsVMDetails { get; set; }
    }
}
