using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClassLibrary
{
    public class Vaccination
    {
        public Vaccination()
        {
            this.PetTypes = new List<PetType>();
            this.Pets = new List<PetVaccination>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<PetType> PetTypes { get; set; }
        public ICollection<PetVaccination> Pets { get; set; } 
    }
}
