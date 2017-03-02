using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClassLibrary;
using DesktopClient.Navigation;
using DesktopClient.ViewModels;

namespace DesktopClient.Views
{
    /// <summary>
    /// Interaction logic for PassportsView.xaml
    /// </summary>
    public partial class PassportsView : Page
    {
        public PassportsView()
        {
            InitializeComponent();
        }

        private void Details_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationManager.Instanse.Navigate(new PassportCreationViewModel(GetPet(sender)));
        }

        private Pet GetPet(object sender)
        {
            var button = sender as Button;
            if (button == null) return new Pet();

            var model = button.DataContext as Pet;
            if (model != null) return model;

            return new Pet();
        }
    }
}
