using System;
using System.Windows.Navigation;
using DesktopClient.ViewModels;
using DesktopClient.Views;

namespace DesktopClient.Navigation
{
    public class NavigationManager
    {
        private readonly NavigationService _navigationService;
        private static NavigationManager _instanse;
        private readonly CardsView _cardsView;

        private NavigationManager(NavigationService navigationService, CardsView cardsView)
        {
            _navigationService = navigationService;

            _cardsView = cardsView;
        }

        public static NavigationManager Instanse
        {
            get
            {
                return _instanse;
            }
        }

        public static void CreateNavigationManager(NavigationService mainFrameService, CardsView cardsView)
        {
            _instanse = new NavigationManager(mainFrameService, cardsView);
        }

        private object GetNavigateObject(object obj)
        {
            if (obj is CardsViewModel)
            {
                return new CardsView { DataContext = obj };
            }
            if (obj is PassportsViewModel)
            {
                return new PassportsView { DataContext = obj };
            }
            if (obj is ProfileViewModel)
            {
                return new ProfileView { DataContext = obj };
            }
            if (obj is CardCreationViewModel)
            {
                return new CardCreationView { DataContext = obj };
            }
            if (obj is PassportCreationViewModel)
            {
                return new PassportCreationView { DataContext = obj };
            }
            if (obj is VisitViewModel)
            {
                return new VisitView { DataContext = obj };
            }
            else
            {
                throw new ArgumentException("Некорректный параметр навигации");
            }
        }

        public void Navigate(object target)
        {
            var navigatedObject = GetNavigateObject(target);

            _navigationService.Navigate(navigatedObject);
        }

        public void NavigateToCards()
        {
            _navigationService.Navigate(_cardsView);
        }
    }
}
