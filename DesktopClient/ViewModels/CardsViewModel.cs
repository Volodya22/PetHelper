using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using ClassLibrary;
using DesktopClient.Interfaces;
using DesktopClient.Services;

namespace DesktopClient.ViewModels
{
    public class CardsViewModel : NavigationTargetViewModel
    {
        private readonly IDataService _dataService;

        public CardsViewModel()
        {
            _dataService = ServiceLocator.Instance.GetDataService();

            Cards = new ObservableCollection<Pet>();

            ReadCards();
        }

        public ObservableCollection<Pet> Cards { get; set; }

        public async void ReadCards()
        {
            Cards.Clear();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:46985/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync("api/pet?masterId=null");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<Pet>>();

                    Cards = new ObservableCollection<Pet>(result);
                }
            }

            OnPropertyChanged("Cards");
        }

        public void DeletePet(Guid id)
        {
            _dataService.DeletePet(id);

            ReadCards();
        }
    }
}
