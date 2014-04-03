using Igooana;
using System;

namespace IgooanaApp.Core {
  public sealed class AppState {
    private static readonly Lazy<AppState> lazy = new Lazy<AppState>(() => new AppState());
    private AppState(){
      // By default showing for last 30 days
      StartDate = DateTime.Now.AddDays(-30);
      EndDate = DateTime.Now;
    }

    public static AppState Current { get { return lazy.Value; } }

    public Profile Profile { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
  }
}
