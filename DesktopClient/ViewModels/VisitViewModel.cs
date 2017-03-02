using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using ClassLibrary;
using DesktopClient.Interfaces;
using DesktopClient.Services;

namespace DesktopClient.ViewModels
{
    public class VisitViewModel : NavigationTargetViewModel
    {
        private readonly IDataService _dataService;

        private User _user;
        private Pet _pet;

        public VisitViewModel()
        {
            _dataService = ServiceLocator.Instance.GetDataService();

            Date = DateTime.Today;

            Users = new ObservableCollection<User>();
            Pets = new ObservableCollection<Pet>();
            Vaccinations = new ObservableCollection<Vaccination>();

            ReadUsers();
        }

        public DateTime Date { get; set; }

        public ObservableCollection<User> Users { get; set; }

        public ObservableCollection<Pet> Pets { get; set; }

        public ObservableCollection<Vaccination> Vaccinations { get; set; }

        public User SelectedUser
        {
            get { return _user; }
            set
            {
                _user = value;
                
                if (_user != null)
                    ReadPets();
            }
        }

        public Pet SelectedPet
        {
            get { return _pet; }
            set
            {
                _pet = value;
                
                if (_pet != null)
                    ReadVaccinations();
            }
        }

        public Vaccination SelectedVaccination { get; set; }

        private async void ReadUsers()
        {
            Users.Clear();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:46985/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync("api/user?roleId=3");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<User>>();

                    Users = new ObservableCollection<User>(result);
                    SelectedUser = Users.FirstOrDefault();
                }
            }

            OnPropertyChanged("Users");
            OnPropertyChanged("SelectedUser");
        }

        private async void ReadPets()
        {
            Pets.Clear();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:46985/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync("api/pet?masterId=" + SelectedUser.Id);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<Pet>>();

                    Pets = new ObservableCollection<Pet>(result);
                    SelectedPet = Pets.FirstOrDefault();
                }
            }

            OnPropertyChanged("Pets");
            OnPropertyChanged("SelectedPet");
        }

        private async void ReadVaccinations()
        {
            Vaccinations.Clear();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:46985/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync("api/vaccination?typeId=" + SelectedPet.Breed.TypeId);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<Vaccination>>();

                    Vaccinations = new ObservableCollection<Vaccination>(result);
                    SelectedVaccination = Vaccinations.FirstOrDefault();
                }
            }

            OnPropertyChanged("Vaccinations");
            OnPropertyChanged("SelectedVaccination");
        }

        public void SaveVisit()
        {
            var petVac = new PetVaccination
            {
                PetId = SelectedPet.Id,
                VaccinationId = SelectedVaccination.Id,
                Date = Date,
                State = false
            };

            _dataService.SaveVisit(petVac);
        }
    }
}
