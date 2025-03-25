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
    public class ProductsController : Controller
    {
        private readonly IAppBll _bll;

        public ProductsController(IAppBll bll)
        {
            _bll = bll;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            var res = await _bll.ProductService.GetAllAsync();
            return View(res);
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _bll.ProductService.FirstOrDefaultAsync((Guid)id);
            
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create
        public async Task<IActionResult> Create()
        {
            ViewData["LatestPriceId"] = new SelectList(await _bll.PriceService.GetAllAsync(), "Id", "Value");
            ViewData["ProductTypeId"] = new SelectList(await _bll.ProductTypeService.GetAllAsync(), "Id", "Name");
            ViewData["RestaurantId"] = new SelectList(await _bll.RestaurantService.GetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Size,Description,ImageUrl,ProductTypeId,LatestPriceId,RestaurantId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] App.BLL.DTO.Product product)
        {
            if (ModelState.IsValid)
            {
                // product.Id = Guid.NewGuid();
                _bll.ProductService.Add(product);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductTypeId"] = new SelectList(await _bll.ProductTypeService.GetAllAsync(), "Id", "Name", product.ProductTypeId);
            ViewData["RestaurantId"] = new SelectList(await _bll.RestaurantService.GetAllAsync(), "Id", "Name", product.RestaurantId);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _bll.ProductService.FirstOrDefaultAsync((Guid)id);
            
            if (product == null)
            {
                return NotFound();
            }
            ViewData["ProductTypeId"] = new SelectList(await _bll.ProductTypeService.GetAllAsync(), "Id", "Name", product.ProductTypeId);
            ViewData["RestaurantId"] = new SelectList(await _bll.RestaurantService.GetAllAsync(), "Id", "Name", product.RestaurantId);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Size,Description,ImageUrl,ProductTypeId,LatestPriceId,RestaurantId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] App.BLL.DTO.Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.ProductService.Update(product);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ProductExists(product.Id))
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
            ViewData["ProductTypeId"] = new SelectList(await _bll.ProductTypeService.GetAllAsync(), "Id", "Name", product.ProductTypeId);
            ViewData["RestaurantId"] = new SelectList(await _bll.RestaurantService.GetAllAsync(), "Id", "Name", product.RestaurantId);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _bll.ProductService.FirstOrDefaultAsync((Guid)id);
            
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = await _bll.ProductService.ExistsAsync(id);
            
            if (product)
            {
                await _bll.ProductService.RemoveAsync(id);
                await _bll.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProductExists(Guid id)
        {
            return await _bll.ProductService.ExistsAsync(id);
        }
    }
}
