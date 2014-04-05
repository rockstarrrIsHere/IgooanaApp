using IgooanaApp.Core.ViewModels;
using System;
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
      var viewModel = new DashboardGoalsViewModel { Busy = true };
      DataContext = viewModel;
      await viewModel.InitAsync();
      //GoalsChart.DataSource = results.Values.Select(x => new { Date = x.Date, GoalConversions = x.GoalCompletionsAll });
    }
  }
}
