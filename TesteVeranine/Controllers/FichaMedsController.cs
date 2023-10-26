using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TesteVeranine.Models;

namespace TesteVeranine.Controllers
{
    public class FichaMedsController : Controller
    {
        private readonly HospitalContext _context;

        public FichaMedsController(HospitalContext context)
        {
            _context = context;
        }

        // GET: FichaMeds
        public async Task<IActionResult> Index()
        {
            var hospitalContext = _context.FichaMeds.Include(f => f.IdmedNavigation).Include(f => f.IdpacNavigation);
            return View(await hospitalContext.ToListAsync());
        }

        // GET: FichaMeds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FichaMeds == null)
            {
                return NotFound();
            }

            var fichaMed = await _context.FichaMeds
                .Include(f => f.IdmedNavigation)
                .Include(f => f.IdpacNavigation)
                .FirstOrDefaultAsync(m => m.Idfic == id);
            if (fichaMed == null)
            {
                return NotFound();
            }

            return View(fichaMed);
        }

        // GET: FichaMeds/Create
        public IActionResult Create()
        {
            ViewData["Idmed"] = new SelectList(_context.Medicos, "Idmed", "Idmed");
            ViewData["Idpac"] = new SelectList(_context.Pacientes, "Idpac", "Idpac");
            return View();
        }

        // POST: FichaMeds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idfic,Idmed,Idpac")] FichaMed fichaMed)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fichaMed);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idmed"] = new SelectList(_context.Medicos, "Idmed", "Idmed", fichaMed.Idmed);
            ViewData["Idpac"] = new SelectList(_context.Pacientes, "Idpac", "Idpac", fichaMed.Idpac);
            return View(fichaMed);
        }

        // GET: FichaMeds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FichaMeds == null)
            {
                return NotFound();
            }

            var fichaMed = await _context.FichaMeds.FindAsync(id);
            if (fichaMed == null)
            {
                return NotFound();
            }
            ViewData["Idmed"] = new SelectList(_context.Medicos, "Idmed", "Idmed", fichaMed.Idmed);
            ViewData["Idpac"] = new SelectList(_context.Pacientes, "Idpac", "Idpac", fichaMed.Idpac);
            return View(fichaMed);
        }

        // POST: FichaMeds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idfic,Idmed,Idpac")] FichaMed fichaMed)
        {
            if (id != fichaMed.Idfic)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fichaMed);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FichaMedExists(fichaMed.Idfic))
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
            ViewData["Idmed"] = new SelectList(_context.Medicos, "Idmed", "Idmed", fichaMed.Idmed);
            ViewData["Idpac"] = new SelectList(_context.Pacientes, "Idpac", "Idpac", fichaMed.Idpac);
            return View(fichaMed);
        }

        // GET: FichaMeds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FichaMeds == null)
            {
                return NotFound();
            }

            var fichaMed = await _context.FichaMeds
                .Include(f => f.IdmedNavigation)
                .Include(f => f.IdpacNavigation)
                .FirstOrDefaultAsync(m => m.Idfic == id);
            if (fichaMed == null)
            {
                return NotFound();
            }

            return View(fichaMed);
        }

        // POST: FichaMeds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FichaMeds == null)
            {
                return Problem("Entity set 'HospitalContext.FichaMeds'  is null.");
            }
            var fichaMed = await _context.FichaMeds.FindAsync(id);
            if (fichaMed != null)
            {
                _context.FichaMeds.Remove(fichaMed);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FichaMedExists(int id)
        {
          return (_context.FichaMeds?.Any(e => e.Idfic == id)).GetValueOrDefault();
        }
    }
}
