using AutoMapper;
using Microsoft.IdentityModel.Tokens;
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
        public async Task AssignPermissionsToRole(AssignPermissionsDTO assignPermissionsDTO)
        {
            var role = await _roleRepository.GetRoleById(assignPermissionsDTO.RoleId);
            if(role == null)
            {
                throw new AppException("Role not found");
            }
            if (EnumerableUtils.IsAny(role.Permissions))
            {
                throw new AppException("Role already has permissions!!");
            }
            ICollection<Permission> permissions = Array.Empty<Permission>();
            foreach(var permissionId in assignPermissionsDTO.PermissionIdList)
            {
                var permission = await _repository.GetPermission(permissionId);
                if (permission == null)
                {
                    throw new AppException($"Permission with id: {permissionId} not found");
                }
            }

            
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
