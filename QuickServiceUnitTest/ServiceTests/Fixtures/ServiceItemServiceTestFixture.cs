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
    public class ServiceItemServiceTestFixture
    {
        public Mock<IServiceItemRepository> MockServiceItemRepository { get; private set; }
        public Mock<ILogger<ServiceItemService>> MockLogger { get; private set; }
        public Mock<IOptions<AzureStorageConfig>> MockStorageConfig { get; private set; }
        public Mock<IMapper> MockMapper { get; private set; }
              
        public Mock<IServiceCategoryRepository> MockServiceCategoryRepository { get; private set; }
        public Mock<IServiceItemCustomFieldRepository> MockServiceItemCustomFieldRepository { get; private set; }
        public Mock<IWorkflowRepository> MockWorkflowRepository { get; private set; }
        public ServiceItemService ServiceItemService { get; private set; }

        public ServiceItemServiceTestFixture()
        {
            MockServiceItemRepository = new Mock<IServiceItemRepository>();
            MockLogger = new Mock<ILogger<ServiceItemService>>();
            MockStorageConfig = new Mock<IOptions<AzureStorageConfig>>();
            MockMapper = new Mock<IMapper>();
            MockServiceCategoryRepository = new Mock<IServiceCategoryRepository>();
            MockServiceItemCustomFieldRepository = new Mock<IServiceItemCustomFieldRepository>();
            MockWorkflowRepository = new Mock<IWorkflowRepository>();
            ServiceItemService = new ServiceItemService(
                MockServiceItemRepository.Object,
                MockMapper.Object,
                MockStorageConfig.Object,
                MockLogger.Object,
                MockServiceCategoryRepository.Object,
                MockServiceItemCustomFieldRepository.Object,
                MockWorkflowRepository.Object);
        }

        [CollectionDefinition("ServiceItemServiceTest")]
        public class ServiceItemServiceTestCollection : ICollectionFixture<ServiceItemServiceTestFixture>
        {
        }
    }


}
