using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace IgooanaApp.WP8.Charts {
  /// <summary>
  /// Displays pie charts.
  /// </summary>
  [TemplatePart(Name = "PART_CanvasBorder", Type = typeof(Border))]
  [TemplatePart(Name = "PART_Canvas", Type = typeof(Canvas))]
  [TemplatePart(Name = "PART_Legend", Type = typeof(Legend))]
  public class PieChart : Control {
    private ObservableCollection<Slice> slices = new ObservableCollection<Slice>();
    private Balloon balloon;
    private Legend legend;
    private List<Brush> brushes = new List<Brush>();
    private Canvas canvas;
    private Border canvasBorder;
    private List<string> titles = new List<string>();
    private List<double> values = new List<double>();
    private double total;
    private bool isSliceEvent = false;
    /// <summary>
    /// Instantiates PieChart.
    /// </summary>
    public PieChart() {
      this.DefaultStyleKey = typeof(PieChart);
      this.slices.CollectionChanged += new NotifyCollectionChangedEventHandler(OnSlicesCollectionChanged);
      Padding = new Thickness(10);
    }


    /// <summary>
    /// Identifies <see cref="LegendVisibility"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty LegendVisibilityProperty = DependencyProperty.Register(
        "LegendVisibility", typeof(Visibility), typeof(PieChart),
        new PropertyMetadata(Visibility.Visible)
        );

    /// <summary>
    /// Gets or sets visibility of the chart legend.
    /// This is a dependency property.
    /// The default is Visible.
    /// </summary>
    public Visibility LegendVisibility {
      get { return (Visibility)GetValue(LegendVisibilityProperty); }
      set { SetValue(LegendVisibilityProperty, value); }
    }


    /// <summary>
    /// Identifies <see cref="Text"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
      "Text", typeof(string), typeof(PieChart), new PropertyMetadata(null));

    /// <summary>
    /// Gets or sets the value inside a doughnut.
    /// This is a dependency property.
    /// </summary>
    public string Text {
      get { return (string)GetValue(TextProperty); }
      set { SetValue(TextProperty, value); }
    }

    /// <summary>
    /// Identifies <see cref="Description"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
      "Description", typeof(string), typeof(PieChart), new PropertyMetadata(null));

    /// <summary>
    /// Gets or sets the description inside a doughnut.
    /// This is a dependency property.
    /// </summary>
    public string Description {
      get { return (string)GetValue(DescriptionProperty); }
      set { SetValue(DescriptionProperty, value); }
    }

    /// <summary>
    /// Identifies <see cref="DataSource"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty DataSourceProperty = DependencyProperty.Register(
        "DataSource", typeof(IEnumerable), typeof(PieChart),
        new PropertyMetadata(null, new PropertyChangedCallback(PieChart.OnDataSourcePropertyChanged)));

    /// <summary>
    /// Gets or sets data source for the chart.
    /// This is a dependency property.
    /// </summary>
    public IEnumerable DataSource {
      get { return (IEnumerable)GetValue(DataSourceProperty); }
      set { SetValue(DataSourceProperty, value); }
    }

    private static void OnDataSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
      PieChart chart = d as PieChart;
      //DetachOldDataSourceCollectionChangedListener(chart, e.OldValue);
      //AttachDataSourceCollectionChangedListener(chart, e.NewValue);
      chart.ProcessData();
    }

    private static void DetachOldDataSourceCollectionChangedListener(PieChart chart, object dataSource) {
      if (dataSource != null && dataSource is INotifyCollectionChanged) {
        (dataSource as INotifyCollectionChanged).CollectionChanged -= chart.OnDataSourceCollectionChanged;
      }
    }

    private static void AttachDataSourceCollectionChangedListener(PieChart chart, object dataSource) {
      if (dataSource != null && dataSource is INotifyCollectionChanged) {
        (dataSource as INotifyCollectionChanged).CollectionChanged += new NotifyCollectionChangedEventHandler(chart.OnDataSourceCollectionChanged);
      }
    }

    private void OnDataSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
      // TODO: implement intelligent mechanism to hanlde multiple changes in one batch
      ProcessData();
    }

    /// <summary>
    /// Identifies <see cref="TitleMemberPath"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty TitleMemberPathProperty = DependencyProperty.Register(
        "TitleMemberPath", typeof(string), typeof(PieChart),
        new PropertyMetadata(null, new PropertyChangedCallback(PieChart.OnMemberPathPropertyChanged))
        );

    /// <summary>
    /// Gets or sets path to the property holding slice titles in data source.
    /// This is a dependency property.
    /// </summary>
    public string TitleMemberPath {
      get { return (string)GetValue(TitleMemberPathProperty); }
      set { SetValue(TitleMemberPathProperty, value); }
    }

    /// <summary>
    /// Identifies <see cref="ValueMemberPath"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty ValueMemberPathProperty = DependencyProperty.Register(
        "ValueMemberPath", typeof(string), typeof(PieChart),
        new PropertyMetadata(null, new PropertyChangedCallback(PieChart.OnMemberPathPropertyChanged))
        );

    /// <summary>
    /// Gets or sets path to the member in the datasource holding values for the slice.
    /// This is a dependency property.
    /// </summary>
    public string ValueMemberPath {
      get { return (string)GetValue(ValueMemberPathProperty); }
      set { SetValue(ValueMemberPathProperty, value); }
    }

    private static void OnMemberPathPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
      PieChart chart = d as PieChart;
      if (chart != null) {
        chart.ProcessData();
      }
    }


    private void ProcessData() {
      if (this.DataSource != null) {
        SetData();
        ReallocateSlices();
      } else {
        titles.Clear();
        values.Clear();
        total = 0;
      }
      RenderSlices();
    }

    private void SetData() {
      titles.Clear();
      values.Clear();
      if (!string.IsNullOrEmpty(TitleMemberPath) && !string.IsNullOrEmpty(ValueMemberPath)) {
        BindingEvaluator titleEval = new BindingEvaluator(TitleMemberPath);
        BindingEvaluator valueEval = new BindingEvaluator(ValueMemberPath);
        foreach (object dataItem in this.DataSource) {
          titles.Add(titleEval.Eval(dataItem).ToString());
          values.Add(Convert.ToDouble(valueEval.Eval(dataItem)));
          if (dataItem is INotifyPropertyChanged) {
            (dataItem as INotifyPropertyChanged).PropertyChanged -= OnDataSourceItemPropertyChanged;
            (dataItem as INotifyPropertyChanged).PropertyChanged += new PropertyChangedEventHandler(OnDataSourceItemPropertyChanged);
          }
        }
        total = values.Sum();
      }
    }

    void OnDataSourceItemPropertyChanged(object sender, PropertyChangedEventArgs e) {
      if (e.PropertyName == TitleMemberPath || e.PropertyName == ValueMemberPath) {
        ProcessData();
      }
    }

    private void ReallocateSlices() {
      if (values.Count > slices.Count) {
        AddSlices();
      } else if (values.Count < slices.Count) {
        RemoveSlices();
      }
      SetSliceData();
    }

    private void SetSliceData() {
      double runningTotal = 0;
      for (int i = 0; i < slices.Count; i++) {
        // title
        slices[i].Title = titles[i];
        SetSliceBrush(i);

        // angle
        ((RotateTransform)slices[i].RenderTransform).Angle = (total != 0 ? runningTotal / total * 360 : 360.0 / slices.Count * i);
        runningTotal += values[i];
      }
      UpdateLegend();
    }

    private void SetSliceBrush(int index) {
      List<Brush> brushes = this.brushes.Count > 0 ? this.brushes : presetBrushes;
      int brushCount = brushes.Count;
      slices[index].Brush = brushes[index % brushCount];
    }

    private void RemoveSlices() {
      for (int i = slices.Count - 1; i >= values.Count; i--) {
        RemoveSliceFromCanvas(slices[i]);
        slices.RemoveAt(i);
      }
    }

    private void RemoveSliceFromCanvas(Slice slice) {
      if (canvas != null && canvas.Children.Contains(slice)) {
        canvas.Children.Remove(slice);
      }
    }

    private void AddSlices() {
      for (int i = slices.Count; i < values.Count; i++) {
        Slice slice = new Slice();
        slice.RenderTransform = new RotateTransform();
        slice.RenderTransformOrigin = new Point(0, 0);
        slices.Add(slice);
        slice.ManipulationStarted += new EventHandler<ManipulationStartedEventArgs>(OnSliceManipulationStarted);
        AddSliceToCanvas(slice);
      }
    }

    void OnSliceManipulationStarted(object sender, ManipulationStartedEventArgs e) {
      GeneralTransform gt = (sender as Slice).TransformToVisual(canvasBorder);
      DisplayBalloon(sender as Slice, gt.Transform(e.ManipulationOrigin));
      //e.Handled = true;
      isSliceEvent = true;
    }

    /// <summary>
    /// Reacts to touch manipulations.
    /// </summary>
    /// <param name="e">Manipulation event arguments</param>
    protected override void OnManipulationStarted(ManipulationStartedEventArgs e) {
      if (!isSliceEvent) {
        HideBaloon();
      } else {
        isSliceEvent = false;
      }
    }

    private void SwitchLegend() {
      if (legend.Visibility == Visibility.Visible) {
        legend.Visibility = Visibility.Collapsed;
      } else {
        legend.Visibility = Visibility.Visible;
      }
    }

    private void AddSlicesToCanvas() {
      foreach (Slice slice in slices) {
        AddSliceToCanvas(slice);
      }
    }

    private void AddSliceToCanvas(Slice slice) {
      if (canvas != null && !canvas.Children.Contains(slice)) {
        canvas.Children.Add(slice);
      }
    }


    /// <summary>
    /// Applies control template.
    /// </summary>
    public override void OnApplyTemplate() {
      canvasBorder = (Border)TreeHelper.TemplateFindName("PART_CanvasBorder", this);
      canvasBorder.SizeChanged += new SizeChangedEventHandler(OnGraphCanvasDecoratorSizeChanged);
      canvas = (Canvas)TreeHelper.TemplateFindName("PART_SliceCanvas", this);
      balloon = (Balloon)TreeHelper.TemplateFindName("PART_Balloon", this);
      AddSlicesToCanvas();
      legend = (Legend)TreeHelper.TemplateFindName("PART_Legend", this);
      UpdateLegend();
    }

    private void UpdateLegend() {
      if (legend != null) {
        legend.LegendItemsSource = slices.Cast<ILegendItem>();
      }
    }

    void OnSlicesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
      UpdateLegend();
    }

    private void OnGraphCanvasDecoratorSizeChanged(object sender, SizeChangedEventArgs e) {
      RenderSlices();
    }


    private void RenderSlices() {
      if (values.Count != slices.Count) {
        ReallocateSlices();
      }
      ArrangeSlices();
      HideBaloon();
    }

    private void ArrangeSlices() {
      if (canvasBorder != null) {
        Point center = new Point(canvasBorder.ActualWidth / 2, canvasBorder.ActualHeight / 2);
        double radius = Math.Min(canvasBorder.ActualWidth, canvasBorder.ActualHeight) / 2;
        for (int i = 0; i < slices.Count; i++) {
          slices[i].SetDimensions(radius, (total != 0 ? values[i] / total : 1.0 / slices.Count));
          slices[i].SetValue(Canvas.LeftProperty, center.X);
          slices[i].SetValue(Canvas.TopProperty, center.Y);
        }
      }
    }

    private List<Brush> presetBrushes = new List<Brush>()
        {
            new SolidColorBrush(Color.FromArgb(0xFF, 0x3A, 0x0C, 0xD0)),
            new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0x0F, 0x00)),
            new SolidColorBrush(Color.FromArgb(0xFF, 0xB0, 0xDE, 0x09)),
            new SolidColorBrush(Color.FromArgb(0xFF, 0x33, 0x33, 0x33)),
            new SolidColorBrush(Color.FromArgb(0xFF, 0x99, 0x00, 0x00)),
            new SolidColorBrush(Color.FromArgb(0xFF, 0x0D, 0x52, 0xD1)),
            new SolidColorBrush(Color.FromArgb(0xFF, 0x0D, 0x8E, 0xCF)),
            new SolidColorBrush(Color.FromArgb(0xFF, 0x8A, 0x0C, 0xCF)),
            new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0x9E, 0x01)),
            new SolidColorBrush(Color.FromArgb(0xFF, 0xCD, 0x0D, 0x74)),
            new SolidColorBrush(Color.FromArgb(0xFF, 0x75, 0x4D, 0xEB)),
            new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0x66, 0x00)),
            new SolidColorBrush(Color.FromArgb(0xFF, 0x04, 0xD2, 0x15)),
            new SolidColorBrush(Color.FromArgb(0xFF, 0xF8, 0xFF, 0x01)),
            new SolidColorBrush(Color.FromArgb(0xFF, 0xFC, 0xD2, 0x02))
        };


    /// <summary>
    /// Gets a collection of preset brushes used for slices.
    /// </summary>
    public List<Brush> Brushes {
      get { return brushes; }
      set { throw new NotSupportedException(); }
    }

    private void DisplayBalloon(Slice slice, Point position) {
      balloon.Visibility = Visibility.Visible;
      balloon.Measure(new Size(canvasBorder.ActualWidth, canvasBorder.ActualHeight));
      double balloonLeft = position.X - balloon.DesiredSize.Width / 2;
      if (balloonLeft < 0) {
        balloonLeft = position.X;
      } else if (balloonLeft + balloon.DesiredSize.Width > canvasBorder.ActualWidth) {
        balloonLeft = position.X - balloon.DesiredSize.Width;
      }
      double balloonTop = position.Y - balloon.DesiredSize.Height - 5;
      if (balloonTop < 0) {
        balloonTop = position.Y + 17;
      }
      balloon.SetValue(Canvas.LeftProperty, balloonLeft);
      balloon.SetValue(Canvas.TopProperty, balloonTop);
      balloon.Text = slice.Title;
    }

    private void HideBaloon() {
      if (balloon != null) {
        balloon.Visibility = Visibility.Collapsed;
      }
    }
  }
}
