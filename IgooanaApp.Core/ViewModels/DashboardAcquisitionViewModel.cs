using Igooana;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
namespace IgooanaApp.Core.ViewModels {
  public class DashboardAcquisitionViewModel : ObservableCollection<IDashboardAcquisitionItem> {
    const float MinimumSlicePercent = .005f;
    public async Task InitAsync() {
      var query = Query.For(AppState.Current.Profile.Id, AppState.Current.StartDate, AppState.Current.EndDate)
        .WithMetrics(Metric.Session.Visits).WithDimensions(Dimension.TrafficSources.Source);
      var result = await Api.Current.Execute(query);
      var gaRows = result.Values;
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

    public bool ControlVisibility {
      get {
        return true;
      }
    }
  }
}
