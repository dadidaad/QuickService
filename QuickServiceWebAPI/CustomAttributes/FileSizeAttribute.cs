using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.CustomAttributes
{
    public class FileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxSizeInBytes;

        public FileSizeAttribute(int maxSizeInBytes)
        {
            _maxSizeInBytes = maxSizeInBytes;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                if (file.Length > _maxSizeInBytes)
                {
                    return new ValidationResult($"File size should not exceed {_maxSizeInBytes} bytes.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
