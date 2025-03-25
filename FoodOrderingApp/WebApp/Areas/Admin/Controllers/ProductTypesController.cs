#pragma warning disable 1591
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductTypesController : Controller
    {
        private readonly IAppBll _bll;

        public ProductTypesController(IAppBll bll)
        {
            _bll = bll;
        }

        // GET: Admin/ProductTypes
        public async Task<IActionResult> Index()
        {
            var res = await _bll.ProductTypeService.GetAllAsync();
            return View(res);
        }

        // GET: Admin/ProductTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _bll.ProductTypeService.FirstOrDefaultAsync((Guid)id);
            
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        // GET: Admin/ProductTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ProductTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductTypeName,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] App.BLL.DTO.ProductType productType)
        {
            if (ModelState.IsValid)
            {
                // productType.Id = Guid.NewGuid();
                _bll.ProductTypeService.Add(productType);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productType);
        }

        // GET: Admin/ProductTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _bll.ProductTypeService.FirstOrDefaultAsync((Guid)id);
            
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        // POST: Admin/ProductTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ProductTypeName,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] App.BLL.DTO.ProductType productType)
        {
            if (id != productType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.ProductTypeService.Update(productType);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ProductTypeExists(productType.Id))
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
            return View(productType);
        }

        // GET: Admin/ProductTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _bll.ProductTypeService.FirstOrDefaultAsync((Guid)id);
            
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        // POST: Admin/ProductTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var productType = await _bll.ProductTypeService.ExistsAsync(id);
            
            if (productType)
            {
                await _bll.ProductTypeService.RemoveAsync(id);
                await _bll.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProductTypeExists(Guid id)
        {
            return await _bll.ProductTypeService.ExistsAsync(id);
        }
    }
}
