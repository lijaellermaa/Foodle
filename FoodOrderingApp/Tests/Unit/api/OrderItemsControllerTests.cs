using App.Contracts.BLL;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Public.DTO.v1;
using WebApp.ApiControllers;

namespace Tests.Unit.api;

using Helper=Helpers.ControllerUnitTestHelper<Public.DTO.AutoMapperConfig>;

public class OrderItemsControllerTests
{
    [Fact]
    public async Task GetAll_ReturnsAllOrderItems_WhenOrderItemsExist()
    {
        // Arrange
        var mockBll = new Mock<IAppBll>();
        var mapper = Helper.GetMapper();
        var controller = new OrderItemsController(mockBll.Object, mapper);
        mockBll
            .Setup(bll => bll.OrderItemService.GetAllAsync(null, false))
            .ReturnsAsync(Helper.GetEntities<App.BLL.DTO.OrderItem>(2));

        // Act
        var result = await controller.GetOrderItems();

        // Assert
        result.Result.Should()
            .BeOfType<OkObjectResult>()
            .Which.Value.Should()
            .BeAssignableTo<IEnumerable<OrderItem>>()
            .Which.Should()
            .HaveCount(2);
    }

    [Fact]
    public async Task GetOrderItem_ReturnsSingleOrderItem_WhenOrderItemExists()
    {
        // Arrange
        var mockBll = new Mock<IAppBll>();
        var mapper = Helper.GetMapper();
        var controller = new OrderItemsController(mockBll.Object, mapper);
        var orderItemId = Guid.NewGuid();
        var orderItemDto = Helper.GetEntity<App.BLL.DTO.OrderItem>(orderItemId);
        mockBll
            .Setup(bll => bll.OrderItemService.FirstOrDefaultAsync(orderItemId, It.IsAny<Guid?>(), It.IsAny<bool>()))
            .ReturnsAsync(orderItemDto);

        // Act
        var result = await controller.GetOrderItem(orderItemId);

        // Assert
        result.Result.Should()
            .BeOfType<OkObjectResult>()
            .Which.Value.Should()
            .BeAssignableTo<OrderItem>()
            .Which.Id.Should()
            .Be(orderItemId);
    }

    [Fact]
    public async Task PostOrderItem_CreatesNewOrderItem_Successfully()
    {
        // Arrange
        var mockBll = new Mock<IAppBll>();
        var mapper = Helper.GetMapper();
        var controller = new OrderItemsController(mockBll.Object, mapper);

        var orderItemRequest = new OrderItemRequest
        {
            Quantity = 5,
            ProductId = Guid.NewGuid(),
            PriceId = Guid.NewGuid(),
            OrderId = Guid.NewGuid()
        };

        mockBll
            .Setup(bll => bll.OrderItemService.Add(It.IsAny<App.BLL.DTO.OrderItem>()))
            .Returns((App.BLL.DTO.OrderItem orderItem) => Helper.AddId(orderItem));

        // Act
        var result = await controller.PostOrderItem(orderItemRequest);

        // Assert
        result.Result.Should()
            .BeOfType<CreatedAtActionResult>()
            .Which.Value.Should()
            .BeAssignableTo<OrderItem>()
            .Which.Quantity.Should()
            .Be(orderItemRequest.Quantity);
    }

    [Fact]
    public async Task PutOrderItem_UpdatesExistingOrderItem_Successfully()
    {
        // Arrange
        var mockBll = new Mock<IAppBll>();
        var mapper = Helper.GetMapper();
        var controller = new OrderItemsController(mockBll.Object, mapper);
        var existingOrderItem = Helper.GetEntity<App.BLL.DTO.OrderItem>();
        var updatedOrderItemRequest = new OrderItemRequest
        {
            Id = existingOrderItem.Id,
            Quantity = 10,
            ProductId = existingOrderItem.ProductId,
            PriceId = existingOrderItem.PriceId,
            OrderId = existingOrderItem.OrderId
        };
        mockBll.Setup(bll => bll.OrderItemService.FirstOrDefaultAsync(existingOrderItem.Id, It.IsAny<Guid?>(), It.IsAny<bool>()))
            .ReturnsAsync(existingOrderItem);

        // Act
        var result = await controller.PutOrderItem(existingOrderItem.Id, updatedOrderItemRequest);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        mockBll.Verify(bll => bll.OrderItemService.Update(It.IsAny<App.BLL.DTO.OrderItem>()), Times.Once);
        mockBll.Verify(bll => bll.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteOrderItem_RemovesExistingOrderItem_Successfully()
    {
        // Arrange
        var mockBll = new Mock<IAppBll>();
        var mapper = Helper.GetMapper();
        var orderItemId = Guid.NewGuid();
        var orderItemToRemove = Helper.GetEntity<App.BLL.DTO.OrderItem>(orderItemId);
        mockBll.Setup(bll => bll.OrderItemService.RemoveAsync(orderItemId, It.IsAny<Guid?>())).ReturnsAsync(orderItemToRemove);
        var controller = new OrderItemsController(mockBll.Object, mapper);

        // Act
        var result = await controller.DeleteOrderItem(orderItemId);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().BeOfType<OrderItem>()
            .Which.Id.Should().Be(orderItemId);
    }
}