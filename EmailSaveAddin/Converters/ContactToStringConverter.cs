using EmailSaveAddin.Models;
using System;
using System.Globalization;
using System.Windows.Data;

namespace EmailSaveAddin.Converters
{
    internal class ContactToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var contact = (Contact)value;
            return contact.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
