using RentaRide.Utilities;

namespace RentaRide.Models.ViewModels
{
    public class CarLogsViewModel
    {
        public int carLogsVMID { get; set; }
        public int carLogsVMTypeID { get; set; }
        public string carLogsVMType
        {
            get
            {
                return TypeNamesUtilities.logTypeNames[carLogsVMTypeID];
            }
        }

        public string carLogsVMTypeClass
        {
            get
            {
                return TypeNamesUtilities.logTypeclassNames[carLogsVMTypeID];
            }
        }
        public int carLogsVMMileage { get; set; }
        public DateTime carLogsVMDate { get; set; }
        public string carLogsVMFormattedDate => ViewModelTools.GetFormattedDate(carLogsVMDate);
        public string carLogsVMDetails { get; set; }
    }
}
