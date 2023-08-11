using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Utilities;
using PermissionEnum = QuickServiceWebAPI.Models.Enums.PermissionEnum;

namespace QuickServiceWebAPI.Seeds
{
    public class DbInitializer : IDbInitializer
    {
        private const string JsonData = "Seeds/data.json";
        private readonly QuickServiceContext _context;
        private readonly IHostEnvironment _hostEnvironment;
        private JObject? jsonData;
        public DbInitializer(QuickServiceContext context, IHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            var rootPath = _hostEnvironment.ContentRootPath; //get the root path

            var fullPath = Path.Combine(rootPath, JsonData); //combine the root path with that of our json file inside mydata directory

            var data = System.IO.File.ReadAllText(fullPath); //read all the content inside the file

            if (string.IsNullOrWhiteSpace(data)) throw new AppException("Error while creating data");

            jsonData = JsonConvert.DeserializeObject<JObject>(data);
        }

        public void SeedPermissions()
        {
            var permissions = Enum.GetValues(typeof(PermissionEnum))
                .Cast<PermissionEnum>().ToList();
            foreach (var permission in permissions)
            {
                if (!_context.Permissions.Any(p => p.PermissionName == permission.GetDisplayName()))
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
            var serviceCategories = jsonData.Value<JArray>("serviceCategories")
                .ToObject<List<ServiceCategory>>();
            if (serviceCategories == null)
            {
                return;
            }
            foreach (var serviceCategory in serviceCategories)
            {
                if (!_context.ServiceCategories.Any(p => p.ServiceCategoryId == serviceCategory.ServiceCategoryId))
                {
                    _context.ServiceCategories.Add(serviceCategory);
                }
            }
            _context.SaveChanges();
        }

        public void SeedSla()
        {
           
            var slas = jsonData.Value<JArray>("Sla").ToObject<List<Sla>>();
            if (slas == null)
            {
                return;
            }
            foreach(var sla in slas)
            {
                if (!_context.Slas.Any(s => s.Slaid == sla.Slaid))
                {
                    _context.Slas.Add(sla);
                }
            }
            _context.SaveChanges();
        }
    }
}
