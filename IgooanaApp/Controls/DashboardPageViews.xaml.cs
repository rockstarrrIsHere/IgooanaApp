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
using System.Windows.Media;

namespace IgooanaApp.Controls {
  public partial class DashboardPageViews : UserControl {
    public DashboardPageViews() {
      InitializeComponent();
      Loaded += DashboardPageViews_Loaded;
    }

    async void DashboardPageViews_Loaded(object sender, RoutedEventArgs e) {
      var query = Query.For(AppState.Current.Profile.Id, AppState.Current.StartDate, AppState.Current.EndDate)
        .WithDimensions(Dimension.Time.DayOfWeek + Dimension.Time.Hour).WithMetrics(Metric.PageTracking.Pageviews);
      var result = await Api.Current.Execute(query);
      var viewModel = new DashboardPageviewsViewModel(result.Values, Color.FromArgb(255, 8, 146, 209));
      foreach (var cell in viewModel.Cells) {
        FrameworkElement fe = cell.Content;
        Grid.SetRow(fe, cell.GridRow);
        Grid.SetColumn(fe, cell.GridColumn);
        LayoutRoot.Children.Add(fe);
      }
    }
  }
}
