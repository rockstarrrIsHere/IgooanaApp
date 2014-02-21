using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace IgooanaApp.Charts {
  /// <summary>
  /// Facilitates rendering of pie chart slices.
  /// </summary>
  public class Slice : Control, ILegendItem {
    private Path slicePath;
    private double radius = 0;
    private double percentage = 0;
    /// <summary>
    /// Instantiates Slice.
    /// </summary>
    public Slice() {
      this.DefaultStyleKey = typeof(Slice);
    }

    /// <summary>
    /// Identifies <see cref="Title"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
        "Title", typeof(string), typeof(Slice),
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
        "Brush", typeof(Brush), typeof(Slice),
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
    /// Applies control template.
    /// </summary>
    public override void OnApplyTemplate() {
      slicePath = (Path)TreeHelper.TemplateFindName("PART_SliceVisual", this);

      RenderSlice();
    }


    /// <summary>
    /// Sets slice dimensions and renders it.
    /// </summary>
    /// <param name="radius">Slice radius.</param>
    /// <param name="percentage">Percentage of the pie occupied by the slice (as 0-1 value).</param>
    public void SetDimensions(double radius, double percentage) {
      this.radius = radius;
      this.percentage = percentage;

      RenderSlice();
    }

    private void RenderSlice() {
      if (slicePath != null) {
        slicePath.Fill = Brush;
        if (percentage < 1) {
          RenderRegularSlice();
        }
        else {
          RenderSingleSlice();
        }
      }
    }

    private void RenderSingleSlice() {
      // single slice
      EllipseGeometry ellipse = new EllipseGeometry() {
        Center = new Point(0, 0),
        RadiusX = radius,
        RadiusY = radius
      };
      slicePath.Data = ellipse;
    }

    private void RenderRegularSlice() {
      PathGeometry geometry = new PathGeometry();
      PathFigure figure = new PathFigure();
      geometry.Figures.Add(figure);
      slicePath.Data = geometry;
      var offset = radius - radius * 0.3;

      double endAngleRad = percentage * 360 * Math.PI / 180;
      Point outerArcEndPoint = new Point(radius * Math.Cos(endAngleRad), radius * Math.Sin(endAngleRad));
      Point innerArcStartPoint = new Point(offset * Math.Cos(endAngleRad), offset * Math.Sin(endAngleRad));

      figure.StartPoint = new Point(offset, 0);
      figure.Segments.Add(new LineSegment { Point = new Point(radius, 0) });
      figure.Segments.Add(new ArcSegment {
        Size = new Size(radius, radius),
        Point = outerArcEndPoint,
        SweepDirection = SweepDirection.Clockwise,
        IsLargeArc = percentage > 0.5
      });
      figure.Segments.Add(new LineSegment() { Point = innerArcStartPoint });
      figure.Segments.Add(new ArcSegment {
        Size = new Size(offset, offset),
        Point = new Point(offset, 0),
        SweepDirection = SweepDirection.Counterclockwise,
        IsLargeArc = percentage > 0.5
      });
    }
  }
}
