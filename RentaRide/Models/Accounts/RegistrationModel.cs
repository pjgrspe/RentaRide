using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RentaRide.Models.Accounts
{
    public class RegistrationModel
    {
        [Required]
        [DisplayName("Email")]
        public string regmodelEmail { get; set; }
        [Required]
        [DisplayName("Username")]
        [StringLength(50, ErrorMessage = "The {0} must atleast be {2} and at max {1} characters long", MinimumLength = 6)]
        public string regmodelUsername { get; set; }
        [Required]
        [DisplayName("Password")]
        [StringLength(100, ErrorMessage = "The {0} must atleast be {2} and at max {1} characters long", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\\W_]).{6,}$", ErrorMessage = "Password must contain atleast 1 Uppercase letter, Numerical characters, and special character")]
        public string regmodelPassword { get; set; }
        [Required]
        [DisplayName("Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("regmodelPassword", ErrorMessage = "Passwords does not match")]
        public string regmodelConfirmPassword { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string regmodelFirstName { get; set; }
        [DisplayName("Middle Name")]
        public string regmodelMiddleName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string regmodelLastName { get; set; }
        [Required]
        [DisplayName("Date of Birth")]
        public DateTime regmodelDOB { get; set; }
        [Required]
        [DisplayName("Street Address")]
        public string regmodelStreetAdd { get; set; }
        [Required]
        [DisplayName("Municipality/City")]
        public string regmodelCityAdd { get; set; }
        [Required]
        [DisplayName("Province")]
        public string regmodelProvinceAdd { get; set; }
        [Required]
        [DisplayName("Contact")]
        public string regmodelContact { get; set; }
        //[Required]
        //[DisplayName("Driver's License")]
        //public string? regmodelLicense { get; set; }
        //[Required]
        //[DisplayName("Secondary Valid ID")]
        //public string? regmodel2ndValidID { get; set; }
        //[Required]
        //[DisplayName("Proof of Billing")]
        //public string? regmodelPOB { get; set; }
        //[Required]
        //[DisplayName("Selfie with ID")]
        //public string? regmodelSelfieProof { get; set; }
    }
}
