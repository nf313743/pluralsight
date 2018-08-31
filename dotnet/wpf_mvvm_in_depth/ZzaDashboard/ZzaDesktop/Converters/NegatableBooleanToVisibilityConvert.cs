using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ZzaDesktop.Converters
{
    internal class NegatableBooleanToVisibilityConvert : IValueConverter
    {
        public NegatableBooleanToVisibilityConvert()
        {
            FalseVisibility = Visibility.Collapsed;
        }

        public Visibility FalseVisibility { get; set; }

        public bool Negate { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool result = bool.TryParse(value.ToString(), out bool bVal);
            if (!result) return value;
            if (bVal && !Negate) return Visibility.Visible;
            if (bVal && Negate) return FalseVisibility;
            if (!bVal && Negate) return Visibility.Visible;
            if (!bVal && !Negate) return FalseVisibility;
            else return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}