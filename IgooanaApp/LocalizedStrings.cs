using IgooanaApp.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace IgooanaApp {
  public class LocalizedStrings {
    private static Localization _localization = new Localization();

    public Localization LocalizedResources { get { return _localization; } }

    /// <summary>
    /// Searches for appropriate plural or single form of text template in localization resource file
    /// </summary>
    public static string PluralizedTemplate(string templateName, int count) {
      ResourceManager rm = new ResourceManager(typeof(Localization));
      return rm.GetString(templateName + (count > 1 ? "Plural" : "Single"));
    }
  }
}
