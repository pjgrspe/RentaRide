using RentaRide.Utilities;
using System.ComponentModel.DataAnnotations;

namespace RentaRide.Models.ViewModels
{
    public class CarDetailsViewModel
    {
        public int cardeetsVM { get; set; }
        public string cardeetsVMMake { get; set; }
        public string cardeetsVMModel { get; set; }
        public int cardeetsVMYear { get; set; }
        public bool cardeetsVMTransmission { get; set; }
        public string cardeetsVMTransType
        {
            get
            {
                if (cardeetsVMTransmission == true)
                {
                    return "Automatic";
                }
                else
                {
                    return "Manual";
                }
            }
        }
        public int cardeetsVMFuelType { get; set; }
        public string cardeetsVMFuelTypeName
        {
            get
            {
                if (cardeetsVMFuelType <= TypeNamesUtilities.fuelTypeNames.Length)
                {
                    return TypeNamesUtilities.fuelTypeNames[cardeetsVMFuelType];
                }
                else
                {
                    return "Invalid";
                }
            }
        }
        public int cardeetsVMTypeID { get; set; }
        public string cardeetsVMCarType { get; set; }
        public string cardeetsVMColor { get; set; }
        public string cardeetsVMLicense { get; set; }
        public int cardeetsVMMileage { get; set; }
        public int cardeetsVMOilChangeInterval { get; set; }
        public int cardeetsVMLastChangeOilMileage { get; set; }
        public DateTime? cardeetsVMLastMaintenance { get; set; }
        public int cardeetsVMSeats { get; set; }
        public DateTime? cardeetsVMLastLog { get; set; }
        public int cardeetsVMStatusID { get; set; }
        public string cardeetsVMStatus
        {
            get 
            {
                if (cardeetsVMStatusID <= TypeNamesUtilities.carStatusNames.Length) 
                { 
                    return TypeNamesUtilities.carStatusNames[cardeetsVMStatusID];
                }
                else
                {
                    return "Invalid";
                }
            }
        }

        public string cardeetsVMORIMG { get; set; }
        public string cardeetsVMCRIMG { get; set; }
        public string cardeetsVMORExt { get; set; }
        public string cardeetsVMCRExt { get; set; }
        public string carIMGVMORIMGType => ViewModelTools.GetFormattedExtension(cardeetsVMORExt);
        public string carIMGVMOR => ViewModelTools.GetIMGSource(cardeetsVMORIMG, carIMGVMORIMGType);
        public string carIMGVMCRIMGType => ViewModelTools.GetFormattedExtension(cardeetsVMCRExt);
        public string carIMGVMCR => ViewModelTools.GetIMGSource(cardeetsVMCRIMG, carIMGVMCRIMGType);
        public string cardeetsVMFormattedLastLog => ViewModelTools.GetFormattedDate(cardeetsVMLastLog);
        public string cardeetsVMFormattedLastMaintenance => ViewModelTools.GetFormattedDate(cardeetsVMLastMaintenance);

        public bool cardeetsVMOilChangeDue
        {
            get
            {
                if (cardeetsVMMileage - cardeetsVMLastChangeOilMileage >= cardeetsVMOilChangeInterval)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
