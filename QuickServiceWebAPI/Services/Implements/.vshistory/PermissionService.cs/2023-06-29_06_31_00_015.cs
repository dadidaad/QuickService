using AutoMapper;
using QuickServiceWebAPI.DTOs.Permission;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
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

    }
}
