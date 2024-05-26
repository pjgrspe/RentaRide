using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace RentaRide.Database.Database_Models
{
    public class CarsDBModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int carID { get; set; }
        public string carPicture { get; set; }
        public string carPictureExt { get; set; }
        public string? carORDoc { get; set; }
        public string? carORDocExt { get; set; }
        public string? carCRDoc { get; set; }
        public string? carCRDocExt { get; set; }
        [Required]
        public string carMake { get; set; }
        [Required]
        public string carModel { get; set; }
        [Required]
        public int carYear { get; set; }
        [Required]
        public bool carTransmission { get; set; }
        [Required]
        public string carColor { get; set; }
        public int carType { get; set; }
        //Car Types
        //-1. Compact
        //-2. Sedan
        //-3. SUV
        //-4. VAN
        //-5. Minivan
        //-6. Electric
        //-7. Hybrid
        //-8. Luxury
        [ForeignKey("carType")]
        public CarTypesDBModel carTypesDBModel { get; set; }
        [Required]
        public int carMileage { get; set; }
        public bool? carFuelType { get; set; }
        //Fuel Types
        //-n. Electric
        //-0. Gasoline
        //-1. Diesel
        public bool? carStatus { get; set; }
        //Status
        //-n. Maintenance
        //-0. Rented
        //-1. Available
        [Required]
        public bool carIsActive { get; set; } = true;
        public string? carInactiveInfo { get; set; }
        public DateTime? carLastMaintenance { get; set; }
        public DateTime? carNextMaintenance { get; set; }
        [Required]
        public int carLastChangeOilMileage { get; set; }
        [Required]
        public int carOilChangeInterval { get; set; }
        [Required]
        public string carLicensePlate { get; set; }

        //carLocation <--- problem for future us
        [Required]
        public bool carIsDeleted { get; set; } = false;
        [Required]
        public DateTime carDateLogged { get; set; } = DateTime.Now;
    }
}
