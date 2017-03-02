using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Role
    {
        public Role()
        {
            this.Users = new List<User>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<User> Users { get; set; } 
    }
}
