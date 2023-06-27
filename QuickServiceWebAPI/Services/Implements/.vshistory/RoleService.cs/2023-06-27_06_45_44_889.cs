using AutoMapper;
using QuickServiceWebAPI.DTOs.Role;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Services.Implements
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;
        private readonly ILogger<RoleService> _logger;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository repository, ILogger<RoleService> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task CreateRole(CreateDTO createDTO)
        {
            var role = _mapper.Map<Role>(createDTO);
            role.RoleId = await GetNextId();
            await _repository.CreateRole(role);
        }

        public async Task DeleteRole(string roleId)
        {
            var role = await _repository.GetRoleById(roleId);
            if(role == null)
            {
                throw new AppException("Role not found");
            }
            if(role.TotalUserInRole > 0)
            {
                foreach(var user in role.Users)
                {
                    user.RoleId = null;
                    user.Role = null;
                }
                await _repository.UpdateUserRole(role);
            }
        }

        public async Task<Role> GetRoleById(string roleId)
        {
            return await _repository.GetRoleById(roleId);
        }

        public List<Role> GetRoles()
        {
            return _repository.GetRoles();
        }

        public Task UpdateRole(UpdateDTO updateDTO)
        {
            throw new NotImplementedException();
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
