using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Nba {

    public partial class VisitorMenu : Page {
        public VisitorMenu() {
            InitializeComponent();
        }

        private void OnPageLoad(object sender, RoutedEventArgs e) {
            Window.GetWindow(this).Title = "Visitor Menu";
        }

        private void BackClick(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(new Uri("StartPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void TeamsClick(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(new Uri("TeamsMain.xaml", UriKind.RelativeOrAbsolute));
        }

        private void PlayersClick(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(new Uri("PlayersMain.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
