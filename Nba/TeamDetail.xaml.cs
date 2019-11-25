using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Data.Entity;
using System.Globalization;

namespace Nba
{

    public partial class TeamDetail : Page
    {
        int teamId;
        int seasonId;
        nbaEntities context = new nbaEntities();
        CollectionViewSource teamViewSource;
        CollectionViewSource seasonViewSource;
        CollectionViewSource playerViewSource;
        CollectionViewSource matchupViewSource;

        public TeamDetail()
        {
            InitializeComponent();
            teamViewSource = ((CollectionViewSource)(FindResource("teamViewSource")));
            seasonViewSource = ((CollectionViewSource)(FindResource("seasonViewSource")));
            playerViewSource = ((CollectionViewSource)(FindResource("playerInTeamViewSource")));
            matchupViewSource = ((CollectionViewSource)(FindResource("matchupViewSource")));
            DataContext = this;
        }

        //Обработчик события загрузки страницы (событие Loaded в файле разметки
        //в разделе Page)
        private void OnPageLoad(object sender, RoutedEventArgs e) {
            //Обновить заголовок окна
            Window.GetWindow(this).Title = "Team Detail";

            //Извлечь ID выбранной команды из URI страницы
            String uri = NavigationService.CurrentSource.OriginalString;
            teamId = Int32.Parse(uri.Substring("TeamDetail.xaml?teamId=".Length));

            //Извлечь из базы данных ID последнего сезона
            seasonId = context.Seasons.ToList().LastOrDefault<Season>().SeasonId;

            //Подготовить источники данных
            context.Teams.Load();
            context.Seasons.Load();
            context.PlayerInTeams.Load();
            context.Matchups.Load();
            teamViewSource.Source = context.Teams.Local;            
            playerViewSource.Source = context.PlayerInTeams.Local;
            matchupViewSource.Source = context.Matchups.Local;
            //Изменить порядок следования сезонов (от текущего к самому раннему)
            seasonViewSource.Source = context.Seasons.Local.Reverse<Season>();

            //tabControl.SelectedIndex = 1;

            //Отфильтровать данные, выбрав сведения только для нужной команды
            Team currentTeam = context.Teams.Where(t => t.TeamId == teamId).FirstOrDefault<Team>();
            teamViewSource.View.MoveCurrentTo(currentTeam);
            
            //Применить фильтр по команде и сезону
            RefreshFilters();
        }        

        //Нажатие кнопки Back
        private void BackClick(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(new Uri("TeamsMain.xaml", UriKind.RelativeOrAbsolute));
        }

        //Обработчик команды Search (см. соответствующую RoutedUICommand в 
        //файле разметки в разделе Page.Resources)
        private void SearchCommand(object sender, ExecutedRoutedEventArgs e) {
            //Извлечь ID сезона, который пользователь выбрал в выпадающем списке
            seasonId = Int32.Parse(e.Parameter.ToString());
            //Применить фильтр по сезону
            RefreshFilters();
        }

        private void RefreshFilters() {
            playerViewSource.View.Filter = PlayerFilter;
            playerViewSource.View.Refresh();
            matchupViewSource.View.Filter = MatchupFilter;
            matchupViewSource.View.Refresh();
            FillLineup();
        }

        private bool PlayerFilter(object item) {
            PlayerInTeam player = item as PlayerInTeam;
            //Выбрать только игроков, выступающих за команду с нужным teamId и в
            //указанном сезоне с seasonId
            return player.TeamId == teamId && player.SeasonId == seasonId;
        }

        private bool MatchupFilter(object item) {
            Matchup matchup = item as Matchup;
            //Выбрать только матчи сезона с seasonId, в которых участвовала данная
            //команда (дома или на выезде)
            return matchup.SeasonId == seasonId && (matchup.Team_Away == teamId 
                                                    || matchup.Team_Home == teamId);
        }

        //Метод для заполнения экрана Lineup
        private void FillLineup() {
            //Выбрать из базы данных игроков для текущей команды и сезона
            List<PlayerInTeam> playerInTeams = context.PlayerInTeams
                .Where(p => p.TeamId == teamId 
                        && p.SeasonId == seasonId)
                .ToList();

            //В TextBlock'и будут выведены эти строки
            String c = "";
            String pf = "";
            String sg = "";
            String sf = "";
            String pg = "";

            //Рассортировать имена игроков в соответствии с их позициями по TextBlock'ам
            for (int i = 0; i < playerInTeams.Count; i++) {
                if(playerInTeams[i].Player.PositionId == 1) {
                    sf += playerInTeams[i].Player.Name + "\n";
                }
                else if(playerInTeams[i].Player.PositionId == 2) {
                    pf += playerInTeams[i].Player.Name + "\n";
                }
                else if (playerInTeams[i].Player.PositionId == 3) {
                    c += playerInTeams[i].Player.Name + "\n";
                }
                else if (playerInTeams[i].Player.PositionId == 4) {
                    sg += playerInTeams[i].Player.Name + "\n";
                }
                else {
                    pg += playerInTeams[i].Player.Name + "\n";
                }
            }

            //Вывести текст
            cTextBlock.Text = c;
            sfTextBlock.Text = sf;
            sgTextBlock.Text = sg;
            pgTextBlock.Text = pg;
            pfTextBlock.Text = pf;
        }
    }

    //Данный конвертер преобразует значения "1" и "-1" из столбца Status таблицы Matchup
    //в строки "Finished" и "Not Started"
    [ValueConversion(typeof(int), typeof(String))]
    public class StatusConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            int status = (int)value;
            if(status == 1) {
                return "Finished";
            }
            else {
                return "Not Started";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
