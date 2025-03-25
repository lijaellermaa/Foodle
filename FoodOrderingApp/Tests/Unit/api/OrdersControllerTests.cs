using System.Security.Claims;
using App.Contracts.BLL;
using AutoMapper;
using FluentAssertions;
using Helpers.Base.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Public.DTO;
using Public.DTO.v1;
using WebApp.ApiControllers;

namespace Tests.Unit.api;

using Helper=Helpers.ControllerUnitTestHelper<Public.DTO.AutoMapperConfig>;

public class OrdersControllerTests
{
    private readonly Mock<IAppBll> _mockBll;
    private readonly IMapper _mapper;
    private readonly OrdersController _controller;

    public OrdersControllerTests()
    {
        _mockBll = new Mock<IAppBll>();
        var config = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperConfig()));
        _mapper = config.CreateMapper();
        _controller = new OrdersController(_mockBll.Object, _mapper);
    }

    private void SetUser(Guid userId)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())
        };
        var identity = new ClaimsIdentity(claims, "TestAuthType");
        var principal = new ClaimsPrincipal(identity);
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = principal }
        };
    }
    
    [Fact]
    public async Task GetAll_ReturnsAllOrders_WhenOrdersExist()
    {
        // Arrange
        var userId = Guid.NewGuid();
        SetUser(userId);
        
        var orders = new List<App.BLL.DTO.Order>
        {
            new App.BLL.DTO.Order { Id = Guid.NewGuid(), AppUserId = userId },
            new App.BLL.DTO.Order { Id = Guid.NewGuid(), AppUserId = userId }
        };
        
        _mockBll.Setup(bll => bll.OrderService.GetAllAsync(userId, It.IsAny<bool>()))
            .ReturnsAsync(orders);

        // Act
        var result = await _controller.GetOrders();

        // Assert
        result.Result.Should()
            .BeOfType<OkObjectResult>()
            .Which.Value.Should()
            .BeAssignableTo<IEnumerable<Order>>()
            .Which.Should()
            .HaveCount(2);
    }

    [Fact]
    public async Task GetOrder_ReturnsSingleOrder_WhenOrderExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        SetUser(userId);
        var orderId = Guid.NewGuid();
        
        var order = new App.BLL.DTO.Order { Id = orderId, AppUserId = userId };
        
        _mockBll
            .Setup(bll => bll.OrderService.FirstOrDefaultAsync(orderId, userId, It.IsAny<bool>()))
            .ReturnsAsync(order);

        // Act
        var result = await _controller.GetOrder(orderId);

        // Assert
        result.Result.Should()
            .BeOfType<OkObjectResult>()
            .Which.Value.Should()
            .BeAssignableTo<Order>()
            .Which.Id.Should()
            .Be(orderId);
    }

    [Fact]
    public async Task PostOrder_CreatesNewOrder_Successfully()
    {
        // Arrange
        var userId = Guid.NewGuid();
        SetUser(userId);
        
        // var mapper = Helper.GetMapper();
        // var controller = new OrdersController(_mockBll.Object, mapper);

        var orderWithItemsRequest = new OrderWithItemsRequest
        {
            PaymentMethod = PaymentMethod.CreditCard,
            DeliveryType = DeliveryType.Delivery,
            DeliverTo = "Kalamaja 17, Tallinn",
            RestaurantId = Guid.NewGuid(),
            AppUserId = userId
        };

        _mockBll
            .Setup(bll => bll.OrderService.Add(It.IsAny<App.BLL.DTO.Order>()))
            .Returns((App.BLL.DTO.Order order) => Helper.AddId(order));

        // Act
        var result = await _controller.PostOrder(orderWithItemsRequest);

        // Assert
        result.Result.Should()
            .BeOfType<CreatedAtActionResult>()
            .Which.Value.Should()
            .BeAssignableTo<Order>()
            .Which.PaymentMethod.Should()
            .Be(orderWithItemsRequest.PaymentMethod);
    }

    [Fact]
    public async Task PutOrder_UpdatesExistingOrder_Successfully()
    {
        // Arrange
        var userId = Guid.NewGuid();
        SetUser(userId);
        
        var existingOrder = Helper.GetEntity<App.BLL.DTO.Order>();
        var updatedOrderRequest = new OrderRequest
        {
            Id = existingOrder.Id,
            PaymentMethod = PaymentMethod.Cash,
            DeliveryType = DeliveryType.PuckUp,
            DeliverTo = "Kalamaja 18, Tallinn",
            RestaurantId = Guid.NewGuid(),
            AppUserId = userId
        };
        
        _mockBll.Setup(bll => bll.OrderService.IsOwnedByUserAsync(updatedOrderRequest.Id.Value, userId))
            .ReturnsAsync(true);

        _mockBll.Setup(bll => bll.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _controller.PutOrder(existingOrder.Id, updatedOrderRequest);

        // Assert
        result.Result.Should().BeOfType<NoContentResult>();
        _mockBll.Verify(bll => bll.OrderService.Update(It.IsAny<App.BLL.DTO.Order>()), Times.Once);
        _mockBll.Verify(bll => bll.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteOrder_RemovesExistingOrder_Successfully()
    {
        // Arrange
        var userId = Guid.NewGuid();
        SetUser(userId);
        
        var orderId = Guid.NewGuid();
        var orderToRemove = Helper.GetEntity<App.BLL.DTO.Order>(orderId);
        _mockBll.Setup(bll => bll.OrderService.RemoveAsync(orderId, userId)).ReturnsAsync(orderToRemove);

        // Act
        var result = await _controller.DeleteOrder(orderId);

        // Assert
        result.Result.Should().BeOfType<NoContentResult>();

        _mockBll.Verify(bll => bll.OrderService.RemoveAsync(orderId, userId), Times.Once);
        _mockBll.Verify(bll => bll.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}