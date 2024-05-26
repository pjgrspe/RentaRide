using Microsoft.VisualBasic.FileIO;
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
        public string? userVMLicenseIMGType => GetFormattedExtension(userVMLicenseExt);
        public string? userVMLicenseBackIMGType => GetFormattedExtension(userVMLicenseBackExt);
        public string? userVM2ndValidIDIMGType => GetFormattedExtension(userVM2ndValidIDExt);
        public string? userVMProofofBillingIMGType => GetFormattedExtension(userVMProofofBillingExt);
        public string? userVMSelfieProofIMGType => GetFormattedExtension(userVMSelfieProofExt);
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
        public string? userVMLicense => GetIMGSource(userVMLicenseIMG, userVMLicenseIMGType);
        public string? userVMLicenseBack => GetIMGSource(userVMLicenseBackIMG, userVMLicenseBackIMGType);
        public string? userVM2ndValidID => GetIMGSource(userVM2ndValidIDIMG, userVM2ndValidIDIMGType);
        public string? userVMProofofBilling => GetIMGSource(userVMProofofBillingIMG, userVMProofofBillingIMGType);
        public string? userVMSelfieProof => GetIMGSource(userVMSelfieProofIMG, userVMSelfieProofIMGType);
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
        public string userVMFullName
        {
            get
            {
                if (userVMMiddleName == null)
                {
                    return userVMFirstName + " " + userVMLastName;
                }
                else
                {
                    return userVMFirstName + " " + userVMMiddleName + " " + userVMLastName;
                }
            }
        }
    }
}
