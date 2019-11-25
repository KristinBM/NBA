using System;
using System.Windows.Data;
using System.Globalization;

namespace Nba
{
    //Данный конвертер преобразует строку с именем файла логотипа команды из поля Logo в
    //таблице Team в URI
    [ValueConversion(typeof(String), typeof(Uri))]
    class TeamLogoConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return new Uri("img/teams/" + value.ToString(), UriKind.Relative);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
