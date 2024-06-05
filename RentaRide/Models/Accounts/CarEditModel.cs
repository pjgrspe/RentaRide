namespace RentaRide.Models.Accounts
{
    public class CarEditModel
    {
        public int careditID { get; set; }
        public List<IFormFile> careditImages { get; set; } = new List<IFormFile>();
        public string careditMake { get; set; }
        public string careditModel { get; set; }
        public int careditYear { get; set; }
        public int careditType { get; set; }
        public string careditColor { get; set; }
        public string careditPlateNumber { get; set; }
        public IFormFile? careditORDoc { get; set; }
        public IFormFile? careditCRDoc { get; set; }
        public bool careditTrans { get; set; }
        public int careditFuelType { get; set; }
        public int careditOilChangeInterval { get; set; }
        public int careditSeats { get; set; }
    }
}
