using Microsoft.VisualBasic.FileIO;
using System.Globalization;

namespace RentaRide.Models.ViewModels
{
    public class UsersViewModel
    {
        public string userVMID { get; set; }
        public string userVMFirstName { get; set; }
        public string? userVMMiddleName { get; set; }
        public string userVMLastName { get; set; }
        public string userVMEmail { get; set; }
        public bool? userVMisApproved { get; set; }
        public bool? userVMisActive { get; set; }
        public int userVMDetailID { get; set; }
        public DateTime userVMDateCreated { get; set; }
        public DateTime? userVMDateLastModified { get; set; }
        public DateTime? userVMDateModified { get; set; }
        public DateTime userVMDOB { get; set; }
        public string? userVMstreetAdd { get; set; }
        public string? userVMCityAdd { get; set; }
        public string? userVMProvinceAdd { get; set; }
        public string? userVMContact { get; set; }
        public string? userVMLicenseIMG { get; set; }
        public string? userVMLicenseBackIMG { get; set; }
        public string? userVM2ndValidIDIMG { get; set; }
        public string? userVMProofofBillingIMG { get; set; }
        public string? userVMSelfieProofIMG { get; set; }
        public string? userVMLicenseExt { get; set; }
        public string? userVMLicenseBackExt { get; set; }
        public string? userVM2ndValidIDExt { get; set; }
        public string? userVMProofofBillingExt { get; set; }
        public string? userVMSelfieProofExt { get; set; }
        
        public string? userVMLicenseIMGType => ViewModelTools.GetFormattedExtension(userVMLicenseExt);
        public string? userVMLicenseBackIMGType => ViewModelTools.GetFormattedExtension(userVMLicenseBackExt);
        public string? userVM2ndValidIDIMGType => ViewModelTools.GetFormattedExtension(userVM2ndValidIDExt);
        public string? userVMProofofBillingIMGType => ViewModelTools.GetFormattedExtension(userVMProofofBillingExt);
        public string? userVMSelfieProofIMGType => ViewModelTools.GetFormattedExtension(userVMSelfieProofExt);
        public string? userVMLicense => ViewModelTools.GetIMGSource(userVMLicenseIMG, userVMLicenseIMGType);
        public string? userVMLicenseBack => ViewModelTools.GetIMGSource(userVMLicenseBackIMG, userVMLicenseBackIMGType);
        public string? userVM2ndValidID => ViewModelTools.GetIMGSource(userVM2ndValidIDIMG, userVM2ndValidIDIMGType);
        public string? userVMProofofBilling => ViewModelTools.GetIMGSource(userVMProofofBillingIMG, userVMProofofBillingIMGType);
        public string? userVMSelfieProof => ViewModelTools.GetIMGSource(userVMSelfieProofIMG, userVMSelfieProofIMGType);
        public string? FormattedDateCreated => ViewModelTools.GetFormattedDate(userVMDateCreated);
        public string? FormattedDateLastModified => ViewModelTools.GetFormattedDate(userVMDateLastModified);
        public string? FormattedDateModified => ViewModelTools.GetFormattedDate(userVMDateModified);
        public string? FormattedDOB => ViewModelTools.GetFormattedDate(userVMDOB);

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
        public string userVMFullName => ViewModelTools.ConvertToFullname(userVMFirstName, userVMMiddleName, userVMLastName);
    }
}
