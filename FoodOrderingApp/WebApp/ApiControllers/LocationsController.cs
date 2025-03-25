using System.Net;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Asp.Versioning;
using AutoMapper;
using Base.Contracts;
using Public.DTO.Mappers;
using Public.DTO.v1;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// Locations Api Controller
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class LocationsController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly LocationMapper _mapper;
        private readonly LocationRequestMapper _mapperRequest;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="mapper"></param>
        public LocationsController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new LocationMapper(mapper);
            _mapperRequest = new LocationRequestMapper(mapper);
        }

        // GET: api/Locations
        /// <summary>
        /// Get a list of all the Locations
        /// </summary>
        /// <returns>List of Locations</returns>
        [HttpGet]
        [ProducesResponseType<IEnumerable<Location>>((int) HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Location>>> GetLocations()
        {
            var location = (await _bll.LocationService.GetAllAsync()).Select(_mapper.Map);

            return Ok(location);
        }

        // GET: api/Locations/5
        /// <summary>
        /// Get a single Location
        /// </summary>
        /// <param name="id">Location id</param>
        /// <returns>Requested Location</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType<Location>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult<Location>> GetLocation(Guid id)
        {
            var location = await _bll.LocationService.FirstOrDefaultAsync(id);

            if (location == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(location)!);
        }

        // PUT: api/Locations/5
        /// <summary>
        /// Update the Location
        /// </summary>
        /// <param name="id">Location id</param>
        /// <param name="location">New data to insert</param>
        /// <returns></returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:guid}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult> PutLocation(Guid id, LocationRequest location)
        {
            if (id != location.Id)
            {
                return BadRequest();
            }

            var data = await _bll.LocationService.FirstOrDefaultAsync(location.Id.Value);

            if (data == null)
            {
                return BadRequest();
            }

            data.Area = location.Area;
            data.Town = location.Town;
            data.Address = location.Address;

            _bll.LocationService.Update(data);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.LocationService.ExistsAsync(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // POST: api/Locations
        /// <summary>
        /// Create a new Location
        /// </summary>
        /// <param name="location">New Location</param>
        /// <returns>Created Location</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType<Location>((int) HttpStatusCode.OK)]
        public async Task<ActionResult<Location>> PostLocation(LocationRequest location)
        {
            var created = _bll.LocationService.Add(_mapperRequest.Map(location)!);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetLocation", new { id = created.Id },_mapper.Map(created));
        }

        // DELETE: api/Locations/5
        /// <summary>
        /// Delete the Location
        /// </summary>
        /// <param name="id">Location id</param>
        /// <returns>Deleted Location</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Location>> DeleteLocation(Guid id)
        {
            // First, check if the location exists to avoid tracking issues
            var exists = await _bll.LocationService.ExistsAsync(id);
            if (!exists) 
            {
                return NotFound("Entity with such Id doesn't exist");
            }

            var result = await _bll.LocationService.RemoveAsync(id);
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