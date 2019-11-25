using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;
using System.Data.Entity;

namespace Nba {

    public partial class TeamsMain : Page {
        nbaEntities context = new nbaEntities();
        CollectionViewSource confViewSource;

        public TeamsMain() {
            InitializeComponent();
            confViewSource = ((CollectionViewSource)
                (FindResource("conferenceViewSource")));            
            DataContext = this;
        }

        //Обработчик события загрузки страницы (событие Loaded в файле разметки
        //в разделе Page)
        private void OnPageLoad(object sender, RoutedEventArgs e) {
            //Обновить заголовок окна
            Window.GetWindow(this).Title = "Teams Main";

            //Подготовить источник данных
            context.Conferences.Load();
            confViewSource.Source = context.Conferences.Local;
        }

        //Нажатие кнопки Back
        private void BackClick(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(new Uri("StartPage.xaml", UriKind.RelativeOrAbsolute));
        }

        //Команда перехода к экрану с детальными сведениями о команде. Для
        //корректного перехода нужно знать ID выбранной команды NBA. При этом
        //ссылка на выбранный объект Team является параметром команды OpenTeamDetail
        private void OpenTeamDetailHandler(object sender, 
                System.Windows.Input.ExecutedRoutedEventArgs e) {
            Team team = e.Parameter as Team;

            //Чтобы передать ID команды на страницу TeamDetail.xaml,
            //добавляем параметр запроса teamId со значением team.TeamId
            //к ссылке на страницу
            String uri = "TeamDetail.xaml?teamId=" + team.TeamId;
            NavigationService.Navigate(new Uri(uri, UriKind.RelativeOrAbsolute));
        }
    }
}
