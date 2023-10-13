using EmailSaveAddin.Helpers;
using EmailSaveAddin.Models;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace EmailSaveAddin.Converters
{
    public class EmailToForegroundColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Contact contact = (Contact)value;
            // here check if email is a part of database emails list then return blue color. TODO: Remove hard coded list
            if (Utilities.IsInternalContact(contact))
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
