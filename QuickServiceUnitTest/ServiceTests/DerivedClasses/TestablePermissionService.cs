using AutoMapper;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Services.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickServiceUnitTest.ServiceTests.DerivedClasses
{
    public class TestablePermissionService : PermissionService
    {
        public TestablePermissionService(
            IPermissionRepository permissionRepository,
            IMapper mapper,
            IRoleRepository roleRepository
           )
            : base(permissionRepository, mapper, roleRepository)
        {
        }
    }
}
