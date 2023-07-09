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
        private readonly IPermissionRepository _repository;
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;
        public PermissionService(IPermissionRepository repository, IMapper mapper, IRoleRepository roleRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _roleRepository = roleRepository;
        }
        public Task AssignPermissionsToRole(AssignPermissionsDTO assignPermissionsDTO)
        {
            var role = _roleRepository.GetRoleById(assignPermissionsDTO.RoleId);
        }

        public async Task<HashSet<string>> GetPermissionsByRole(string roleId)
        {
            List<Permission> permissions = await _repository.GetPermissionsByRole(roleId);
            return permissions.Select(p => p.PermissionName).ToHashSet();
        }

        public async Task<List<Permission>> GetPermissionsByRoleType(RoleType roleType)
        {
            return await _repository.GetPermissionsForRoleType(roleType);
        }
    }
}
