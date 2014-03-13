using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Igooana;
using IgooanaApp.ViewModels;

namespace IgooanaApp.Controls {
  public partial class DashboardVisitsByCountry : UserControl {

    public DashboardVisitsByCountry() {
      InitializeComponent();
      Loaded += DashboardVisitsByCountry_Loaded;
    }

    async void DashboardVisitsByCountry_Loaded(object sender, RoutedEventArgs e) {
      Query query = Query.For(AppState.Current.Profile.Id, AppState.Current.StartDate, AppState.Current.EndDate)
        .WithDimensions(Dimension.GeoNetwork.Country)
        .WithMetrics(Metric.Visitor.Visitors)
        //.Take(SHOW_ROWS_COUNT)
        .OrderByDescending(Metric.Visitor.Visitors);
      
      var result = await Api.Current.Execute(query);
      var viewModel = new DashboardVisitsByCountryViewModel(result.Values,result.Totals);
     
      LayoutRoot.DataContext = viewModel;
    }
  }
}
