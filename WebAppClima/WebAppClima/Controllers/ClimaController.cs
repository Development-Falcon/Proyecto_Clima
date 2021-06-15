/* -----------------------------------------------
   Javier Cañadulce H.
   Copyright: Junio-2021
----------------------------------------------- */
/* -----------------------------------------------
   Prueba Técnica Ingeneo SAS 
   Código C# para controlar los métodos
   de acceso de la entidad a la tabla Clima.
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

namespace WebAppClima.Controllers
{
    [Authorize]
    public class ClimaController : Controller
    {
        private StrConnection db = new StrConnection();

        // GET: Clima
        public async Task<ActionResult> Index()
        {
            return View(await db.Clima.ToListAsync());
        }

        // GET: Clima/Detalles/5
        public async Task<ActionResult> Detalles(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clima clima = await db.Clima.FindAsync(id);
            if (clima == null)
            {
                return HttpNotFound();
            }
            return View(clima);
        }

        // GET: Clima/Crear
        public ActionResult Crear()
        {
            return View();
        }

        // POST: Clima/Crear
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Crear([Bind(Include = "Id,Ciudad,Celsius,Fahrenheit,Latitud,Longitud,Fecha")] Clima clima)
        {
            if (ModelState.IsValid)
            {
                // Valores por defecto.
                clima.Fecha = DateTime.Now;
                clima.Fahrenheit = (clima.Celsius * (9/5)) + 32;
                db.Clima.Add(clima);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(clima);
        }

        // GET: Clima/Editar/5
        public async Task<ActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clima clima = await db.Clima.FindAsync(id);
            if (clima == null)
            {
                return HttpNotFound();
            }
            return View(clima);
        }

        // POST: Clima/Editar/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar([Bind(Include = "Id,Ciudad,Celsius,Fahrenheit,Latitud,Longitud,Fecha")] Clima clima)
        {
            if (ModelState.IsValid)
            {
                // Valores por defecto.
                clima.Fecha = DateTime.Now;
                clima.Fahrenheit = (clima.Celsius * (9/5)) + 32;
                db.Entry(clima).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(clima);
        }

        // GET: Clima/Eliminar/5
        public async Task<ActionResult> Eliminar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clima clima = await db.Clima.FindAsync(id);
            if (clima == null)
            {
                return HttpNotFound();
            }
            return View(clima);
        }

        // POST: Clima/Delete/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ConfirmarBorrado(int id)
        {
            Clima clima = await db.Clima.FindAsync(id);
            db.Clima.Remove(clima);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
