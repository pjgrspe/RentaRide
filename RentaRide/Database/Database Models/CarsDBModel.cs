﻿using Microsoft.AspNetCore.Http.HttpResults;
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
        public string? carThumbnail { get; set; }
        public string? carThumbnailExt { get; set; }
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
        //Transmission types
        //-0. Manual
        //-1. Automatic
        [Required]
        public string carColor { get; set; }
        public int carType { get; set; }
        [Required]
        public int carMileage { get; set; }
        public int carFuelType { get; set; }
        //Fuel Types
        //-1. Gasoline
        //-2. Diesel
        //-3. Electric
        public int carStatus { get; set; }
        //Status
        //-1. Available
        //-2. Rented
        //-3. Maintenance
        //-4. Repair
        public DateTime? carLastMaintenance { get; set; }
        [Required]
        public int carLastChangeOilMileage { get; set; } = 0;
        [Required]
        public int carOilChangeInterval { get; set; }
        [Required]
        public string carLicensePlate { get; set; }
        //carLocation <--- problem for future us
        [Required]
        public bool carIsDeleted { get; set; } = false;
        [Required]
        public DateTime carDateRegistered { get; set; } = DateTime.Now;
        public DateTime? carLastLogDate { get; set; }
        [Required]
        public int carSeats { get; set; } = 2;
    }
}
