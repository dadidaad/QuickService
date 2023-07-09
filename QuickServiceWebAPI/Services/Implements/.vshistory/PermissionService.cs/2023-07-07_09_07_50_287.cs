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
        public PermissionService(IPermissionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public Task AssignPermissionsToRole(AssignPermissionsDTO assignPermissionsDTO)
        {
            return Task.CompletedTask;
        }

        public async Task<HashSet<string>> GetPermissionsByRole(string roleId)
        {
            List<Permission> permissions = await _repository.GetPermissionsByRole(roleId);
            return permissions.Select(p => $"{p.PermissionModule}.{p.PermissionName}").ToHashSet();
        }
    }
}
