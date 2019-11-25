using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Nba
{

    public partial class TechAdminMenu : Page
    {
        public TechAdminMenu()
        {
            InitializeComponent();
        }

        private void BackClick(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(new Uri("AdminLogin.xaml", UriKind.RelativeOrAbsolute));
        }

        //Обработчик нажатия на кнопку "Manage Executions"
        private void ManageClick(object sender, RoutedEventArgs e) {
            //показать панель msgGrid
            msgGrid.Visibility = Visibility.Visible;
        }

        //Обработчик нажатия на кнопку закрытия панели с сообщением
        private void CloseGridClick(object sender, RoutedEventArgs e) {
            //скрыть панель msgGrid
            msgGrid.Visibility = Visibility.Hidden;
        }
    }
}
