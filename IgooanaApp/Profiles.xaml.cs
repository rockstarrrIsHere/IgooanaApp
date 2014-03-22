using Igooana;
using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace IgooanaApp {
  public partial class Profiles : PhoneApplicationPage {
    public Profiles() {
      InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigationEventArgs e) {
      base.OnNavigatedTo(e);
      try {
        profilesList.ItemsSource = await Api.Current.Management.GetProfilesAsync();
      }
      catch (TokenRevokedException) {
        MessageBox.Show("You revoked the token and need to relogin");
        PhoneStorage.ClearCredentials();
        NavigationService.Navigate(new Uri("/Auth.xaml", UriKind.Relative));
      }
      catch (ConnectionException ex) {
        MessageBox.Show(ex.Message);
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message);
      }
    }

    private void OnProfileSelected(object sender, SelectionChangedEventArgs e) {
      Profile profile = e.AddedItems[0] as Profile;
      if (profile != null) {
        AppState.Current.Profile = profile;
        NavigationService.Navigate(new Uri("/Dashboard.xaml", UriKind.Relative));
      }
    }
  }
}