using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClassLibrary;

namespace PetHelperApi.Models
{
    public class PostedUser 
    {
        public User User { get; set; }
        public int Role { get; set; }
    }
}