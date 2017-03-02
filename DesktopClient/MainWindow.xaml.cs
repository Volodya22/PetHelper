using DesktopClient.ViewModels;

namespace DesktopClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            var mainMenu = new MenuControl();

            Menu.Content = mainMenu;
            Content.Content = new MainContentControl(new CardsViewModel(), mainMenu);
        }
    }
}
