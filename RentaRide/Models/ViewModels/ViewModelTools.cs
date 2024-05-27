using System.Globalization;

namespace RentaRide.Models.ViewModels
{
    public class ViewModelTools
    {
        public static string ConvertToFullname(string firstName, string? middleName, string lastName)
        {
            if (middleName == null)
            {
                return firstName + " " + lastName;
            }
            else
            {
                return firstName + " " + middleName + " " + lastName;
            }
        }
        public static string? GetFormattedExtension(string? ext)
        {
            if (ext == ".jpg" || ext == ".jpeg")
            {
                return "jpeg";


            }else if (ext == ".png")
            {
                return "png";


            }else if (ext == ".gif")
            {
                return "gif";


            }else if (ext == ".bmp")
            {
                return "bmp";


            }else if (ext == ".svg")
            {
                return "svg+html";


            }else if (ext == ".webp")
            {
                return "webp";


            }
            else
            {
                return null;
            }
        }
        public static string? GetIMGSource(string? file, string? type)
        {
            if (file != null || type != null)
            {
                return $"data:image/{type};base64,{file}";
            }
            else
            {
                return null;
            }
        }
        public static string? GetFormattedDate(DateTime? date)
        {
            if (date == null)
            {
                return "No Date Recorded";
            }
            return date?.ToString("MMMM dd, yyyy", CultureInfo.InvariantCulture);
        
        }
    }
}
