#pragma warning disable 1591
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RestaurantsController : Controller
    {
        private readonly IAppBll _bll;

        public RestaurantsController(IAppBll bll)
        {
            _bll = bll;
        }

        // GET: Admin/Restaurants
        public async Task<IActionResult> Index()
        {
            var res = await _bll.RestaurantService.GetAllAsync();
            return View(res);
        }

        // GET: Admin/Restaurants/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _bll.RestaurantService.FirstOrDefaultAsync((Guid)id);
            
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // GET: Admin/Restaurants/Create
        public async Task<IActionResult> Create()
        {
            ViewData["LocationId"] = new SelectList(await _bll.LocationService.GetAllAsync(), "Id", "Address");
            return View();
        }

        // POST: Admin/Restaurants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,PhoneNumber,OpenTime,CloseTime,LocationId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] App.BLL.DTO.Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                // restaurant.Id = Guid.NewGuid();
                _bll.RestaurantService.Add(restaurant);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new SelectList(await _bll.LocationService.GetAllAsync(), "Id", "Address", restaurant.LocationId);
            return View(restaurant);
        }

        // GET: Admin/Restaurants/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _bll.RestaurantService.FirstOrDefaultAsync((Guid)id);
            
            if (restaurant == null)
            {
                return NotFound();
            }
            ViewData["LocationId"] = new SelectList(await _bll.LocationService.GetAllAsync(), "Id", "Address", restaurant.LocationId);
            return View(restaurant);
        }

        // POST: Admin/Restaurants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,PhoneNumber,OpenTime,CloseTime,LocationId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] App.BLL.DTO.Restaurant restaurant)
        {
            if (id != restaurant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.RestaurantService.Update(restaurant);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await RestaurantExists(restaurant.Id))
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
            ViewData["LocationId"] = new SelectList(await _bll.LocationService.GetAllAsync(), "Id", "Address", restaurant.LocationId);
            return View(restaurant);
        }

        // GET: Admin/Restaurants/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _bll.RestaurantService.FirstOrDefaultAsync((Guid)id);
            
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // POST: Admin/Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var restaurant = await _bll.RestaurantService.ExistsAsync(id);
            
            if (restaurant)
            {
                await _bll.RestaurantService.RemoveAsync(id);
                await _bll.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> RestaurantExists(Guid id)
        {
            return await _bll.RestaurantService.ExistsAsync(id);
        }
    }
}
