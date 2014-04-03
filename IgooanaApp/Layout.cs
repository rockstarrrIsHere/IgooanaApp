using System.Windows;
using System.Windows.Controls;

namespace IgooanaApp.WP8 {
  public class Layout : ContentControl {
    private bool menuOpen = false;
    public Layout() {
      DefaultStyleKey = typeof(Layout);
    }

    public static DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(Layout), null);
    public string Title {
      get { return GetValue(TitleProperty) as string; }
      set { SetValue(TitleProperty, value); }
    }


    public override void OnApplyTemplate() {
      base.OnApplyTemplate();
      Button openMenuButton = GetTemplateChild("OpenMenuButton") as Button;
      if (openMenuButton != null) {
        openMenuButton.Click += (s, e) => {
          if (menuOpen) {
            VisualStateManager.GoToState(this, "MenuClosedState", true);
            menuOpen = false;
          }
          else {
            VisualStateManager.GoToState(this, "MenuOpenState", true);
            menuOpen = true;
          }
        };
      }
    }
  }
}
