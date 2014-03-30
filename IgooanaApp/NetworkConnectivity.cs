using System;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;

namespace IgooanaApp {
  public static class NetworkConnectivity {
    public static async Task<bool> IsOnline() {
      var googleHost = new HostName("google.com");
      var socket = new StreamSocket();
      try {
        await socket.ConnectAsync(googleHost, "http");
        return socket.Information.RemoteAddress != null;
      } catch {
        return false;
      }
    }
  }
}
