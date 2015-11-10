using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Pihalve.Player.ValueConverters
{
    [ValueConversion(typeof(ICollection<string>), typeof(string))]
    public class CollectionToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!((ICollection<string>) value).Any())
            {
                return DependencyProperty.UnsetValue;
            }

            return ((ICollection<string>)value).Aggregate((a, b) => string.Format(culture, "{0}, {1}", a, b));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
