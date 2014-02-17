using System;

namespace IgooanaApp.ViewModels {
  public interface IDashboardAcquisitionItem {
    int Value { get; }
    string Title { get; }
  }
  public class DashboardAcquisitionViewModelItem : IDashboardAcquisitionItem{

    public DashboardAcquisitionViewModelItem(dynamic gaRow, int totalVisits) {
      Visits = gaRow.Visits;
      VisitsPercentage = Visits / Convert.ToSingle(totalVisits);
      Source = gaRow.Source;
    }
    public int Visits { get; set; }
    public string Source { get; set; }
    public float VisitsPercentage { get; set; }

    public int Value { get { return Visits; } }

    public string Title {
      get {
        return string.Format("{0:P} {1}", VisitsPercentage, Source);
      }
    }
  }

  public class DashboardAcquisitionTheRestViewModel : IDashboardAcquisitionItem {
    private readonly float theRestPercentage;
    public DashboardAcquisitionTheRestViewModel(int theRestTotalVisits, int totalVisits) {
      theRestPercentage = Convert.ToSingle(theRestTotalVisits) / totalVisits;
      Value = theRestTotalVisits;
    }


    public int Value { get; set; }

    public string Title {
      get {
        //TODO: localize
        return string.Format("{0:P} {1}", theRestPercentage, "(the rest combined)");
      }
    }
  }
}
