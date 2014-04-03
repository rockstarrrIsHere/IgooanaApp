using Igooana;
using IgooanaApp.Core.Resources;
using IgooanaApp.WP8.Resources;
using Microsoft.Phone.Controls;
using System;
using System.Windows;

namespace IgooanaApp.WP8 {
  public partial class Auth : PhoneApplicationPage {
    private Api api;
    public Auth() {
      InitializeComponent();
    }
    private async void AuthLoaded(object sender, RoutedEventArgs e) {
      await ((App)(App.Current)).VerifyConnectivityToGoogle();
      if (PhoneStorage.AccessTokenExists) {
        Api.Restore(PhoneStorage.Credentials);
        Api.Current.AccessTokenRefreshed += (s, args) => PhoneStorage.AccessToken = args.AccessToken;
        NavigationService.Navigate(new Uri("/Profiles.xaml", UriKind.Relative));
      } else {
        StartAuthentication();
      }
    }

    private async void OnNavigating(object sender, NavigatingEventArgs e) {
      try {
        ICredentials credentials = await api.Authenticate(e.Uri);
        if (credentials.Authenticated) {
          PhoneStorage.Credentials = credentials;
          NavigationService.Navigate(new Uri("/Profiles.xaml", UriKind.Relative));
        }
      } catch (AccessDeniedException) {
        PhoneStorage.ClearCredentials();
        MessageBox.Show(L.OAuthUserConsentDenyMessage);
        Browser.Navigate(api.AuthenticateUri);
      } catch (Exception ex) {
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