using Moq;
using QuickServiceUnitTest.ServiceTests.DerivedClasses;
using QuickServiceUnitTest.ServiceTests.Fixtures;
using QuickServiceWebAPI.DTOs.Group;
using QuickServiceWebAPI.DTOs.ServiceCategory;
using QuickServiceWebAPI.DTOs.Sla;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QuickServiceUnitTest.ServiceTests
{
    public class SlaServiceTest
    {
        private readonly SlaServiceTestFixture _fixture;

        public SlaServiceTest()
        {
            _fixture = new SlaServiceTestFixture();
        }
        [Fact]
        public async Task CreateSla_ValidInput_Success()
        {
            // Arrange
            var slaService = _fixture.SlaService;

            var createSlaDTO = new CreateSlaDTO
            {
                Slaname = "Sla Name Test Valid Input",
                Description = "Description Test Valid Input",
            };
            var defaultSLa = new Sla
            {
                Slaid = "SELA000001"
            };

            _fixture.MockMapper.Setup(mapper => mapper.Map<Sla>(It.IsAny<CreateSlaDTO>())).Returns(new Sla());
            _fixture.MockMapper.Setup(mapper => mapper.Map<Sla>(createSlaDTO)).Returns(new Sla());
            _fixture.MockMapper.Setup(mapper => mapper.Map<Sla>(It.IsAny<CreateSlaDTO>())).Returns(new Sla());
            _fixture.MockSlaRepository.Setup(repo => repo.GetDefaultSla()).ReturnsAsync((Sla?)defaultSLa);
            _fixture.MockSlaRepository.Setup(repo => repo.AddSLA(It.IsAny<Sla>())).ReturnsAsync((Sla?)defaultSLa);

            await slaService.CreateSLA(createSlaDTO);

            // Assert
            _fixture.MockSlaRepository.Verify(repo => repo.AddSLA(It.IsAny<Sla>()), Times.Once);
        }
        [Fact]
        public async Task UpdateSla_ValidData_UpdatesSlaSuccessfully()
        {

            var testableSla = new TestableSlaSerivce(
                _fixture.MockSlaRepository.Object,
                _fixture.MockMapper.Object,
                _fixture.MockISlametricService.Object,
                _fixture.MockSlametricRepository.Object
            );

            var updateSlaDTO = new UpdateSlaDTO
            {
                SlaId = "SELA000001",
                Slaname = "Sla Name Test Valid Input",
                Description = "Sla Description Test Valid Input",
            };

            var existingSla = new Sla
            {
                Slaid = "SECA000001",
                Slaname = "Slaname Name Test Update Valid Input",
                Description = "Sla Description Test Update Valid Input",
                // Other properties
            };
            _fixture.MockSlaRepository.Setup(repo => repo.GetSLAById(It.IsAny<string>()))
                .ReturnsAsync(existingSla);

            _fixture.MockMapper.Setup(map => map.Map(It.IsAny<UpdateSlaDTO>(), It.IsAny<Sla>()))
                .Returns(existingSla);
            // Act
            await testableSla.UpdateSLA(updateSlaDTO);

            // Assert
            _fixture.MockSlaRepository.Verify(repo => repo.UpdateSLA(existingSla), Times.Once);
        }
        [Fact]
        public async Task UpdateSla_SlaNotFound_ThrowsAppException()
        {

            var slaService = _fixture.SlaService;

            var updateDTO = new UpdateSlaDTO
            {
                SlaId = "SELA000000",
            // Other properties
            };

            _fixture.MockSlaRepository.Setup(repo => repo.GetSLAById(It.IsAny<string>()))
                .ReturnsAsync((Sla)null);

            // Act and Assert
            await Assert.ThrowsAsync<AppException>(async () => await slaService.UpdateSLA(updateDTO));
        }

        [Fact]
        public async Task DeleteSLA_InvalidId_ThrowsAppException()
        {
            // Arrange
            var id = "invalid_id";

            _fixture.MockSlaRepository.Setup(repo => repo.GetSLAById(id))
                .ReturnsAsync((Sla)null);

            // Act & Assert
            await Assert.ThrowsAsync<AppException>(
                () => _fixture.SlaService.DeleteSLA(id));
        }
        [Fact]
        public async Task DeleteSLA_ValidId_DeletesSuccessfully()
        {
            // Arrange
            var id = "valid_id";
            
            var slaDelete = new Sla
            {
                Slaid = "SELA000001",
                Slaname = "Default SLA policy",
                Description = "Default policy"
                // Initialize properties
            };
            var sla = new Sla
            {
                Slaid = "SELA000001"
            };
            var slaMetric = new Slametric
            {
                SlametricId = "SLAM000001"
            };
            _fixture.MockSlaRepository.Setup(repo => repo.GetSLAById(id))
                .ReturnsAsync(slaDelete);
            _fixture.MockSlametricRepository.Setup(repo => repo.DeleteSlaMetricsOfSla(sla));
            // Act
            await _fixture.SlaService.DeleteSLA(id);

            // Assert
            _fixture.MockSlaRepository.Verify(repo => repo.DeleteSLA(sla), Times.Once);
        }
    }
}
