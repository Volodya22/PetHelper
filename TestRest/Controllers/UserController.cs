using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ClassLibrary;
using DataContext;

namespace TestRest.Controllers
{
    public class UserController : ApiController
    {
        private readonly IRepository _rep;
        
        public UserController(IRepository rep)
        {
            _rep = rep;
        }

        // GET api/User
        public List<User> GetUsers(int roleId)
        {
            var result = _rep.Users.Where(u => u.Roles.Any(r => r.Id == roleId)).ToList();
            return result;
        }

        // GET api/User/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUser(Guid id)
        {
            var user = await _rep.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT api/User/5
        public async Task<IHttpActionResult> PutUser(Guid id, User user)
        {
            var dbuser = _rep.Users.Find(user.Id);
            if (!ModelState.IsValid)
            {
                if (dbuser != null && user.ImageUrl != null && dbuser.ImageUrl != user.ImageUrl)
                {
                    dbuser.ImageUrl = user.ImageUrl;
                    _rep.Users.AddOrUpdate(dbuser);

                    await _rep.SaveChangesAsync();
                }


                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            if (user.ImageUrl == null)
            {
                user.ImageUrl = dbuser.ImageUrl;
            }

            _rep.Users.AddOrUpdate(user);

            try
            {
                await _rep.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST api/User
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var roles = user.Roles.Select(x => x.Id).ToList();

            using (var md5 = MD5.Create())
            {
                user.Password = CryptService.GetMd5Hash(md5, user.Password);
            }
            user.Id = Guid.NewGuid();
            user.Roles.Clear();
            _rep.Users.Add(user);

            try
            {
                await _rep.SaveChangesAsync();

                var saved = _rep.Users.Find(user.Id);
                foreach (var role in roles)
                {
                    var dbrole = _rep.Roles.Find(role);
                    saved.Roles.Add(dbrole);
                    dbrole.Users.Add(saved);
                }

                await _rep.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(user.Id);
        }

        // DELETE api/User/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(Guid id)
        {
            User user = await _rep.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _rep.Users.Remove(user);
            await _rep.SaveChangesAsync();

            return Ok(user);
        }

        private bool UserExists(Guid id)
        {
            return _rep.Users.Count(e => e.Id == id) > 0;
        }
    }
}