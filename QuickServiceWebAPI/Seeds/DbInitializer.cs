using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Utilities;
using PermissionEnum = QuickServiceWebAPI.Services.Authentication.PermissionEnum;

namespace QuickServiceWebAPI.Seeds
{
    public class DbInitializer : IDbInitializer
    {
        private const string JsonData = "Seeds/data.json";
        private readonly QuickServiceContext _context;
        private readonly IHostEnvironment _hostEnvironment;
        public DbInitializer(QuickServiceContext context, IHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public void SeedPermissions()
        {
            var permissions = Enum.GetValues(typeof(PermissionEnum))
                .Cast<PermissionEnum>().ToList();
            foreach (var permission in permissions)
            {
                if(!_context.Permissions.Any(p => p.PermissionName == permission.GetDisplayName()))
                {
                    var newPermission = new Permission();
                    newPermission.PermissionId = IDGenerator.GeneratePermissionId(permissions.IndexOf(permission));
                    newPermission.PermissionName = permission.GetDisplayName();
                    _context.Permissions.Add(newPermission);
                }
            }
            _context.SaveChanges();
        }

        public void SeedServiceCategories()
        {
            var rootPath = _hostEnvironment.ContentRootPath; //get the root path

            var fullPath = Path.Combine(rootPath, JsonData); //combine the root path with that of our json file inside mydata directory

            var data = System.IO.File.ReadAllText(fullPath); //read all the content inside the file

            if (string.IsNullOrWhiteSpace(data)) throw new AppException("Error while creating data");

            var jsonData = JsonConvert.DeserializeObject<JObject>(data);

            var serviceCategories = jsonData.Value<JArray>("serviceCategories")
                .ToObject<List<ServiceCategory>>();
            foreach(var serviceCategory in serviceCategories)
            {
                if(!_context.ServiceCategories.Any(p => p.ServiceCategoryId == serviceCategory.ServiceCategoryId))
                {
                    _context.ServiceCategories.Add(serviceCategory);
                }
            }
            _context.SaveChanges();
        }
    }
}
