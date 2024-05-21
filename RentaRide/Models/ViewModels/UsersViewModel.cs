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
    }
}
