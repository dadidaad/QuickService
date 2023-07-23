using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories.Implements
{
    public class CustomFieldRepository : ICustomFieldRepository
    {
        private readonly QuickServiceContext _context;

        private readonly ILogger<CustomFieldRepository> _logger;
        public CustomFieldRepository(QuickServiceContext context, ILogger<CustomFieldRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task AddCustomField(CustomField customField)
        {
            try
            {
                _context.CustomFields.Add(customField);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<CustomField> GetCustomFieldById(string customFieldId)
        {
            try
            {
                CustomField customField = await _context.CustomFields.AsNoTracking().FirstOrDefaultAsync(x => x.CustomFieldId == customFieldId);
                return customField;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public List<CustomField> GetCustomFields()
        {
            try
            {
                return _context.CustomFields.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task UpdateCustomField(CustomField customField)
        {
            try
            {
                _context.CustomFields.Update(customField);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task DeleteCustomField(CustomField customField)
        {
            try
            {
                _context.CustomFields.Remove(customField);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<CustomField> GetLastCustomField()
        {
            try
            {
                return await _context.CustomFields.OrderByDescending(u => u.CustomFieldId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }
    }
}
