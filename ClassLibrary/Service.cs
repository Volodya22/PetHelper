using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClassLibrary
{
    public class Service
    {
        public Service()
        {
            this.Pets = new List<ServiceForPet>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Description { get; set; }

        public ICollection<ServiceForPet> Pets { get; set; } 
    }
}
