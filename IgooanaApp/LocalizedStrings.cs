using IgooanaApp.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgooanaApp {
  public class LocalizedStrings {
    private static Localization _localization = new Localization();

    public Localization LocalizedResources { get { return _localization; } }
  }
}
