using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Data.Entity;
using System.Globalization;

namespace Nba {

    public partial class PlayersMain : Page {
        int teamId;
        int seasonId;
        String nameLetter;
        Hyperlink selectedLetter;
        nbaEntities context = new nbaEntities();
        CollectionViewSource teamViewSource;
        CollectionViewSource seasonViewSource;
        CollectionViewSource playerInTeamViewSource;

        public PlayersMain() {
            InitializeComponent();
            teamViewSource = ((CollectionViewSource)(FindResource("teamViewSource")));
            seasonViewSource = ((CollectionViewSource)(FindResource("seasonViewSource")));
            playerInTeamViewSource = ((CollectionViewSource)(FindResource("playerInTeamViewSource")));
            DataContext = this;
        }

        //Обработчик события загрузки страницы (событие Loaded в файле разметки
        //в разделе Page)
        private void OnPageLoad(object sender, RoutedEventArgs e) {
            //Обновить заголовок окна
            Window.GetWindow(this).Title = "Players Main";

            //Подготовить источники данных
            context.Seasons.Load();
            context.Teams.Load();
            context.PlayerInTeams.Load();
            playerInTeamViewSource.Source = context.PlayerInTeams.Local;
            seasonViewSource.Source = context.Seasons.Local.Reverse<Season>();
            teamViewSource.Source = context.Teams.Local;                    

            //Извлечь из базы данных ID последнего сезона
            seasonId = context.Seasons.ToList().LastOrDefault<Season>().SeasonId;            
            //Извлечь из базы данных ID первой команды
            teamId = context.Teams.ToList().FirstOrDefault<Team>().TeamId;

            playerInTeamViewSource.View.Filter = PlayerFilter;            
            playerInTeamViewSource.View.Refresh();
        }

        private void BackClick(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(new Uri("VisitorMenu.xaml", UriKind.RelativeOrAbsolute));
        }

        //Событие выбора начальной буквы имени (обработчик команды LetterSelect,
        //см. RoutedUICommand в блоке <Page.Resources> в файле разметки PlayersMain.xaml)
        private void LetterSelectHandler(object sender, ExecutedRoutedEventArgs e) {
            //Извлечь выбранную букву
            nameLetter = e.Parameter as String;

            //Убрать выделение предыдущей выбранной буквы
            if(selectedLetter != null) {
                selectedLetter.Foreground = Application.Current.FindResource("WhiteForeground") as Brush;
            }
            
            //Выделить черным цветом выбранную букву
            selectedLetter = (Hyperlink)e.Source;
            selectedLetter.Foreground = Application.Current.FindResource("BlackForeground") as Brush;

            //Применить фильтр по начальной букве имени
            playerInTeamViewSource.View.Filter = PlayerLetterFilter;
            playerInTeamViewSource.View.Refresh();
        }

        //Фильтр игроков по команде и сезону
        private bool PlayerFilter(object item) {
            PlayerInTeam player = item as PlayerInTeam;
            //Выбрать только игроков, выступающих за команду с нужным teamId и в
            //указанном сезоне с seasonId
            return player.TeamId == teamId &&
                player.SeasonId == seasonId;
        }

        //Фильтр игроков по первой букве имени
        private bool PlayerLetterFilter(object item) {
            PlayerInTeam player = item as PlayerInTeam;

            //Если выбраны все игроки, то показать всех игроков выбранного сезона
            if(nameLetter == "ALL") {
                return player.SeasonId == seasonId;
            } else {
                //Если выбрана буква, то показать всех игроков данного сезона,
                //чье имя начинается с указанной буквы
                return player.SeasonId == seasonId && 
                    player.Player.Name.StartsWith(nameLetter);
            }            
        }

        //Обработчик выбора сезона
        private void seasonComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            //Извлечь выбранный сезон из элемента формы с именем seasonComboBox
            Season season = seasonComboBox.SelectedItem as Season;
            //Параметр seasonId теперь равен ID выбранного сезона
            seasonId = season.SeasonId;

            //Обновить фильтр для списка игроков
            playerInTeamViewSource.View.Filter = PlayerFilter;
            playerInTeamViewSource.View.Refresh();
        }

        //Обработчик выбора команды
        private void teamComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            //Извлечь выбранную команду из элемента формы с именем teamComboBox
            Team team = teamComboBox.SelectedItem as Team;
            //Параметр teamId теперь равен ID выбранной команды
            teamId = team.TeamId;

            //Обновить фильтр для списка игроков
            playerInTeamViewSource.View.Filter = PlayerFilter;
            playerInTeamViewSource.View.Refresh();
        }
    }

    //Данный конвертер преобразует строку с именем файла фото игрока из поля Img в
    //таблице Player в URI
    [ValueConversion(typeof(String), typeof(Uri))]
    class PlayerImgConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value.ToString().Length > 0) {
                return new Uri("img/players/" + value.ToString(), UriKind.Relative);
            }
            else {
                return new Uri("img/person.png", UriKind.Relative);
            }            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    //Данный конвертер преобразует дату вступления игрока в NBA в опыт,
    //т.е. текущий год - год вступления
    [ValueConversion(typeof(DateTime), typeof(int))]
    class ExperienceConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            int result = 0;

            try {
                int joinYear = ((DateTime)value).Year;
                result = 2017 - joinYear;
            } catch {

            }
            return result;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
