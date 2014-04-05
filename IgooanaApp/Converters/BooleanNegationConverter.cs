using System;
using System.Windows.Data;

namespace IgooanaApp.WP8.Converters {
  public sealed class BooleanNegationConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
      return !(value is bool && (bool)value);
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
      return !(value is bool && (bool)value);
    }
  }
}
