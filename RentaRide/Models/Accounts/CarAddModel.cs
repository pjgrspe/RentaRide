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
        public string caraddType { get; set; }
        [Required]
        [DisplayName("Color")]
        public string caraddColor { get; set; }
        [Required]
        [DisplayName("License Number")]
        public string caraddPlateNumber { get; set; }
        [DisplayName("OR")]
        public IFormFile? caraddORDoc { get; set; }
        [DisplayName("CR")]
        public IFormFile? caraddCRDoc { get; set; }
    }
}
