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
                if (value is string)
                {
                    var stringVal = value as string;
                    if (string.IsNullOrEmpty(stringVal))
                    {
                        return new ValidationResult("Field is required for incident");
                    }
                }
            }
            else
            {
                if (validationContext.MemberName == "ServiceItemId")
                {
                    if (value is string)
                    {
                        var stringVal = value as string;
                        if (string.IsNullOrEmpty(stringVal))
                        {
                            return new ValidationResult("Field is required for service request");
                        }
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}
