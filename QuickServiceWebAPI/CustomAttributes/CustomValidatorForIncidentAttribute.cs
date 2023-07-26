using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.CustomAttributes
{
    public class CustomValidatorForIncidentAttribute : ValidationAttribute
    {
        private readonly string _OtherProperty;

        public CustomValidatorForIncidentAttribute(string otherProperty) : base("NOT OK")
        {
            _OtherProperty = otherProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Get the dependent property 
            var isIncident = validationContext.ObjectInstance.GetType().GetProperty(_OtherProperty);
            // Get the value of the dependent property 
            var otherPropertyValue = (bool)isIncident.GetValue(validationContext.ObjectInstance, null);

            if (otherPropertyValue)
            {
                if(value is string)
                {
                    var stringVal = value as string;
                    if (string.IsNullOrEmpty(stringVal))
                    {
                        return new ValidationResult("Field is required for incident");
                    }
                }
                if(value is IFormFile)
                {
                    var file = value as IFormFile;
                    if(file != null)
                    {
                        if (file.Length >= 4194304)
                        {
                            return new ValidationResult($"Not allowed larger than 4MB");
                        }
                    }
                }
            }
            else
            {
                if(value != null)
                {
                    return new ValidationResult($"Only accept when it is incident");
                }
            }
            return ValidationResult.Success;
        }
    }
}
