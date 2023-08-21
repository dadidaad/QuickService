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
    public class ServiceCategoryTestFixture
    {
        public Mock<IServiceCategoryRepository> MockServiceCategoryRepository { get; private set; }
        public Mock<ILogger<RoleService>> MockLogger { get; private set; }
        public Mock<IJWTUtils> MockJWTUtils { get; private set; }
        public Mock<IMapper> MockMapper { get; private set; }
        public Mock<IOptions<AzureStorageConfig>> MockStorageConfig { get; private set; }
        public ServiceCategoryService ServiceCategoryService { get; private set; }
        public Mock<IPermissionRepository> MockPermissionRepository { get; private set; }

        public ServiceCategoryTestFixture()
        {
            MockServiceCategoryRepository = new Mock<IServiceCategoryRepository>();
            MockMapper = new Mock<IMapper>();

            ServiceCategoryService = new ServiceCategoryService(
               MockServiceCategoryRepository.Object,
               MockMapper.Object
            );
        }
    }
}
