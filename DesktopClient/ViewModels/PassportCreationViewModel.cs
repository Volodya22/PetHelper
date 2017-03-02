using System;
using System.Collections.ObjectModel;
using ClassLibrary;

namespace DesktopClient.ViewModels
{
    public class PassportCreationViewModel : NavigationTargetViewModel
    {
        private readonly Pet _pet;

        public PassportCreationViewModel(Pet pet)
        {
            _pet = pet;

            PetsVaccinations = new ObservableCollection<PetVaccination>(_pet.VaccinationsForPet);
        }

        public string Name
        {
            get { return _pet.Name; }
        }

        public string BirthDate
        {
            get { return _pet.BirthDate.ToString("s").Split('T')[0]; }
        }

        public string Type
        {
            get { return _pet.Breed.Type.Name; }
        }

        public string Breed
        {
            get { return _pet.Breed.Name; }
        }

        public string Special
        {
            get { return _pet.Special; }
        }

        public string Image
        {
            get
            {
                var local = "http://localhost:24744/Content/images/";
                var ext = "http://localhost:46985/Content/images/";

                if (!string.IsNullOrEmpty(_pet.ImageUrl))
                {
                    return local + _pet.ImageUrl;
                }

                switch (_pet.Breed.Type.Id)
                {
                    case 1:
                        return ext + "dog.gif";
                    case 2:
                        return ext + "cat.png";
                    default:
                        return ext + "parrot.jpg";
                }
            }
        }

        public bool IsMale
        {
            get { return _pet.Gender; }
        }

        public bool IsFemale
        {
            get { return !_pet.Gender; }
        }

        public ObservableCollection<PetVaccination> PetsVaccinations { get; set; } 
    }
}
