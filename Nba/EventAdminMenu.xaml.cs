using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Nba
{

    public partial class EventAdminMenu : Page
    {
        public EventAdminMenu()
        {
            InitializeComponent();
        }

        private void BackClick(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(new Uri("AdminLogin.xaml", UriKind.RelativeOrAbsolute));
        }

        private void MatchupsClick(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(new Uri("AddMatchup.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) {
            //Обновить заголовок окна
            Window.GetWindow(this).Title = "Event Administrator menu";
        }
    }
}
