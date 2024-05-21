using System.Globalization;

namespace RentaRide.Models.ViewModels
{
    public class UsersViewModel
    {
        public string? userVMID { get; set; }
        public string? userVMFirstName { get; set; }
        public string? userVMMiddleName { get; set; }
        public string? userVMLastName { get; set; }
        public string? userVMEmail { get; set; }
        public bool? userVMisApproved { get; set; }
        public bool? userVMisActive { get; set; }
        public int? userVMDetailID { get; set; }
        public DateTime? userVMDateCreated { get; set; }
        public DateTime? userVMDateLastModified { get; set; }
        public DateTime? userVMDateModified { get; set; }
        public DateTime? userVMDOB { get; set; }
        public string? userVMstreetAdd { get; set; }
        public string? userVMCityAdd { get; set; }
        public string? userVMProvinceAdd { get; set; }
        public string? userVMContact { get; set; }
        public string? userVMLicense { get; set; }
        public string? userVM2ndValidID { get; set; }
        public string? userVMProofofBilling { get; set; }
        public string? userVMSelfieProof { get; set; }

        public string? GetFormattedDate(DateTime? date)
        {
            return date?.ToString("MMMM dd, yyyy", CultureInfo.InvariantCulture);
        }

        public string? FormattedDateCreated => GetFormattedDate(userVMDateCreated);
        public string? FormattedDateLastModified => GetFormattedDate(userVMDateLastModified);
        public string? FormattedDateModified => GetFormattedDate(userVMDateModified);
        public string? FormattedDOB => GetFormattedDate(userVMDOB);

        public string userVMStatus
        {
            get
            {
                if (userVMisActive == true)
                {
                    
                    if (userVMisApproved == null)
                    {
                        return "Pending";
                    }
                    else if (userVMisApproved == true)
                    {
                        return "Approved";
                    }
                    else
                    {
                        return "Denied";
                    }
                }
                else
                {
                    return "Inactive";
                }
            }

        }

        public string userVMfilterStatus
        {
            get
            {
                if (userVMStatus == "Pending")
                {
                    return "pending";
                }else if (userVMStatus == "Approved")
                {
                    return "active";
                }
                else if (userVMStatus == "Denied")
                {
                    return "denied";
                }
                else
                {
                    return "delay";
                }
                
            }

        }
    }
}
