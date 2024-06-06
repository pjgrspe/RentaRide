using Microsoft.AspNetCore.Mvc;
using RentaRide.Utilities;

namespace RentaRide.Services
{
    public class FileService : IFileServices
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public FileService(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        public string? ProcessUploadedFile(IFormFile? img, string imgCategory, string UID)
        {
            string? uniqueFileName = null;
            string UploadFolder = Path.Combine(FileLoc.FileUploadFolder, imgCategory);
            string path = Path.Combine(FileLoc.WWWRootFileUploadFolder, UploadFolder);
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

        public string? ProcessEncryptUploadedFile(IFormFile? img, string imgCategory)
        {
            string? uniqueFileName = null;
            var key = _configuration["ImageEncryption:ImageKey"];
            var iv = _configuration["ImageEncryption:ImageIV"];
            string UploadFolder = Path.Combine(FileLoc.FileUploadFolder, imgCategory);
            string path = Path.Combine(_environment.WebRootPath, UploadFolder);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (img != null)
            {
                uniqueFileName = ImageUtilities.ProcessImageUpload(img,path,key!,iv!, out string imageFilePath);
            }

            return uniqueFileName;
        }

        public string imgNullCheck(string? img, string imgCategory)
        {
            var key = _configuration["ImageEncryption:ImageKey"];
            var iv = _configuration["ImageEncryption:ImageIV"];
            string UploadFolder = Path.Combine(FileLoc.FileUploadFolder, imgCategory);
            string path = Path.Combine(_environment.WebRootPath, UploadFolder);
            if (img == null)
            {
                img = "Default.png";
            }
            var imageBytes = ImageUtilities.ProcessDecodeImage(img,path,key!,iv!);
            var base64Image = Convert.ToBase64String(imageBytes);
            var filePath = base64Image;
            return filePath;
        }
    }
}
