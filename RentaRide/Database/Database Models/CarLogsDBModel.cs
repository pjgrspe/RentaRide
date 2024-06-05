using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RentaRide.Database.Database_Models
{
    public class CarLogsDBModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int logID { get; set; }
        [Required]
        public int carID { get; set; }
        [ForeignKey("carID")]
        public CarsDBModel CarsDBModel { get; set; }
        
        [Required]
        public DateTime LogDate { get; set; }
        
        [Required]
        public int LogMileage { get; set; }
        public int LogType { get; set; }
        //LOG TYPES
        // 1 = Manual
        // 2 = Maintenance
        // 3 = Repair
        // 4 = Rented
        [Required]
        public string LogDetails { get; set; }
        [Required]
        public bool LogIsDeleted { get; set; } = false;
    }
}
