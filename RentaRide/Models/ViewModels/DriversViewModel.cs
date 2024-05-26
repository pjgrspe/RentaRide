using System.Globalization;

namespace RentaRide.Models.ViewModels
{
    public class DriversViewModel
    {
        public int? driverVMID { get; set; }
        public string? driverVMFirstName { get; set; }
        public string? driverVMMiddleName { get; set; }
        public string? driverVMLastName { get; set; }
        public string? driverVMFullName 
        {
            get
            {
                if (driverVMMiddleName == null)
                {
                    return driverVMFirstName + " " + driverVMLastName;
                }
                else
                {
                    return driverVMFirstName + " " + driverVMMiddleName + " " + driverVMLastName;
                }
            }
        }
        public string? driverVMEmail { get; set; }
        public string? driverVMContact { get; set; }
        
        public string? driverVMImageIMG { get; set; }
        public string? driverVMLicenseIMG { get; set; }
        public string? driverVMLicenseBackIMG { get; set; }
        public string? driverVMImageExt { get; set; }
        public string? driverVMLicenseExt { get; set; }
        public string? driverVMLicenseBackExt { get; set; }
        public string? GetFormattedExtension(string? ext)
        {
            if (ext == ".jpg" || ext == ".jpeg")
            {
                return "jpeg";


            }else if (ext == ".png")
            {
                return "png";


            }else if (ext == ".gif")
            {
                return "gif";


            }else if (ext == ".bmp")
            {
                return "bmp";


            }else if (ext == ".svg")
            {
                return "svg+html";


            }else if (ext == ".webp")
            {
                return "webp";


            }
            else
            {
                return null;
            }
        }
        public string? driverVMImageIMGType => GetFormattedExtension(driverVMImageExt);
        public string? driverVMLicenseIMGType => GetFormattedExtension(driverVMLicenseExt);
        public string? driverVMLicenseBackIMGType => GetFormattedExtension(driverVMLicenseBackExt);
        public string? GetIMGSource(string? file, string? type)
        {
            if (file != null || type != null)
            {
                return $"data:image/{type};base64,{file}";
            }
            else
            {
                return null;
            }
        }
        public string? driverVMImage => GetIMGSource(driverVMImageIMG, driverVMImageIMGType);
        public string? driverVMLicense => GetIMGSource(driverVMLicenseIMG, driverVMLicenseIMGType);
        public string? driverVMLicenseBack => GetIMGSource(driverVMLicenseBackIMG, driverVMLicenseBackIMGType);

        public DateTime driverVMDateCreated { get; set; }
        public DateTime? driverVMDateLastDutyDate { get; set; }
        public string? GetFormattedDate(DateTime? date)
        {
            if (date == null)
            {
                return "No Date Recorded";
            }
            return date?.ToString("MMMM dd, yyyy", CultureInfo.InvariantCulture);
        
        }
        public string? FormattedDateCreated => GetFormattedDate(driverVMDateCreated);
        public string? FormattedDateLastDuty => GetFormattedDate(driverVMDateLastDutyDate);
        
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
