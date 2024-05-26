using System.ComponentModel.DataAnnotations;

namespace RentaRide.Models
{
    public class ImageTestModel
    { 
        [Required]
        [Display(Name = "Upload Image")]
        public IFormFile ImageFile { get; set; }
    }
}
