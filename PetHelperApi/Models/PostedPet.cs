using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClassLibrary;

namespace PetHelperApi.Models
{
    public class PostedPet
    {
        public Pet pet { get; set; }
        public HttpPostedFile img { get; set; }
    }
}