using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Unziper.Converters
{
    public class IsBoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isVisibility = (bool)value;
            return isVisibility ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var visibility = (Visibility)value;
            switch (visibility)
            {
                case Visibility.Visible:
                    return true;
                case Visibility.Hidden:
                    return false;
                case Visibility.Collapsed:
                    return false;
                default:
                    return true;
            }
        }
    }
}
