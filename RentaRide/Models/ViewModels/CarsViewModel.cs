using RentaRide.Utilities;

namespace RentaRide.Models.ViewModels
{
    public class CarsViewModel
    {
        public int carVMID { get; set; }
        public string carVMPictureIMG{ get; set; }
        public string carVMPictureExt { get; set; }
        public string carVMPictureIMGType => ViewModelTools.GetFormattedExtension(carVMPictureExt);
        public string carVMPicture => ViewModelTools.GetIMGSource(carVMPictureIMG, carVMPictureIMGType);
        public string carVMMake { get; set; }
        public string carVMModel { get; set; }
        public int carVMYear { get; set; }
        public bool carVMTransmission { get; set; }
        public string carVMColor { get; set; }
        public int carVMTypeID { get; set; }
        public string carVMType { get; set; }
        public int carVMMileage { get; set; }
        public int carVMFuelType { get; set; }
        public string carVMFuelTypeName
        {
            get
            {
                if (carVMFuelType <= TypeNamesUtilities.fuelTypeNames.Length)
                {
                    return TypeNamesUtilities.fuelTypeNames[carVMFuelType];
                }
                else
                {
                    return "Unknown type";
                }
            }
        }
        public int carVMStatus { get; set; }
        public string carVMStatusName
        {
            get 
            {
                if (carVMStatus <= TypeNamesUtilities.carStatusNames.Length) 
                { 
                    return TypeNamesUtilities.carStatusNames[carVMStatus];
                }
                else
                {
                    return "Unknown type";
                }
            }
        }



        public int carVMLastChangeOilMileage { get; set; } //
        public int carVMOilChangeInterval { get; set; } //
        public string carVMPlateNumber { get; set; }

        public bool carVMOilChangeDue
        {
            get
            {
                if (carVMMileage - carVMLastChangeOilMileage >= carVMOilChangeInterval)
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
