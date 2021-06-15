/* -----------------------------------------------
   Javier Cañadulce H.
   Copyright: Junio-2021
----------------------------------------------- */
/* -----------------------------------------------
   Prueba Técnica Ingeneo SAS 
   Código C# para controlar los métodos
   de acceso de la entidad a la tabla Usuarios.
----------------------------------------------- */
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppClima.Models;
using System.Web.Security;

namespace WebAppClima.Controllers
{
    public class UsuariosController : Controller
    {
        private StrConnection db = new StrConnection();

        // GET: Usuarios
        [Authorize]
        public async Task<ActionResult> Index()
        {
            return View(await db.Usuarios.ToListAsync());
        }

        // GET: Usuarios/Detalles/5
        [Authorize]
        public async Task<ActionResult> Detalles(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = await db.Usuarios.FindAsync(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // GET: Usuarios/Crear
        [Authorize]
        public ActionResult Crear()
        {
            return View();
        }

        // POST: Usuarios/Crear
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Crear([Bind(Include = "Id,Usuario,Password")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuarios);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(usuarios);
        }

        // GET: Usuarios/Editar/5
        [Authorize]
        public async Task<ActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = await db.Usuarios.FindAsync(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // POST: Usuarios/Editar/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar([Bind(Include = "Id,Usuario,Password")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuarios).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(usuarios);
        }

        // GET: Usuarios/Eliminar/5
        [Authorize]
        public async Task<ActionResult> Eliminar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = await db.Usuarios.FindAsync(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Eliminar")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ConfirmarBorrado(int id)
        {
            Usuarios usuarios = await db.Usuarios.FindAsync(id);
            db.Usuarios.Remove(usuarios);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        // GET: Usuarios/IniciarSesion
        public ActionResult IniciarSesion(string Mensaje_Error = "")
        {
            ViewBag.Mensaje_Error = Mensaje_Error;
            return View();
        }

        // POST: Usuarios/IniciarSesion
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> IniciarSesion([Bind(Include = "Id, Usuario, Password")] Usuarios usuarios)
        {
            if (!string.IsNullOrEmpty(usuarios.Usuario) && !string.IsNullOrEmpty(usuarios.Password))
            {
                var oUsuario = await db.Usuarios.FirstOrDefaultAsync(u => u.Usuario == usuarios.Usuario && u.Password == usuarios.Password);
                if (oUsuario == null)
                {
                    // return HttpNotFound();
                    return RedirectToAction("IniciarSesion", new { Mensaje_Error = "Usuario NO Encontrado." });
                }
                else
                {
                    
                    FormsAuthentication.SetAuthCookie(oUsuario.Usuario, true);
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("IniciarSesion", new {Mensaje_Error = "Debe Escribir el Nombre de Usuario y Contraseña." });
            }
        }

        
        // GET: Usuarios/CerrarSesion
        [Authorize]
        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("IniciarSesion", "Usuarios");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
