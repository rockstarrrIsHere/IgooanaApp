using Igooana;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace IgooanaApp.Controls {
  public partial class DashboardAcquisition : UserControl {
    public DashboardAcquisition() {
      InitializeComponent();
      Loaded += OnLoaded;
    }

    //private void ToggleLoading() {
    //  ProgressBar.Visibility = ToggleVisibility(ProgressBar.Visibility);
    //  AcquisitionList.Visibility = ToggleVisibility(AcquisitionList.Visibility);
    //}

    private Visibility ToggleVisibility(Visibility visibility) {
      if (visibility == System.Windows.Visibility.Collapsed) {
        return System.Windows.Visibility.Visible;
      }
      return System.Windows.Visibility.Collapsed;
    }

    async void OnLoaded(object sender, RoutedEventArgs e) {
      var query = Query.For(AppState.Current.Profile.Id, AppState.Current.StartDate, AppState.Current.EndDate)
        .WithMetrics(Metric.Visits).WithDimensions(Dimension.TrafficSources.Source);
      var result = await Api.Current.Execute(query);
      var data = new ObservableCollection<object>(result.Values.Select(x => new { Source = x.Source, Visits = x.Visits }));
      AcquisitionChart.DataSource = new []{new {Source = "USA", Visits = 35}, new {Source = "Russia", Visits = 70}};

      //ToggleLoading();
    }
  }
}
