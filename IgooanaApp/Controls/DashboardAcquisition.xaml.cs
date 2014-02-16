using Igooana;
using System;
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
      var sum = result.Values.Sum(x => x.Visits);
      var data = result.Values
        .Select(x=> new {Title = x.Source, Value = Convert.ToSingle(x.Visits), Percent = Convert.ToSingle(x.Visits)/sum})
        .Select(x => new {Title = string.Format("{0} ({1:P})", x.Title, x.Percent), Value = x.Value, Percent = x.Percent})
        .Where(x => x.Percent > .005)
        .OrderByDescending(x => x.Percent);
      AcquisitionChart.DataSource = data;
      ToggleLoading();
    }
  }
}
