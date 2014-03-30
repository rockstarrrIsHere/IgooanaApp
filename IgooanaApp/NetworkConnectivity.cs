using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;

namespace IgooanaApp {
  public static class NetworkConnectivity {
    const int TimeoutMilliseconds = 10000;
    public static async Task<bool> IsOnline() {
      var googleHost = new HostName("google.com");
      var socket = new StreamSocket();
      try {
        var cts = new CancellationTokenSource(TimeoutMilliseconds);
        await socket.ConnectAsync(googleHost, "http").AsTask(cts.Token);
        return socket.Information.RemoteAddress != null;
      } catch {
        return false;
      }
    }
  }
}
