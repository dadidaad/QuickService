using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories.Implements
{
    public class GroupRepository : IGroupRepository
    {
        private readonly QuickServiceContext _context;

        private readonly ILogger<GroupRepository> _logger;
        public GroupRepository(QuickServiceContext context, ILogger<GroupRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task AddGroup(Group group)
        {
            try
            {
                _context.Groups.Add(group);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<Group> GetGroupById(string groupId)
        {
            try
            {
                Group group = await _context.Groups.AsNoTracking().Include(g => g.Users).Include(b => b.BusinessHour).Include(u => u.GroupLeaderNavigation.Role).FirstOrDefaultAsync(x => x.GroupId == groupId);
                return group;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public List<Group> GetGroups()
        {
            try
            {
                return _context.Groups.Include(b => b.BusinessHour).Include(u => u.GroupLeaderNavigation.Role).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task UpdateGroup(Group group)
        {
            try
            {
                _context.Groups.Update(group);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task DeleteGroup(Group group)
        {
            try
            {
                _context.Groups.Remove(group);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<Group> GetLastGroup()
        {
            try
            {
                return await _context.Groups.OrderByDescending(u => u.GroupId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }

        public async Task AddUserToGroup(string userId, string groupId)
        {
            try
            {
                Group group = await _context.Groups.AsNoTracking().FirstOrDefaultAsync(x => x.GroupId == groupId);
                User user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == userId);
                group.Users.Add(user);
                _context.Groups.Update(group);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }
    }
}
