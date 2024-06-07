using RentaRide.Utilities;

namespace RentaRide.Models.ViewModels
{
    public class OrderDetailsViewModel
    {
        public int orderdeetsVMID { get; set; }
        public string orderdeetsVMReceipt { get; set; }
        public string orderdeetsVMCustFName { get; set; }
        public string orderdeetsVMCustLName { get; set; }
        public string orderdeetsVMCustName {
            get
            {
                return orderdeetsVMCustFName + " " + orderdeetsVMCustLName;
            }
        }
        public string orderdeetsVMCarName { get; set; }
        public string orderdeetsVMPlateNumber { get; set; }
        public DateTime orderdeetsVMStartDate { get; set; }
        public DateTime orderdeetsVMEndDate { get; set; }
        public string orderdeetsVMFormattedStartDate => ViewModelTools.GetFormattedDate(orderdeetsVMStartDate);
        public string orderdeetsVMFormattedEndDate => ViewModelTools.GetFormattedDate(orderdeetsVMEndDate);
        public decimal orderdeetsVMTotalCost { get; set; }
        public decimal orderdeetsVMExtraFees { get; set; }
        public int orderdeetsVMStatusID { get; set; }
        public string orderdeetsVMPOPIMG { get; set; }
        public string orderdeetsVMPOPIMGExt { get; set; }
        public string orderdeetsVMPOPIMGType => ViewModelTools.GetFormattedExtension(orderdeetsVMPOPIMGExt);
        public string orderdeetsVMPOPIMGPicture => ViewModelTools.GetIMGSource(orderdeetsVMPOPIMG, orderdeetsVMPOPIMGType);
        public string orderdeetsVMPStatusName
        {
            get
            {
                if (orderdeetsVMStatusID > TypeNamesUtilities.OrderStatusNames.Length)
                {
                    return "Unknown";
                }
                else
                {
                    return TypeNamesUtilities.OrderStatusNames[orderdeetsVMStatusID];
                }
            }
        }
    }
}
