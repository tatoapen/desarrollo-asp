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
    public class recetasController : Controller
    {
        private readonly FaDBContext _context;

        public recetasController(FaDBContext context)
        {
            _context = context;
        }

        // GET: recetas
        public async Task<IActionResult> Index()
        {
            return View(await _context.receta.ToListAsync());
        }

        // GET: recetas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receta = await _context.receta
                .FirstOrDefaultAsync(m => m.id == id);
            if (receta == null)
            {
                return NotFound();
            }

            return View(receta);
        }

        // GET: recetas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: recetas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,cantidad,observaciones,medicina")] receta receta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(receta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(receta);
        }

        // GET: recetas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receta = await _context.receta.FindAsync(id);
            if (receta == null)
            {
                return NotFound();
            }
            return View(receta);
        }

        // POST: recetas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,cantidad,observaciones,medicina")] receta receta)
        {
            if (id != receta.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!recetaExists(receta.id))
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
            return View(receta);
        }

        // GET: recetas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receta = await _context.receta
                .FirstOrDefaultAsync(m => m.id == id);
            if (receta == null)
            {
                return NotFound();
            }

            return View(receta);
        }

        // POST: recetas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var receta = await _context.receta.FindAsync(id);
            _context.receta.Remove(receta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool recetaExists(int id)
        {
            return _context.receta.Any(e => e.id == id);
        }
    }
}
