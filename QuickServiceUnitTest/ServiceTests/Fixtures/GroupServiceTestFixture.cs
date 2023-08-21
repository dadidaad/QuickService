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
    public class GroupServiceTestFixture
    {
        public Mock<IGroupRepository> MockGroupRepository { get; private set; }
        public Mock<IMapper> MockMapper { get; private set; }
        public GroupService GroupService { get; private set; }
        public Mock<IUserRepository> MockUserRepository { get; private set; }
        public Mock<IBusinessHourRepository> MockBusinessHourRepository { get; private set; }

        public GroupServiceTestFixture()
        {
            MockGroupRepository = new Mock<IGroupRepository>();
            MockMapper = new Mock<IMapper>();
            MockUserRepository = new Mock<IUserRepository>();
            MockBusinessHourRepository = new Mock<IBusinessHourRepository>();

            GroupService = new GroupService(
              MockGroupRepository.Object,
              MockMapper.Object,
              MockUserRepository.Object,
              MockBusinessHourRepository.Object
           );
        }
    }
}
