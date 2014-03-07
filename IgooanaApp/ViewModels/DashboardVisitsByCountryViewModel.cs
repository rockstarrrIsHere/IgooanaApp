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
    private IEnumerable<string> visitsByCountry= new List<string>();

    public string VisitsTotal {
      get {
        return String.Format(Resources.Env.VisitsByCountryTotalStringTemplate, totalVisitsCount);
      }
    }
    public IEnumerable<string> VisitsByCountry { get { return visitsByCountry; } }
    public string CountriesOverall { 
      get {
        return restCountriesCount > 1 ? String.Format(Resources.Env.VisitsByCountryCountriesOverallTemplate, restCountriesCount)
          : String.Empty;
        } 
    }

    public DashboardVisitsByCountryViewModel(IEnumerable<dynamic> gaRows, dynamic total) {
      try {
        var listOfVisits = gaRows.Take(SHOW_ROWS_COUNT).Select(x => new {Visitors = (int)x.Visitors, Country = (string)x.Country});
        totalVisitsCount = total.Visitors;
        restCountriesCount = gaRows.Count() - SHOW_ROWS_COUNT;

        visitsByCountry = listOfVisits.Select((item, index) => String.Format(Resources.Env.VisitsByCountryStringTemplate, index + 1, item.Visitors, item.Country));
      }
      catch (Exception ex) {
        //TODO ex handling
        throw ex;
      }

    }



  }
}
