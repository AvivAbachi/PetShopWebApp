using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;

namespace PetShopWebApp.Validation
{
    public class PictureFileType : ValidationAttribute, IClientModelValidator
    {
        public virtual void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-accept", GetErrorMessage());
            MergeAttribute(context.Attributes, "data-val-accept", "image/bmp, image/jpeg, image/png");
            MergeAttribute(context.Attributes, "accept", "image/bmp, image/jpeg, image/png");
        }
        public string GetErrorMessage() => "Please Enter Pet Picture On Support Type: Bmp, Jpg, Jpeg, Png";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;
            try
            {
                if (value is IFormFile file)
                {
                    using (var img = Image.FromStream(file.OpenReadStream()))
                    {
                        if (img.RawFormat == ImageFormat.Jpeg ||
                            img.RawFormat.Equals(ImageFormat.Png) ||
                            img.RawFormat.Equals(ImageFormat.Bmp))
                        {
                            return ValidationResult.Success;
                        }
                    }
                }
            }
            catch { }
            return new ValidationResult(GetErrorMessage());
        }

        private static bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }
            attributes.Add(key, value);
            return true;
        }
    }
}
