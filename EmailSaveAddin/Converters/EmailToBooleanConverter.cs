using EmailSaveAddin.Helpers;
using EmailSaveAddin.Models;
using System;
using System.Globalization;
using System.Windows.Data;

namespace EmailSaveAddin.Converters
{
    public class EmailToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Contact contact = (Contact)value;
            // here check if email is a part of database emails list then return blue color. TODO: Remove hard coded list
            if (Utilities.IsInternalContact(contact))
            {
                return false;
            }
            return true;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
