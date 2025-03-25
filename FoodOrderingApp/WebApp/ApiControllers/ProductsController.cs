using System.Net;
using App.Contracts.BLL;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Public.DTO.Mappers;
using Public.DTO.v1;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// Products Api Controller
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly ProductMapper _mapper;
        private readonly ProductRequestMapper _mapperRequest;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="mapper"></param>
        public ProductsController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new ProductMapper(mapper);
            _mapperRequest = new ProductRequestMapper(mapper);
        }

        // GET: api/Products
        /// <summary>
        /// Get a list of all the Products
        /// </summary>
        /// <returns>List of Products</returns>
        [HttpGet]
        [ProducesResponseType<IEnumerable<Product>>((int) HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var product = (await _bll.ProductService.GetAllAsync()).Select(p => _mapper.Map(p));
            return Ok(product);
        }

        // GET: api/Products/5
        /// <summary>
        /// Get a single Product
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns>Requested Product</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType<Product>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
            var product = await _bll.ProductService.FirstOrDefaultAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(product));
        }

        // PUT: api/Products/5
        /// <summary>
        /// Update the Product
        /// </summary>
        /// <param name="id">Product id</param>
        /// <param name="product">New data to insert</param>
        /// <returns></returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:guid}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> PutProduct(Guid id, ProductRequest product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            var data = await _bll.ProductService.FirstOrDefaultAsync(product.Id.Value);

            if (data == null)
            {
                return BadRequest();
            }

            data.Name = product.Name;
            data.Size = product.Size;
            data.Description = product.Description;
            data.ImageUrl = product.ImageUrl;

            _bll.ProductService.Update(data);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.ProductService.ExistsAsync(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // POST: api/Products
        /// <summary>
        /// Create a new Product
        /// </summary>
        /// <param name="product">New Product</param>
        /// <returns>Created Product</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType<Product>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Product>> PostProduct(ProductRequest product)
        {
            var mapped = _mapperRequest.Map(product);
            if (mapped == null)
            {
                return BadRequest();
            }

            var created = _bll.ProductService.Add(mapped);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = created.Id }, created);
        }

        // DELETE: api/Products/5
        /// <summary>
        /// Delete the Product
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns>Deleted Product</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult<Product>> DeleteProduct(Guid id)
        {
            var product = await _bll.ProductService.RemoveAsync(id);
            if (product == null) return NotFound();

            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}