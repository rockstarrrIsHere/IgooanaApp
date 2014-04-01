using System.Windows;
using System.Windows.Controls;

namespace IgooanaApp.Controls {
  public class ControlLayout : ContentControl {
    public ControlLayout() {
      DefaultStyleKey = typeof(ControlLayout);
    }

    public static DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(ControlLayout), null);
    public string Title {
      get { return GetValue(TitleProperty) as string; }
      set { SetValue(TitleProperty, value); }
    }
  }
}
