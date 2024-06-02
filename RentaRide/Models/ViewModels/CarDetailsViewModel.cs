using System.ComponentModel.DataAnnotations;

namespace RentaRide.Models.ViewModels
{
    public class CarDetailsViewModel
    {
        public int cardeetsVM { get; set; }
        public string cardeetsVMMake { get; set; }
        public string cardeetsVMModel { get; set; }
        public int cardeetsVMYear { get; set; }
        public bool? cardeetsVMTransmission { get; set; }

        public string cardeetsVMCarType { get; set; }
        public string cardeetsVMColor { get; set; }
        public string cardeetsVMLicense { get; set; }
        public int cardeetsVMMileage { get; set; }
        public DateTime cardeetsVMLastLog { get; set; }
        public bool? cardeetsVMStatusID { get; set; }
        public string? cardeetsVMStatus 
        {
            get
            {
                if (cardeetsVMStatusID == true)
                {
                    return "Available";
                    
                }
                else if (cardeetsVMStatusID == false)
                {
                    return "Rented";
                }
                else
                {
                    return "Maintenance";

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
    }
}
