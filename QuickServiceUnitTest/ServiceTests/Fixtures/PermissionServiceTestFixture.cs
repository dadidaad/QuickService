using AutoMapper;
using Moq;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Services.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickServiceUnitTest.ServiceTests.Fixtures
{
    public class PermissionServiceTestFixture
    {
        public Mock<IPermissionRepository> MockPermissionRepository { get; private set; }
        public Mock<IMapper> MockMapper { get; private set; }
        public PermissionService PermissionService { get; private set; }
        public Mock<IRoleRepository> MockRoleRepository { get; private set; }
        public Mock<IBusinessHourRepository> MockBusinessHourRepository { get; private set; }
        public object MockGroupRepository { get; internal set; }

        public PermissionServiceTestFixture()
        {
            MockPermissionRepository = new Mock<IPermissionRepository>();
            MockMapper = new Mock<IMapper>();
            MockRoleRepository = new Mock<IRoleRepository>();

            PermissionService = new PermissionService(
              MockPermissionRepository.Object,
              MockMapper.Object,
              MockRoleRepository.Object
           );
        }
    }
}
