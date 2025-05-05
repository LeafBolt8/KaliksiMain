using System.Globalization;

namespace Kalikse.Converters
{
    public class AllergensToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is List<string> allergens)
            {
                if (allergens.Count == 0)
                    return "No allergens";
                return string.Join(", ", allergens);
            }
            return "No allergens";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
} 