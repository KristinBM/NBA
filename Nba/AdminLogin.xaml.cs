using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Linq;

namespace Nba {

    public partial class AdminLogin : Page {
        nbaEntities context = new nbaEntities();

        public AdminLogin() {
            InitializeComponent();
            DataContext = this;
        }

        private void OnPageLoad(object sender, RoutedEventArgs e) {
            Window.GetWindow(this).Title = "Admin Login";
        }

        private void BackClick(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(new Uri("StartPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void CancelClick(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(new Uri("StartPage.xaml", UriKind.RelativeOrAbsolute));
        }

        //Обработчик нажатия на кнопку Login
        private void LoginClick(object sender, RoutedEventArgs e) {
            //Извлечь из формы введенные данные
            String jobnumber = jobnameTextBox.Text;
            String password = passwordTextBox.Password;

            //Выполнить проверку заполнения формы
            if(CheckInput(jobnumber, password)) {
                errorTextBlock.Text = "";

                //Запросить из БД запись с указанным Jobnumber
                Admin admin = context.Admins
                    .Where(a => a.Jobnumber == jobnumber)
                    .FirstOrDefault<Admin>();

                //Указанного Jobnumber нет в базе данных
                if(admin == null) {
                    errorTextBlock.Text = "Invalid jobnumber!";
                    return;
                }

                //Введены верные Jobnumber и пароль
                if (admin.Passwords == password) {                    
                    errorTextBlock.Text = "";

                    if(admin.RoleId == "1") {
                        
                        NavigationService.Navigate(new Uri("EventAdminMenu.xaml", 
                            UriKind.RelativeOrAbsolute));
                    } else if (admin.RoleId == "2") {
                       
                        NavigationService.Navigate(new Uri("TechAdminMenu.xaml", 
                            UriKind.RelativeOrAbsolute));
                    }
                    return;
                }
                else {
                    //Jobnumber есть, но пароль неверный
                    errorTextBlock.Text = "Wrong password!";
                    return;
                }
            }
            //Неправильно заполнены поля
            else {
                errorTextBlock.Text = "Enter jobnumber and password!";
            }
        }

        //Проверяет, заполнены ли поля Jobnumber и Password на форме
        private bool CheckInput(String jobnumber, String pass) {
            if(jobnumber != null && jobnumber.Length > 0 &&
                pass != null && pass.Length > 0) {
                return true;
            }
            else {
                return false;
            }
        }
    }
}
