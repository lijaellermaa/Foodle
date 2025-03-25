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
    public class OrderItemsController : Controller
    {
        private readonly IAppBll _bll;

        public OrderItemsController(IAppBll bll)
        {
            _bll = bll;
        }

        // GET: Admin/OrderItems
        public async Task<IActionResult> Index()
        {
            var res = await _bll.OrderItemService.GetAllAsync();
            return View(res);
        }

        // GET: Admin/OrderItems/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItem = await _bll.OrderItemService.FirstOrDefaultAsync((Guid)id);
            
            if (orderItem == null)
            {
                return NotFound();
            }

            return View(orderItem);
        }

        // GET: Admin/OrderItems/Create
        public async Task<IActionResult> Create()
        {
            ViewData["OrderId"] = new SelectList(await _bll.OrderService.GetAllAsync(), "Id", "CreatedBy");
            ViewData["PriceId"] = new SelectList(await _bll.PriceService.GetAllAsync(), "Id", "CreatedBy");
            ViewData["ProductId"] = new SelectList(await _bll.ProductService.GetAllAsync(), "Id", "CreatedBy");
            return View();
        }

        // POST: Admin/OrderItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Quantity,ProductId,PriceId,OrderId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] App.BLL.DTO.OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                // orderItem.Id = Guid.NewGuid();
                _bll.OrderItemService.Add(orderItem);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(await _bll.OrderService.GetAllAsync(), "Id", "CreatedBy", orderItem.OrderId);
            ViewData["PriceId"] = new SelectList(await _bll.PriceService.GetAllAsync(), "Id", "CreatedBy", orderItem.PriceId);
            ViewData["ProductId"] = new SelectList(await _bll.ProductService.GetAllAsync(), "Id", "CreatedBy", orderItem.ProductId);
            return View(orderItem);
        }

        // GET: Admin/OrderItems/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItem = await _bll.OrderItemService.FirstOrDefaultAsync((Guid)id);
            
            if (orderItem == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(await _bll.OrderService.GetAllAsync(), "Id", "CreatedBy", orderItem.OrderId);
            ViewData["PriceId"] = new SelectList(await _bll.PriceService.GetAllAsync(), "Id", "CreatedBy", orderItem.PriceId);
            ViewData["ProductId"] = new SelectList(await _bll.ProductService.GetAllAsync(), "Id", "CreatedBy", orderItem.ProductId);
            return View(orderItem);
        }

        // POST: Admin/OrderItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Quantity,ProductId,PriceId,OrderId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] App.BLL.DTO.OrderItem orderItem)
        {
            if (id != orderItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.OrderItemService.Update(orderItem);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await OrderItemExists(orderItem.Id))
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
            ViewData["OrderId"] = new SelectList(await _bll.OrderService.GetAllAsync(), "Id", "CreatedBy", orderItem.OrderId);
            ViewData["PriceId"] = new SelectList(await _bll.PriceService.GetAllAsync(), "Id", "CreatedBy", orderItem.PriceId);
            ViewData["ProductId"] = new SelectList(await _bll.ProductService.GetAllAsync(), "Id", "CreatedBy", orderItem.ProductId);
            return View(orderItem);
        }

        // GET: Admin/OrderItems/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItem = await _bll.OrderItemService.FirstOrDefaultAsync((Guid)id);
            
            if (orderItem == null)
            {
                return NotFound();
            }

            return View(orderItem);
        }

        // POST: Admin/OrderItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var orderItem = await _bll.OrderItemService.ExistsAsync(id);
            
            if (orderItem)
            {
                await _bll.OrderItemService.RemoveAsync(id);
                await _bll.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> OrderItemExists(Guid id)
        {
            return await _bll.OrderItemService.ExistsAsync(id);
        }
    }
}
