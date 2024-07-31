using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using TonsNeutros.Admin.Context;
using TonsNeutros.Admin.Models;

namespace TonsNeutros.Admin.Controllers;

[Authorize(Roles = "Admin")]
public class AdminProductsController : Controller
{
    private readonly AppDbContext _context;

    public AdminProductsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: AdminProducts
    //public async Task<IActionResult> Index()
    //{
    //    var appDbContext = _context.Products.Include(p => p.Category);
    //    return View(await appDbContext.ToListAsync());
    //}

    public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "ProductName")
    {
        var result = _context.Products.Include(l => l.Category).AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter))
        {
            result = result.Where(p => p.ProductName.Contains(filter));
        }

        var model = await PagingList.CreateAsync(result, 10, pageindex, sort, "ProductName");
        model.RouteValue = new RouteValueDictionary { { "filter", filter } };

        return View(model);
    }

    // GET: AdminProducts/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(m => m.ProductId == id);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    // GET: AdminProducts/Create
    public IActionResult Create()
    {
        ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryDescription");
        return View();
    }

    // POST: AdminProducts/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ProductId,ProductName,ShortDescription,Description,Price,ImageURL,ImageThumbnailURL,Stock,IsFeatured,StockInHand,CategoryId")] Product product)
    {
        if (ModelState.IsValid)
        {
            product.CreatedAt = DateTime.Now;
            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryDescription", product.CategoryId);
        return View(product);
    }

    // GET: AdminProducts/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryDescription", product.CategoryId);
        return View(product);
    }

    // POST: AdminProducts/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,ShortDescription,Description,Price,ImageURL,ImageThumbnailURL,Stock,IsFeatured,StockInHand,CreatedAt,CategoryId")] Product product)
    {
        if (id != product.ProductId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.ProductId))
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
        ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryDescription", product.CategoryId);
        return View(product);
    }

    // GET: AdminProducts/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(m => m.ProductId == id);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    // POST: AdminProducts/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ProductExists(int id)
    {
        return _context.Products.Any(e => e.ProductId == id);
    }
}
