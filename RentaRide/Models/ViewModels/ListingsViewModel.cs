﻿using Microsoft.VisualStudio.Web.CodeGeneration.Templating;
using RentaRide.Utilities;

namespace RentaRide.Models.ViewModels
{
    public class ListingsViewModel
    {
        public int listingVMID { get; set; }
        public int listingVMcarID { get; set; }
        public string listingVMcarName { get; set; }
        public string listingVMcarNameNoLicense { get; set; }
        public string listingVMcarIMG { get; set; }
        public string listingVMcarIMGext { get; set; }
        public string listingVMcarIMGType => ViewModelTools.GetFormattedExtension(listingVMcarIMGext);
        public string listingVMcarPicture => ViewModelTools.GetIMGSource(listingVMcarIMG, listingVMcarIMGType);
        public string listingVMDetails { get; set; }
        public string listingVMColor {  get; set; }
        public bool listingVMTransmission {  get; set; }
        public string listingVMTransType
        {
            get
            {
                if (listingVMTransmission == true)
                {
                    return "Automatic";
                }
                else
                {
                    return "Manual";
                }
            }
        }
        public int listingVMFuelType { get; set; }
        public string listingVMFuel
        {
            get
            {
                return TypeNamesUtilities.fuelTypeNames[listingVMFuelType];
            }
        }
        public int listingVMType { get; set; }
        public string listingVMTypeName
        {
            get
            {
                return TypeNamesUtilities.CarTypeNames[listingVMType];
            }
        }
        public int listingVMSeats { get; set; }
        public decimal? listingVMHourlyPrice { get; set; }
        public decimal listingVMDailyPrice { get; set; }
        public decimal listingVMWeeklyPrice { get; set; }
        public decimal listingVMMonthlyPrice { get; set; }
        public int listingVMStatus { get; set; }
        public string listingVMStatusName
            {
            get
            {
                if(listingVMStatus <= TypeNamesUtilities.ListingStatusNames.Length)
                {
                    return TypeNamesUtilities.ListingStatusNames[listingVMStatus];
                }
                else
                {
                    return "Invalid";
                }
            }
        }

        public string listingVMStatusNameClass
        {
            get
            {
                return TypeNamesUtilities.ListingStatusClassNames[listingVMStatus];
            }
        }

        public DateTime listingVMAvailabilityStart { get; set; }
        public DateTime? listingVMAvailabilityEnd { get; set; }
        public string listingVMFormattedStartDate => ViewModelTools.GetFormattedDate(listingVMAvailabilityStart);
        public string listingVMFormattedEndDate => ViewModelTools.GetFormattedDate(listingVMAvailabilityEnd);
    }
}
