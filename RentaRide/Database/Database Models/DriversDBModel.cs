using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RentaRide.Database.Database_Models
{
    public class DriversDBModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int driverID { get; set; }
        [Required]
        public string driverPicture { get; set; }
        [Required]
        public string driverPictureExt { get; set; }
        [Required]
        public string driverLicense { get; set; }
        [Required]
        public string driverLicenseExt { get; set; }
        [Required]
        public string driverLicenseBack { get; set; }
        [Required]
        public string driverLicenseBackExt { get; set; }
        [Required]
        public string driverFirstName { get; set; }
        public string? driverMiddleName { get; set; }
        [Required]
        public string driverLastName { get; set; }
        [Required]
        public string driverContact { get; set; }
        [Required]
        public string driverEmail { get; set; }
        [Required]
        public DateTime driverRegisteredDate { get; set; }
        public DateTime? driverLastDutyDate { get; set; }
        [Required]
        public bool driverOnDuty { get; set; } = false;
        [Required]
        public bool driverIsActive { get; set; } = false;
        [Required]
        public bool driverIsDeleted { get; set; } = false;

    }
}
