using Igooana;
using System.IO.IsolatedStorage;

namespace IgooanaApp {
  public static class PhoneStorage {
    const string AccessTokenKey = "AccessToken";
    const string RefreshTokenKey = "RefreshToken";
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
    public static string RefreshToken {
      get {
        return IsolatedStorageSettings.ApplicationSettings[RefreshTokenKey].ToString();
      }
      set {
        IsolatedStorageSettings.ApplicationSettings[RefreshTokenKey] = value;
      }
    }

    public static void ClearCredentials() {
      IsolatedStorageSettings.ApplicationSettings.Remove(AccessToken);
      IsolatedStorageSettings.ApplicationSettings.Remove(RefreshTokenKey);
    }


    public static ICredentials Credentials {
      get {
        return new Credentials{ AccessToken = AccessToken, RefreshToken = RefreshToken };
      }
      set {
        AccessToken = value.AccessToken;
        RefreshToken = value.RefreshToken;
      }
    }
  }
}
