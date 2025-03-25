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
    /// Prices Api Controller
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class PricesController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly PriceMapper _mapper;
        private readonly PriceRequestMapper _mapperRequest;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="mapper"></param>
        public PricesController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new PriceMapper(mapper);
            _mapperRequest = new PriceRequestMapper(mapper);
        }

        // GET: api/Prices
        /// <summary>
        /// Get a list of all the Prices
        /// </summary>
        /// <returns>List of Prices</returns>
        [HttpGet]
        [ProducesResponseType<IEnumerable<Price>>((int) HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Price>>> GetPrices()
        {
            var price = (await _bll.PriceService.GetAllAsync()).Select(p => _mapper.Map(p));

            return Ok(price);
        }

        // GET: api/Prices/5
        /// <summary>
        /// Get a single Price
        /// </summary>
        /// <param name="id">Price id</param>
        /// <returns>Requested Price</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType<Price>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult<Price>> GetPrice(Guid id)
        {
            var price = await _bll.PriceService.FirstOrDefaultAsync(id);
            if (price == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(price));
        }

        // PUT: api/Prices/5
        /// <summary>
        /// Update the Price
        /// </summary>
        /// <param name="id">Price id</param>
        /// <param name="price">New data to insert</param>
        /// <returns></returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:guid}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult<Price>> PutPrice(Guid id, PriceRequest price)
        {
            if (id != price.Id)
            {
                return BadRequest();
            }

            var data = await _bll.PriceService.FirstOrDefaultAsync(price.Id.Value);
            if (data == null)
            {
                return BadRequest();
            }

            data.Value = price.Value;
            _bll.PriceService.Update(data);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.PriceService.ExistsAsync(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // POST: api/Prices
        /// <summary>
        /// Create a new Price
        /// </summary>
        /// <param name="price">New Price</param>
        /// <returns>Created Price</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType<Price>((int) HttpStatusCode.OK)]
        public async Task<ActionResult<Price>> PostPrice(PriceRequest price)
        {
            var created = _bll.PriceService.Add(_mapperRequest.Map(price)!);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetPrice", new { id = created.Id }, created);
        }

        // DELETE: api/Prices/5
        /// <summary>
        /// Delete the Price
        /// </summary>
        /// <param name="id">Price id</param>
        /// <returns>Deleted Price</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult<Price>> DeletePrice(Guid id)
        {
            var price = await _bll.PriceService.RemoveAsync(id);
            if (price == null) return NotFound();

            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
