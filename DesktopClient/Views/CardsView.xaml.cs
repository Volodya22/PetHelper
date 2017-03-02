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
    /// Interaction logic for CardsView.xaml
    /// </summary>
    public partial class CardsView
    {
        public CardsView()
        {
            InitializeComponent();
        }

        private CardsViewModel Model
        {
            get { return (CardsViewModel) DataContext; }
        }

        private void NewCard_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationManager.Instanse.Navigate(new CardCreationViewModel());
        }

        private void Edit_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationManager.Instanse.Navigate(new CardCreationViewModel(GetPet(sender)));
        }

        private void Delete_OnClick(object sender, RoutedEventArgs e)
        {
            Model.DeletePet(GetPet(sender).Id);
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
