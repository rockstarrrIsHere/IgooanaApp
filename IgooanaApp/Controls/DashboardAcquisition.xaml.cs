using Igooana;
using IgooanaApp.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace IgooanaApp.Controls {
  public partial class DashboardAcquisition : UserControl {
    public DashboardAcquisition() {
      InitializeComponent();
      Loaded += OnLoaded;
    }

    private void ToggleLoading() {
      ProgressBar.Visibility = ToggleVisibility(ProgressBar.Visibility);
      AcquisitionChart.Visibility = ToggleVisibility(AcquisitionChart.Visibility);
    }

    private Visibility ToggleVisibility(Visibility visibility) {
      if (visibility == System.Windows.Visibility.Collapsed) {
        return System.Windows.Visibility.Visible;
      }
      return System.Windows.Visibility.Collapsed;
    }

    async void OnLoaded(object sender, RoutedEventArgs e) {
      var query = Query.For(AppState.Current.Profile.Id, AppState.Current.StartDate, AppState.Current.EndDate)
        .WithMetrics(Metric.Session.Visits).WithDimensions(Dimension.TrafficSources.Source);
      var result = await Api.Current.Execute(query);
      AcquisitionChart.DataSource = new DashboardAcquisitionViewModel(result.Values);
      AcquisitionChart.DataContext = AcquisitionChart.DataSource;
      ToggleLoading();
    }
  }
}
