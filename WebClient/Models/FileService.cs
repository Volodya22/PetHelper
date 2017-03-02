using System;
using RestSharp;

namespace WebClient.Models
{
    public class FileService
    {
        public static void SavePetImage(Guid id, string img)
        {
            var rest = new RestClient("http://localhost:46985/");
            var request = new RestRequest("api/pet/" + id, Method.PUT);

            request.AddParameter("Id", id);
            request.AddParameter("ImageUrl", img);

            rest.Execute(request);
        }
        public static void SaveUserImage(Guid id, string img)
        {
            var rest = new RestClient("http://localhost:46985/");
            var request = new RestRequest("api/user/" + id, Method.PUT);

            request.AddParameter("Id", id);
            request.AddParameter("ImageUrl", img);

            rest.Execute(request);
        }
    }
}