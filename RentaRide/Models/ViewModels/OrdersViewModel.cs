using RentaRide.Utilities;

namespace RentaRide.Models.ViewModels
{
    public class OrdersViewModel
    {
        public int ordersVMID { get; set; }
        public string ordersVMreceipt { get; set; }
        public string ordersVMCustFName { get; set; }
        public string ordersVMCustLName { get; set; }
        public string ordersVMCarName { get; set; }
        public string ordersVMPlateNumber { get; set; }
        public DateTime ordersVMStartDate { get; set; }
        public DateTime ordersVMEndDate { get; set; }
        public string ordersVMFormattedStartDate => ordersVMStartDate.ToString("MM/dd/yyyy");
        public string ordersVMFormattedEndDate => ordersVMEndDate.ToString("MM/dd/yyyy");
        public decimal ordersVMTotalCost { get; set; }
        public decimal ordersVMExtraFees { get; set; }
        public int ordersVMStatusID { get; set; }
        public string ordersVMPaymentIMG { get; set; }
        public string ordersVMPaymentIMGExt { get; set; }
        public string ordersVMPaymentIMGType => ViewModelTools.GetFormattedExtension(ordersVMPaymentIMGExt);
        public string ordersVMPaymentIMGPicture => ViewModelTools.GetIMGSource(ordersVMPaymentIMG, ordersVMPaymentIMGType);
        public string ordersVMPStatusName
        {
            get
            {
                if (ordersVMStatusID > TypeNamesUtilities.OrderStatusNames.Length)
                {
                    return "Unknown";
                }
                else
                {
                    return TypeNamesUtilities.OrderStatusNames[ordersVMStatusID];
                }
            }
        }


    }
}
