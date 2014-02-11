using Igooana;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace IgooanaApp.Controls {
  public partial class DashboardAcquisition : UserControl {
    public DashboardAcquisition() {
      InitializeComponent();
      Loaded += OnLoaded;
    }

    async void OnLoaded(object sender, RoutedEventArgs e) {
      var query = Query.For(AppState.Current.Profile.Id, AppState.Current.StartDate, AppState.Current.EndDate)
        .WithMetrics(Metric.Visits).WithDimensions(Dimension.TrafficSources.Source);
      var result = await Api.Current.Execute(query);
      AcquisitionList.ItemsSource = result.Values.Select(x=>(dynamic)x).Select(x => new { Source = x.Source, Visits = x.Visits });
    }
  }
}
