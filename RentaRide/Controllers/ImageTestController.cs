using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using RentaRide.Utilities;
using RentaRide.Models;

namespace RentaRide.Controllers
{
    public class ImageTestController : Controller
    { 
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageTestController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment) 
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(ImageTestModel model)
        {
            if (ModelState.IsValid)
            {
                var key = _configuration["ImageEncryption:ImageKey"];
                var iv = _configuration["ImageEncryption:ImageIV"];
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var res = ImageUtilities.ProcessImageUpload(model.ImageFile,uploadsFolder,key!,iv!, out string imageFilePath);

                //if (res)
                //{
                //    var imageBytes = ImageUtilities.ProcessDecodeImage(imageFilePath,key!,iv!);
                //    return File(imageBytes, "image/jpeg");
                //}
                //else
                //{
                //    return Content("Image upload failed");
                //}
                
            }
            return View(model);
        }
    }
}
