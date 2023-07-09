﻿using Microsoft.EntityFrameworkCore;
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
            catch(Exception ex)
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
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
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
                User user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.UserId == userId);
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
                return _context.Users.Include(r => r.Role).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving user");
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }

        public async Task UpdateUser(User existingUser, User updateUser)
        {
            try
            {
                _context.Entry(existingUser).CurrentValues.SetValues(updateUser);
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