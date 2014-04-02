using Igooana;
using IgooanaApp.Core;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace IgooanaApp.WP8.Controls {
  public partial class DashboardGoals : UserControl {
    private Random r = new Random();
    public DashboardGoals() {
      InitializeComponent();
      Loaded += OnLoaded;
    }

    async void OnLoaded(object sender, RoutedEventArgs e) {
      var query = Query.For(AppState.Current.Profile.Id, AppState.Current.StartDate, AppState.Current.EndDate)
        .WithMetrics(Metric.GoalConversions.GoalCompletionsAll)
        .WithDimensions(Dimension.Time.Date);
      var results = await Api.Current.Execute(query);
      GoalsChart.DataSource = results.Values.Select(x => new { Date = x.Date, GoalConversions = x.GoalCompletionsAll });
    }
  }
}
