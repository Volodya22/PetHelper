using System.Windows.Controls;
using System.Windows.Navigation;
using DesktopClient.Interfaces;
using DesktopClient.Navigation;
using DesktopClient.ViewModels;
using DesktopClient.Views;

namespace DesktopClient
{
    /// <summary>
    /// Interaction logic for MainContentControl.xaml
    /// </summary>
    public partial class MainContentControl
    {
        private readonly IMenuControl _menu;

        public MainContentControl(CardsViewModel cards, IMenuControl menu)
        {
            InitializeComponent();

            _menu = menu;

            var cardsView = new CardsView { DataContext = cards };

            NavigationManager.CreateNavigationManager(NavigationService, cardsView);
            NavigationManager.Instanse.NavigateToCards();
        }
        
        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            _menu.Model = GetPageModel(e.Content);
        }

        private object GetPageModel(object obj)
        {
            if (obj is Page)
            {
                return (obj as Page).DataContext;
            }

            return obj;
        }
    }
}
