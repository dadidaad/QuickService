using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.Models;
using System.Data;

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

        public async Task DeleteServiceItemCustomField(ServiceItemCustomField serviceItemCustomField)
        {
            try
            {
                _context.ServiceItemCustomFields.Remove(serviceItemCustomField);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving serviceItem with ID: {ServiceItemId}", serviceItemCustomField.ServiceItemId);
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }

        public async Task DeleteServiceItemCustomFieldsByCustomField(CustomField customField)
        {
            try
            {
                await _context.Entry(customField).Collection("ServiceItemCustomFields").LoadAsync();

                List<ServiceItemCustomField> serviceItemCustomFields = _context.ServiceItemCustomFields
                    .Where(sicf => sicf.CustomFieldId == customField.CustomFieldId).ToList();
                _context.ServiceItemCustomFields.RemoveRange(serviceItemCustomFields);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving serviceItem with ID: {ServiceItemId}", serviceItem.ServiceItemId);
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }

        public async Task DeleteServiceItemCustomFieldsByServiceItem(ServiceItem serviceItem)
        {
            try
            {
                await _context.Entry(serviceItem).Collection("ServiceItemCustomFields").LoadAsync();

                List<ServiceItemCustomField> serviceItemCustomFields = _context.ServiceItemCustomFields
                    .Where(sicf => sicf.ServiceItemId == serviceItem.ServiceItemId).ToList();
                _context.ServiceItemCustomFields.RemoveRange(serviceItemCustomFields);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving serviceItem with ID: {ServiceItemId}", serviceItem.ServiceItemId);
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }

        public async Task<ServiceItemCustomField> GetServiceItemCustomField(string serviceItemId, string customFieldId)
        {
            try
            {
                return await _context.ServiceItemCustomFields.FindAsync(serviceItemId, customFieldId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving serviceItemCustomField with ID: {serviceItemId} and {customFieldId}", serviceItemId, customFieldId);
                throw; // Rethrow the exception to propagate it up the call stack if necessary
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

        public Task<List<ServiceItemCustomField>> GetServiceItemCustomFieldsByServiceItem(string serviceItemId)
        {
            try
            {
                return _context.ServiceItemCustomFields
                    .Include(sicf => sicf.CustomField)
                    .Where(sicf => sicf.ServiceItemId == serviceItemId).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }
    }
}
