namespace RentaRide.Services
{
    public interface IFileServices
    {
        string? ProcessUploadedFile(IFormFile? img, string imgCategory, string UID);
        string? GetFileExtension(IFormFile? img);
    }
}
