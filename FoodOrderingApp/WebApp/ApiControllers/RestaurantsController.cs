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
    /// Restaurants Api Controller
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class RestaurantsController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly RestaurantMapper _mapper;
        private readonly RestaurantRequestMapper _mapperRequest;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="mapper"></param>
        public RestaurantsController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new RestaurantMapper(mapper);
            _mapperRequest = new RestaurantRequestMapper(mapper);
        }

        // GET: api/Restaurants
        /// <summary>
        /// Get a list of all the Restaurants
        /// </summary>
        /// <returns>List of Restaurants</returns>
        [HttpGet]
        [ProducesResponseType<IEnumerable<Restaurant>>((int) HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurants()
        {
            var restaurant = (await _bll.RestaurantService.GetAllAsync()).Select(restaurant1 => _mapper.Map(restaurant1));
            return Ok(restaurant);
        }

        // GET: api/Restaurants/5
        /// <summary>
        /// Get a single Restaurant
        /// </summary>
        /// <param name="id">Restaurant id</param>
        /// <returns>Requested Restaurant</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType<Restaurant>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult<Restaurant>> GetRestaurant(Guid id)
        {
            var restaurant = await _bll.RestaurantService.FirstOrDefaultAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(restaurant));
        }

        // PUT: api/Restaurants/5
        /// <summary>
        /// Update a Restaurant
        /// </summary>
        /// <param name="id">Restaurant id</param>
        /// <param name="restaurant">New data to insert</param>
        /// <returns></returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:guid}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> PutRestaurant(Guid id, RestaurantRequest restaurant)
        {
            if (id != restaurant.Id)
            {
                return BadRequest();
            }

            var data = await _bll.RestaurantService.FirstOrDefaultAsync(restaurant.Id.Value);
            if (data == null)
            {
                return BadRequest();
            }

            data.Name = restaurant.Name;
            data.PhoneNumber = restaurant.PhoneNumber;
            data.OpenTime = restaurant.OpenTime;
            data.CloseTime = restaurant.CloseTime;

            _bll.RestaurantService.Update(data);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.RestaurantService.ExistsAsync(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // POST: api/Restaurants
        /// <summary>
        /// Create a new Restaurant
        /// </summary>
        /// <param name="restaurant">New Restaurant</param>
        /// <returns>Created Restaurant</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType<Restaurant>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Restaurant>> PostRestaurant(RestaurantRequest restaurant)
        {
            var mapped = _mapperRequest.Map(restaurant);
            if (mapped == null)
            {
                return BadRequest();
            }

            var created = _bll.RestaurantService.Add(mapped);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetRestaurant", new { id = created.Id }, created);
        }

        // DELETE: api/Restaurants/5
        /// <summary>
        /// Delete a Restaurant
        /// </summary>
        /// <param name="id">Restaurant id</param>
        /// <returns>Deleted Restaurant</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult<Restaurant>> DeleteRestaurant(Guid id)
        {
            var restaurant = await _bll.RestaurantService.RemoveAsync(id);
            if (restaurant == null) return NotFound();

            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}