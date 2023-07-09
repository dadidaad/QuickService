using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using QuickServiceWebAPI.DTOs.Permission;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;
using System.Net.WebSockets;
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
            var existingRole = await _roleRepository.GetRoleById(assignPermissionsDTO.RoleId);
            if(existingRole == null)
            {
                throw new AppException("Role not found");
            }
            if (EnumerableUtils.IsAny(existingRole.Permissions))
            {
                throw new AppException("Role already has permissions!!");
            }
            ICollection<Permission> permissions = Array.Empty<Permission>();
            var updateRole = _mapper.Map<Role>(existingRole);
            foreach (var permissionId in assignPermissionsDTO.PermissionIdList)
            {
                var permission = await _repository.GetPermission(permissionId);
                if (permission == null)
                {
                    throw new AppException($"Permission with id: {permissionId} not found");
                }
                permissions.Add(permission);
            }
            updateRole.Permissions = permissions;
            await _roleRepository.UpdateRole(existingRole, updateRole);
        }

        public async Task<List<Permission>> GetPermissionsByRole(string roleId)
        {
            List<Permission> permissions = await _repository.GetPermissionsByRole(roleId);
            return permissions;
        }

        public async Task<List<Permission>> GetPermissionsByRoleType(RoleType roleType)
        {
            return await _repository.GetPermissionsForRoleType(roleType);
        }

        public async Task UpdatePermissionsToRole(UpdatePermissionsDTO updatePermissionsDTO)
        {
            var existingRole = await _roleRepository.GetRoleById(updatePermissionsDTO.RoleId);
            if (existingRole == null)
            {
                throw new AppException("Role not found");
            }
            List<Permission> permissionsFromDb = await GetPermissionsByRoleType(existingRole.RoleType);
            var updateRole = _mapper.Map<Role>(existingRole);
            foreach (KeyValuePair<string, bool> entry in updatePermissionsDTO.Permissions)
            {
                if(!permissionsFromDb.Any(p => p.PermissionId == entry.Key))
                {
                    throw new AppException($"Permission with id: {entry.Key} not found");
                }
                if (entry.Value && !existingRole.Permissions.Any(p => p.PermissionId == entry.Key))
                {
                    var permission = await _repository.GetPermission(entry.Key);
                    updateRole.Permissions.Add(permission);
                }
                if(!entry.Value && existingRole.Permissions.Any(p => p.PermissionId == entry.Key))
                {
                    var permission = await _repository.GetPermission(entry.Key);
                    updateRole.Permissions.Remove(permission);
                }
            }
            await _roleRepository.UpdateRole(existingRole, updateRole);
        }
    }
}
