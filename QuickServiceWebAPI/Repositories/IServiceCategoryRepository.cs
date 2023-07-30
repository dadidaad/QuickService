using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IServiceCategoryRepository
    {
        public List<ServiceCategory> GetServiceCategories();
        public List<ServiceCategory> GetServiceCategoriesWithServiceItems();
        public Task<ServiceCategory> GetServiceCategoryById(string serviceCategoryId);
        public Task<ServiceCategory> GetServiceCategoryByIdWithServiceItems(string serviceCategoryId);
        public Task AddServiceCategory(ServiceCategory serviceCategory);
        public Task UpdateServiceCategory(ServiceCategory serviceCategory);
        public Task DeleteServiceCategory(ServiceCategory serviceCategory);
        public Task<ServiceCategory> GetLastServiceCategory();
    }
}
