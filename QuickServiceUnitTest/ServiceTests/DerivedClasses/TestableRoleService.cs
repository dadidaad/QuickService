using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Repositories.Implements;
using QuickServiceWebAPI.Services.Implements;
using QuickServiceWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickServiceUnitTest.ServiceTests.DerivedClasses
{
    public class TestableRoleService : RoleService
    {
        public TestableRoleService(
        IRoleRepository roleRepository,
        IMapper mapper,
        IPermissionRepository permissionRepository)
        : base(roleRepository, mapper, permissionRepository)
        {
        }

    }
}
