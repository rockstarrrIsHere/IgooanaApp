using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
namespace IgooanaApp.ViewModels {
  public class DashboardAcquisitionViewModel : ObservableCollection<IDashboardAcquisitionItem> {
    const int NumberOfItems = 10;
    public DashboardAcquisitionViewModel(IEnumerable<dynamic> gaRows) {
      var rows = gaRows.ToArray();
      var totalVisits = gaRows.Sum(x => x.Visits);
      foreach (var row in rows.OrderByDescending(x => x.Visits).Take(NumberOfItems)) {
        Add(new DashboardAcquisitionViewModelItem(row, totalVisits));
      }
      if (rows.Length > NumberOfItems) {
        int theRestTotalVisits = rows.Skip(NumberOfItems).Sum(x => x.Visits);
        Add(new DashboardAcquisitionTheRestViewModel(theRestTotalVisits, totalVisits));
      }
    }
  }
}
