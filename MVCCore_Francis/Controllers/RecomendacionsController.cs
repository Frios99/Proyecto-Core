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
    public class RecomendacionsController : Controller
    {
        private readonly AppDBContext _context;

        public RecomendacionsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Recomendacions
        public async Task<IActionResult> Index()
        {

            return _context.Recomendaciones != null ? 
                          View(await _context.Recomendaciones.ToListAsync()) :
                          Problem("Entity set 'AppDBContext.Recomendaciones'  is null.");
        }

        // GET: Recomendacions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Recomendaciones == null)
            {
                return NotFound();
            }

            var recomendacion = await _context.Recomendaciones
                .FirstOrDefaultAsync(m => m.RecomendacionID == id);
            if (recomendacion == null)
            {
                return NotFound();
            }

            return View(recomendacion);
        }

        // GET: Recomendacions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Recomendacions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecomendacionID,Descripcion,Fecha")] Recomendacion recomendacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recomendacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recomendacion);
        }

        // GET: Recomendacions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Recomendaciones == null)
            {
                return NotFound();
            }

            var recomendacion = await _context.Recomendaciones.FindAsync(id);
            if (recomendacion == null)
            {
                return NotFound();
            }
            return View(recomendacion);
        }

        // POST: Recomendacions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RecomendacionID,Descripcion,Fecha")] Recomendacion recomendacion)
        {
            if (id != recomendacion.RecomendacionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recomendacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecomendacionExists(recomendacion.RecomendacionID))
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
            return View(recomendacion);
        }

        // GET: Recomendacions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Recomendaciones == null)
            {
                return NotFound();
            }

            var recomendacion = await _context.Recomendaciones
                .FirstOrDefaultAsync(m => m.RecomendacionID == id);
            if (recomendacion == null)
            {
                return NotFound();
            }

            return View(recomendacion);
        }

        // POST: Recomendacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Recomendaciones == null)
            {
                return Problem("Entity set 'AppDBContext.Recomendaciones'  is null.");
            }
            var recomendacion = await _context.Recomendaciones.FindAsync(id);
            if (recomendacion != null)
            {
                _context.Recomendaciones.Remove(recomendacion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecomendacionExists(int id)
        {
          return (_context.Recomendaciones?.Any(e => e.RecomendacionID == id)).GetValueOrDefault();
        }

        public async Task<ActionResult> MisRecomendaciones(int? id)
        {

            var historial = _context.Historiales.Where(x => x.HistorialID == id).FirstOrDefault();
            var keyWordsDb = _context.palabrasClaves.ToList();

            //string[] keyWords = {};

            var auxKeysFound = new List<string>();

            foreach (var row in keyWordsDb)
            {
                if (historial.Historia.Contains(row.PalabrasClaves)) 
                {
                    auxKeysFound.Add(row.PalabrasClaves); 
                }
            }

            var recomendaciones = await _context.Recomendaciones.FromSqlRaw($"SELECT * FROM Recomendaciones WHERE Descripcion = ''").ToListAsync();
            foreach (var key in auxKeysFound)
            {
                recomendaciones.AddRange(await _context.Recomendaciones.FromSqlRaw($"SELECT * FROM Recomendaciones WHERE Descripcion like '%{key}%'").ToListAsync());
            }

            return View(recomendaciones);
        }
    }
}
