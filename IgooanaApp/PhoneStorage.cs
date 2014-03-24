using Igooana;
using IgooanaApp.Resources;
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
        string token;
        IsolatedStorageSettings.ApplicationSettings.TryGetValue(RefreshTokenKey, out token);
        return token;
      }
      set {
        IsolatedStorageSettings.ApplicationSettings[RefreshTokenKey] = value;
      }
    }

    public static void ClearCredentials() {
      IsolatedStorageSettings.ApplicationSettings.Remove(AccessTokenKey);
      IsolatedStorageSettings.ApplicationSettings.Remove(RefreshTokenKey);
    }


    public static ICredentials Credentials {
      get {
        return new Credentials {
          AccessToken = AccessToken,
          RefreshToken = RefreshToken,
          ClientId = Env.ApiClientId,
          ClientSecret = Env.ApiClientSecret
        };
      }
      set {
        AccessToken = value.AccessToken;
        RefreshToken = value.RefreshToken;
      }
    }
  }
}
