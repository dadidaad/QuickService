using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.DTOs.Permission;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Services.Authentication;
using QuickServiceWebAPI.Utilities;
using System.Text.Json;
using System.Xml.Linq;
using PermissionEnum = QuickServiceWebAPI.Services.Authentication.PermissionEnum;

namespace QuickServiceWebAPI.Seeds
{
    public class DbInitializer : IDbInitializer
    {
        private readonly QuickServiceContext _context;

        public DbInitializer(QuickServiceContext context)
        {
            _context = context;
        }

        public void SeedPermissions()
        {
            var permissions = Enum.GetValues(typeof(PermissionEnum))
                .Cast<PermissionEnum>()
                .Select(p => p.ToString()).ToList();
            foreach (var permission in permissions)
            {
                if(!_context.Permissions.Any(p => p.PermissionName == permission))
                {
                    var newPermission = new Permission();
                    newPermission.PermissionId = IDGenerator.GeneratePermissionId(permissions.IndexOf(permission));
                    newPermission.PermissionName = permission;
                    _context.Permissions.Add(newPermission);
                }
            }
            _context.SaveChanges();
        }
    }
}
