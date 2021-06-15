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
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ServicioWebDatos.Modelo;

namespace ServicioWebApi.Controllers
{
    public class ClimaController : ApiController
    {
        private StrConnection db = new StrConnection();

        // GET: api/Clima
        public IQueryable<Clima> GetClima()
        {
            return db.Clima;
        }

        // GET: api/Clima/5
        [ResponseType(typeof(Clima))]
        public async Task<IHttpActionResult> GetClima(int id)
        {
            Clima clima = await db.Clima.FindAsync(id);
            if (clima == null)
            {
                return NotFound();
            }

            return Ok(clima);
        }

        // PUT: api/Clima/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutClima(int id, Clima clima)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != clima.Id)
            {
                return BadRequest();
            }

            // Valores por defecto.
            clima.Fecha = DateTime.Now;
            clima.Fahrenheit = (clima.Celsius * (9/5)) + 32;
            db.Entry(clima).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClimaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Clima
        [ResponseType(typeof(Clima))]
        public async Task<IHttpActionResult> PostClima(Clima clima)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            clima.Fecha = DateTime.Now;
            clima.Fahrenheit = (clima.Celsius * (9 / 5)) + 32;
            db.Clima.Add(clima);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = clima.Id }, clima);
        }

        // DELETE: api/Clima/5
        [ResponseType(typeof(Clima))]
        public async Task<IHttpActionResult> DeleteClima(int id)
        {
            Clima clima = await db.Clima.FindAsync(id);
            if (clima == null)
            {
                return NotFound();
            }

            db.Clima.Remove(clima);
            await db.SaveChangesAsync();

            return Ok(clima);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClimaExists(int id)
        {
            return db.Clima.Count(e => e.Id == id) > 0;
        }
    }
}