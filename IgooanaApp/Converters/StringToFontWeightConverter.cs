using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace IgooanaApp.WP8.Converters {
  public class StringToFontWeightConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
      if (value != null) {
        switch (value.ToString()) {
          case "Light": return FontWeights.Light;
          case "Bold": return FontWeights.Bold;
          default: return FontWeights.Normal;
        }
      } else {
        return FontWeights.Normal;
      }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
      throw new NotImplementedException();
    }
  }
}
