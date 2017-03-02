using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class PetBreed
    {
        public PetBreed()
        {
            this.Pets = new List<Pet>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int TypeId { get; set; }

        public virtual PetType Type { get; set; }

        public ICollection<Pet> Pets { get; set; }
    }
}
