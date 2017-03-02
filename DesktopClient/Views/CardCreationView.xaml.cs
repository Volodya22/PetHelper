using System.Windows;
using DesktopClient.Navigation;
using DesktopClient.ViewModels;

namespace DesktopClient.Views
{
    /// <summary>
    /// Interaction logic for CardCreationView.xaml
    /// </summary>
    public partial class CardCreationView
    {
        public CardCreationView()
        {
            InitializeComponent();
        }

        public CardCreationViewModel Model
        {
            get { return DataContext as CardCreationViewModel; }
        }

        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Model.Name))
            {
                MessageBox.Show("Введите кличку питомца!", "Ошибка", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            if (Model.Weight == 0)
            {
                MessageBox.Show("Заполните поле вес!", "Ошибка", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            Model.SaveCard();
            MessageBox.Show("Данные успешно сохранены!", "Сообщение об изменениях", MessageBoxButton.OK,
               MessageBoxImage.Information);
            NavigationManager.Instanse.Navigate(new CardsViewModel());
        }

        private void AddVac_OnClick(object sender, RoutedEventArgs e)
        {
            Model.AddVaccination();
        }

        private void DeleteVac_OnClick(object sender, RoutedEventArgs e)
        {
            Model.DeleteVaccination();
        }
    }
}
