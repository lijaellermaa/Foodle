#pragma warning disable 1591
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Helpers.Base;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        private readonly IAppBll _bll;

        public OrdersController(IAppBll bll)
        {
            _bll = bll;
        }

        // GET: Admin/Orders
        public async Task<IActionResult> Index()
        {
            var res = await _bll.OrderService.GetAllAsync();
            return View(res);
        }

        // GET: Admin/Orders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _bll.OrderService.FirstOrDefaultAsync((Guid)id);
            
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Admin/Orders/Create
        public async Task<IActionResult> Create()
        {
            ViewData["RestaurantId"] = new SelectList(await _bll.RestaurantService.GetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: Admin/Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Status,PaymentMethod,DeliveryType,DeliverTo,RestaurantId,AppUserId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] App.BLL.DTO.Order order)
        {
            var userId = User.GetUserId();

            if (userId == null)
            {
                return Unauthorized();
            }

            order.AppUserId = userId.Value;
            
            if (ModelState.IsValid)
            {
                // order.Id = Guid.NewGuid();
                _bll.OrderService.Add(order);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["RestaurantId"] = new SelectList(await _bll.RestaurantService.GetAllAsync(), "Id", "Name", order.RestaurantId);
            return View(order);
        }

        // GET: Admin/Orders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _bll.OrderService.FirstOrDefaultAsync((Guid)id);
            
            if (order == null)
            {
                return NotFound();
            }
            
            ViewData["RestaurantId"] = new SelectList(await _bll.RestaurantService.GetAllAsync(), "Id", "Name", order.RestaurantId);
            return View(order);
        }

        // POST: Admin/Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Status,PaymentMethod,DeliveryType,DeliverTo,RestaurantId,AppUserId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] App.BLL.DTO.Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userId = User.GetUserId();

                    if (userId == null)
                    {
                        return Unauthorized();
                    }

                    order.AppUserId = userId.Value;
                    _bll.OrderService.Update(order);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await OrderExists(order.Id))
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
            
            ViewData["RestaurantId"] = new SelectList(await _bll.RestaurantService.GetAllAsync(), "Id", "Name", order.RestaurantId);
            return View(order);
        }

        // GET: Admin/Orders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _bll.OrderService.FirstOrDefaultAsync((Guid)id);
            
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Admin/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var orderExists = await _bll.OrderService.ExistsAsync(id);
            
            if (orderExists)
            {
                await _bll.OrderService.RemoveAsync(id);
                await _bll.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> OrderExists(Guid id)
        {
            return await _bll.OrderService.ExistsAsync(id);
        }
    }
}
