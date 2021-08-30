using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PersonasController : ApiController
    {
        private Model1 db = new Model1();

        // GET: api/Personas
        public IQueryable<Personas> GetPersonas()
        {
            return db.Personas;
        }

        // GET: api/Personas/5
        [ResponseType(typeof(Personas))]
        public async Task<IHttpActionResult> GetPersonas(int id)
        {
            Personas personas = new Personas();
            try
            {
                personas = await db.Personas.FindAsync(id);
                if (personas == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Ok(personas);
        }

        // PUT: api/Personas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPersonas(int id, Personas personas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != personas.IdPersona)
            {
                return BadRequest();
            }

            db.Entry(personas).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonasExists(id))
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

        // POST: api/Personas
        [ResponseType(typeof(Personas))]
        public async Task<IHttpActionResult> PostPersonas(Personas personas)
        {
            bool rta = false;
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                #region VALIDACIONES
                if (string.IsNullOrEmpty(personas.Documento))
                    return BadRequest("Campo Documento es obligatorio");

                if (string.IsNullOrEmpty(personas.Nombres))
                    return BadRequest("Campo Nombres es obligatorio");

                if (string.IsNullOrEmpty(personas.Apellidos))
                    return BadRequest("Campo Apellidos obligatorio");

                if (string.IsNullOrEmpty(Convert.ToString(personas.FechaNacimiento)))
                    return BadRequest("Campo Fecha Nacimiento es obligatorio");

                if (IsNumber(personas.Nombres))
                    return BadRequest("Campo Nombres no puede tener números");

                if (IsNumber(personas.Apellidos))
                    return BadRequest("Campo Apellidos no puede tener números");
                #endregion VALIDACIONES

                db.Personas.Add(personas);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return CreatedAtRoute("DefaultApi", new { id = personas.IdPersona }, personas);
        }

        // DELETE: api/Personas/5
        [ResponseType(typeof(Personas))]
        public async Task<IHttpActionResult> DeletePersonas(int id)
        {
            Personas personas = await db.Personas.FindAsync(id);
            if (personas == null)
            {
                return NotFound();
            }

            db.Personas.Remove(personas);
            await db.SaveChangesAsync();

            return Ok(personas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonasExists(int id)
        {
            return db.Personas.Count(e => e.IdPersona == id) > 0;
        }

        private bool IsNumber(string cadena)
        {
            foreach (char c in cadena)
            {
                if (c >= '0' && c <= '9')
                {
                    return true;
                }
            }

            return false;
        }
    }
}