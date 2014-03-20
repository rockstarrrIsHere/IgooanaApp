using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IgooanaApp {
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
  }
}
