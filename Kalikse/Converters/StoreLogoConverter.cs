using System;
using System.Globalization;
using Kalikse.Models;
using Microsoft.Maui.Controls;

namespace Kalikse.Converters
{
    public class StoreLogoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var store = value as Store;
            return store?.LogoUrl;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
} 