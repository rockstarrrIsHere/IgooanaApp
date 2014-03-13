using IgooanaApp.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgooanaApp.ViewModels {
  public class DashboardVisitsByCountryViewModel {
    /// <summary>
    /// Show n top data rows 
    /// </summary>
    const int SHOW_ROWS_COUNT = 10;
    private int totalVisitsCount;
    private int restCountriesCount;
    private IEnumerable<dynamic> visitsByCountry= new List<string>();

    public int VisitsTotal {
      get {
        return totalVisitsCount;
      }
    }
    public IEnumerable<dynamic> VisitsByCountry { get { return visitsByCountry; } }
    public string CountriesOverall { 
      get {
        return restCountriesCount > 1 ? String.Format(Localization.VisitsByCountryCountriesOverallTemplate, restCountriesCount)
          : String.Empty;
        } 
    }

    public DashboardVisitsByCountryViewModel(IEnumerable<dynamic> gaRows, dynamic total) {
      try {
        var listOfVisits = gaRows.Take(SHOW_ROWS_COUNT).Select(x => new {Visitors = x.Visitors, Country = x.Country});
        totalVisitsCount = total.Visitors;
        restCountriesCount = gaRows.Count() - SHOW_ROWS_COUNT;
        
        visitsByCountry = listOfVisits.Select((item, index) => new {Count = item.Visitors, Text = String.Format(Localization.VisitsByCountryStringTemplate, item.Country), Number =  index + 1});
      }
      catch (Exception ex) {
        //TODO ex handling
        throw ex;
      }

    }



  }
}
