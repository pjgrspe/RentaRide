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
        //[Required]
        //public string? userLicense { get; set; }
        //[Required]
        //public string? user2ndValidID { get; set; }
        //[Required]
        //public string? user2ndValidIDType { get; set; }
        //[Required]
        //public string? userPlaceOfBirth { get; set; }
        //[Required]
        //public string? userSelfieProof { get; set; }
    }
}
