using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EccommerceV3.Model.EF;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Razor.Language.Extensions;

namespace EccommerceV3.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ecommerceDBContext _context = new ecommerceDBContext();

        //public OrdersController(ecommerceDBContext context)
        //{
        //    _context = context;
        //}


        // GET: Orders
        public async Task<IActionResult> Index()
        {
            // matches customer id to aspnetusers id
            var customer = from c in _context.Customers select c;
            customer = customer.Where(s => s.LoginId.Contains(this.User.FindFirstValue(ClaimTypes.NameIdentifier)));
            var cID = customer.FirstOrDefault();
            
            // matches customer id to same one in orders table
            var rawData2 = (from s in _context.Orders select s).ToList();
            var orders = from s in rawData2 select s;
            orders = orders.Where(s => s.CustomerId == cID.CustomerId);
            var lastord = orders.LastOrDefault();

            if (lastord != null) {
                //int orderid = lastord.OrderId;
                lastord.OrderTotal = 0;

                foreach (var item in _context.OrdersDetails.Where(s => s.OrderId == lastord.OrderId)) {
                    lastord.OrderTotal += item.Subtotal;
                }
            }

            var ecommerceDBContext = _context.Orders.Where(s => s.CustomerId == cID.CustomerId).Include(o => o.Customer);

            if (ModelState.IsValid) {
                try {
                    if (lastord != null) {
                        _context.Update(lastord);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException) {
                        throw;
                }
                //return RedirectToAction(nameof(Create));
            }
            if (lastord != null) {
                ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", lastord.CustomerId);
            }
            return View(await ecommerceDBContext.ToListAsync());
        }

        // GET: Orders/Details/
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,OrderNo,OrderDate,OrderTotal,CustomerId")] Order order)
        {
            if (ModelState.IsValid)
            {
                // matches customer id to aspnetusers id
                var customer = from c in _context.Customers select c;
                customer = customer.Where(s => s.LoginId.Contains(User.FindFirstValue(ClaimTypes.NameIdentifier)));
                var cID = customer.FirstOrDefault();

                // sets default values
                order.CustomerId = cID.CustomerId;
                order.OrderDate = DateTime.Now;
                order.OrderNo = 0;
                order.OrderTotal = 0;

                //var rawData2 = (from s in _context.OrdersDetails select s).ToList();
                //var oDetails = from s in rawData2 select s;
                //oDetails = oDetails.Where(s => s.OrderId.Equals(order.OrderId));
                //decimal? total = 0;
                //foreach (var item in oDetails) {
                //    total += item.Subtotal;
                //}
                //order.OrderTotal = total;

                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", order.CustomerId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,OrderNo,OrderDate,OrderTotal,CustomerId")] Order order)
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'ecommerceDBContext.Orders'  is null.");
            }
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
          return (_context.Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
