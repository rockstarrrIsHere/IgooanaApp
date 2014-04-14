using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace IgooanaApp.WP8 {
  public partial class NoConnection : PhoneApplicationPage {
    public NoConnection() {
      InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e) {
      NavigationService.GoBack();
    }
  }
}
