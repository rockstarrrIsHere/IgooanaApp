using IgooanaApp.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace IgooanaApp.Core {
  public class LocalizedStrings {
    private static L _localization = new L();

    public L LocalizedResources { get { return _localization; } }

    /// <summary>
    /// Searches for appropriate plural or single form of text template in localization resource file
    /// </summary>
    public static string PluralizedTemplate(string templateName, int count) {
      ResourceManager rm = new ResourceManager(typeof(L));
      return rm.GetString(templateName + (count > 1 ? "Plural" : "Single"));
    }
  }
}
