using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using ClassLibrary;

namespace DesktopClient.ViewModels
{
    public class PassportsViewModel : NavigationTargetViewModel
    {
        public PassportsViewModel()
        {
            Passports = new ObservableCollection<Pet>();

            ReadPassports();
        }

        public ObservableCollection<Pet> Passports { get; set; }

        public async void ReadPassports()
        {
            Passports.Clear();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:46985/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync("api/pet?masterId=null");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<Pet>>();

                    Passports = new ObservableCollection<Pet>(result);
                }
            }

            OnPropertyChanged("Passports");
        }
    }
}
