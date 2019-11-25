using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;
using System.Data.Entity;

namespace Nba {

    public partial class AddMatchup : Page {
        int seasonId;
        int matchupTypeId;
        int teamAwayId;
        int teamHomeId;

        nbaEntities context = new nbaEntities();
        CollectionViewSource teamAwayViewSource;
        CollectionViewSource teamHomeViewSource;

        public AddMatchup() {
            InitializeComponent();
            teamAwayViewSource = ((CollectionViewSource)(FindResource("teamAwayViewSource")));
            teamHomeViewSource = ((CollectionViewSource)(FindResource("teamHomeViewSource")));
            DataContext = this;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) {
            //Обновить заголовок окна
            Window.GetWindow(this).Title = "Add a new matchup for regular season";

            //Подготовить источник данных
            context.Teams.Load();
            teamAwayViewSource.Source = context.Teams.Local;
            teamHomeViewSource.Source = context.Teams.Local;

            //Данные можно задать вручную для экономии времени
            //Извлечь из базы данных ID последнего сезона
            seasonId = context.Seasons.ToList().LastOrDefault<Season>().SeasonId;
            //Извлечь из базы данных ID типа матча "Regular season"
            matchupTypeId = context.MatchupTypes
                    .Where(m => m.Name == "Regular Season")
                    .FirstOrDefault<MatchupType>()
                    .MatchupTypeId;

            teamAwayId = 1;
            teamHomeId = 1;
        }

        private void BackClick(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(new Uri("EventAdminMenu.xaml", UriKind.RelativeOrAbsolute));
        }

        //Обработчик нажатия на кнопку Submit
        private void SubmitClick(object sender, RoutedEventArgs e) {
            //Извлечь и проверить дату из формы
            DateTime startTime;
            if (datePicker.SelectedDate.HasValue) {
                startTime = datePicker.SelectedDate.Value;
            } else {
                errorLabel.Content = "Укажите дату начала матча!";
                return;
            }

            //Извлечь время
            String time = timeTextBox.Text;
            try {
                double hour = Double.Parse(time.Substring(0, 2));
                double minutes = Double.Parse(time.Substring(3, 2));
                startTime = startTime.AddHours(hour);
                startTime = startTime.AddMinutes(minutes);
            }
            catch {
                errorLabel.Content = "Неправильный формат времени!";
                return;
            }
            
            if(teamAwayId == teamHomeId) {
                errorLabel.Content = "Выберите разные команды!";
                return;
            }

            //Извлечь место матча
            String location = locationTextBox.Text;

            //Создать новый матч на основе извлеченных данных
            Matchup matchup = new Matchup() {
                SeasonId = seasonId,
                MatchupTypeId = matchupTypeId,
                Location = location,
                Starttime = startTime,
                Team_Away = teamAwayId,
                Team_Home = teamHomeId,
                Status = -1,
                Team_Away_Score = 0,
                Team_Home_Score = 0,
                CurrentQuarter = "",
            };

            //Добавить новый матч в БД и сохранить изменения
            context.Matchups.Add(matchup);
            context.SaveChanges();
            errorLabel.Content = "Матч успешно добавлен!";
        }

        //Обработчик события смены команды Team Away
        private void teamAwaySelection(object sender, SelectionChangedEventArgs e) {
            //Извлечь выбранную команду из элемента формы с именем teamAwayComboBox
            Team team = teamAwayComboBox.SelectedItem as Team;
            //Параметр teamAwayId теперь равен ID выбранной команды
            teamAwayId = team.TeamId;
        }

        //Обработчик события смены команды Team Home
        private void teamHomeSelection(object sender, SelectionChangedEventArgs e) {
            //Извлечь выбранную команду из элемента формы с именем teamHomeComboBox
            Team team = teamHomeComboBox.SelectedItem as Team;
            //Параметр teamHomeId теперь равен ID выбранной команды
            teamHomeId = team.TeamId;
        }
    }
}
