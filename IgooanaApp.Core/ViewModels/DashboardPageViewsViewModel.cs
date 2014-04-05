using Igooana;
using IgooanaApp.Core.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IgooanaApp.Core.ViewModels {
  public class DashboardPageViewsViewModel : ViewModelBase {
    public string ControlTitle { get { return L.Behavior; } }
    public string HeaderTitle { get { return L.PageViews; } }

    private int pageViews;
    public int PageViews {
      get { return pageViews; }
      set {
        SetProperty(ref pageViews, value);
      }
    }

    public async Task<IEnumerable<dynamic>> InitAsync() {
      var query = Query.For(AppState.Current.Profile.Id, AppState.Current.StartDate, AppState.Current.EndDate)
        .WithDimensions(Dimension.Time.DayOfWeek + Dimension.Time.Hour).WithMetrics(Metric.PageTracking.Pageviews);
      var result = await Api.Current.Execute(query);
      PageViews = result.Totals.Pageviews;
      Busy = false;
      return result.Values;
    }
  }
}
