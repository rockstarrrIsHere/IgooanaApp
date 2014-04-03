using Igooana;
using IgooanaApp.Core.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace IgooanaApp.WP8.Controls {
  public partial class DashboardAcquisition : UserControl {
    public DashboardAcquisition() {
      InitializeComponent();
      DataContext = new DashboardAcquisitionViewModel(); 
    }

    async void OnLoaded(object sender, RoutedEventArgs e) {
      await ((DashboardAcquisitionViewModel)DataContext).InitAsync();
    }
  }
}
