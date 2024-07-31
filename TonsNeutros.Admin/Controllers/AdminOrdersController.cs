using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using TonsNeutros.Admin.Context;
using TonsNeutros.Admin.Models;

namespace TonsNeutros.Admin.Controllers;

[Authorize(Roles = "Admin")]
public class AdminOrdersController : Controller
{
    private readonly AppDbContext _context;

    public AdminOrdersController(AppDbContext context)
    {
        _context = context;
    }

    // GET: AdminOrders
    //public async Task<IActionResult> Index()
    //{
    //    var appDbContext = _context.Orders.Include(o => o.Address).Include(o => o.Buyer);
    //    return View(await appDbContext.ToListAsync());
    //}

    public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "OrderDate")
    {
        var result = _context.Orders.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter))
        {
            result = result.Where(o => o.Buyer.Name.Contains(filter));
        }

        var model = await PagingList.CreateAsync(result, 10, pageindex, sort, "OrderDate");
        model.RouteValue = new RouteValueDictionary { { "filter", filter } };

        return View(model);
    }

    // GET: AdminOrders/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var order = await _context.Orders
            .Include(o => o.Address)
            .Include(o => o.Buyer)
            .FirstOrDefaultAsync(m => m.OrderId == id);
        if (order == null)
        {
            return NotFound();
        }

        return View(order);
    }

    // GET: AdminOrders/Create
    public IActionResult Create()
    {
        ViewData["AddressId"] = new SelectList(_context.Addressess, "AddressId", "AddressId");
        ViewData["BuyerId"] = new SelectList(_context.Users, "Id", "Id");
        return View();
    }

    // POST: AdminOrders/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("OrderId,OrderDate,OrderSentDate,BuyerId,AddressId")] Order order)
    {
        if (ModelState.IsValid)
        {
            _context.Add(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["AddressId"] = new SelectList(_context.Addressess, "AddressId", "AddressId", order.AddressId);
        ViewData["BuyerId"] = new SelectList(_context.Users, "Id", "Id", order.BuyerId);
        return View(order);
    }

    // GET: AdminOrders/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var order = await _context.Orders.FindAsync(id);
        if (order == null)
        {
            return NotFound();
        }
        ViewData["AddressId"] = new SelectList(_context.Addressess, "AddressId", "AddressId", order.AddressId);
        ViewData["BuyerId"] = new SelectList(_context.Users, "Id", "Id", order.BuyerId);
        return View(order);
    }

    // POST: AdminOrders/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("OrderId,OrderDate,OrderSentDate,BuyerId,AddressId")] Order order)
    {
        if (id != order.OrderId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(order);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(order.OrderId))
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
        ViewData["AddressId"] = new SelectList(_context.Addressess, "AddressId", "AddressId", order.AddressId);
        ViewData["BuyerId"] = new SelectList(_context.Users, "Id", "Id", order.BuyerId);
        return View(order);
    }

    // GET: AdminOrders/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var order = await _context.Orders
            .Include(o => o.Address)
            .Include(o => o.Buyer)
            .FirstOrDefaultAsync(m => m.OrderId == id);
        if (order == null)
        {
            return NotFound();
        }

        return View(order);
    }

    // POST: AdminOrders/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order != null)
        {
            _context.Orders.Remove(order);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool OrderExists(int id)
    {
        return _context.Orders.Any(e => e.OrderId == id);
    }
}
