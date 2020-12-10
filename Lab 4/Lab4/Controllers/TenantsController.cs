using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab4.Models;

namespace Lab4.Controllers
{
    public class TenantsController : Controller
    {
        private readonly PublicUtilitiesContext _context;

        public TenantsController(PublicUtilitiesContext context)
        {
            _context = context;
        }

        // GET: Tenants
        public async Task<IActionResult> Index()
        {
            var publicUtilitiesContext = _context.Tenants.Include(t => t.Apartment);
            return View(await publicUtilitiesContext.ToListAsync());
        }

        // GET: Tenants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenants = await _context.Tenants
                .Include(t => t.Apartment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tenants == null)
            {
                return NotFound();
            }

            return View(tenants);
        }

        // GET: Tenants/Create
        public IActionResult Create()
        {
            ViewData["ApartmentId"] = new SelectList(_context.Apartment, "Id", "Id");
            return View();
        }

        // POST: Tenants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,FatherName,AccountNumber,ApartmentId")] Tenants tenants)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tenants);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApartmentId"] = new SelectList(_context.Apartment, "Id", "Id", tenants.ApartmentId);
            return View(tenants);
        }

        // GET: Tenants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenants = await _context.Tenants.FindAsync(id);
            if (tenants == null)
            {
                return NotFound();
            }
            ViewData["ApartmentId"] = new SelectList(_context.Apartment, "Id", "Id", tenants.ApartmentId);
            return View(tenants);
        }

        // POST: Tenants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,FatherName,AccountNumber,ApartmentId")] Tenants tenants)
        {
            if (id != tenants.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tenants);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TenantsExists(tenants.Id))
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
            ViewData["ApartmentId"] = new SelectList(_context.Apartment, "Id", "Id", tenants.ApartmentId);
            return View(tenants);
        }

        // GET: Tenants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenants = await _context.Tenants
                .Include(t => t.Apartment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tenants == null)
            {
                return NotFound();
            }

            return View(tenants);
        }

        // POST: Tenants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tenants = await _context.Tenants.FindAsync(id);
            _context.Tenants.Remove(tenants);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TenantsExists(int id)
        {
            return _context.Tenants.Any(e => e.Id == id);
        }
    }
}
