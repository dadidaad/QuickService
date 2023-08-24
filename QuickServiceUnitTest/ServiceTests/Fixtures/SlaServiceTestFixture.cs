using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Services;
using QuickServiceWebAPI.Services.Implements;
using QuickServiceWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickServiceUnitTest.ServiceTests.Fixtures
{
    public class SlaServiceTestFixture
    {
        public Mock<ISlaRepository> MockSlaRepository { get; private set; }
        public Mock<ILogger<RoleService>> MockLogger { get; private set; }
        public Mock<IJWTUtils> MockJWTUtils { get; private set; }
        public Mock<IMapper> MockMapper { get; private set; }
        public Mock<IOptions<AzureStorageConfig>> MockStorageConfig { get; private set; }
        public SlaService SlaService { get; private set; }
        public Mock<ISlametricService> MockISlametricService { get; private set; }
        public Mock<ISlametricRepository> MockSlametricRepository { get; private set; }

        public SlaServiceTestFixture()
        {
            MockSlaRepository = new Mock<ISlaRepository>();
            MockMapper = new Mock<IMapper>();
            MockISlametricService = new Mock<ISlametricService>();
            MockSlametricRepository = new Mock<ISlametricRepository>();

            SlaService = new SlaService(
               MockSlaRepository.Object,
               MockMapper.Object,
               MockISlametricService.Object,
               MockSlametricRepository.Object
            );
        }
    }
}
