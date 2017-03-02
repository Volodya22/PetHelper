using System;
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
using TestRest.Services;

namespace TestRest.Controllers
{
    public class ServiceForPetController : ApiController
    {
        private IRepository _rep;

        public ServiceForPetController(IRepository rep)
        {
            _rep = rep;
        }

        // GET api/ServiceForPet
        public List<ServiceForPet> GetServicesForPets(Guid? masterId)
        {
            var services =
                _rep.ServicesForPets.Include("Pet.Master")
                    .Include("Service")
                    .Where(s => (masterId == null || s.Pet.MasterId == masterId) && s.State == false)
                    .ToList()
                    .Select(TransformService.TransformServiceForPet).ToList();

            return services;
        }
        
        // GET api/ServiceForPet/5
        [ResponseType(typeof(ServiceForPet))]
        public async Task<IHttpActionResult> GetServiceForPet(int id)
        {
            ServiceForPet serviceforpet = _rep.ServicesForPets.Include("Pet")
                    .Include("Service").Select(TransformService.TransformServiceForPet).FirstOrDefault(s => s.Id == id);
            if (serviceforpet == null)
            {
                return NotFound();
            }

            return Ok(serviceforpet);
        }

        // PUT api/ServiceForPet/5
        public async Task<IHttpActionResult> PutServiceForPet(int id, ServiceForPet serviceforpet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != serviceforpet.Id)
            {
                return BadRequest();
            }

            _rep.ServicesForPets.AddOrUpdate(serviceforpet);

            try
            {
                await _rep.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceForPetExists(id))
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

        // POST api/ServiceForPet
        [ResponseType(typeof(ServiceForPet))]
        public async Task<IHttpActionResult> PostServiceForPet(ServiceForPet serviceforpet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _rep.ServicesForPets.Add(serviceforpet);
            await _rep.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = serviceforpet.Id }, serviceforpet);
        }

        // DELETE api/ServiceForPet/5
        [ResponseType(typeof(ServiceForPet))]
        public async Task<IHttpActionResult> DeleteServiceForPet(int id)
        {
            ServiceForPet serviceforpet = await _rep.ServicesForPets.FindAsync(id);
            if (serviceforpet == null)
            {
                return NotFound();
            }

            _rep.ServicesForPets.Remove(serviceforpet);
            await _rep.SaveChangesAsync();

            return Ok(serviceforpet);
        }

        private bool ServiceForPetExists(int id)
        {
            return _rep.ServicesForPets.Count(e => e.Id == id) > 0;
        }
    }
}