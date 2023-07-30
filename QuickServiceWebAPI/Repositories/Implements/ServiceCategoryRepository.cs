using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories.Implements
{
    public class ServiceCategoryRepository : IServiceCategoryRepository
    {
        private readonly QuickServiceContext _context;

        private readonly ILogger<ServiceCategoryRepository> _logger;
        public ServiceCategoryRepository(QuickServiceContext context, ILogger<ServiceCategoryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task AddServiceCategory(ServiceCategory serviceCategory)
        {
            try
            {
                _context.ServiceCategories.Add(serviceCategory);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<ServiceCategory> GetServiceCategoryById(string serviceCategoryId)
        {
            try
            {
                ServiceCategory serviceCategory = await _context.ServiceCategories.AsNoTracking().FirstOrDefaultAsync(x => x.ServiceCategoryId == serviceCategoryId);
                return serviceCategory;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<ServiceCategory> GetServiceCategoryByIdWithServiceItems(string serviceCategoryId)
        {
            try
            {
                ServiceCategory serviceCategory = await _context.ServiceCategories.AsNoTracking().Include(x => x.ServiceItems).FirstOrDefaultAsync(x => x.ServiceCategoryId == serviceCategoryId);
                return serviceCategory;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public List<ServiceCategory> GetServiceCategories()
        {
            try
            {
                return _context.ServiceCategories.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public List<ServiceCategory> GetServiceCategoriesWithServiceItems()
        {
            try
            {
                return _context.ServiceCategories.Include(x => x.ServiceItems).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task UpdateServiceCategory(ServiceCategory serviceCategory)
        {
            try
            {
                _context.ServiceCategories.Update(serviceCategory);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task DeleteServiceCategory(ServiceCategory serviceCategory)
        {
            try
            {
                _context.ServiceCategories.Remove(serviceCategory);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<ServiceCategory> GetLastServiceCategory()
        {
            try
            {
                return await _context.ServiceCategories.OrderByDescending(u => u.ServiceCategoryId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }
    }
}
