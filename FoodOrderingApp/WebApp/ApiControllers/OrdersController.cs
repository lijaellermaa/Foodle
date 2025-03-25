using System.Net;
using App.Contracts.BLL;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Helpers.Base;
using Helpers.Base.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Public.DTO.Mappers;
using Public.DTO.v1;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// Orders Api Controller
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class OrdersController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly OrderMapper _mapper;
        private readonly OrderRequestMapper _mapperRequest;
        private readonly OrderItemRequestMapper _mapperOrderItemRequest;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="mapper"></param>
        public OrdersController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new OrderMapper(mapper);
            _mapperRequest = new OrderRequestMapper(mapper);
            _mapperOrderItemRequest = new OrderItemRequestMapper(mapper);
        }

        // GET: api/Orders
        /// <summary>
        /// Get a list of all the Orders
        /// </summary>
        /// <returns>List of Orders</returns>
        [HttpGet]
        [ProducesResponseType<IEnumerable<Order>>((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var userId = User.GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var order = (await _bll.OrderService.GetAllAsync(userId.Value))
                .Select(_mapper.Map);

            return Ok(order);
        }

        // GET: api/Orders/5
        /// <summary>
        /// Get a single Order
        /// </summary>
        /// <param name="id">Order id</param>
        /// <returns>Requested Order</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType<Order>((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Order>> GetOrder(Guid id)
        {
            var userId = User.GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var order = await _bll.OrderService.FirstOrDefaultAsync(id, userId.Value);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(order));
        }

        // PUT: api/Orders/5
        /// <summary>
        /// Update the Order
        /// </summary>
        /// <param name="id">Order id</param>
        /// <param name="order">New data to insert</param>
        /// <returns></returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Order>> PutOrder(Guid id, OrderRequest order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            var userId = User.GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            if (!await _bll.OrderService.IsOwnedByUserAsync(order.Id.Value, userId.Value))
            {
                return BadRequest("No hacking (bad user id)!");
            }

            order.AppUserId = userId.Value;
            _bll.OrderService.Update(_mapperRequest.Map(order)!);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Orders
        /// <summary>
        /// Create a new Order
        /// </summary>
        /// <param name="order">New Order</param>
        /// <returns>Created Order</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType<Order>((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<ActionResult<Order>> PostOrder(OrderWithItemsRequest orderWithItems)
        {
            var userId = User.GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var orderRequest = new OrderRequest
            {
                AppUserId = userId.Value,
                DeliveryType = orderWithItems.DeliveryType,
                PaymentMethod = orderWithItems.PaymentMethod,
                Status = orderWithItems.Status,
                DeliverTo = orderWithItems.DeliverTo,
                RestaurantId = orderWithItems.RestaurantId,
            };

            var orderResponse = _bll.OrderService.Add(_mapperRequest.Map(orderRequest)!);
            var orderItemsRequest = orderWithItems.OrderItems
                .Select(x => new OrderItemRequest
                {
                    OrderId = orderResponse.Id,
                    ProductId = x.ProductId,
                    PriceId = x.PriceId,
                    Quantity = x.Quantity
                });
            foreach (var orderItem in orderItemsRequest)
            {
                _bll.OrderItemService.Add(_mapperOrderItemRequest.Map(orderItem)!);
            }

            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = orderResponse.Id }, _mapper.Map(orderResponse));
        }

        // DELETE: api/Orders/5
        /// <summary>
        /// Delete the Order with all its OrderItems
        /// </summary>
        /// <param name="id">Order id</param>
        /// <returns>Deleted Order</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Order>> DeleteOrder(Guid id)
        {
            var exists = await _bll.OrderService.ExistsAsync(id);
            if (!exists)
            {
                return NotFound("Entity with such Id doesn't exist");
            }

            var userId = User.GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            await _bll.OrderItemService.RemoveByOrderIdAsync(id);
            var order = await _bll.OrderService.RemoveAsync(id, userId.Value);
            if (order == null) return NotFound();

            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Get a list of all the Orders by AppUserId and OrderStatus
        /// </summary>
        /// <param name="userId">AppUserId</param>
        /// <param name="status">OrderStatus</param>
        /// <returns>List of Orders</returns>
        [HttpGet("byUserIdAndStatus")]
        [ProducesResponseType<IEnumerable<Order>>((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByAppUserIdAndOrderStatus(Guid userId,
            OrderStatus status)
        {
            var order = await _bll.OrderService.FindByUserAndStatusAsync(userId, status);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(order));
        }

        /// <summary>
        /// Pay an Order
        /// </summary>
        /// <param name="id">Order id</param>
        /// <returns></returns>
        [HttpPost("{id:guid}/Pay")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PayOrder(Guid id)
        {
            var userId = User.GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var paid = await _bll.OrderService.PayOrderAsync(id, userId.Value);
            if (!paid)
            {
                return BadRequest("Order can't be paid");
            }

            return Ok();
        }
    }
}