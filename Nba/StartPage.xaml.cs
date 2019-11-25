using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace Nba {

    public partial class StartPage : Page {
        private const String picturePath = "img/pictures/";
        nbaEntities context = new nbaEntities();
        List<Picture> pictures = new List<Picture>();

        //Для переключения страниц с картинками
        int index = 0;
        int maxIndex;

        public StartPage() {
            InitializeComponent();
            DataContext = this;
        }

        private void OnPageLoad(object sender, RoutedEventArgs e) {
            //Обновить заголовок окна
            Window.GetWindow(this).Title = "Main Screen";
            //Отключить кнопку Back
            backButton.IsEnabled = false;

            pictures = context.Pictures.ToList();
            //Определить максимальный номер страницы
            maxIndex = pictures.Last().Id / 3 - 1;

            //Вывести первые 3 картинки
            ShowPictures();            
        }

        private void AdminClick(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(new Uri("AdminLogin.xaml", UriKind.RelativeOrAbsolute));
        }

        private void BackClick(object sender, RoutedEventArgs e) {
            if(index > 0) {
                index--;
                if(index == 0) {
                    backButton.IsEnabled = false;
                }
                nextButton.IsEnabled = true;
                ShowPictures();
            }
        }

        private void NextClick(object sender, RoutedEventArgs e) {
            if (index < maxIndex) {
                index++;
                if (index == maxIndex) {
                    nextButton.IsEnabled = false;
                }
                backButton.IsEnabled = true;
                ShowPictures();
            }
        }

        private void ShowPictures() {
            //Подготовка картинки
            BitmapImage bitmap1 = new BitmapImage();
            bitmap1.BeginInit();
            //Сформировать путь к картинке:
            //"img/pictures/" + название файла, которое было указано в таблице Pictures
            bitmap1.UriSource = new Uri(picturePath 
                + pictures[index * 3].Img, 
                UriKind.Relative);
            bitmap1.EndInit();
            //Показать картинку
            image1.Source = bitmap1;

            BitmapImage bitmap2 = new BitmapImage();
            bitmap2.BeginInit();
            bitmap2.UriSource = new Uri(picturePath 
                + pictures[index * 3 + 1].Img, UriKind.Relative);
            bitmap2.EndInit();
            image2.Source = bitmap2;

            BitmapImage bitmap3 = new BitmapImage();
            bitmap3.BeginInit();
            bitmap3.UriSource = new Uri(picturePath 
                + pictures[index * 3 + 2].Img, UriKind.Relative);
            bitmap3.EndInit();
            image3.Source = bitmap3;
        }

        private void VisitorClick(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(new Uri("VisitorMenu.xaml", 
                UriKind.RelativeOrAbsolute));
        }
    }
}
