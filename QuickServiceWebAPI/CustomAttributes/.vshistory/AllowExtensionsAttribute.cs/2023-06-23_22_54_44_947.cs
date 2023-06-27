using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.CustomAttributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            var extension = Path.GetExtension(file.FileName);
            if (file != null)
            {
                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult($"Your image's filetype is not valid.");
                }
                if(file.Length >= 2097152)
                {
                    return new ValidationResult($"Not allowed larger than 2MB");
                }
            }
            return ValidationResult.Success;
        }
    }
}
