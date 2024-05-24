using System.ComponentModel;

namespace RentaRide.Models.Accounts
{
    public class DriverEditModel
    {
        public int driveditmodelID { get; set; }
        [DisplayName("First Name")]
        public string driveditmodelFirstName { get; set; }
        [DisplayName("Middle Name")]
        public string driveditmodelMiddleName { get; set; }
        [DisplayName("Last Name")]
        public string driveditmodelLastName { get; set; }
        [DisplayName("Email")]
        public string driveditmodelEmail { get; set; }
        [DisplayName("Contact")]
        public string driveditmodelContact { get; set; }
        [DisplayName("Status")]
        public string driveditmodelStatus { get; set; }
        [DisplayName("Update Driver Picture")]
        public IFormFile driveditmodelImage { get; set; }
        [DisplayName("Update Driver's License (Front)")]
        public IFormFile driveditmodelLicense { get; set; }
        [DisplayName("Update Driver's License (Back)")]
        public IFormFile driveditmodelLicenseBack { get; set; }
    }
}
