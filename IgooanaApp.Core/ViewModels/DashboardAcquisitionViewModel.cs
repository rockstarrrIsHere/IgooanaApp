using Igooana;
using IgooanaApp.Core.Resources;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
namespace IgooanaApp.Core.ViewModels {
  public class DashboardAcquisitionViewModel : BusyBindableBase {
    const float MinimumSlicePercent = .005f;
    private int visitsTotal;
    private readonly ObservableCollection<IDashboardAcquisitionItem> data;

    public DashboardAcquisitionViewModel() {
      data = new ObservableCollection<IDashboardAcquisitionItem>();
    }


    public string HeaderTitle {
      get { return L.Visits; }
    }

    public int VisitsTotal {
      get { return visitsTotal; }
      set { SetProperty(ref visitsTotal, value); }
    }

    public string CenterText {
      get {
        return data.Count > 10 ? "10+" : data.Count.ToString();
      }
    }

    public string CenterTextDescription {
      get {
        //TODO: localize
        return data.Count > 1 ? "Sources" : "Source";
      }
    }

    public ObservableCollection<IDashboardAcquisitionItem> Data { get { return data; } }

    public async Task InitAsync() {
      await BeBusyWithAsync(async () => {
        var query = Query.For(AppState.Current.Profile.Id, AppState.Current.StartDate, AppState.Current.EndDate)
          .WithMetrics(Metric.Session.Visits).WithDimensions(Dimension.TrafficSources.Source);
        var result = await Api.Current.Execute(query);
        var total = result.Totals.Visits;
        foreach (var row in result.Values.OrderByDescending(x => x.Visits).TakeWhile(x => Convert.ToSingle(x.Visits) / total > MinimumSlicePercent)) {
          data.Add(new DashboardAcquisitionViewModelItem(row, total));
        }
        var theRest = result.Values.OrderByDescending(x => x.Visits).SkipWhile(x => Convert.ToSingle(x.Visits) / total > MinimumSlicePercent);
        if (theRest.Any()) {
          int theRestTotalVisits = theRest.Sum(x => x.Visits);
          data.Add(new DashboardAcquisitionTheRestViewModel(theRestTotalVisits, total));
        }
        VisitsTotal = total;
      });
    }
  }
}
