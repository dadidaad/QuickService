using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using QuickServiceWebAPI.Controllers;
using QuickServiceWebAPI.DTOs.Attachment;
using QuickServiceWebAPI.DTOs.Change;
using QuickServiceWebAPI.DTOs.Group;
using QuickServiceWebAPI.DTOs.ServiceCategory;
using QuickServiceWebAPI.DTOs.User;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Repositories.Implements;
using QuickServiceWebAPI.Services;
using QuickServiceWebAPI.Services.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickServiceUnitTest
{
    public class ChangesControllerTest
    {
        private readonly ChangesController _controller;
        public ChangesControllerTest()
        {
            var dbContext = new QuickServiceContext();
            var mockLogger = new Mock<ILogger<ChangeRepository>>();

            var changeService = new ChangeService(
                new ChangeRepository(dbContext, mockLogger.Object),
                new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Change, ChangeDTO>();
                    cfg.CreateMap<User, UserDTO>();
                    cfg.CreateMap<Group, GroupDTO>();
                }).CreateMapper(),
            new Mock<IUserRepository>().Object,
                new Mock<IGroupRepository>().Object,
                new Mock<ILogger<ChangeService>>().Object,
                new Mock<IAttachmentService>().Object
            );
            _controller = new ChangesController(changeService);
        }

        [Fact]
        public async Task GetAllChange_ReturnsOkResult()
        {
            var result = await _controller.GetAllChange();
            var resultType = result as OkObjectResult;
            var resultList = resultType.Value as List<ChangeDTO>;

            Assert.NotNull(result);
            Assert.IsType<List<ChangeDTO>>(resultType.Value);
            Assert.Equal(1, resultList.Count);
        }
    }
}
