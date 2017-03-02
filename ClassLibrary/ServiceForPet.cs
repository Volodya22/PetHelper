using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class ServiceForPet
    {
        public int Id { get; set; }
        public Guid PetId { get; set; }
        public int ServiceId { get; set; }
        public DateTime Date { get; set; }
        public bool State { get; set; }

        public virtual Service Service { get; set; }
        public virtual Pet Pet{ get; set; }
    }
}
