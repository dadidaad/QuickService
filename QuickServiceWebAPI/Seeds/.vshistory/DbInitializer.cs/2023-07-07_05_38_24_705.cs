using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.DTOs.Permission;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Utilities;
using System.Text.Json;
using System.Xml.Linq;

namespace QuickServiceWebAPI.Seeds
{
    public class PermissionSeeder
    {
        private readonly QuickServiceContext _context;
        private readonly static string configFilePath = "PermissionsModule.xml";

        public PermissionSeeder(QuickServiceContext context)
        {
            _context = context;
        }

        public void SeedPermissions()
        {
            List<CreateDTO> permissionListFromConfig = new List<CreateDTO>();
            XDocument xDocument = XDocument.Load(configFilePath);
            foreach (XElement element in xDocument.Descendants("quickservice")
                .Descendants("permissions").Descendants("permission"))
            {
                CreateDTO createDTO = new CreateDTO();
                createDTO.PermissionName = element.Element("name").Value;
                createDTO.PermissionModule = element.Attribute("module").Value;
                permissionListFromConfig.Add(createDTO);
            }
            var permissions = new List<Permission>();
            int permissionId = 1;
            foreach(var createDTO in permissionListFromConfig)
            {
                var permission = new Permission();
                permission.PermissionId = IDGenerator.GeneratePermissionId(permissionId++);
                permission.PermissionName = createDTO.PermissionName;
                permission.PermissionModule = createDTO.PermissionModule;
                permissions.Add(permission);
            }

            foreach(var permission in permissions)
            {
                if(!_context.Permissions.Any(p => p == permission))
                {
                    _context.Permissions.Add(permission);
                }
            }
            _context.SaveChanges();
        }
    }
}
