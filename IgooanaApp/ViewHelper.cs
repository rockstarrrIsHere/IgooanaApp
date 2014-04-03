using System.Collections.Generic;
using System.Windows;
using Windows.UI;

namespace IgooanaApp.WP8 {
  /// <summary>
  /// Contains methods that can be helpful in views
  /// </summary>
  public static class ViewHelper {
    /// <summary>
    /// Toggles visibility
    /// </summary>
    public static Visibility ToggleVisibility(Visibility visibility) {
      if (visibility == System.Windows.Visibility.Collapsed) {
        return System.Windows.Visibility.Visible;
      }
      return System.Windows.Visibility.Collapsed;
    }
    public static IEnumerable<Color> PaletteColors {
      get {
        yield return Color.FromArgb(byte.MaxValue, 247, 140, 64);
        yield return Color.FromArgb(byte.MaxValue, 171, 227, 0);
        yield return Color.FromArgb(byte.MaxValue, 251, 200, 22);
        yield return Color.FromArgb(byte.MaxValue, 2, 186, 63);
        yield return Color.FromArgb(byte.MaxValue, 0, 219, 227);
        yield return Color.FromArgb(byte.MaxValue, 255, 103, 200);
        yield return Color.FromArgb(byte.MaxValue, 0, 228, 70);
        yield return Color.FromArgb(byte.MaxValue, 129, 16, 101);
        yield return Color.FromArgb(byte.MaxValue, 243, 57, 57);
        yield return Color.FromArgb(byte.MaxValue, 36, 36, 36);
      }
    }
  }
}
