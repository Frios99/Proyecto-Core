using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCCore_Francis.Data;
using MVCCore_Francis.Models;

namespace MVCCore_Francis.Controllers
{
    public class CuestionariosController : Controller
    {
        private readonly AppDBContext _context;

        public CuestionariosController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Cuestionarios
        public async Task<IActionResult> Index()
        {
              return _context.Cuestionarios != null ? 
                          View(await _context.Cuestionarios.ToListAsync()) :
                          Problem("Entity set 'AppDBContext.Cuestionarios'  is null.");
        }

        // GET: Cuestionarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cuestionarios == null)
            {
                return NotFound();
            }

            var cuestionario = await _context.Cuestionarios
                .FirstOrDefaultAsync(m => m.CuestionarioID == id);
            if (cuestionario == null)
            {
                return NotFound();
            }

            return View(cuestionario);
        }

        // GET: Cuestionarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cuestionarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CuestionarioID,Pregunta,Opcion1,Opcion2,Opcion3,Opcion4,Puntaje1,Puntaje2,Puntaje3,Puntaje4")] Cuestionario cuestionario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cuestionario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cuestionario);
        }

        // GET: Cuestionarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cuestionarios == null)
            {
                return NotFound();
            }

            var cuestionario = await _context.Cuestionarios.FindAsync(id);
            if (cuestionario == null)
            {
                return NotFound();
            }
            return View(cuestionario);
        }

        // POST: Cuestionarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CuestionarioID,Pregunta,Opcion1,Opcion2,Opcion3,Opcion4,Puntaje1,Puntaje2,Puntaje3,Puntaje4")] Cuestionario cuestionario)
        {
            if (id != cuestionario.CuestionarioID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cuestionario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CuestionarioExists(cuestionario.CuestionarioID))
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
            return View(cuestionario);
        }

        // GET: Cuestionarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cuestionarios == null)
            {
                return NotFound();
            }

            var cuestionario = await _context.Cuestionarios
                .FirstOrDefaultAsync(m => m.CuestionarioID == id);
            if (cuestionario == null)
            {
                return NotFound();
            }

            return View(cuestionario);
        }

        // POST: Cuestionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cuestionarios == null)
            {
                return Problem("Entity set 'AppDBContext.Cuestionarios'  is null.");
            }
            var cuestionario = await _context.Cuestionarios.FindAsync(id);
            if (cuestionario != null)
            {
                _context.Cuestionarios.Remove(cuestionario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CuestionarioExists(int id)
        {
          return (_context.Cuestionarios?.Any(e => e.CuestionarioID == id)).GetValueOrDefault();
        }

        // GET
        public async Task<IActionResult> Cuestionario ()
        {
            return _context.Cuestionarios != null ?
                          View(await _context.Cuestionarios.ToListAsync()) :
                          Problem("Entity set 'AppDBContext.Cuestionarios'  is null.");
        }

        // POST: Cuestionarios/Cuestionario
        [HttpPost, ActionName("Cuestionario")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CuestionarioPOST(IFormCollection form)
        {

            

            var rows = await _context.Cuestionarios.FromSqlRaw($"SELECT * FROM Cuestionarios").ToListAsync();
            int puntajes = 0;
            int nt = 0;

            foreach (var row in rows)
            {

                if (row.Opcion1 == form[$"Opcion_{row.CuestionarioID}"])
                {
                    puntajes += row.Puntaje1;
                }
                if (row.Opcion2 == form[$"Opcion_{row.CuestionarioID}"])
                {
                    puntajes += row.Puntaje2;
                   
                }
                if (row.Opcion3 == form[$"Opcion_{row.CuestionarioID}"])
                {
                    puntajes += row.Puntaje3;

                }
                if (row.Opcion4 == form[$"Opcion_{row.CuestionarioID}"])
                {
                    puntajes += row.Puntaje4;

                }

                if(puntajes < 12)
                {
                    var nv = _context.NivelTrastornos.FromSqlRaw("SELECT * FROM NivelTrastornos WHERE Nivel = 'Ansiedad Nula'").FirstOrDefault();
                    
                    
                    nt = nv.NivelTrastornoID;
                }
                else if (puntajes > 12 && puntajes <= 24)
                {
                    var nv = _context.NivelTrastornos.FromSqlRaw("SELECT * FROM NivelTrastornos WHERE Nivel = 'Ansiedad Leve'").FirstOrDefault();
                    
                    nt = nv.NivelTrastornoID;
                }
                else if (puntajes > 24 && puntajes <= 36)
                {
                    var nv = _context.NivelTrastornos.FromSqlRaw("SELECT * FROM NivelTrastornos WHERE Nivel = 'Ansiedad Moderada'").FirstOrDefault();
                    
                    nt = nv.NivelTrastornoID;
                }
                else if (puntajes > 36 && puntajes <= 48)
                {
                    var nv = _context.NivelTrastornos.FromSqlRaw("SELECT * FROM NivelTrastornos WHERE Nivel = 'Ansiedad Grave'").FirstOrDefault();
                    
                    
                    nt = nv.NivelTrastornoID;
                }
            }

            var ssid = HttpContext.Session.GetString("Id");

            if (ssid != null)
            {
                var usr = _context.Usuarios.FromSqlRaw($"Select * from Usuarios where UsuarioID = {ssid}"). FirstOrDefault();
                usr.NivelTranstorno = nt;
                _context.Usuarios.Update(usr);
                await _context.SaveChangesAsync();

            }

            return RedirectToAction("LoggedIn", "Usuarios");
        }
    }
}
