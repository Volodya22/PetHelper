using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;

namespace DesktopClient.Interfaces
{
    public interface IDataService
    {
        void SaveUser(User user);
        void SavePet(Pet pet);
        void SaveVisit(PetVaccination vac);

        void DeletePet(Guid id);
    }
}
