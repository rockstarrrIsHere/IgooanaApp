using Igooana;
using IgooanaApp.Resources;
using Microsoft.Phone.Controls;
using System;
using System.Windows;

namespace IgooanaApp {
  public partial class Auth : PhoneApplicationPage {
    private Api api;
    public Auth() {
      InitializeComponent();
    }
    private void AuthLoaded(object sender, RoutedEventArgs e) {
      api = new Api(Env.ApiClientId, Env.ApiClientSecret);
      Browser.Navigate(api.AuthenticateUri);
    }

    private async void OnNavigating(object sender, NavigatingEventArgs e) {
      try {
        if (await api.Authenticate(e.Uri)) {
          NavigationService.Navigate(new Uri("/Profiles.xaml", UriKind.Relative));
        }
      }
      catch (AccessRefusedException) {
        MessageBox.Show("You must allow accessing your information in order to use the application");
      }
    }
  }
}