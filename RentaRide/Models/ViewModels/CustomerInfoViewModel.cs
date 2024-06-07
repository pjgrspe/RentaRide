namespace RentaRide.Models.ViewModels
{
    public class CustomerInfoViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public bool? UserIsApproved { get; set; }
    }
}
