using AutoMapper;
using QuickServiceWebAPI.DTOs.Role;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;

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
            await _repository.CreateRole(role);
        }

        public Task DeleteRole(string roleId)
        {
            throw new NotImplementedException();
        }

        public Task<Role> GetRoleById(string roleId)
        {
            throw new NotImplementedException();
        }

        public List<Role> GetRoles()
        {
            throw new NotImplementedException();
        }

        public Task UpdateRole(UpdateDTO updateDTO)
        {
            throw new NotImplementedException();
        }
    }
}
