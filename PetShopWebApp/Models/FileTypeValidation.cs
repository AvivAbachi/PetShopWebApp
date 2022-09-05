using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;

namespace PetShopWebApp.Models
{
    public class FileTypeValidation : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return true;
            try
            {
                if (value is IFormFile file)
                {
                    using var img = Image.FromStream(file!.OpenReadStream());
                    return img.RawFormat.Equals(ImageFormat.Jpeg)
                        || img.RawFormat.Equals(ImageFormat.Png)
                        || img.RawFormat.Equals(ImageFormat.Bmp);
                }
            }
            catch { }
            return false;
        }
    }
}
