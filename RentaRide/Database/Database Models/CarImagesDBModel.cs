using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RentaRide.Database.Database_Models
{
    public class CarImagesDBModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int carimgID { get; set; }
        [Required]
        public int carID { get; set; }
        [ForeignKey("carID")]
        public CarsDBModel carsDBModel { get; set; }
        [Required]
        public string carimgName { get; set; }
        [Required]
        public string carimgExt { get; set; }
    }
}
