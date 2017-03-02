using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using DesktopClient.ViewModels;
using Microsoft.Win32;

namespace DesktopClient.Views
{
    /// <summary>
    /// Interaction logic for ProfileView.xaml
    /// </summary>
    public partial class ProfileView
    {
        public ProfileView()
        {
            InitializeComponent();
        }

        private ProfileViewModel Model
        {
            get { return DataContext as ProfileViewModel; }
        }

        private void AddTrophy_OnClick(object sender, RoutedEventArgs e)
        {
            Model.AddTrophy(Trophies.Text);
            Trophies.Clear();
        }

        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Model.FullName))
            {
                MessageBox.Show("Введите ФИО!", "Ошибка", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            Model.SaveUser();
            MessageBox.Show("Изменения успешно сохранены!", "Сообщение об изменениях", MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
    }
}
