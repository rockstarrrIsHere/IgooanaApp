using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace IgooanaApp.ViewModels {
  public class DashboardPageviewsViewModel {
    const int HOURS_COUNT = 24;
    const int DAYS_IN_WEEK_COUNT = 7;
    private readonly int[,] cellData = new int[HOURS_COUNT, DAYS_IN_WEEK_COUNT];
    private readonly Cell[,] cells = new Cell[HOURS_COUNT + 1, DAYS_IN_WEEK_COUNT + 1]; // +1 for headers
    private readonly Color baseColor;
    private readonly DateTimeFormatInfo dateTimeFormatInfo;

    public DashboardPageviewsViewModel(IEnumerable<dynamic> gaRows, Color baseColor) {
      this.baseColor = baseColor;
      dateTimeFormatInfo = DateTimeFormatInfo.CurrentInfo;
      FillCellData(gaRows);
      // getting strings like Sun, Mon, Tue etc..
      FillTopRow();
      FillOtherRows(cellData, baseColor);
    }
    public Cell[,] Cells { get { return cells; } }

    private void FillCellData(IEnumerable<dynamic> gaRows) {
      for (int hour = 0; hour < HOURS_COUNT; hour++) {
        for (int dayOfWeek = 0; dayOfWeek < DAYS_IN_WEEK_COUNT; dayOfWeek++) {
          dynamic gaValue = gaRows.FirstOrDefault(x => GetLocalDayOfWeekIndex(x.DayOfWeek) == dayOfWeek && x.Hour == hour);
          if (gaValue != null) {
            cellData[hour, dayOfWeek] = gaValue.Pageviews;
          }
        }
      }
    }

    private int GetLocalDayOfWeekIndex(int index) {
      if (dateTimeFormatInfo.FirstDayOfWeek == DayOfWeek.Sunday) {
        // the data from GA comes in this format already, no need to do anything
        return index;
      }
      else {
        return DAYS_IN_WEEK_COUNT - (int)dateTimeFormatInfo.FirstDayOfWeek + index;
      }
    }


    private void FillTopRow() {
      string[] abbreviatedDaysOfWeek = EnumerateEnum<DayOfWeek>(dateTimeFormatInfo.FirstDayOfWeek)
        .Select(x => dateTimeFormatInfo.GetAbbreviatedDayName(x))
        .ToArray();
      // upper-left corner is empty cell
      cells[0, 0] = Cell.Empty;

      // filling days of weeks in the upper row
      for (int dayOfWeek = 0; dayOfWeek < DAYS_IN_WEEK_COUNT; dayOfWeek++) {
        int cellColumn = dayOfWeek + 1;
        cells[0, cellColumn] = new HeaderCell(abbreviatedDaysOfWeek[dayOfWeek], 0, cellColumn);
      }
    }
    private void FillOtherRows(int[,] cellData, Color baseColor) {
      int maxPageviews = cellData.Cast<int>().Max();
      for (int hour = 0; hour < HOURS_COUNT; hour++) {
        for (int dayOfWeek = 0; dayOfWeek < DAYS_IN_WEEK_COUNT; dayOfWeek++) {
          int cellRow = hour + 1;
          int cellColumn = dayOfWeek + 1;
          // setting the hour cell
          TimeSpan ts = new TimeSpan(hour, 0, 0);
          cells[cellRow, 0] = new HeaderCell(new DateTime(ts.Ticks).ToString("t"), cellRow, 0);
          int pageviews = cellData[hour, dayOfWeek];
          float ratio = ((float)(pageviews)) / maxPageviews;
          baseColor.A = (byte)(Byte.MaxValue * ratio);
          cells[cellRow, cellColumn] = new ColorCell(baseColor, cellRow, cellColumn);
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
        this.gridColumn = gridColumn;
        this.gridRow = gridRow;
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
          return new TextBlock { Text = text, HorizontalAlignment = HorizontalAlignment.Center, Foreground = new SolidColorBrush(Colors.Gray) };
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
          return new Rectangle { Fill = new SolidColorBrush { Color = color }, Margin = new Thickness(2) };
        }
      }
    }
  }
}
