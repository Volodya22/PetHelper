using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClassLibrary
{
    public class PetType
    {
        public PetType()
        {
            this.PetBreeds = new List<PetBreed>();
            this.Vaccinations = new List<Vaccination>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<PetBreed> PetBreeds { get; set; } 
        public ICollection<Vaccination> Vaccinations { get; set; } 
    }
}
