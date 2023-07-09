using AutoMapper;
using QuickServiceWebAPI.DTOs.Permission;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;
using System.Xml.Linq;

namespace QuickServiceWebAPI.Services.Implements
{
    public class PermissionService : IPermissionService
    {
        private readonly static string configFilePath = "PermissionsModule.xml";
        private List<CreateDTO> permissionListFromConfig;
        private readonly IPermissionRepository _repository;
        private readonly IMapper _mapper;
        public PermissionService(IPermissionRepository repository, IMapper mapper)
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
            _repository = repository;
            _mapper = mapper;
        }
        public Task CreatePermission()
        {
            throw new NotImplementedException();
        }

        private bool CompareChangeInConfigWithDatabase()
        {
            List<Permission> permissions = _repository.GetPermissions();
            if(permissionListFromConfig.Count == permissions.Count)
            {
                return true;
            }
            return false;
        }
        private async Task<string> GetNextId()
        {
            Permission lastPermission = await _repository.GetLastPermission();
            int id = 0;
            if (lastPermission == null)
            {
                id = 1;
            }
            else
            {
                id = IDGenerator.ExtractNumberFromId(lastPermission.PermissionId) + 1;
            }
            string permissionId = IDGenerator.GeneratePermissionId(id);
            return permissionId;
        }
    }
}
