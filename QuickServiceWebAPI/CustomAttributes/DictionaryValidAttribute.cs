using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class DictionaryValidAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!(value is Dictionary<string, bool>))
                return new ValidationResult("Object is not of proper type");

            var dictionary = (Dictionary<string, bool>)value;

            if (dictionary.Count <= 0)
                return new ValidationResult("Must have at least one");

            bool checkFlags = dictionary.Values.All(v => !v);
            if (!checkFlags)
            {
                return new ValidationResult("Must have at least one");
            }
            return ValidationResult.Success;
        }
    }

}

