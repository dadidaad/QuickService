using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.CustomAttributes
{
    public class DatetimeRangeAttribute : ValidationAttribute
    {
        private readonly string _minDateProperty;
        private readonly string _maxDateProperty;

        public DatetimeRangeAttribute(string minDateProperty, string maxDateProperty)
        {
            _minDateProperty = minDateProperty;
            _maxDateProperty = maxDateProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currentValue = (DateTime)value;

            var minDateProperty = validationContext.ObjectType.GetProperty(_minDateProperty);
            var maxDateProperty = validationContext.ObjectType.GetProperty(_maxDateProperty);

            if (minDateProperty == null && _minDateProperty != "now")
            {
                throw new ArgumentException("Property with this name not found", _minDateProperty);
            }

            if (maxDateProperty == null)
            {
                throw new ArgumentException("Property with this name not found", _maxDateProperty);
            }

            var minDateValue = minDateProperty != null ? (DateTime)minDateProperty.GetValue(validationContext.ObjectInstance) : DateTime.Now;
            var maxDateValue = (DateTime)maxDateProperty.GetValue(validationContext.ObjectInstance);

            if (_minDateProperty == "now" && currentValue <= DateTime.Now || currentValue >= maxDateValue)
            {
                return new ValidationResult(ErrorMessage);
            }

            if (_minDateProperty != "now" && currentValue <= minDateValue || currentValue >= maxDateValue)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
