#pragma warning disable 1591
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class LocationsController : Controller
    {
        private readonly IAppBll _bll;

        public LocationsController(IAppBll bll)
        {
            _bll = bll;
        }

        // GET: Admin/Locations
        public async Task<IActionResult> Index()
        {
            var res = await _bll.LocationService.GetAllAsync();
            return View(res);
        }

        // GET: Admin/Locations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _bll.LocationService.FirstOrDefaultAsync((Guid)id);
            
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // GET: Admin/Locations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Locations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Area,Town,Address,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] App.BLL.DTO.Location location)
        {
            if (ModelState.IsValid)
            {
                // location.Id = Guid.NewGuid();
                _bll.LocationService.Add(location);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(location);
        }

        // GET: Admin/Locations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _bll.LocationService.FirstOrDefaultAsync((Guid)id);
            
            if (location == null)
            {
                return NotFound();
            }
            return View(location);
        }

        // POST: Admin/Locations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Area,Town,Address,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] App.BLL.DTO.Location location)
        {
            if (id != location.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.LocationService.Update(location);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await LocationExists(location.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(location);
        }

        // GET: Admin/Locations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _bll.LocationService.FirstOrDefaultAsync((Guid)id);
            
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // POST: Admin/Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var location = await _bll.LocationService.ExistsAsync(id);
            
            if (location)
            {
                await _bll.LocationService.RemoveAsync(id);
                await _bll.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> LocationExists(Guid id)
        {
            return await _bll.LocationService.ExistsAsync(id);
        }
    }
}
