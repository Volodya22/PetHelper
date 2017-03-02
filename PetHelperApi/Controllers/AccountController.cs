using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ClassLibrary;
using DataContext;
using PetHelperApi.Models;
using PetHelperApi.Services;

namespace PetHelperApi.Controllers
{
    public class AccountController : ApiController
    {
        private readonly IRepository _rep;

        public AccountController(IRepository rep)
        {
            _rep = rep;
        }

        // POST api/Account
        [ResponseType(typeof(string))]
        public string Post(UserViewModel uservm)
        {
            var result = "";
            var user = _rep.Users.Include("Roles").FirstOrDefault(u => u.Email == uservm.Email);

            if (user == null) return null;
            result = user.Surname + ";" + user.Id + ";";

            using (var md5Hash = MD5.Create())
            {
                return CryptService.VerifyMd5Hash(md5Hash, uservm.Password, user.Password)
                    ? user.Roles.Aggregate(result, (current, role) => current + (role.Name + ", "))
                    : null;
            }
        }
    }
}