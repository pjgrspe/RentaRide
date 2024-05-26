using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentaRide.Models.Accounts
{
    public class CarAddModel
    {
        [Required]
        
        [DisplayName("Car Images")]
        public IFormFile? caraddImage { get; set; }
        [DisplayName("Make")]
        public string caraddMake { get; set; }
        [DisplayName("Model")]
        public string caraddModel { get; set; }
        [DisplayName("Year")]
        public int caraddYear { get; set; }
        [DisplayName("Type")]
        public string caraddType { get; set; }
        [DisplayName("Color")]
        public string caraddColor { get; set; }
        [DisplayName("License Number")]
        public string caraddPlateNumber { get; set; }
        [DisplayName("OR")]
        public IFormFile? caraddORDoc { get; set; }
        [DisplayName("CR")]
        public IFormFile? caraddCRDoc { get; set; }
    }
}
