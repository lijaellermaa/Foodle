using App.Contracts.BLL;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Public.DTO.v1;
using WebApp.ApiControllers;

namespace Tests.Unit.api;

using Helper=Helpers.ControllerUnitTestHelper<Public.DTO.AutoMapperConfig>;

public class PricesControllerTests
{
    [Fact]
    public async Task GetAll_ReturnsAllPrices_WhenPricesExist()
    {
        // Arrange
        var mockBll = new Mock<IAppBll>();
        var mapper = Helper.GetMapper();
        var controller = new PricesController(mockBll.Object, mapper);
        mockBll
            .Setup(bll => bll.PriceService.GetAllAsync(null, false))
            .ReturnsAsync(Helper.GetEntities<App.BLL.DTO.Price>(2));

        // Act
        var result = await controller.GetPrices();

        // Assert
        result.Result.Should()
            .BeOfType<OkObjectResult>()
            .Which.Value.Should()
            .BeAssignableTo<IEnumerable<Price>>()
            .Which.Should()
            .HaveCount(2);
    }

    [Fact]
    public async Task GetPrice_ReturnsSinglePrice_WhenPriceExists()
    {
        // Arrange
        var mockBll = new Mock<IAppBll>();
        var mapper = Helper.GetMapper();
        var controller = new PricesController(mockBll.Object, mapper);
        var priceId = Guid.NewGuid();
        var priceDto = Helper.GetEntity<App.BLL.DTO.Price>(priceId);
        mockBll
            .Setup(bll => bll.PriceService.FirstOrDefaultAsync(priceId, It.IsAny<Guid?>(), It.IsAny<bool>()))
            .ReturnsAsync(priceDto);

        // Act
        var result = await controller.GetPrice(priceId);

        // Assert
        result.Result.Should()
            .BeOfType<OkObjectResult>()
            .Which.Value.Should()
            .BeAssignableTo<Price>()
            .Which.Id.Should()
            .Be(priceId);
    }

    [Fact]
    public async Task PostPrice_CreatesNewPrice_Successfully()
    {
        // Arrange
        var mockBll = new Mock<IAppBll>();
        var mapper = Helper.GetMapper();
        var controller = new PricesController(mockBll.Object, mapper);

        var priceRequest = new PriceRequest
        {
            Value = 7,
            PreviousValue = 9,
            ProductId = Guid.NewGuid(),
            Comment = "Discount Company"
        };

        mockBll
            .Setup(bll => bll.PriceService.Add(It.IsAny<App.BLL.DTO.Price>()))
            .Returns((App.BLL.DTO.Price price) => Helper.AddId(price));

        // Act
        var result = await controller.PostPrice(priceRequest);

        // Assert
        result.Result.Should()
            .BeOfType<CreatedAtActionResult>()
            .Which.Value.Should()
            .BeAssignableTo<Price>()
            .Which.Value.Should()
            .Be(priceRequest.Value);
    }

    [Fact]
    public async Task PutPrice_UpdatesExistingPrice_Successfully()
    {
        // Arrange
        var mockBll = new Mock<IAppBll>();
        var mapper = Helper.GetMapper();
        var controller = new PricesController(mockBll.Object, mapper);
        var existingPrice = Helper.GetEntity<App.BLL.DTO.Price>();
        var updatedPriceRequest = new PriceRequest
        {
            Id = existingPrice.Id,
            Value = 10,
            PreviousValue = 20,
            ProductId = Guid.NewGuid(),
            Comment = "New Discount Company"
        };
        mockBll.Setup(bll => bll.PriceService.FirstOrDefaultAsync(existingPrice.Id, It.IsAny<Guid?>(), It.IsAny<bool>()))
            .ReturnsAsync(existingPrice);

        // Act
        var result = await controller.PutPrice(existingPrice.Id, updatedPriceRequest);

        // Assert
        result.Result.Should().BeOfType<NoContentResult>();
        mockBll.Verify(bll => bll.PriceService.Update(It.IsAny<App.BLL.DTO.Price>()), Times.Once);
        mockBll.Verify(bll => bll.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeletePrice_RemovesExistingPrice_Successfully()
    {
        // Arrange
        var mockBll = new Mock<IAppBll>();
        var mapper = Helper.GetMapper();
        var priceId = Guid.NewGuid();
        var priceToRemove = Helper.GetEntity<App.BLL.DTO.Price>(priceId);
        mockBll.Setup(bll => bll.PriceService.RemoveAsync(priceId, It.IsAny<Guid?>())).ReturnsAsync(priceToRemove);
        var controller = new PricesController(mockBll.Object, mapper);

        // Act
        var result = await controller.DeletePrice(priceId);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().BeOfType<Price>()
            .Which.Id.Should().Be(priceId);
    }
}