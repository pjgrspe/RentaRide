using RentaRide.Models.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentaRide.Database.Database_Models
{
    public class UserDetailsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userDetailID { get; set; }
        [Required]
        public DateTime userDateCreated { get; set; }
        public DateTime? userDateLastModified { get; set; }
        public DateTime? userDateModified { get; set; }
        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public RentaRideAppUsers RentaRideAppUsers { get; set; }
        [Required]
        public DateTime userDOB { get; set; }
        [Required]
        public string userStreetAdd { get; set; }
        [Required]
        public string userCityAdd { get; set; }
        [Required]
        public string userProvinceAdd { get; set; }
        [Required]
        public string userContact { get; set; }
        public string? userLicense { get; set; }
        public string? userLicenseFileExt { get; set; }
        public string? user2ndValidID { get; set; }
        public string? user2ndValidIDFileExt { get; set; }
        public string? userProofofBilling { get; set; }
        public string? userProofofBillingFileExt { get; set; }
        public string? userSelfieProof { get; set; }
        public string? userSelfieProofFileExt { get; set; }
    }
}
