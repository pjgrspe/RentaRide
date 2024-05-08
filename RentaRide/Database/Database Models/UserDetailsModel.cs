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
        public int ID { get; set; }
        [ForeignKey("UserID")]
        public RentaRideAppUsers RentaRideAppUsers { get; set; }
        [Required]
        public DateTime DOB { get; set; }
        [Required]
        public string? StreetAdd { get; set; }
        [Required]
        public string? CityAdd { get; set; }
        [Required]
        public string? ProvinceAdd { get; set; }
        [Required]
        public string? Contact { get; set; }
        //[Required]
        //public string? License { get; set; }
        //[Required]
        //public string? 2ndValidID { get; set; }
        //[Required]
        //public string? PlaceOfBirth { get; set; }
        //[Required]
        //public string? SelfieProof { get; set; }
    }
}
