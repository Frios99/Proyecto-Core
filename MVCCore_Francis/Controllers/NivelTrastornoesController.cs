using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCCore_Francis.Data;
using MVCCore_Francis.Models;

namespace MVCCore_Francis.Controllers
{
    public class NivelTrastornoesController : Controller
    {
        private readonly AppDBContext _context;

        public NivelTrastornoesController(AppDBContext context)
        {
            _context = context;
        }

        // GET: NivelTrastornoes
        public async Task<IActionResult> Index()
        {
              return _context.NivelTrastornos != null ? 
                          View(await _context.NivelTrastornos.ToListAsync()) :
                          Problem("Entity set 'AppDBContext.NivelTrastornos'  is null.");
        }

        // GET: NivelTrastornoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NivelTrastornos == null)
            {
                return NotFound();
            }

            var nivelTrastorno = await _context.NivelTrastornos
                .FirstOrDefaultAsync(m => m.NivelTrastornoID == id);
            if (nivelTrastorno == null)
            {
                return NotFound();
            }

            return View(nivelTrastorno);
        }

        // GET: NivelTrastornoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NivelTrastornoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NivelTrastornoID,Nivel,Descripcion")] NivelTrastorno nivelTrastorno)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nivelTrastorno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nivelTrastorno);
        }

        // GET: NivelTrastornoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NivelTrastornos == null)
            {
                return NotFound();
            }

            var nivelTrastorno = await _context.NivelTrastornos.FindAsync(id);
            if (nivelTrastorno == null)
            {
                return NotFound();
            }
            return View(nivelTrastorno);
        }

        // POST: NivelTrastornoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NivelTrastornoID,Nivel,Descripcion")] NivelTrastorno nivelTrastorno)
        {
            if (id != nivelTrastorno.NivelTrastornoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nivelTrastorno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NivelTrastornoExists(nivelTrastorno.NivelTrastornoID))
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
            return View(nivelTrastorno);
        }

        // GET: NivelTrastornoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.NivelTrastornos == null)
            {
                return NotFound();
            }

            var nivelTrastorno = await _context.NivelTrastornos
                .FirstOrDefaultAsync(m => m.NivelTrastornoID == id);
            if (nivelTrastorno == null)
            {
                return NotFound();
            }

            return View(nivelTrastorno);
        }

        // POST: NivelTrastornoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.NivelTrastornos == null)
            {
                return Problem("Entity set 'AppDBContext.NivelTrastornos'  is null.");
            }
            var nivelTrastorno = await _context.NivelTrastornos.FindAsync(id);
            if (nivelTrastorno != null)
            {
                _context.NivelTrastornos.Remove(nivelTrastorno);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NivelTrastornoExists(int id)
        {
          return (_context.NivelTrastornos?.Any(e => e.NivelTrastornoID == id)).GetValueOrDefault();
        }
    }
}
