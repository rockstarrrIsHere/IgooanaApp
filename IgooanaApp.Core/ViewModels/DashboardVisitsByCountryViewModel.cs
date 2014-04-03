using IgooanaApp.Core;
using IgooanaApp.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IgooanaApp.ViewModels {
  public class DashboardVisitsByCountryViewModel {
    /// <summary>
    /// Show n top data rows 
    /// </summary>
    const int ShowRowsCount = 10;
    const int CharPxLength = 16;
    const int StartCountRsstTextLevel = 2;
    private int totalVisitsCount;
    private int restCountriesCount;
    private IEnumerable<dynamic> visitsByCountry = new List<string>();

    public int VisitsTotal {
      get {
        return totalVisitsCount;
      }
    }
    public IEnumerable<dynamic> VisitsByCountry { get { return visitsByCountry; } }
    public string CountriesRest {
      get {
        return restCountriesCount > 1 ? String.Format(LocalizedStrings.PluralizedTemplate("VisitsByCountryCountriesRestTemplate", restCountriesCount), restCountriesCount)
          : String.Empty;
      }
    }

    public bool HasData { get { return visitsByCountry.Any(); } }

    public int PaddingCountriesOverall { get; private set; }

    public DashboardVisitsByCountryViewModel(IEnumerable<dynamic> gaRows, dynamic total) {
      try {
        restCountriesCount = gaRows.Count() - ShowRowsCount;
        var listOfVisits = gaRows.Take(ShowRowsCount).Select(x => new { Visitors = x.Visitors, Country = x.Country });
        totalVisitsCount = total.Visitors;

        int maxDigitsCount = (gaRows.First().Visitors).ToString("N0").Length;
        visitsByCountry = listOfVisits.Select((item, index) => new {
          Count = item.Visitors.ToString("N0"),
          Text = String.Format(L.VisitsByCountryStringTemplate, item.Country),
          Number = index + 1,
          CountFieldWidth = (maxDigitsCount) * CharPxLength
        });

        //OtherCountriesItemMargin should be Bullet width + Count margin
        PaddingCountriesOverall = CharPxLength * (maxDigitsCount > StartCountRsstTextLevel ? maxDigitsCount - StartCountRsstTextLevel : 0);
      } catch (Exception ex) {
        //TODO ex handling
        throw ex;
      }
    }
  }
}
