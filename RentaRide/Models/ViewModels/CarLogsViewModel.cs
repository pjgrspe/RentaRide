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
                if (carLogsVMTypeID == 1)
                {
                    return "Manual";
                }
                else if (carLogsVMTypeID == 2)
                {
                    return "Maintenance";
                }
                else if (carLogsVMTypeID == 3)
                {
                    return "Change Oil";
                }else if (carLogsVMTypeID == 4)
                {
                    return "Repair";
                }
                else
                {
                    return "Rented";
                }
            }
        }
        public int carLogsVMMileage { get; set; }
        public DateTime carLogsVMDate { get; set; }
        public string carLogsVMFormattedDate => ViewModelTools.GetFormattedDate(carLogsVMDate);
        
        public string carLogsVMDetails { get; set; }
    }
}
