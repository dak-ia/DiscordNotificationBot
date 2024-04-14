using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DiscordNotificationBot
{
    public class ConvertBooleanToEnum : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var parameterString = parameter as string;
            if (parameterString == null || !Enum.IsDefined(value.GetType(), value))
            {
                return DependencyProperty.UnsetValue;
            }
            var parameterValue = Enum.Parse(value.GetType(), parameterString);
            return parameterValue.Equals(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string? parameterString = parameter as string;
            if (parameterString != null && true.Equals(value))
            {
                return Enum.Parse(targetType, parameterString);
            }
            return DependencyProperty.UnsetValue;
        }
    }
}