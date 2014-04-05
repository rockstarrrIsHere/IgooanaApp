using Igooana;
using IgooanaApp.Core.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IgooanaApp.Core.ViewModels {
  public class DashboardGoalsViewModel : ViewModelBase {
    public string ControlTitle { get { return L.GoalCompletion; } }

    private int goals;
    public int Goals {
      get { return goals; }
      set {
        SetProperty(ref goals, value);
      }
    }

    private IEnumerable<dynamic> data;
    public IEnumerable<dynamic> Data {
      get { return data; }
      set { SetProperty(ref data, value); }
    }

    public async Task InitAsync() {
      var query = Query.For(AppState.Current.Profile.Id, AppState.Current.StartDate, AppState.Current.EndDate)
        .WithMetrics(Metric.GoalConversions.GoalCompletionsAll)
        .WithDimensions(Dimension.Time.Date);
      var results = await Api.Current.Execute(query);
      Data = results.Values;
      Goals = results.Totals.GoalCompletionsAll;
      Busy = false;
    }
  }
}
