using Moq;
using QuickServiceUnitTest.ServiceTests.DerivedClasses;
using QuickServiceUnitTest.ServiceTests.Fixtures;
using QuickServiceWebAPI.DTOs.Group;
using QuickServiceWebAPI.DTOs.Role;
using QuickServiceWebAPI.DTOs.ServiceCategory;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickServiceUnitTest.ServiceTests
{
    public class ServiceCategoryServiceTest
    {
        private readonly ServiceCategoryTestFixture _fixture;

        public ServiceCategoryServiceTest()
        {
            _fixture = new ServiceCategoryTestFixture();
        }
        [Fact]
        public async Task CreateServiceCategory_ValidInput_Success()
        {
            // Arrange
            var serviceCategoryService = _fixture.ServiceCategoryService;

            var createUpdateServiceCategoryDTO = new CreateUpdateServiceCategoryDTO
            {
                ServiceCategoryName = "Service Category Name Test Valid Input",
                Description = "Service Category Description Test Valid Input",
            };

            _fixture.MockMapper.Setup(mapper => mapper.Map<ServiceCategory>(It.IsAny<CreateUpdateGroupDTO>())).Returns(new ServiceCategory());
            _fixture.MockMapper.Setup(mapper => mapper.Map<ServiceCategory>(createUpdateServiceCategoryDTO)).Returns(new ServiceCategory());

            _fixture.MockMapper.Setup(mapper => mapper.Map<ServiceCategory>(It.IsAny<CreateUpdateGroupDTO>())).Returns(new ServiceCategory());


            // Act
            await serviceCategoryService.CreateServiceCategory(createUpdateServiceCategoryDTO);

            // Assert
            _fixture.MockServiceCategoryRepository.Verify(repo => repo.AddServiceCategory(It.IsAny<ServiceCategory>()), Times.Once);
        }
        [Fact]
        public async Task UpdateSerivceCategory_ValidData_UpdatesGroupSuccessfully()
        {

            var testableServiceCategory = new TestableServiceCategory(
                _fixture.MockServiceCategoryRepository.Object,
                _fixture.MockMapper.Object
            );

            var createUpdateServiceCategoryDTO = new CreateUpdateServiceCategoryDTO
            {
                ServiceCategoryName = "Service Category Name Test Valid Input",
                Description = "Service Category Description Test Valid Input",
            };

            var existingServiceCategory = new ServiceCategory
            {
                ServiceCategoryId = "SECA000001",
                ServiceCategoryName = "Service Category Name Test Valid Input",
                Description = "Service Category Description Test Valid Input",
                // Other properties
            };
            var serivceCategoryId = "SECA000001";
            _fixture.MockServiceCategoryRepository.Setup(repo => repo.GetServiceCategoryById(It.IsAny<string>()))
                .ReturnsAsync(existingServiceCategory);

            _fixture.MockMapper.Setup(map => map.Map(It.IsAny<CreateUpdateServiceCategoryDTO>(), It.IsAny<ServiceCategory>()))
                .Returns(existingServiceCategory);
            // Act
            await testableServiceCategory.UpdateServiceCategory(serivceCategoryId, createUpdateServiceCategoryDTO);

            // Assert
            _fixture.MockServiceCategoryRepository.Verify(repo => repo.UpdateServiceCategory(existingServiceCategory), Times.Once);
        }
        [Fact]
        public async Task UpdateGroup_ServiceCategoryNotFound_ThrowsAppException()
        {

            var serviceCategoryService = _fixture.ServiceCategoryService;

            var updateDTO = new CreateUpdateServiceCategoryDTO
            {
                ServiceCategoryName = "Service Category Name Test Invalid Input",
                Description = "Service Category Description Test Invalid Input",
                // Other properties
            };
            string serviceCategoryId = "SECA000000";

            _fixture.MockServiceCategoryRepository.Setup(repo => repo.GetServiceCategoryById(It.IsAny<string>()))
                .ReturnsAsync((ServiceCategory)null);

            // Act and Assert
            await Assert.ThrowsAsync<AppException>(async () => await serviceCategoryService.UpdateServiceCategory(serviceCategoryId, updateDTO));
        }

        [Fact]
        public async Task DeleteServiceCategory_InvalidId_ThrowsAppException()
        {
            // Arrange
            var id = "invalid_id";

            _fixture.MockServiceCategoryRepository.Setup(repo => repo.GetServiceCategoryById(id))
                .ReturnsAsync((ServiceCategory)null);

            // Act & Assert
            await Assert.ThrowsAsync<AppException>(
                () => _fixture.ServiceCategoryService.DeleteServiceCategory(id));
        }
        [Fact]
        public async Task DeleteServiceCategory_ValidId_DeletesSuccessfully()
        {
            // Arrange
            var id = "valid_id";
            var serviceCategory = new ServiceCategory
            {
                ServiceCategoryId = "SECA000001",
                ServiceCategoryName = "Hardware Problems",
                Description = "All about hardware"
                // Initialize properties
            };

            _fixture.MockServiceCategoryRepository.Setup(repo => repo.GetServiceCategoryById(id))
                .ReturnsAsync(serviceCategory);

            // Act
            await _fixture.ServiceCategoryService.DeleteServiceCategory(id);

            // Assert
            _fixture.MockServiceCategoryRepository.Verify(repo => repo.DeleteServiceCategory(serviceCategory), Times.Once);
        }

    }
}
