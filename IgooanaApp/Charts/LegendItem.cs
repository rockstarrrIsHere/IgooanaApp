﻿using System.Windows;
using System.Windows.Media;

namespace IgooanaApp.WP8.Charts {
  // Note: This class is used to facilitate a workaround with Silverlight issue of displaying objects 
  // inherited from DependencyObject in ItemsControl

  /// <summary>
  /// Represents a single chart legend item.
  /// </summary>
  public class LegendItem : DependencyObject, ILegendItem {
    /// <summary>
    /// Instantiates new LegendItem object and binds it to the original chart items.
    /// </summary>
    /// <param name="originalItem"></param>
    public LegendItem(ILegendItem originalItem) {
      OriginalItem = originalItem;

      SetBindings();
    }

    private void SetBindings() {
      // TODO: deal with this issue so the data is bound (updated). Can't bind to DependencyObject in SL3/WP7
      this.Title = OriginalItem.Title;
      this.Brush = OriginalItem.Brush;
    }

    /// <summary>
    /// Identifies <see cref="Title"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
        "Title", typeof(string), typeof(LegendItem),
        new PropertyMetadata(null)
        );

    /// <summary>
    /// Gets or sets the title of the slice.
    /// This is a dependency property.
    /// </summary>
    public string Title {
      get { return (string)GetValue(TitleProperty); }
      set { SetValue(TitleProperty, value); }
    }

    /// <summary>
    /// Identifies <see cref="Brush"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty BrushProperty = DependencyProperty.Register(
        "Brush", typeof(Brush), typeof(LegendItem),
        new PropertyMetadata(null)
        );

    /// <summary>
    /// Gets or sets brush for the slice.
    /// This is a dependency property.
    /// </summary>
    public Brush Brush {
      get { return (Brush)GetValue(BrushProperty); }
      set { SetValue(BrushProperty, value); }
    }

    /// <summary>
    /// Gets or sets original source object for this item.
    /// </summary>
    public ILegendItem OriginalItem {
      get;
      private set;
    }
  }
}
