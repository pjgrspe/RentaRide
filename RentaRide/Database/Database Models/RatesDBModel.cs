using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentaRide.Database.Database_Models
{
    public class RatesDBModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int listingRateID { get; set; }
        [Required]
        public string listingRateName { get; set; }
    }
}
