using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using ClassLibrary;
using DesktopClient.Interfaces;
using DesktopClient.Services;

namespace DesktopClient.ViewModels
{
    public class CardCreationViewModel : NavigationTargetViewModel
    {
        private IDataService _dataService;
        private readonly Pet _pet;
        private PetType _petType;

        public CardCreationViewModel()
        {
            _pet = new Pet
            {
                BirthDate = DateTime.Today,
                BreedId = 1,
                Breed = new PetBreed
                {
                    Id = 1,
                    TypeId = 1
                }
            };
            _petType = new PetType
            {
                Id = 1
            };

            Initialize(false);
        }

        public CardCreationViewModel(Pet pet)
        {
            _pet = pet;
            _petType = new PetType
            {
                Id = _pet.Breed.TypeId
            };

            Initialize(true);
        }

        public string Name
        {
            get { return _pet.Name; }
            set { _pet.Name = value; }
        }

        public DateTime BirthDate
        {
            get { return _pet.BirthDate; }
            set { _pet.BirthDate = value; }
        }

        public double Weight
        {
            get { return _pet.Weight; }
            set { _pet.Weight = value; }
        }

        public User SelectedUser
        {
            get { return _pet.Master; }
            set { _pet.Master = value; }
        }

        public PetType SelectedType
        {
            get { return _petType; }
            set
            {
                if (value == null) return;

                _petType = value; 
                
                RefreshBreeds();
                ReadTotalVaccinations();
            }
        }

        public PetBreed SelectedBreed
        {
            get { return _pet.Breed; }
            set { _pet.Breed = value; }
        }

        public Vaccination SelectedVaccination { get; set; }

        public ObservableCollection<User> Users { get; set; }

        public ObservableCollection<PetVaccination> PetsVaccinations { get; set; } 

        public ObservableCollection<Vaccination> TotalVaccinations { get; set; }

        public ObservableCollection<PetType> Types { get; set; }

        public ObservableCollection<PetBreed> Breeds { get; set; } 

        public void Initialize(bool haveVacs)
        {
            _dataService = ServiceLocator.Instance.GetDataService();

            Users = new ObservableCollection<User>();
            TotalVaccinations = new ObservableCollection<Vaccination>();
            PetsVaccinations = new ObservableCollection<PetVaccination>();
            Types = new ObservableCollection<PetType>();
            Breeds = new ObservableCollection<PetBreed>();

            if (haveVacs)
            {
                PetsVaccinations = new ObservableCollection<PetVaccination>(_pet.VaccinationsForPet.ToList());

                OnPropertyChanged("PetsVaccinations");
            }

            ReadUsers();
            ReadTypes();
            ReadTotalVaccinations();
        }

        public async void ReadUsers()
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
                    SelectedUser = _pet.Master != null ? Users.FirstOrDefault(u => u.Id == _pet.MasterId) : Users.FirstOrDefault();
                }
            }

            OnPropertyChanged("Users");
            OnPropertyChanged("SelectedUser");
        }

        public async void ReadTypes()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:46985/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync("api/vaccination?id=4");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<Vaccination>();

                    Types = new ObservableCollection<PetType>(result.PetTypes);
                    var ss = Types.Where(t => t.PetBreeds.Any(b => b.Id == _pet.BreedId)).ToList();
                    SelectedType = Types.FirstOrDefault(t => t.PetBreeds.Any(b => b.Id == _pet.BreedId));
                }
            }

            OnPropertyChanged("Types");
            OnPropertyChanged("SelectedType");

            RefreshBreeds();

            if (_pet.BreedId != 1)
            {
                SelectedBreed = Breeds.FirstOrDefault(b => b.Id == _pet.BreedId);
                OnPropertyChanged("SelectedBreed");
            }
        }

        private void RefreshBreeds()
        {
            Breeds = new ObservableCollection<PetBreed>(SelectedType.PetBreeds);

            SelectedBreed = Breeds.FirstOrDefault();

            OnPropertyChanged("Breeds");
            OnPropertyChanged("SelectedBreed");
        }

        public async void ReadTotalVaccinations()
        {
            TotalVaccinations.Clear();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:46985/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync("api/vaccination?typeId=" + SelectedType.Id);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<Vaccination>>();

                    TotalVaccinations = new ObservableCollection<Vaccination>(result);
                    SelectedVaccination = TotalVaccinations.FirstOrDefault();
                }
            }

            OnPropertyChanged("TotalVaccinations");
            OnPropertyChanged("SelectedVaccination");
        }

        public void AddVaccination()
        {
            if (PetsVaccinations.Any(v => v.VaccinationId == SelectedVaccination.Id)) return;

            var vac = TotalVaccinations.FirstOrDefault(v => v.Id == SelectedVaccination.Id);
            PetsVaccinations.Add(new PetVaccination
            {
                VaccinationId = vac.Id,
                Vaccination = new Vaccination
                {
                    Id = vac.Id,
                    Name = vac.Name
                }
            });

            OnPropertyChanged("PetsVaccinations");
        }

        public void DeleteVaccination()
        {
            var vac = PetsVaccinations.FirstOrDefault(v => v.VaccinationId == SelectedVaccination.Id);
            if (vac == null) return;

            PetsVaccinations.Remove(vac);

            OnPropertyChanged("PetsVaccinations");
        }

        public void SaveCard()
        {
            var pet = new Pet
            {
                Id = _pet.Id,
                Name = _pet.Name,
                Weight = _pet.Weight,
                BirthDate = _pet.BirthDate.AddHours(6),
                ChipId = _pet.ChipId,
                Gender = _pet.Gender,
                ImageUrl = _pet.ImageUrl,
                BreedId = SelectedBreed.Id,
                MasterId = SelectedUser.Id,
                Special = _pet.Special,
                VaccinationsForPet = PetsVaccinations
            };

            _dataService.SavePet(pet);
        }
    }
}
