using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Pet
    {
        public Pet()
        {
            this.ServicesForPet = new List<ServiceForPet>();
            this.VaccinationsForPet = new List<PetVaccination>();
        }

        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool Gender { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        public string Special { get; set; }
        public int ChipId { get; set; }
        public int BreedId { get; set; }
        public Guid MasterId { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public double Weight { get; set; }

        public virtual PetBreed Breed { get; set; }
        public virtual User Master { get; set; }

        public ICollection<ServiceForPet> ServicesForPet { get; set; } 
        public ICollection<PetVaccination> VaccinationsForPet { get; set; } 
    }
}
