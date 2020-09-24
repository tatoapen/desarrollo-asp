using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FarmaciaDB.Data;
using FarmaciaDB.Models;

namespace FarmaciaDB.Controllers
{
    public class MedicinasController : Controller
    {
        private readonly FaDBContext _context;

        public MedicinasController(FaDBContext context)
        {
            _context = context;
        }

        // GET: Medicinas
        public async Task<IActionResult> Index()
        {
            return View(await _context.medicina.ToListAsync());
        }

        // GET: Medicinas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicina = await _context.medicina
                .FirstOrDefaultAsync(m => m.id == id);
            if (medicina == null)
            {
                return NotFound();
            }

            return View(medicina);
        }

        // GET: Medicinas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medicinas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,medicina,precio")] Medicina medicinas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicinas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medicinas);
        }

        // GET: Medicinas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicina = await _context.medicina.FindAsync(id);
            if (medicina == null)
            {
                return NotFound();
            }
            return View(medicina);
        }

        // POST: Medicinas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,medicina,precio")] Medicina medicinas)
        {
            if (id != medicinas.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicinas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicinaExists(medicinas.id))
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
            return View(medicinas);
        }

        // GET: Medicinas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicina = await _context.medicina
                .FirstOrDefaultAsync(m => m.id == id);
            if (medicina == null)
            {
                return NotFound();
            }

            return View(medicina);
        }

        // POST: Medicinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicina = await _context.medicina.FindAsync(id);
            _context.medicina.Remove(medicina);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicinaExists(int id)
        {
            return _context.medicina.Any(e => e.id == id);
        }
    }
}
