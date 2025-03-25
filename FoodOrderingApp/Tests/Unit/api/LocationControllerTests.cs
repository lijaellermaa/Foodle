using App.Contracts.BLL;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Public.DTO.v1;
using WebApp.ApiControllers;

namespace Tests.Unit.api;

using Helper=Helpers.ControllerUnitTestHelper<Public.DTO.AutoMapperConfig>;

public class LocationControllerTests
{

    [Fact]
    public async Task GetAll_ReturnsAllLocations_WhenLocationsExist()
    {
        // Arrange
        var mockBll = new Mock<IAppBll>();
        var mapper = Helper.GetMapper();
        var controller = new LocationsController(mockBll.Object, mapper);
        mockBll
            .Setup(bll => bll.LocationService.GetAllAsync(null, false))
            .ReturnsAsync(Helper.GetEntities<App.BLL.DTO.Location>(2));

        // Act
        var result = await controller.GetLocations();

        // Assert
        result.Result.Should()
            .BeOfType<OkObjectResult>()
            .Which.Value.Should()
            .BeAssignableTo<IEnumerable<Location>>()
            .Which.Should()
            .HaveCount(2);
    }

    [Fact]
    public async Task GetLocation_ReturnsSingleLocation_WhenLocationExists()
    {
        // Arrange
        var mockBll = new Mock<IAppBll>();
        var mapper = Helper.GetMapper();
        var controller = new LocationsController(mockBll.Object, mapper);
        var locationId = Guid.NewGuid();
        var locationDto = Helper.GetEntity<App.BLL.DTO.Location>(locationId);
        mockBll
            .Setup(bll => bll.LocationService.FirstOrDefaultAsync(locationId, It.IsAny<Guid?>(), It.IsAny<bool>()))
            .ReturnsAsync(locationDto);

        // Act
        var result = await controller.GetLocation(locationId);

        // Assert
        result.Result.Should()
            .BeOfType<OkObjectResult>()
            .Which.Value.Should()
            .BeAssignableTo<Location>()
            .Which.Id.Should()
            .Be(locationId);
    }

    [Fact]
    public async Task PostLocation_CreatesNewLocation_Successfully()
    {
        // Arrange
        var mockBll = new Mock<IAppBll>();
        var mapper = Helper.GetMapper();
        var controller = new LocationsController(mockBll.Object, mapper);

        var locationRequest = new LocationRequest
        {
            Area = "Test Area",
            Town = "Test Town",
            Address = "Test Address"
        };

        mockBll
            .Setup(bll => bll.LocationService.Add(It.IsAny<App.BLL.DTO.Location>()))
            .Returns((App.BLL.DTO.Location location) => Helper.AddId(location));

        // Act
        var result = await controller.PostLocation(locationRequest);

        // Assert
        result.Result.Should()
            .BeOfType<CreatedAtActionResult>()
            .Which.Value.Should()
            .BeAssignableTo<Location>()
            .Which.Area.Should()
            .Be(locationRequest.Area);
    }

    [Fact]
    public async Task PutLocation_UpdatesExistingLocation_Successfully()
    {
        // Arrange
        var mockBll = new Mock<IAppBll>();
        var mapper = Helper.GetMapper();
        var controller = new LocationsController(mockBll.Object, mapper);
        var existingLocation = Helper.GetEntity<App.BLL.DTO.Location>();
        var updatedLocationRequest = new LocationRequest
        {
            Id = existingLocation.Id,
            Area = "Updated Area",
            Town = "Updated Town",
            Address = "Updated Address"
        };
        mockBll.Setup(bll => bll.LocationService.FirstOrDefaultAsync(existingLocation.Id, It.IsAny<Guid?>(), It.IsAny<bool>()))
            .ReturnsAsync(existingLocation);

        // Act
        var result = await controller.PutLocation(existingLocation.Id, updatedLocationRequest);

        // Assert
        result.Should().BeOfType<NoContentResult>();
        mockBll.Verify(bll => bll.LocationService.Update(It.IsAny<App.BLL.DTO.Location>()), Times.Once);
        mockBll.Verify(bll => bll.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteLocation_RemovesExistingLocation_Successfully()
    {
        // Arrange
        var mockBll = new Mock<IAppBll>();
        var mapper = Helper.GetMapper();
        var locationId = Guid.NewGuid();
        var locationToRemove = Helper.GetEntity<App.BLL.DTO.Location>(locationId);
        mockBll.Setup(bll => bll.LocationService.RemoveAsync(locationId, It.IsAny<Guid?>())).ReturnsAsync(locationToRemove);
        var controller = new LocationsController(mockBll.Object, mapper);

        // Act
        var result = await controller.DeleteLocation(locationId);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().BeOfType<Location>()
            .Which.Id.Should().Be(locationId);
    }
}