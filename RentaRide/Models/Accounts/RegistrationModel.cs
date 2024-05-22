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
        public string? regmodelMiddleName { get; set; }
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
        [DisplayName("Driver's License (Front)")]
        [RequireBothOrNone("regmodelLicenseBack", ErrorMessage = "Both License and License Back must be provided, or both must be empty.")]

        public IFormFile? regmodelLicense { get; set; }
        [RequireBothOrNone("regmodelLicense", ErrorMessage = "Both License and License Back must be provided, or both must be empty.")]

        [DisplayName("Driver's License (Back)")]
        public IFormFile? regmodelLicenseBack { get; set; }
        [DisplayName("Secondary Valid ID")]
        public IFormFile? regmodel2ndValidID { get; set; }
        [DisplayName("Proof of Billing")]
        public IFormFile? regmodelPOB { get; set; }
        [DisplayName("Selfie with ID")]
        public IFormFile? regmodelSelfieProof { get; set; }
    }
}
