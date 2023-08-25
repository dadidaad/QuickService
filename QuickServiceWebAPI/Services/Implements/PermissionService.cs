using AutoMapper;
using QuickServiceWebAPI.DTOs.Permission;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;

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

        public async Task<List<Permission>> GetPermissionsByRole(string roleId)
        {
            var existingRole = await _roleRepository.GetRoleById(roleId);
            if (existingRole == null)
            {
                throw new AppException("Role not found");
            }
            List<Permission> permissions = await _repository.GetPermissionsByRole(roleId);
            return permissions;
        }

        public async Task<PermissionForRoleResponseDTO> GetPermissionsForRoleWithDTO(string roleId)
        {
            var existingRole = await _roleRepository.GetRoleById(roleId);
            if (existingRole == null)
            {
                throw new AppException("Role not found");
            }
            List<Permission> permissionsForRole = await _repository.GetPermissionsByRole(roleId);
            List<Permission> permissions = await _repository.GetPermissions();
            List<PermissionDTO> permissionDTOs = new List<PermissionDTO>();
            foreach (var permission in permissions)
            {
                var permissionDTO = _mapper.Map<PermissionDTO>(permission);
                if (permissionsForRole.Any(p => p.PermissionId == permission.PermissionId))
                {
                    permissionDTO.IsGranted = true;
                }
                else
                {
                    permissionDTO.IsGranted = false;
                }
                permissionDTOs.Add(permissionDTO);
            }
            return new PermissionForRoleResponseDTO { RoleId = roleId, Permissions = permissionDTOs };
        }

        public async Task UpdatePermissionsToRole(UpdatePermissionsDTO updatePermissionsDTO)
        {
            var existingRole = await _roleRepository.GetRoleById(updatePermissionsDTO.RoleId);
            if (existingRole == null)
            {
                throw new AppException("Role not found");
            }
            var updateRole = _mapper.Map<Role>(existingRole);
            foreach (var permissionDTO in updatePermissionsDTO.Permissions)
            {
                var permission = await _repository.GetPermission(permissionDTO.PermissionId);
                if (permission == null)
                {
                    throw new AppException($"Permission with id: {permissionDTO.PermissionId} not found");
                }
                if (permissionDTO.IsGranted && !existingRole.Permissions.Any(p => p.PermissionId == permissionDTO.PermissionId))
                {
                    updateRole.Permissions.Add(permission);
                }
                if (!permissionDTO.IsGranted && existingRole.Permissions.Any(p => p.PermissionId == permissionDTO.PermissionId))
                {
                    updateRole.Permissions.Remove(permission);
                }
            }
            await _roleRepository.UpdateRole(updateRole);
        }
    }
}
