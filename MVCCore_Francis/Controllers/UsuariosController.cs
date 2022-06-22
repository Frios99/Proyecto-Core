using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCCore_Francis.Data;
using MVCCore_Francis.Models;
using Microsoft.AspNetCore.Http;


namespace MVCCore_Francis.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly AppDBContext _context;

        public UsuariosController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            return _context.Usuarios != null ?
                        View(await _context.Usuarios.ToListAsync()) :
                        Problem("Entity set 'AppDBContext.Usuarios'  is null.");
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.UsuarioID == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsuarioID,Rol,Nombre,Correo,Contrasena,NivelTranstorno")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UsuarioID,Rol,Nombre,Correo,Contrasena,NivelTranstorno")] Usuario usuario)
        {
            if (id != usuario.UsuarioID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.UsuarioID))
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
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.UsuarioID == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'AppDBContext.Usuarios'  is null.");
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return (_context.Usuarios?.Any(e => e.UsuarioID == id)).GetValueOrDefault();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Usuario account)
        {
            if (ModelState.IsValid)
            {
                _context.Usuarios.Add(account);
                _context.SaveChanges();
                ModelState.Clear();
                ViewBag.Message = account.Nombre + " successfully registered.";
            }
            return View("Login");
        }
        //Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Usuario user)
        {
            var usr = _context.Usuarios.Single(u => u.Correo == user.Correo && u.Contrasena == user.Contrasena && u.Rol == u.Rol);
            if (usr != null)
            {

                HttpContext.Session.SetString("Id", usr.UsuarioID.ToString());
                HttpContext.Session.SetString("Nombre", usr.Nombre.ToString());
                HttpContext.Session.SetString("role", usr.Rol.ToString());
                return RedirectToAction("LoggedIn");

            }
            else
            {
                ModelState.AddModelError("", "Username or Password is wrong");
            }
            return View();
        }
        public ActionResult LoggedIn()
        {
            var ssid = HttpContext.Session.GetString("Id");
            if ( ssid != null)
            {
            
                var usr = _context.Usuarios.FromSqlRaw($"SELECT * FROM Usuarios Where UsuarioID = {ssid}").FirstOrDefault();
                if(usr.NivelTranstorno != null)
                {
                    var nv = _context.NivelTrastornos.FromSqlRaw($"SELECT * FROM NivelTrastornos Where NivelTrastornoID = {usr.NivelTranstorno}").FirstOrDefault();
                    TempData["Nivel"] = nv.Nivel;
                    TempData["Desc"] = nv.Descripcion;

                }
                
                return View();
            }
            else
            {
                return RedirectToAction("Login");

            }
        }
    }
}
