using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Services.Implements;
using QuickServiceWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickServiceUnitTest.ServiceTests.Fixtures
{
    public class RoleServiceTestFixture
    {
        public Mock<IRoleRepository> MockRoleRepository { get; private set; }
        public Mock<ILogger<RoleService>> MockLogger { get; private set; }
        public Mock<IJWTUtils> MockJWTUtils { get; private set; }
        public Mock<IMapper> MockMapper { get; private set; }
        public Mock<IOptions<AzureStorageConfig>> MockStorageConfig { get; private set; }
        public RoleService RoleService { get; private set; }
        public Mock<IPermissionRepository> MockPermissionRepository { get; private set; }

        public RoleServiceTestFixture()
        {
            MockRoleRepository = new Mock<IRoleRepository>();
            MockLogger = new Mock<ILogger<RoleService>>();
            MockMapper = new Mock<IMapper>();
            MockPermissionRepository = new Mock<IPermissionRepository>();

            RoleService = new RoleService(
               MockRoleRepository.Object,
               MockMapper.Object,
               MockPermissionRepository.Object
            );
        }

        [CollectionDefinition("RoleServiceTest")]
        public class RoleServiceCollection : ICollectionFixture<RoleServiceTestFixture>
        {
        }
    }
}
