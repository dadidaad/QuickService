using QuickServiceWebAPI.DTOs.ServiceItem;

namespace QuickServiceWebAPI.DTOs.ServiceCategory
{
    public class ServiceCategoryDTO
    {
        public string ServiceCategoryId { get; set; }

        public string ServiceCategoryName { get; set; }

        public string? Description { get; set; }
        public virtual ICollection<ServiceItemDTOSecond> ServiceItemEntities { get; set; }
    }
}
