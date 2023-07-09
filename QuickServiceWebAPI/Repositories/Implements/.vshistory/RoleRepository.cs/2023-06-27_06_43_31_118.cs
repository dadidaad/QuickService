﻿using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.Models;
using System.Data;

namespace QuickServiceWebAPI.Repositories.Implements
{
    public class RoleRepository : IRoleRepository
    {
        private readonly QuickServiceContext _context;
        private readonly ILogger<RoleRepository> _logger;

        public RoleRepository(QuickServiceContext context, ILogger<RoleRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CreateRole(Role role)
        {
            try
            {
                _context.Roles.Add(role);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving role with ID: {RoleId}", role.RoleId);
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }

        }

        public async Task DeleteRole(Role role)
        {
            try
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving role with ID: {RoleId}", role.RoleId);
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }

        public async Task<Role> GetLastRole()
        {
            try
            {
                return await _context.Roles.OrderByDescending(r => r.RoleId).FirstAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving role");
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }

        public async Task<Role> GetRoleById(string roleId)
        {
            try
            {
                return await _context.Roles.Include(r => r.Users).SingleOrDefaultAsync(r => r.RoleId == roleId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving role with ID: {RoleId}", roleId);
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }

        public List<Role> GetRoles()
        {
            try
            {
                return _context.Roles.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving roles");
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }

        public List<Role> GetRolesByType(RoleType roleType)
        {
            return _context.Roles.Where(r => r.RoleType == roleType).ToList();
        }

        public async Task UpdateRole(Role existingRole, Role updateRole)
        {
            try
            {
                _context.Entry(existingRole).CurrentValues.SetValues(updateRole);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving role with ID: {RoleId}", updateRole.RoleId);
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }

        public Task UpdateUserRole(Role existingRole)
        {
            foreach (var userModel in existingRole.Users)
            {
                var exsitingUser = existingRole.Users
                    .Where(u => u.UserId == userModel.UserId && u.UserId == default(string))
                    .SingleOrDefault();
                if(exsitingUser != null)
                {
                    _context.Entry(exsitingUser).CurrentValues.SetValues(userModel);
                }
            }
        }
    }
}