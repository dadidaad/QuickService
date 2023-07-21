using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories.Implements
{
    public class ServiceItemCustomFieldRepository : IServiceItemCustomFieldRepository
    {
        private readonly QuickServiceContext _context;

        private readonly ILogger<ServiceItemCustomFieldRepository> _logger;
        public ServiceItemCustomFieldRepository(QuickServiceContext context, ILogger<ServiceItemCustomFieldRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task AddServiceItemCustomField(ServiceItemCustomField serviceItemCustomField)
        {
            try
            {
                _context.ServiceItemCustomFields.Add(serviceItemCustomField);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }


        public List<ServiceItemCustomField> GetServiceItemCustomFields()
        {
            try
            {
                return _context.ServiceItemCustomFields.Include(c => c.CustomField).Include(s => s.ServiceItem).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }
    }
}
