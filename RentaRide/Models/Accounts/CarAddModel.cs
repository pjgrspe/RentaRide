using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentaRide.Models.Accounts
{
    public class CarAddModel
    {
        [Required]
        [DisplayName("Car Images")]
        public List<IFormFile> caraddImages { get; set; }
        [Required]
        [DisplayName("Make")]
        public string caraddMake { get; set; }
        [Required]
        [DisplayName("Model")]
        public string caraddModel { get; set; }
        [Required]
        [DisplayName("Year")]
        public int caraddYear { get; set; }
        [Required]
        [DisplayName("Type")]
        public int caraddType { get; set; }
        [Required]
        [DisplayName("Color")]
        public string caraddColor { get; set; }
        [Required]
        [DisplayName("License Number")]
        public string caraddPlateNumber { get; set; }
        [Required]
        [DisplayName("OR")]
        public IFormFile? caraddORDoc { get; set; }
        [Required]
        [DisplayName("CR")]
        public IFormFile? caraddCRDoc { get; set; }


        [Required]
        public bool? caraddTrans { get; set; }
        [Required]
        public bool? caraddFuelType { get; set; }
        [Required]
        public int caraddMileage { get; set; }
        [Required]
        public int caraddLastChangeOilMileage { get; set; }
        [Required]
        public int caraddOilChangeInterval { get; set; }
        [Required]
        public int caraddSeats { get; set; }
        [Required]
        public DateTime? caraddLastMaintenance { get; set; }

    }
}
