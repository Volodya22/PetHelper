using System.Windows;
using DesktopClient.Services;
using DesktopClient.ViewModels;
using RestSharp;

namespace DesktopClient
{
    /// <summary>
    /// Interaction logic for AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow
    {
        private RestClient _restClient;

        public AuthWindow()
        {
            InitializeComponent();

            _restClient = new RestClient("http://localhost:46985/");
        }

        private void Login_OnClick(object sender, RoutedEventArgs e)
        {
            Auth(Login.Text, Password.Password);
        }

        private void Auth(string login, string password)
        {
            var model = new AuthViewModel
            {
                Email = login,
                Password = password
            };

            var request = new RestRequest("api/account", Method.POST);

            request.AddJsonBody(model);

            var result = _restClient.Execute(request).Content;
            if (result != "null" && result.Split(';')[2].Contains("Doctor"))
            {
                AuthService.CreateAuthService(result.Split(';')[0], result.Split(';')[1]);

                var mainWindow = new MainWindow();
                mainWindow.Show();

                Close();
            }
            else
            {
                Error.Visibility = Visibility.Visible;
            }
        }
    }
}
