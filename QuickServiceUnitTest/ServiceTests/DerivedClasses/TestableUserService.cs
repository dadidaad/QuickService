using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Services.Implements;
using QuickServiceWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickServiceUnitTest.ServiceTests.DerivedClasses
{
    public class TestableUserService : UserService
    {
        public TestableUserService(
        IUserRepository userRepository,
        ILogger<UserService> logger,
        IJWTUtils jwtUtils,
        IMapper mapper,
        IOptions<AzureStorageConfig> storageConfig,
        IRoleRepository roleRepository)
        : base(userRepository, logger, jwtUtils, mapper, storageConfig, roleRepository)
        {
        }

        protected override async Task<string?> GetPathImpageUpload(IFormFile image, string userId, string container)
        {
            // Mocked behavior to return a string directly
            return "mocked_path";
        }
    }
}
