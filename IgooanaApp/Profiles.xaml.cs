using Igooana;
using Microsoft.Phone.Controls;
using System;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace IgooanaApp {
  public partial class Profiles : PhoneApplicationPage {
    public Profiles() {
      InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigationEventArgs e) {
      base.OnNavigatedTo(e);
      profilesList.ItemsSource = await Api.Current.Management.GetProfilesAsync();
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