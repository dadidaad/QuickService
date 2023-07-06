using QuickServiceWebAPI.DTOs.Permission;
using QuickServiceWebAPI.Models;
using System.Xml.Linq;

namespace QuickServiceWebAPI.Services.Implements
{
    public class PermissionService : IPermissionService
    {
        private readonly static string configFilePath = "PermissionsModule.xml";
        List<CreateDTO> permissionListFromConfig;
        
        public PermissionService()
        {
            permissionListFromConfig = new List<CreateDTO>();
            XDocument xDocument = XDocument.Load(configFilePath);
            foreach (XElement element in xDocument.Descendants("quickservice")
                .Descendants("permissions").Descendants("permission"))
            {
                CreateDTO createDTO = new CreateDTO();
                createDTO.PermissionName = element.Element("name").Value;
                createDTO.PermissionModule = element.Attribute("module").Value;
                permissionListFromConfig.Add(createDTO);
            }
        }
        public Task CreatePermission()
        {
            throw new NotImplementedException();
        }

    }
}
