namespace RentaRide.Models.Orders
{
    public class OrderAddModel
    {
        public bool orderaddFromAdmin { get; set; }
        public int orderaddListingID { get; set; }
        public string orderaddUserID { get; set; }
        public int? orderaddDriverID { get; set; }
        public DateTime orderaddStart { get; set; }
        public DateTime orderaddEnd { get; set; }
        public int orderaddPaymentID { get; set; }
        public int orderaddStatusID { get; set; }
        public IFormFile? orderaddPaymentIMG { get; set; }
        public decimal orderaddCost { get; set; }
        public decimal orderaddExtraFee { get; set; }
        public string? orderaddLocationLimit { get; set; }
        public string? orderaddNotes { get; set; }
    }
}
