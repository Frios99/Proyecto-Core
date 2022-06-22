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
    public class HistorialsController : Controller
    {
        private readonly AppDBContext _context;

        public HistorialsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Historials
        public async Task<IActionResult> Index()
        {
            int ssid = Int32.Parse(HttpContext.Session.GetString("Id"));
            return _context.Historiales != null ? 
                          View(await _context.Historiales.Where(x => x.UsuarioID == ssid).ToListAsync()) :
                          Problem("Entity set 'AppDBContext.Historiales'  is null.");
        }

        // GET: Historials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Historiales == null)
            {
                return NotFound();
            }

            var historial = await _context.Historiales
                .FirstOrDefaultAsync(m => m.HistorialID == id);
            if (historial == null)
            {
                return NotFound();
            }

            return View(historial);
        }

        // GET: Historials/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Historials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HistorialID,Fecha,Historia,Titulo,UsuarioID")] Historial historial)
        {
            Console.WriteLine($"\n\n\n\n {historial.UsuarioID} \n\n\n\n");
            if (ModelState.IsValid)
            {
                _context.Add(historial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(historial);
        }

        // GET: Historials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Historiales == null)
            {
                return NotFound();
            }

            var historial = await _context.Historiales.FindAsync(id);
            if (historial == null)
            {
                return NotFound();
            }
            return View(historial);
        }

        // POST: Historials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HistorialID,Fecha,Historia,Titulo,UsuarioID")] Historial historial)
        {
            if (id != historial.HistorialID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistorialExists(historial.HistorialID))
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
            return View(historial);
        }

        // GET: Historials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Historiales == null)
            {
                return NotFound();
            }

            var historial = await _context.Historiales
                .FirstOrDefaultAsync(m => m.HistorialID == id);
            if (historial == null)
            {
                return NotFound();
            }

            return View(historial);
        }

        // POST: Historials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Historiales == null)
            {
                return Problem("Entity set 'AppDBContext.Historiales'  is null.");
            }
            var historial = await _context.Historiales.FindAsync(id);
            if (historial != null)
            {
                _context.Historiales.Remove(historial);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistorialExists(int id)
        {
          return (_context.Historiales?.Any(e => e.HistorialID == id)).GetValueOrDefault();
        }

        public async Task<ActionResult> Recomendacion(int id)
        {
            return RedirectToAction("MisRecomendaciones", "Recomendacions", new {id = id});
        }
    }
}
