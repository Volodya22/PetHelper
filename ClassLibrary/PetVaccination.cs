using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class PetVaccination
    {
        public int Id { get; set; }
        public Guid PetId { get; set; }
        public int VaccinationId { get; set; }
        public DateTime Date { get; set; }
        public bool State { get; set; }

        public virtual Vaccination Vaccination { get; set; }
        public virtual Pet Pet { get; set; }
    }
}
