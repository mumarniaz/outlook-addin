using EmailSaveAddin.Models;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;

namespace EmailSaveAddin.Converters
{
    public class CheckboxToStringConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string s = "";
            var options = (ObservableCollection<Choice>)values[0];
            foreach (var option in options)
            {
                if (option.IsSelected)
                    s += option.Option + ",";
            }
            return s.TrimEnd(',');
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
