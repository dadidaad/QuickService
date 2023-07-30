using QuickServiceWebAPI.DTOs.ServiceCategory;

namespace QuickServiceWebAPI.Services
{
    public interface IServiceCategoryService
    {
        public List<ServiceCategoryDTO> GetServiceCategories();
        public Task<ServiceCategoryDTO> GetServiceCategoryById(string serviceCategoryId);
        public Task<ServiceCategoryDTO> GetLastServiceCategoryWithServiceItems();
        public Task CreateServiceCategory(CreateUpdateServiceCategoryDTO createUpdateServiceCategoryDTO);
        public Task UpdateServiceCategory(string serviceCategoryId, CreateUpdateServiceCategoryDTO createUpdateServiceCategoryDTO);
        public Task DeleteServiceCategory(string serviceCategoryId);
        public Task<string> GetNextId();
    }
}
