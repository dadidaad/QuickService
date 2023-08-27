using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories.Implements
{
    public class UserRepository : IUserRepository
    {
        private readonly QuickServiceContext _context;

        private readonly ILogger<UserRepository> _logger;

        public UserRepository(QuickServiceContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving user with ID: {UserId}", user.UserId);
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }

        public async Task<User> GetLastUser()
        {
            try
            {
                return await _context.Users.OrderByDescending(u => u.UserId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving user");
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            try
            {
                User user = await _context.Users
                    .Include(u => u.Role).ThenInclude(r => r.Permissions)
                    .Include(u => u.GroupsNavigation)
                    .FirstOrDefaultAsync(u => u.Email == email);
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }

        public async Task<User> GetUserDetails(string userId)
        {
            try
            {
                User user = await _context.Users
                    .Include(u => u.GroupsNavigation)
                    .Include(u => u.Role).ThenInclude(r => r.Permissions).FirstOrDefaultAsync(u => u.UserId == userId);
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving user with ID: {UserId}", userId);
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }

        public List<User> GetUsers()
        {
            try
            {
                return _context.Users.Include(r => r.Role).Include(g => g.Groups).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving user");
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }

        public async Task<List<User>> GetUsersByContainString(string containStr, string? groupId)
        {
            try
            {
                IQueryable<User> query = _context.Users.Include(u => u.GroupsNavigation).Include(u => u.Role);
                if (!string.IsNullOrEmpty(groupId))
                {
                    query = query.Where(u => u.GroupsNavigation.Any(u => u.GroupId == groupId));
                }

                query = query.Where(u => u.Email.Contains(containStr)
                || string.Concat(u.FirstName, " ", u.MiddleName, " ", u.LastName).Contains(containStr));
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving user");
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }

        public async Task UpdateUser(User updateUser)
        {
            try
            {
                _context.Users.Update(updateUser);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving user with ID: {UserId}", updateUser.UserId);
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }

    }
}
