using Igooana;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace IgooanaApp.WP8.Controls {
  public partial class DashboardPageViews : UserControl {
    public DashboardPageViews() {
      InitializeComponent();
      Loaded += DashboardPageViews_Loaded;
    }

    async void DashboardPageViews_Loaded(object sender, RoutedEventArgs e) {
      //var query = Query.For(AppState.Current.Profile.Id, AppState.Current.StartDate, AppState.Current.EndDate)
      //  .WithDimensions(Dimension.Time.DayOfWeek + Dimension.Time.Hour).WithMetrics(Metric.PageTracking.Pageviews);
      //var result = await Api.Current.Execute(query);
      //var color = (Color)Application.Current.Resources[App.AccentBackgroundColor];
      //var viewModel = new DashboardPageviewsViewModel(result.Values, color);
      //foreach (var cell in viewModel.Cells) {
      //  FrameworkElement fe = cell.Content;
      //  Grid.SetRow(fe, cell.GridRow);
      //  Grid.SetColumn(fe, cell.GridColumn);
      //  LayoutRoot.Children.Add(fe);
      //}
    }
  }
}
