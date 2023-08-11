using System.ComponentModel.DataAnnotations;
using static QuickServiceWebAPI.DTOs.SLAMetric.CreateSlametricDTO;
using System.Linq;
using QuickServiceWebAPI.DTOs.SLAMetric;
using QuickServiceWebAPI.Models.Enums;

namespace QuickServiceWebAPI.CustomAttributes
{

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class AllPrioritiesIncludedAttribute : ValidationAttribute
    {
        private readonly Type _enumType;
        private readonly string _propertyName;

        public AllPrioritiesIncludedAttribute(Type enumType, string propertyName)
        {
            _enumType = enumType;
            _propertyName = propertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var listProperty = validationContext.ObjectType.GetProperty(_propertyName);

            if (listProperty != null && value is IEnumerable<CreateSlametricDTO> list)
            {
                var priorities = Enum.GetValues(_enumType).Cast<PriorityEnum>().Select(e => e.ToString());

                foreach (var item in list)
                {
                    if (!priorities.Contains(item.Piority))
                    {
                        return new ValidationResult($"All priorities must be included in {string.Join(", ", priorities)}");
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}
