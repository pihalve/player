using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Pihalve.Player.ValueConverters
{
    [ValueConversion(typeof(TimeSpan), typeof(string))]
    public class TimespanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return DependencyProperty.UnsetValue;
            }

            var val = (TimeSpan)value;
            if (val == TimeSpan.Zero) val = new TimeSpan(3,4,5);

            return val.TotalDays >= 1 ? $@"{val:d\:hh\:mm\:ss}" : val.TotalHours >= 1 ? $@"{val:h\:mm\:ss}" : $@"{val:m\:ss}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
