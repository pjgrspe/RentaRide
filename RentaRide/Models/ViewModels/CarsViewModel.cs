namespace RentaRide.Models.ViewModels
{
    public class CarsViewModel
    {
        public int carVMID { get; set; }
        public string carVMPictureIMG{ get; set; }
        public string carVMORDocIMG { get; set; }
        public string carVMCRDocIMG { get; set; }
        public string carVMPictureExt { get; set; }
        public string carVMORDocExt { get; set; }
        public string carVMCRDocExt { get; set; }
        public string carVMPictureIMGType => ViewModelTools.GetFormattedExtension(carVMPictureExt);
        public string carVMORDocIMGType => ViewModelTools.GetFormattedExtension(carVMORDocExt);
        public string carVMCRDocIMGType => ViewModelTools.GetFormattedExtension(carVMCRDocExt);
        public string carVMPicture => ViewModelTools.GetIMGSource(carVMPictureIMG, carVMPictureIMGType);
        public string carVMPORDoc => ViewModelTools.GetIMGSource(carVMORDocIMG, carVMORDocIMGType);
        public string carVMPCRDoc => ViewModelTools.GetIMGSource(carVMCRDocIMG, carVMCRDocExt);
        public string carVMMake { get; set; }
        public string carVMModel { get; set; }
        public int carVMYear { get; set; }
        public bool carVMTransmission { get; set; }
        public string carVMColor { get; set; }
        public int carVMTypeID { get; set; }
        public string carVMType { get; set; }
        public int carVMMileage { get; set; }
        public bool? carVMFuelType { get; set; }
        public bool? carVMStatus { get; set; }
        public bool carVMisActive { get; set; }
        public string? carVMInactiveInfo { get; set; }
        public int carVMLastChangeOilMileage { get; set; }
        public int carVMOilChangeInterval { get; set; }
        public bool carVMIsDeleted { get; set; } = false;
        public string carVMPlateNumber { get; set; }
        public DateTime carVMDateRegistered { get; set; }
        public DateTime? carVMLastLogDate { get; set; }
        public string carVMFormattedDateRegistered => ViewModelTools.GetFormattedDate(carVMDateRegistered);
        public string carVMFormattedLastLogDate => ViewModelTools.GetFormattedDate(carVMLastLogDate);

    }
}
