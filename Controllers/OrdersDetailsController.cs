using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EccommerceV3.Model.EF;
using Microsoft.AspNetCore.Authorization;

namespace EccommerceV3.Controllers
{
    [Authorize]
    public class OrdersDetailsController : Controller
    {
        private readonly ecommerceDBContext _context = new ecommerceDBContext();

        //public OrdersDetailsController(ecommerceDBContext context)
        //{
        //    _context = context;
        //}

        // GET: OrdersDetails
        public async Task<IActionResult> Index()
        {
            var ecommerceDBContext = _context.OrdersDetails.Include(o => o.Order).Include(o => o.Product);
            return View(await ecommerceDBContext.ToListAsync());
        }

        // GET: OrdersDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrdersDetails == null)
            {
                return NotFound();
            }

            var ordersDetail = await _context.OrdersDetails
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderDetailsId == id);
            if (ordersDetail == null)
            {
                return NotFound();
            }

            return View(ordersDetail);
        }

        // GET: OrdersDetails/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId");
            return View();
        }

        // POST: OrdersDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderDetailsId,ProductId,ProductQty,ProductPrice,OrderId")] OrdersDetail ordersDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ordersDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", ordersDetail.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", ordersDetail.ProductId);
            return View(ordersDetail);
        }

        // GET: OrdersDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrdersDetails == null)
            {
                return NotFound();
            }

            var ordersDetail = await _context.OrdersDetails.FindAsync(id);
            if (ordersDetail == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", ordersDetail.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", ordersDetail.ProductId);
            return View(ordersDetail);
        }

        // POST: OrdersDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderDetailsId,ProductId,ProductQty,ProductPrice,OrderId")] OrdersDetail ordersDetail)
        {
            if (id != ordersDetail.OrderDetailsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ordersDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersDetailExists(ordersDetail.OrderDetailsId))
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
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", ordersDetail.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", ordersDetail.ProductId);
            return View(ordersDetail);
        }

        // GET: OrdersDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrdersDetails == null)
            {
                return NotFound();
            }

            var ordersDetail = await _context.OrdersDetails
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderDetailsId == id);
            if (ordersDetail == null)
            {
                return NotFound();
            }

            return View(ordersDetail);
        }

        // POST: OrdersDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrdersDetails == null)
            {
                return Problem("Entity set 'ecommerceDBContext.OrdersDetails'  is null.");
            }
            var ordersDetail = await _context.OrdersDetails.FindAsync(id);
            if (ordersDetail != null)
            {
                _context.OrdersDetails.Remove(ordersDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdersDetailExists(int id)
        {
          return (_context.OrdersDetails?.Any(e => e.OrderDetailsId == id)).GetValueOrDefault();
        }
    }
}
