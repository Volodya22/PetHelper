using System;
using ClassLibrary;
using DesktopClient.Interfaces;
using RestSharp;

namespace DesktopClient.Services
{
    public class DataService : IDataService
    {
        private readonly RestClient _restClient;

        public DataService()
        {
            _restClient = new RestClient("http://localhost:46985/");
        }

        public void SaveUser(User user)
        {
            var request = new RestRequest("api/user/{id}", Method.PUT);

            request.AddUrlSegment("id", user.Id.ToString());
            request.AddJsonBody(user);

            _restClient.Execute(request);
        }

        public void SavePet(Pet pet)
        {
            if (pet.Id == new Guid())
            {
                CreatePet(pet);
            }
            else
            {
                UpdatePet(pet);
            }
        }

        public void SaveVisit(PetVaccination vac)
        {
            var request = new RestRequest("api/petvaccination", Method.POST);

            request.AddJsonBody(vac);

            _restClient.Execute(request);
        }

        private void CreatePet(Pet pet)
        {
            var request = new RestRequest("api/pet", Method.POST);

            request.AddJsonBody(pet);

            _restClient.Execute(request);
        }

        private void UpdatePet(Pet pet)
        {
            var request = new RestRequest("api/pet/{id}", Method.PUT);

            request.AddUrlSegment("id", pet.Id.ToString());
            request.AddJsonBody(pet);

            _restClient.Execute(request);
        }

        public void DeletePet(Guid id)
        {
            var request = new RestRequest("api/pet/{id}", Method.DELETE);

            request.AddUrlSegment("id", id.ToString());

            _restClient.Execute(request);
        }
    }
}
