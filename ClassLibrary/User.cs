using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClassLibrary
{
    public class User
    {
        public User()
        {
            this.Roles = new List<Role>();
            this.Pets = new List<Pet>();
        }

        public Guid Id { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Name { get; set; }
        public string Patronymic { get; set; }
        [Required]
        public bool Gender { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string Contacts { get; set; }

        // для врачей
        public string ImageUrl { get; set; }
        public string Specialization { get; set; }
        public string Education { get; set; }
        public string Trophies { get; set; }

        public ICollection<Role> Roles { get; set; }
        public ICollection<Pet> Pets { get; set; }
    }
}
