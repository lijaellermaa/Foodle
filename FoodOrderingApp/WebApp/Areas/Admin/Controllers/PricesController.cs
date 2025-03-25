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
    public class PricesController : Controller
    {
        private readonly IAppBll _bll;

        public PricesController(IAppBll bll)
        {
            _bll = bll;
        }

        // GET: Admin/Prices
        public async Task<IActionResult> Index()
        {
            var res = await _bll.PriceService.GetAllAsync();
            return View(res);
        }

        // GET: Admin/Prices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _bll.PriceService.FirstOrDefaultAsync((Guid)id);
            
            if (price == null)
            {
                return NotFound();
            }

            return View(price);
        }

        // GET: Admin/Prices/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ProductId"] = new SelectList(await _bll.ProductService.GetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: Admin/Prices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Value,PreviousValue,ProductId,Comment,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] App.BLL.DTO.Price price)
        {
            if (ModelState.IsValid)
            {
                // price.Id = Guid.NewGuid();
                _bll.PriceService.Add(price);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(await _bll.ProductService.GetAllAsync(), "Id", "Name", price.ProductId);
            return View(price);
        }

        // GET: Admin/Prices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _bll.PriceService.FirstOrDefaultAsync((Guid)id);
            
            if (price == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(await _bll.ProductService.GetAllAsync(), "Id", "Name", price.ProductId);
            return View(price);
        }

        // POST: Admin/Prices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Value,PreviousValue,ProductId,Comment,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] App.BLL.DTO.Price price)
        {
            if (id != price.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.PriceService.Update(price);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await PriceExists(price.Id))
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
            ViewData["ProductId"] = new SelectList(await _bll.ProductService.GetAllAsync(), "Id", "Name", price.ProductId);
            return View(price);
        }

        // GET: Admin/Prices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _bll.PriceService.FirstOrDefaultAsync((Guid)id);
            
            if (price == null)
            {
                return NotFound();
            }

            return View(price);
        }

        // POST: Admin/Prices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var price = await _bll.PriceService.ExistsAsync(id);
            
            if (price)
            {
                await _bll.PriceService.RemoveAsync(id);
                await _bll.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> PriceExists(Guid id)
        {
            return await _bll.PriceService.ExistsAsync(id);
        }
    }
}
