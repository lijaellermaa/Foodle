using System.Net;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Asp.Versioning;
using AutoMapper;
using Public.DTO.Mappers;
using Public.DTO.v1;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// OrderItems Api Controller
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class OrderItemsController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly OrderItemMapper _mapper;
        private readonly OrderItemRequestMapper _mapperRequest;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="automapper"></param>
        public OrderItemsController(IAppBll bll, IMapper automapper)
        {
            _bll = bll;
            _mapper = new OrderItemMapper(automapper);
            _mapperRequest = new OrderItemRequestMapper(automapper);
        }

        // GET: api/OrderItems
        /// <summary>
        /// Get a list of all the Order Items
        /// </summary>
        /// <returns>List of OrderItems</returns>
        [HttpGet]
        [ProducesResponseType<IEnumerable<OrderItem>>((int) HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetOrderItems()
        {
            var orderItem = (await _bll.OrderItemService.GetAllAsync()).Select(_mapper.Map);
            return Ok(orderItem);
        }

        // GET: api/OrderItems/5
        /// <summary>
        /// Get a single Order Item
        /// </summary>
        /// <param name="id">OrderItem id</param>
        /// <returns>Requested OrderItem</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType<OrderItem>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult<OrderItem>> GetOrderItem(Guid id)
        {
            var orderItem = _mapper.Map(await _bll.OrderItemService.FirstOrDefaultAsync(id));
            if (orderItem == null)
            {
                return NotFound();
            }

            return Ok(orderItem);
        }

        // PUT: api/OrderItems/5
        /// <summary>
        /// Update the Order Item
        /// </summary>
        /// <param name="id">OrderItem id</param>
        /// <param name="orderItem">New data to insert</param>
        /// <returns></returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:guid}")]
        [ProducesResponseType<OrderItem>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult<OrderItem>> PutOrderItem(Guid id, OrderItemRequest orderItem)
        {
            if (id != orderItem.Id)
            {
                return BadRequest();
            }

            var data = await _bll.OrderItemService.FirstOrDefaultAsync(orderItem.Id.Value);
            if (data == null)
            {
                return BadRequest();
            }

            data.Quantity = orderItem.Quantity;
            _bll.OrderItemService.Update(data);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.OrderItemService.ExistsAsync(id))
                {
                    return NotFound();
                }
                throw;
            }

            return Ok(orderItem);
        }

        // POST: api/OrderItems
        /// <summary>
        /// Create a new Order Item
        /// </summary>
        /// <param name="orderItem">New OrderItem</param>
        /// <returns>Created OrderItem</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType<OrderItem>((int) HttpStatusCode.OK)]
        public async Task<ActionResult<OrderItem>> PostOrderItem(OrderItemRequest orderItem)
        {
            var created = _bll.OrderItemService.Add(_mapperRequest.Map(orderItem)!);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetOrderItem", new { id = created.Id }, _mapper.Map(created));
        }

        // DELETE: api/OrderItems/5
        /// <summary>
        /// Delete the Order Item
        /// </summary>
        /// <param name="id">OrderItem id</param>
        /// <returns>Deleted OrderItem</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<OrderItem>> DeleteOrderItem(Guid id)
        {
            // First, check if the location exists to avoid tracking issues
            var exists = await _bll.OrderItemService.ExistsAsync(id);
            if (!exists) 
            {
                return NotFound("Entity with such Id doesn't exist");
            }
            
            var result = await _bll.OrderItemService.RemoveAsync(id);
            if (result == null) 
            {
                return BadRequest("Couldn't delete an entity");
            }

            // Save changes to commit the deletion
            await _bll.SaveChangesAsync();

            // Return the deleted location
            return NoContent();
        }
    }
}