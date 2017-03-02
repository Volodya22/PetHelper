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

namespace PetHelperApi.Controllers
{
    public class VaccinationController : ApiController
    {
        private readonly IRepository _rep;
        
        public VaccinationController(IRepository rep)
        {
            _rep = rep;
        }

        // GET api/Vaccination
        public List<Vaccination> GetVaccinations(int? typeId)
        {
            return _rep.Vaccinations.Where(t => typeId == null || t.PetTypes.Any(p => p.Id == typeId)).ToList();
        }

        // GET api/Vaccination/5
        [ResponseType(typeof(Vaccination))]
        public async Task<IHttpActionResult> GetVaccination(int id)
        {
            var vaccination = await _rep.Vaccinations.FindAsync(id);
            if (vaccination == null)
            {
                return NotFound();
            }

            vaccination.PetTypes = _rep.PetTypes.Where(t => t.Vaccinations.Any(v => v.Id == vaccination.Id)).ToList();
            foreach (var type in vaccination.PetTypes)
            {
                type.PetBreeds = _rep.PetBreeds.Where(b => b.TypeId == type.Id).ToList().Select(s => new PetBreed
                {
                    Id = s.Id,
                    Name = s.Name
                }).ToList();
            }

            return Ok(vaccination);
        }

        // PUT api/Vaccination/5
        public async Task<IHttpActionResult> PutVaccination(int id, Vaccination vaccination)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vaccination.Id)
            {
                return BadRequest();
            }

            _rep.Vaccinations.AddOrUpdate(vaccination);

            try
            {
                await _rep.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VaccinationExists(id))
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

        // POST api/Vaccination
        [ResponseType(typeof(Vaccination))]
        public async Task<IHttpActionResult> PostVaccination(Vaccination vaccination)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _rep.Vaccinations.Add(vaccination);
            await _rep.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = vaccination.Id }, vaccination);
        }

        // DELETE api/Vaccination/5
        [ResponseType(typeof(Vaccination))]
        public async Task<IHttpActionResult> DeleteVaccination(int id)
        {
            Vaccination vaccination = await _rep.Vaccinations.FindAsync(id);
            if (vaccination == null)
            {
                return NotFound();
            }

            _rep.Vaccinations.Remove(vaccination);
            await _rep.SaveChangesAsync();

            return Ok(vaccination);
        }

        private bool VaccinationExists(int id)
        {
            return _rep.Vaccinations.Count(e => e.Id == id) > 0;
        }
        #region  
        public VaccinationController()
        {
            _rep = new PetHelperContext();
        }
        #endregion
    }
}