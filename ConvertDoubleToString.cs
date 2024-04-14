using System;
using System.Globalization;
using System.Windows.Data;

namespace DiscordNotificationBot
{
    public class ConvertDoubleToString : IValueConverter
    {
        private string? lastString = "";
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double)
            {
                var stringValue = lastString ?? value.ToString();
                lastString = null;

                return stringValue;
            }
            return null;
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (double.TryParse(value as string, out double result))
            {
                lastString = value as string;
                return result;
            }
            return null;
        }
    }
}