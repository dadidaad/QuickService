using Moq;
using QuickServiceUnitTest.ServiceTests.Fixtures;
using QuickServiceWebAPI.DTOs.Role;
using QuickServiceWebAPI.DTOs.ServiceItem;
using QuickServiceWebAPI.DTOs.User;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Services;
using QuickServiceWebAPI.Services.Implements;
using QuickServiceWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickServiceUnitTest.ServiceTests
{
    [Collection("ServiceItemServiceTest")]
    public class ServiceItemServiceTest
    {
        
        private readonly ServiceItemServiceTestFixture _fixture;

        public ServiceItemServiceTest()
        {
            _fixture = new ServiceItemServiceTestFixture();
        }

        [Fact]
        public async Task CreateServiceItem_Valid_Success()
        {
            // Arrange
            var serviceItemService = _fixture.ServiceItemService;

            var serviceCategory = new ServiceCategory
            {
                ServiceCategoryId = "SECA1"
            };


            var createUpdateServiceItemDTO = new CreateUpdateServiceItemDTO
            {
                ServiceCategoryId = serviceCategory.ServiceCategoryId
            };

            _fixture.MockServiceCategoryRepository.Setup(repo => repo.GetServiceCategoryById(It.IsAny<string>())).ReturnsAsync((ServiceCategory?)serviceCategory);

            _fixture.MockMapper.Setup(mapper => mapper.Map<ServiceItem>(It.IsAny<CreateUpdateServiceItemDTO>())).Returns(new ServiceItem());
            _fixture.MockMapper.Setup(mapper => mapper.Map<ServiceItem>(createUpdateServiceItemDTO)).Returns(new ServiceItem());
            // Act
            await serviceItemService.CreateServiceItem(createUpdateServiceItemDTO);

            // Assert
            _fixture.MockServiceItemRepository.Verify(repo => repo.AddServiceItem(It.IsAny<ServiceItem>()), Times.Once);
        }

        [Fact]
        public async Task CreateServiceItem_InValidServiceCategory_Success()
        {
            // Arrange
            var serviceItemService = _fixture.ServiceItemService;

            var createUpdateServiceItemDTO = new CreateUpdateServiceItemDTO
            {
                ServiceCategoryId = null
            };

            _fixture.MockServiceCategoryRepository.Setup(repo => repo.GetServiceCategoryById(It.IsAny<string>()))
                 .ReturnsAsync((ServiceCategory?)null);

            _fixture.MockMapper.Setup(mapper => mapper.Map<ServiceItem>(It.IsAny<CreateUpdateServiceItemDTO>())).Returns(new ServiceItem());
            _fixture.MockMapper.Setup(mapper => mapper.Map<ServiceItem>(createUpdateServiceItemDTO)).Returns(new ServiceItem());

            // Act and Assert
            await Assert.ThrowsAsync<AppException>(async () => await serviceItemService.CreateServiceItem(createUpdateServiceItemDTO));
        }

        [Fact]
        public async Task DeleteServiceItem_ValidId_Success()
        {
            // Arrange
            var serviceItemId = "valid_id";
            var serviceItem = new ServiceItem
            {
                // Initialize properties
            };


            _fixture.MockServiceItemRepository.Setup(repo => repo.GetServiceItemById(serviceItemId))
                .ReturnsAsync(serviceItem);

            // Act
            await _fixture.ServiceItemService.DeleteServiceItem(serviceItemId);

            // Assert
            _fixture.MockServiceItemRepository.Verify(repo => repo.DeleteServiceItem(serviceItem), Times.Once);
        }

        [Fact]
        public async Task DeleteServiceItem_InvalidId_ThrowsAppException()
        {
            // Arrange
            var serviceItemId = "valid_id";

            _fixture.MockServiceItemRepository.Setup(repo => repo.GetServiceItemById(serviceItemId))
                .ReturnsAsync((ServiceItem?)null);

            // Act & Assert
            await Assert.ThrowsAsync<AppException>(
                () => _fixture.ServiceItemService.DeleteServiceItem(serviceItemId));
        }
    }
}
