using System;
using System.Threading.Tasks;

namespace IgooanaApp.Core.ViewModels {
  public abstract class ViewModelBase : BindableBase {
    private bool busy;
    public bool Busy {
      get { return busy; }
      set {
        SetProperty(ref busy, value);
      }
    }
  }
}
