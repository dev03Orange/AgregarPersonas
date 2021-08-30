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
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class InfoContactosController : ApiController
    {
        private Model1 db = new Model1();

        // GET: api/InfoContactos
        public IQueryable<InfoContacto> GetInfoContacto()
        {
            return db.InfoContacto;
        }

        // GET: api/InfoContactos/5
        [ResponseType(typeof(InfoContacto))]
        public async Task<IHttpActionResult> GetInfoContacto(int id)
        {
            InfoContacto infoContacto = await db.InfoContacto.FindAsync(id);
            if (infoContacto == null)
            {
                return NotFound();
            }

            return Ok(infoContacto);
        }

        // PUT: api/InfoContactos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutInfoContacto(int id, InfoContacto infoContacto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != infoContacto.IdInfo)
            {
                return BadRequest();
            }

            db.Entry(infoContacto).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InfoContactoExists(id))
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

        // POST: api/InfoContactos
        [ResponseType(typeof(InfoContacto))]
        public async Task<IHttpActionResult> PostInfoContacto(InfoContacto infoContacto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Modelo invalido");
                }

                bool rpta = InfoContactoCount(infoContacto.IdPersona);

                if (rpta)
                {
                    return BadRequest("La persona ya ha registrado 2 datos de contacto");
                }

                db.InfoContacto.Add(infoContacto);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return CreatedAtRoute("DefaultApi", new { id = infoContacto.IdInfo }, infoContacto);
        }

        // DELETE: api/InfoContactos/5
        [ResponseType(typeof(InfoContacto))]
        public async Task<IHttpActionResult> DeleteInfoContacto(int id)
        {
            InfoContacto infoContacto = await db.InfoContacto.FindAsync(id);
            if (infoContacto == null)
            {
                return NotFound();
            }

            db.InfoContacto.Remove(infoContacto);
            await db.SaveChangesAsync();

            return Ok(infoContacto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InfoContactoExists(int id)
        {
            return db.InfoContacto.Count(e => e.IdInfo == id) > 0;
        }

        private bool InfoContactoCount(int id)
        {
            bool rpta = db.InfoContacto.Count(e => e.IdPersona == id) >= 2;

            return rpta;
        }
    }
}