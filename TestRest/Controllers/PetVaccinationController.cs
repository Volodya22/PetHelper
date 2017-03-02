using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;
using ClassLibrary;
using DataContext;

namespace TestRest.Controllers
{
    public class PetVaccinationController : ApiController
    {
        private IRepository _rep;

        public PetVaccinationController(IRepository rep)
        {
            _rep = rep;
        }

        // GET api/PetVaccination
        public List<PetVaccination> GetPetsVaccinations()
        {
            return _rep.PetsVaccinations.ToList();
        }

        // GET api/PetVaccination/5
        [ResponseType(typeof(PetVaccination))]
        public async Task<IHttpActionResult> GetPetVaccination(int id)
        {
            PetVaccination petvaccination = await _rep.PetsVaccinations.FindAsync(id);
            if (petvaccination == null)
            {
                return NotFound();
            }

            return Ok(petvaccination);
        }

        // PUT api/PetVaccination/5
        public async Task<IHttpActionResult> PutPetVaccination(int id, PetVaccination petvaccination)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != petvaccination.Id)
            {
                return BadRequest();
            }

            _rep.PetsVaccinations.AddOrUpdate(petvaccination);

            try
            {
                await _rep.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetVaccinationExists(id))
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

        // POST api/PetVaccination
        [ResponseType(typeof(PetVaccination))]
        public async Task<IHttpActionResult> PostPetVaccination(PetVaccination petvaccination)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _rep.PetsVaccinations.Add(petvaccination);
            await _rep.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = petvaccination.Id }, petvaccination);
        }

        // DELETE api/PetVaccination/5
        [ResponseType(typeof(PetVaccination))]
        public async Task<IHttpActionResult> DeletePetVaccination(int id)
        {
            PetVaccination petvaccination = await _rep.PetsVaccinations.FindAsync(id);
            if (petvaccination == null)
            {
                return NotFound();
            }

            _rep.PetsVaccinations.Remove(petvaccination);
            await _rep.SaveChangesAsync();

            return Ok(petvaccination);
        }

        private bool PetVaccinationExists(int id)
        {
            return _rep.PetsVaccinations.Count(e => e.Id == id) > 0;
        }
    }
}