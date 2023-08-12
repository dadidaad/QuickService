using AutoMapper;
using QuickServiceWebAPI.DTOs.Role;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Services.Implements
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPermissionRepository _permissionRepository;
        public RoleService(IRoleRepository repository, IMapper mapper, IPermissionRepository permissionRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _permissionRepository = permissionRepository;
        }

        private static readonly List<PermissionEnum> DefaultPermissionForRoles = new List<PermissionEnum>()
        { PermissionEnum.ManageTickets, PermissionEnum.ManageChange, PermissionEnum.ManageProblems};

        public async Task CreateRole(CreateDTO createDTO)
        {
            var role = _mapper.Map<Role>(createDTO);
            role.RoleId = await GetNextId();
            var permissionDefaults = (await _permissionRepository.GetPermissions())
                .Where(p => DefaultPermissionForRoles.Any(pE => pE.GetDisplayName() == p.PermissionName)).ToList();
            role.Permissions = permissionDefaults;
            await _repository.CreateRole(role);
        }

        public async Task DeleteRole(string roleId)
        {
            var role = await _repository.GetRoleById(roleId);
            if (role == null)
            {
                throw new AppException("Role not found");
            }
            if (_repository.CountUserHaveRole(roleId) > 0) // Check if existing user have this role so update it to null
            {
                foreach (var user in role.Users)
                {
                    user.RoleId = null;
                    //user.Role = null;
                }
                await _repository.UpdateUserRole(role);
            }
            await _repository.DeleteRolePermissions(role);
            await _repository.DeleteRole(role);
        }

        public async Task<RoleDTO> GetRoleById(string roleId)
        {
            //return await _repository.GetRoleById(roleId);
            var role = await _repository.GetRoleById(roleId);
            return _mapper.Map<RoleDTO>(role);
        }

        public List<RoleDTO> GetRoles()
        {
            //return _repository.GetRoles();
            var roles = _repository.GetRoles();
            return roles.Select(role => _mapper.Map<RoleDTO>(role)).ToList();
        }

        public List<Role> GetRolesByType(RoleType roleType)
        {
            return _repository.GetRolesByType(roleType);
        }

        public async Task UpdateRole(UpdateDTO updateDTO)
        {
            var existingRole = await _repository.GetRoleById(updateDTO.RoleId);
            if (existingRole == null)
            {
                throw new AppException("Role not found");
            }
            var updateRole = _mapper.Map<UpdateDTO, Role>(updateDTO, existingRole);
            await _repository.UpdateRole(existingRole, updateRole);
        }
        private async Task<string> GetNextId()
        {
            Role lastRole = await _repository.GetLastRole();
            int id = 0;
            if (lastRole == null)
            {
                id = 1;
            }
            else
            {
                id = IDGenerator.ExtractNumberFromId(lastRole.RoleId) + 1;
            }
            string roleId = IDGenerator.GenerateRoleId(id);
            return roleId;
        }
    }
}
