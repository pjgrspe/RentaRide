using System.Globalization;

namespace RentaRide.Models.ViewModels
{
    public class DriversViewModel
    {
        public int? driverVMID { get; set; }
        public string? driverVMFirstName { get; set; }
        public string? driverVMMiddleName { get; set; }
        public string? driverVMLastName { get; set; }
        public string? driverVMFullName => ViewModelTools.ConvertToFullname(driverVMFirstName, driverVMMiddleName, driverVMLastName);
        public string? driverVMEmail { get; set; }
        public string? driverVMContact { get; set; }
        
        public string? driverVMImageIMG { get; set; }
        public string? driverVMLicenseIMG { get; set; }
        public string? driverVMLicenseBackIMG { get; set; }
        public string? driverVMImageExt { get; set; }
        public string? driverVMLicenseExt { get; set; }
        public string? driverVMLicenseBackExt { get; set; }
        public string? driverVMImageIMGType => ViewModelTools.GetFormattedExtension(driverVMImageExt);
        public string? driverVMLicenseIMGType => ViewModelTools.GetFormattedExtension(driverVMLicenseExt);
        public string? driverVMLicenseBackIMGType => ViewModelTools.GetFormattedExtension(driverVMLicenseBackExt);
        public string? driverVMImage => ViewModelTools.GetIMGSource(driverVMImageIMG, driverVMImageIMGType);
        public string? driverVMLicense => ViewModelTools.GetIMGSource(driverVMLicenseIMG, driverVMLicenseIMGType);
        public string? driverVMLicenseBack => ViewModelTools.GetIMGSource(driverVMLicenseBackIMG, driverVMLicenseBackIMGType);
        public DateTime driverVMDateCreated { get; set; }
        public DateTime? driverVMDateLastDutyDate { get; set; }

        public string? FormattedDateCreated => ViewModelTools.GetFormattedDate(driverVMDateCreated);
        public string? FormattedDateLastDuty => ViewModelTools.GetFormattedDate(driverVMDateLastDutyDate);
        
        public bool driverVMOnDuty { get; set; }
        public bool driverVMIsActive { get; set; }
        
        public string? driverVMStatus 
        {
            get
            {
                if (driverVMIsActive == true)
                {
                    
                    if (driverVMOnDuty == true)
                    {
                        return "On Duty";
                    }
                    else
                    {
                        return "Active";
                    }
                }
                else
                {
                    return "Inactive";
                }
            }

        }
        public string driverVMfilterStatus
        {
            get
            {
                if (driverVMStatus == "Active")
                {
                    return "active";
                }else if (driverVMStatus == "On Duty")
                {
                    return "onduty";
                }
                else
                {
                    return "delay";
                }
                
            }

        }
    }
}
