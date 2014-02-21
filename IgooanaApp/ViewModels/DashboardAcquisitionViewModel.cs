using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
namespace IgooanaApp.ViewModels {
  public class DashboardAcquisitionViewModel : ObservableCollection<IDashboardAcquisitionItem> {
    const float MinimumSlicePercent = .01f;
    public DashboardAcquisitionViewModel(IEnumerable<dynamic> gaRows) {
      var totalVisits = gaRows.Sum(x => x.Visits);
      foreach (var row in gaRows.OrderByDescending(x => x.Visits).TakeWhile(x => Convert.ToSingle(x.Visits) / totalVisits > MinimumSlicePercent)) {
        Add(new DashboardAcquisitionViewModelItem(row, totalVisits));
      }
      var theRest = gaRows.OrderByDescending(x => x.Visits).SkipWhile(x => Convert.ToSingle(x.Visits) / totalVisits > MinimumSlicePercent);
      if (theRest.Any()) {
        int theRestTotalVisits = theRest.Sum(x => x.Visits);
        Add(new DashboardAcquisitionTheRestViewModel(theRestTotalVisits, totalVisits));
      }
    }

    public string CenterText {
      get {
        return Count > 10 ? "10+" : Count.ToString();
      }
    }

    public string CenterTextDescription {
      get {
        return Count > 1 ? "Sources" : "Source";
      }
    }
  }
}
