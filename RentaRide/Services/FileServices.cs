using RentaRide.Utilities;

namespace RentaRide.Services
{
    public class FileService : IFileServices
{
    private readonly IWebHostEnvironment _environment;

    public FileService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public string? ProcessUploadedFile(IFormFile? img, string imgCategory, string UID)
        {
            string? uniqueFileName = null;
            string UploadFolder = Path.Combine(FileLoc.FileUploadFolder, imgCategory);
            string path = Path.Combine(_environment.WebRootPath, UploadFolder);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (img != null)
            {
                uniqueFileName = UID;
                string filePath = Path.Combine(path, uniqueFileName);
                using FileStream fileStream = new(filePath, FileMode.Create);
                img.CopyTo(fileStream);
            }

            return uniqueFileName;
        }

    public string? GetFileExtension(IFormFile? img)
        {
            string? fileExtension = null;
            if (img != null)
            {
                fileExtension = Path.GetExtension(img.FileName);
            }

            return fileExtension;
        }
}
}
