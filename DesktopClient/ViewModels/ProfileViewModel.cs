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
    public class ProfileViewModel : NavigationTargetViewModel
    {
        private User _user;
        private readonly IDataService _dataService;

        public ProfileViewModel()
        {
            _user = new User();
            _dataService = ServiceLocator.Instance.GetDataService();

            Trophies = new ObservableCollection<string>();

            ReadUser();
        }

        public string FullName
        {
            get
            {
                return _user.Surname + " " + _user.Name + " " + (_user.Patronymic ?? "");
            }
            set
            {
                if (string.IsNullOrEmpty(value)) return;

                var spl = value.Split(' ');

                switch (spl.Count())
                {
                    case 3:
                        _user.Surname = spl[0];
                        _user.Name = spl[1];
                        _user.Patronymic = spl[2];
                        break;
                    case 2:
                        _user.Surname = spl[0];
                        _user.Name = spl[1];
                        break;
                    default:
                        _user.Surname = spl[0];
                        break;
                }
            }
        }

        public string Image
        {
            get
            {
                var local = "http://localhost:24744/Content/images/";
                var ext = "http://localhost:46985/Content/images/";
                var img = _user.ImageUrl;

                if (string.IsNullOrEmpty(img)) {
                    if (_user.Gender) {
                        return ext + "male-doc.jpg";
                    } else {
                        return ext + "female-doc.jpg";
                    }
                } else {
                    return local + img;
                }
            }
        }

        public string Education
        {
            get { return _user.Education; }
            set { _user.Education = value; }
        }

        public string Specialization
        {
            get { return _user.Specialization; }
            set { _user.Specialization = value; }
        }

        public DateTime BirthDate
        {
            get { return _user.BirthDate; }
            set { _user.BirthDate = value; }
        }

        public bool IsMale
        {
            get { return _user.Gender; }
            set { _user.Gender = value; }
        }

        public bool IsFemale
        {
            get { return !_user.Gender; }
            set { _user.Gender = !value; }
        }

        public ObservableCollection<string> Trophies { get; set; }

        private async void ReadUser()
        {
            Trophies.Clear();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:46985/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync("api/user?id=" + AuthService.Instance.UserId);

                if (response.IsSuccessStatusCode)
                {
                    _user = await response.Content.ReadAsAsync<User>();
                    _user.Roles = new List<Role>
                    {
                        new Role
                        {
                            Id = 2,
                            Name = "Doctor"
                        }
                    };

                    if (!string.IsNullOrEmpty(_user.Trophies))
                    {
                        Trophies = new ObservableCollection<string>(_user.Trophies.Split(';'));
                        OnPropertyChanged("Trophies");
                    } 
                    else if (_user.Trophies == null)
                    {
                        _user.Trophies = "";
                    }
                }
            }

            OnPropertyChanged("FullName");
            OnPropertyChanged("Image");
            OnPropertyChanged("Education");
            OnPropertyChanged("Specialization");
            OnPropertyChanged("BirthDate");
            OnPropertyChanged("IsMale");
            OnPropertyChanged("IsFemale");
        }

        public void AddTrophy(string t)
        {
            if (_user.Trophies == "")
            {
                _user.Trophies = t;
            }
            else
            {
                _user.Trophies += ";" + t;
            }

            Trophies.Add(t);
            OnPropertyChanged("Trophies");
        }

        public void SaveUser()
        {
            _dataService.SaveUser(_user);
        }
    }
}
