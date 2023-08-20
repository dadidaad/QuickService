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
    public class UserServiceTestFixture
    {
        public Mock<IUserRepository> MockUserRepository { get; private set; }
        public Mock<ILogger<UserService>> MockLogger { get; private set; }
        public Mock<IJWTUtils> MockJWTUtils { get; private set; }
        public Mock<IMapper> MockMapper { get; private set; }
        public Mock<IOptions<AzureStorageConfig>> MockStorageConfig { get; private set; }
        public Mock<IRoleRepository> MockRoleRepository { get; private set; }
        public UserService UserService { get; private set; }

        public UserServiceTestFixture()
        {
            MockUserRepository = new Mock<IUserRepository>();
            MockLogger = new Mock<ILogger<UserService>>();
            MockJWTUtils = new Mock<IJWTUtils>();
            MockMapper = new Mock<IMapper>();
            MockStorageConfig = new Mock<IOptions<AzureStorageConfig>>();
            MockRoleRepository = new Mock<IRoleRepository>();
            var testStorageConfig = new AzureStorageConfig
            {
                ImageContainer = "test_images_container",
                WallpaperContainer = "test_wallpaper_container"
            };

            MockStorageConfig.Setup(config => config.Value)
                .Returns(testStorageConfig);
 
            UserService = new UserService(
               MockUserRepository.Object,
               MockLogger.Object,
               MockJWTUtils.Object,
               MockMapper.Object,
               MockStorageConfig.Object,
               MockRoleRepository.Object
            );
        }

    }
    [CollectionDefinition("UserServiceTests")]
    public class UserServiceCollection : ICollectionFixture<UserServiceTestFixture>
    {
    }
}
