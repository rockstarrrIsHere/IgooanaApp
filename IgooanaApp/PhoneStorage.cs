using System.IO.IsolatedStorage;

namespace IgooanaApp {
  public static class PhoneStorage {
    const string AccessTokenKey = "AccessToken";
    public static bool AccessTokenExists {
      get { return IsolatedStorageSettings.ApplicationSettings.Contains(AccessTokenKey); }
    }

    public static string AccessToken {
      get {
        return IsolatedStorageSettings.ApplicationSettings[AccessTokenKey].ToString();
      }
      set {
        IsolatedStorageSettings.ApplicationSettings[AccessTokenKey] = value;
      }
    }

    public static void ClearCredentials() {
      IsolatedStorageSettings.ApplicationSettings.Remove(AccessToken);
    }
  }
}
