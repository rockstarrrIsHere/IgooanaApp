using Igooana;
using IgooanaApp.Core;
using IgooanaApp.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace IgooanaApp.WP8.Controls {
  public partial class DashboardVisitsByCountry : UserControl {
    const int visitsCountLetterWidth = 16;
    const int startCountOverallTextLevel = 2;

    public DashboardVisitsByCountry() {
      InitializeComponent();
      Loaded += DashboardVisitsByCountry_Loaded;
    }
    private void ToggleLoading(bool hasData) {
      ProgressBar.Visibility = ViewHelper.ToggleVisibility(ProgressBar.Visibility);
      VisitsLayout.Visibility = ViewHelper.ToggleVisibility(VisitsLayout.Visibility);
      if (hasData) {
        NoDataText.Visibility = ViewHelper.ToggleVisibility(NoDataText.Visibility);
        CountriesListPanel.Visibility = ViewHelper.ToggleVisibility(CountriesListPanel.Visibility);
      }
    }

    private void ToggleIfNoData() {

    }

    async void DashboardVisitsByCountry_Loaded(object sender, RoutedEventArgs e) {
      Query query = Query.For(AppState.Current.Profile.Id, AppState.Current.StartDate, AppState.Current.EndDate)
        .WithDimensions(Dimension.GeoNetwork.Country)
        .WithMetrics(Metric.Visitor.Visitors)
        //.Take(SHOW_ROWS_COUNT)
        .OrderByDescending(Metric.Visitor.Visitors);

      var result = await Api.Current.Execute(query);
      var viewModel = new DashboardVisitsByCountryViewModel(result.Values, result.Totals);

      DataContext = viewModel;

      ToggleLoading(viewModel.HasData);
    }
  }
}
