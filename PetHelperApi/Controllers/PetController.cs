using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using ClassLibrary;
using DataContext;
using PetHelperApi.Models;
using PetHelperApi.Services;

namespace PetHelperApi.Controllers
{
    public class PetController : ApiController
    {
        private readonly IRepository _rep;
        
        public PetController(IRepository rep)
        {
            _rep = rep;
        }

        // GET api/Pet
        public List<Pet> GetPets(Guid? masterId)
        {
            List<Pet> result;

            if (masterId != null && masterId != new Guid())
            {
                result =
                    _rep.Pets.Where(q => q.MasterId == masterId)
                        .Include("Master")
                        .Include("Breed.Type")
                        .ToList();
            }
            else
            {
                result = _rep.Pets.Include("Master").Include("Breed.Type").ToList();
            }

            result = result.Select(TransformService.TransformPet).ToList();

            return result;
        }

        // GET api/Pet/5
        [ResponseType(typeof(Pet))]
        public async Task<IHttpActionResult> GetPet(Guid id)
        {
            var pet = _rep.Pets.Include("Master").Include("Breed.Type").Include("VaccinationsForPet").FirstOrDefault(p => p.Id == id);
            if (pet == null)
            {
                return NotFound();
            }

            pet = TransformService.TransformPet(pet);

            return Ok(pet);
        }

        // PUT api/Pet/5
        public async Task<IHttpActionResult> PutPet(Guid id, Pet pet)
        {
            var dbpet = _rep.Pets.Find(pet.Id);
            if (!ModelState.IsValid)
            {
                if (dbpet != null && pet.ImageUrl != null && dbpet.ImageUrl != pet.ImageUrl)
                {
                    dbpet.ImageUrl = pet.ImageUrl;
                    _rep.Pets.AddOrUpdate(dbpet);

                    await _rep.SaveChangesAsync();
                }

                return BadRequest(ModelState);
            }

            if (id != pet.Id)
            {
                return BadRequest();
            }

            if (pet.ImageUrl == null)
            {
                pet.ImageUrl = dbpet.ImageUrl;
            }

            var vacs = pet.VaccinationsForPet.Select(p => p.VaccinationId).ToList();
            var vacsToRemove = pet.VaccinationsForPet.Where(v => v.PetId == pet.Id && !vacs.Contains(v.VaccinationId)).ToList();
            foreach (var vac in vacsToRemove)
            {
                _rep.PetsVaccinations.Remove(vac);
            }

            var srvs = pet.ServicesForPet.Select(p => p.ServiceId).ToList();
            var srvsToRemove = pet.ServicesForPet.Where(v => v.PetId == pet.Id && !srvs.Contains(v.ServiceId)).ToList();
            foreach (var srv in srvsToRemove)
            {
                _rep.ServicesForPets.Remove(srv);
            }

            _rep.Pets.AddOrUpdate(pet);

            try
            {
                await _rep.SaveChangesAsync();

                foreach (var vac in vacs.Where(v => _rep.PetsVaccinations.All(p => p.PetId != pet.Id && p.VaccinationId != v)))
                {
                    _rep.PetsVaccinations.Add(new PetVaccination
                    {
                        PetId = pet.Id,
                        VaccinationId = vac,
                        Date = DateTime.Today,
                        State = true
                    });
                }

                foreach (var srv in srvs.Where(v => _rep.ServicesForPets.All(p => p.PetId != pet.Id && p.ServiceId != v)))
                {
                    _rep.PetsVaccinations.Add(new PetVaccination
                    {
                        PetId = pet.Id,
                        VaccinationId = srv,
                        Date = DateTime.Today,
                        State = true
                    });
                }

                await _rep.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetExists(id))
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

        // POST api/Pet
        [ResponseType(typeof(Pet))]
        public async Task<IHttpActionResult> PostPet(Pet pet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var vacs = pet.VaccinationsForPet.Select(p => p.VaccinationId).ToList();
            var srvs = pet.ServicesForPet.Select(p => p.ServiceId).ToList();

            pet.Id = Guid.NewGuid();
            pet.VaccinationsForPet.Clear();
            _rep.Pets.Add(pet);

            try
            {
                await _rep.SaveChangesAsync();

                foreach (var vac in vacs)
                {
                    _rep.PetsVaccinations.Add(new PetVaccination
                    {
                        PetId = pet.Id,
                        VaccinationId = vac,
                        Date = DateTime.Today,
                        State = true
                    });
                }
                foreach (var srv in srvs)
                {
                    _rep.ServicesForPets.Add(new ServiceForPet
                    {
                        PetId = pet.Id,
                        ServiceId = srv,
                        Date = DateTime.Today,
                        State = true
                    });
                }

                await _rep.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PetExists(pet.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(pet.Id);
        }

        // DELETE api/Pet/5
        [ResponseType(typeof(Pet))]
        public async Task<IHttpActionResult> DeletePet(Guid id)
        {
            var vacs = _rep.PetsVaccinations.Where(v => v.PetId == id).ToList();
            _rep.PetsVaccinations.RemoveRange(vacs);

            var srvs = _rep.ServicesForPets.Where(v => v.PetId == id).ToList();
            _rep.ServicesForPets.RemoveRange(srvs);

            await _rep.SaveChangesAsync();

            var pet = await _rep.Pets.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }

            _rep.Pets.Remove(pet);
            await _rep.SaveChangesAsync();

            return Ok(pet);
        }

        private bool PetExists(Guid id)
        {
            return _rep.Pets.Count(e => e.Id == id) > 0;
        }
    }
}