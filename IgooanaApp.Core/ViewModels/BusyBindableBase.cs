using System;
using System.Threading.Tasks;

namespace IgooanaApp.Core.ViewModels {
  public abstract class BusyBindableBase : BindableBase {
    private bool busy;
    public bool Busy {
      get { return busy; }
      set {
        SetProperty(ref busy, value);
      }
    }


    public async Task BeBusyWithAsync(Func<Task> func) {
      Busy = true;
      try {
        await func();
      } finally {
        Busy = false;
      }
    }
  }
}
