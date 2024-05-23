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
        public string? driverVMImage { get; set; }
        public string? driverVMLicense { get; set; }
        public string? driverVMLicenseBack { get; set; }
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
