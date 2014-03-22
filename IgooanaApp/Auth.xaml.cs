using Igooana;
using IgooanaApp.Resources;
using Microsoft.Phone.Controls;
using System;
using System.Diagnostics;
using System.Windows;

namespace IgooanaApp {
  public partial class Auth : PhoneApplicationPage {
    private Api api;
    public Auth() {
      InitializeComponent();
    }
    private async void AuthLoaded(object sender, RoutedEventArgs e) {
      if (PhoneStorage.AccessTokenExists) {
        try {
          Api.Restore(PhoneStorage.AccessToken);
          NavigationService.Navigate(new Uri("/Profiles.xaml", UriKind.Relative));
        }
        catch (UnauthorizedException) {
          //token was revoked
          StartAuthentication();
        }
      }
      else {
        StartAuthentication();
      }
    }

    private async void OnNavigating(object sender, NavigatingEventArgs e) {
      try {
        AuthResponse response = await api.Authenticate(e.Uri);
        if (response.IsAuthenticated) {
          PhoneStorage.AccessToken = response.AccessToken;
          NavigationService.Navigate(new Uri("/Profiles.xaml", UriKind.Relative));
        }
      }
      catch (AccessRefusedException) {
        PhoneStorage.ClearCredentials();
        MessageBox.Show(Localization.OAuthUserConsentDenyMessage);
        Browser.Navigate(api.AuthenticateUri);
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message);
      }
    }

    private void OnNavigated(object sender, System.Windows.Navigation.NavigationEventArgs e) {
      BackgroundGrid.Visibility = Visibility.Collapsed;
      BrowserGrid.Visibility = Visibility.Visible;
    }

    private void StartAuthentication() {
      api = new Api(Env.ApiClientId, Env.ApiClientSecret);
      Browser.Navigate(api.AuthenticateUri);
    }
  }
}