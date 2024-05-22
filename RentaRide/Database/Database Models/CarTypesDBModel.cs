using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RentaRide.Database.Database_Models
{
    public class CarTypesDBModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int cartypeID { get; set; }
        [Required]
        public string cartypeName { get; set; }
    }
}
