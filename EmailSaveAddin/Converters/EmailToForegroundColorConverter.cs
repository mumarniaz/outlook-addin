using EmailSaveAddin.Helpers;
using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace EmailSaveAddin.Converters
{
    public class EmailToForegroundColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var email = value.ToString();
            // here check if email is a part of database emails list then return blue color. TODO: Remove hard coded list
            if (Utilities.IsInternalEmail(email))
            {
                return new SolidColorBrush(Colors.White);
            }
            return new SolidColorBrush(Colors.Black);

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
