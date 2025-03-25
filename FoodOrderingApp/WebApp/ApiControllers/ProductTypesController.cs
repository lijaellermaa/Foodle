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
    /// ProductTypes Api Controller
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class ProductTypesController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly ProductTypeMapper _mapper;
        private readonly ProductTypeRequestMapper _mapperRequest;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="mapper"></param>
        public ProductTypesController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new ProductTypeMapper(mapper);
            _mapperRequest = new ProductTypeRequestMapper(mapper);
        }

        // GET: api/ProductTypes
        /// <summary>
        /// Get a list of all the Product Types
        /// </summary>
        /// <returns>List of ProductTypes</returns>
        [HttpGet]
        [ProducesResponseType<IEnumerable<ProductType>>((int) HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetProductTypes()
        {
            var productType = (await _bll.ProductTypeService.GetAllAsync()).Select(p => _mapper.Map(p));
            return Ok(productType);
        }

        // GET: api/ProductTypes/5
        /// <summary>
        /// Get a single Product Type
        /// </summary>
        /// <param name="id">ProductType id</param>
        /// <returns>Requested ProductType</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType<ProductType>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProductType>> GetProductType(Guid id)
        {
            var productType = await _bll.ProductTypeService.FirstOrDefaultAsync(id);
            if (productType == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(productType));
        }

        // PUT: api/ProductTypes/5
        /// <summary>
        /// Update a Product Type
        /// </summary>
        /// <param name="id">ProductType id</param>
        /// <param name="productType">New data to insert</param>
        /// <returns></returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:guid}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> PutProductType(Guid id, ProductTypeRequest productType)
        {
            if (id != productType.Id)
            {
                return BadRequest();
            }

            var data = await _bll.ProductTypeService.FirstOrDefaultAsync(productType.Id.Value);

            if (data == null)
            {
                return BadRequest();
            }

            data.Name = productType.Name;
            _bll.ProductTypeService.Update(data);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.ProductTypeService.ExistsAsync(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // POST: api/ProductTypes
        /// <summary>
        /// Create a new Product Type
        /// </summary>
        /// <param name="productType">New ProductType</param>
        /// <returns>Created ProductType</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType<ProductType>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ProductType>> PostProductType(ProductTypeRequest productType)
        {
            var mapped = _mapperRequest.Map(productType);
            if (mapped == null)
            {
                return BadRequest();
            }

            var created = _bll.ProductTypeService.Add(mapped);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetProductType", new { id = created.Id }, created);
        }

        // DELETE: api/ProductTypes/5
        /// <summary>
        /// Delete a Product Type
        /// </summary>
        /// <param name="id">ProductType id</param>
        /// <returns>Deleted ProductType</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProductType>> DeleteProductType(Guid id)
        {
            var productType = await _bll.ProductTypeService.RemoveAsync(id);
            if (productType == null) return NotFound();

            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}