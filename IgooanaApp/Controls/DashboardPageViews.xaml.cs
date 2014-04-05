using IgooanaApp.Controls;
using IgooanaApp.Core.ViewModels;
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
      var viewModel = new DashboardPageViewsViewModel { Busy = true };
      DataContext = viewModel;
      var data = await viewModel.InitAsync();
      var color = (Color)Application.Current.Resources[App.AccentBackgroundColor];
      var cells = new PageViewsGridCells(data, color);
      foreach (var cell in cells.Cells) {
        FrameworkElement fe = cell.Content;
        Grid.SetRow(fe, cell.GridRow);
        Grid.SetColumn(fe, cell.GridColumn);
        Cells.Children.Add(fe);
      }
    }
  }
}
