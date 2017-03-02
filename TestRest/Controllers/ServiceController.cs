using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ClassLibrary;
using DataContext;

namespace TestRest.Controllers
{
    public class ServiceController : ApiController
    {
        private readonly IRepository _rep;
        
        public ServiceController(IRepository rep)
        {
            _rep = rep;
        }

        // GET api/Service
        public List<Service> GetServices()
        {
            return _rep.Services.ToList();
        }

        // GET api/Service/5
        [ResponseType(typeof(Service))]
        public async Task<IHttpActionResult> GetService(int id)
        {
            Service service = await _rep.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            return Ok(service);
        }

        // PUT api/Service/5
        public async Task<IHttpActionResult> PutService(int id, Service service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != service.Id)
            {
                return BadRequest();
            }

            _rep.Services.AddOrUpdate(service);

            try
            {
                await _rep.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
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

        // POST api/Service
        [ResponseType(typeof(Service))]
        public async Task<IHttpActionResult> PostService(Service service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _rep.Services.Add(service);
            await _rep.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = service.Id }, service);
        }

        // DELETE api/Service/5
        [ResponseType(typeof(Service))]
        public async Task<IHttpActionResult> DeleteService(int id)
        {
            Service service = await _rep.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            _rep.Services.Remove(service);
            await _rep.SaveChangesAsync();

            return Ok(service);
        }

        private bool ServiceExists(int id)
        {
            return _rep.Services.Count(e => e.Id == id) > 0;
        }
    }
}