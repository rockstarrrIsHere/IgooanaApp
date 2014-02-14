using System.Windows;
using System.Windows.Controls;

namespace IgooanaApp.Charts {
  /// <summary>
  /// Represents a value balloon (tooltip).
  /// </summary>
  public class Balloon : Control {
    /// <summary>
    /// Instantiates Balloon.
    /// </summary>
    public Balloon() {
      this.DefaultStyleKey = typeof(Balloon);
      this.IsHitTestVisible = false;
    }

    /// <summary>
    /// Identifies <see cref="Text"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        "Text", typeof(string), typeof(Balloon),
        new PropertyMetadata(null)
        );

    /// <summary>
    /// Gets or sets balloon text.
    /// This is a dependency property.
    /// </summary>
    public string Text {
      get { return (string)GetValue(TextProperty); }
      set { SetValue(TextProperty, value); }
    }
  }
}
