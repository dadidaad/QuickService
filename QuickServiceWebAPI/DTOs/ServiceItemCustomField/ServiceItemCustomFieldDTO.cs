using QuickServiceWebAPI.DTOs.CustomField;
using QuickServiceWebAPI.DTOs.ServiceItem;

namespace QuickServiceWebAPI.DTOs.ServiceItemCustomField
{
    public class ServiceItemCustomFieldDTO
    {
        public string ServiceItemId { get; set; } = null!;

        public string CustomFieldId { get; set; } = null!;

        public DateTime CreatedTime { get; set; }

        public bool? Mandatory { get; set; }

        public virtual CustomFieldDTO CustomField { get; set; } = null!;

        public virtual ServiceItemDTO ServiceItem { get; set; } = null!;
    }
}
