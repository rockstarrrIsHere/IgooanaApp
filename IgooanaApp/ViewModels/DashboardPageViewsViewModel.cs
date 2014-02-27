using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace IgooanaApp.ViewModels {
  public class DashboardPageViewsViewModel {
    const int ROW_COUNT = 25; // 24 hours + 1 header
    const int COLUMN_COUNT = 8; // 7 days a week + 1 header
    private readonly Cell[,] cells = new Cell[ROW_COUNT, COLUMN_COUNT];
    private readonly Color baseColor;

    public DashboardPageViewsViewModel(IEnumerable<dynamic> gaRows, Color baseColor) {
      this.baseColor = baseColor;
      DateTimeFormatInfo dtf = DateTimeFormatInfo.CurrentInfo;
      // getting strings like Sun, Mon, Tue etc..
      FillTopRow(dtf);
      FillOtherRows(gaRows, baseColor);
    }
    public Cell[,] Cells { get { return cells; } }

    private void FillTopRow(DateTimeFormatInfo dtf) {
      string[] abbreviatedDaysOfWeek = EnumerateEnum<DayOfWeek>(dtf.FirstDayOfWeek)
        .Select(x => dtf.GetAbbreviatedDayName(x))
        .ToArray();
      // upper-left corner is empty cell
      cells[0, 0] = Cell.Empty;

      // filling days of weeks in the upper row
      for (int c = 1; c < COLUMN_COUNT; c++) {
        cells[0, c] = new HeaderCell(abbreviatedDaysOfWeek[c - 1], 0, c);
      }
    }
    private void FillOtherRows(IEnumerable<dynamic> gaRows, Color baseColor) {
      for (int r = 1; r < ROW_COUNT; r++) {
        // setting the hour cell
        var ts = new TimeSpan(r - 1, 0, 0);
        cells[r, 0] = new HeaderCell(new DateTime(ts.Ticks).ToString("t"), r, 0);
        var tr = new Random();
        for (int c = 1; c < COLUMN_COUNT; c++) {
          var gaValue = gaRows.ElementAt(r * (c - 1));
          // TODO: something is wrong here
          //int pageViews = gaValue.PageViews;
          baseColor.A = (byte)tr.Next(0, 255);
          cells[r, c] = new ColorCell(baseColor, r, c);
        }
      }
    }

    private IEnumerable<T> EnumerateEnum<T>(T startFrom) {
      var values = Enum.GetValues(typeof(T)).Cast<T>().ToArray();
      int index = Array.IndexOf(values, startFrom);
      return values.Skip(index).Concat(values.Take(index));
    }

    /// <summary>
    /// Represents a single cell at the intersection of hour and week day
    /// </summary>
    public abstract class Cell {
      private readonly int gridRow;
      private readonly int gridColumn;


      protected Cell() { }
      public Cell(int gridRow, int gridColumn) {
        this.gridRow = gridRow;
        this.gridColumn = gridColumn;
      }

      public virtual int GridRow { get { return gridRow; } }
      public virtual int GridColumn { get { return gridColumn; } }
      public abstract FrameworkElement Content { get; }
      public static Cell Empty { get { return new EmptyCell(); } }
    }

    public class EmptyCell : Cell {
      public override int GridRow { get { return 0; } }
      public override int GridColumn { get { return 0; } }

      public override FrameworkElement Content {
        get { return new TextBlock(); }
      }
    }

    public class HeaderCell : Cell {
      private readonly string text;
      public HeaderCell(string text, int gridRow, int gridColumn)
        : base(gridRow, gridColumn) {
        this.text = text;
      }
      public override FrameworkElement Content {
        get {
          return new TextBlock { Text = text, HorizontalAlignment = HorizontalAlignment.Center, Foreground = new SolidColorBrush(Colors.Gray)};
        }
      }
    }

    public class ColorCell : Cell {
      private readonly Color color;

      public ColorCell(Color color, int gridRow, int gridColumn)
        : base(gridRow, gridColumn) {
        this.color = color;
      }

      public override FrameworkElement Content {
        get {
          return new Rectangle { Fill = new SolidColorBrush { Color = color }, Margin = new Thickness(1) };
        }
      }
    }
  }
}
