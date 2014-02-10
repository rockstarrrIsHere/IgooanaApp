using System.Windows;
using System.Windows.Controls;

namespace IgooanaApp {
  public class Layout : ContentControl {
    private bool menuOpen = false;
    public Layout() {
      DefaultStyleKey = typeof(Layout);
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
