namespace RentaRide.Models.ViewModels
{
    public class CarImagesViewModel
    {
        public int carIMGVMID { get; set; }
        public string carIMGVMCarIMG { get; set; }
        public string carIMGVMCarExt { get; set; }
        public string carIMGVMCarIMGType => ViewModelTools.GetFormattedExtension(carIMGVMCarExt);
        public string carIMGVMPicture => ViewModelTools.GetIMGSource(carIMGVMCarIMG, carIMGVMCarIMGType);

    }
}
