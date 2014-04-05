using IgooanaApp.Core.Resources;
using System;

namespace IgooanaApp.Core.ViewModels {
  public interface IDashboardAcquisitionItem {
    int Value { get; }
    string Title { get; }

    string FontWeight { get; }
  }
  public class DashboardAcquisitionViewModelItem : IDashboardAcquisitionItem {

    public DashboardAcquisitionViewModelItem(dynamic gaRow, int totalVisits) {
      Visits = gaRow.Visits;
      VisitsPercentage = Visits / Convert.ToSingle(totalVisits) * 100;
      Source = gaRow.Source;
    }
    public int Visits { get; set; }
    public string Source { get; set; }
    public float VisitsPercentage { get; set; }

    public int Value { get { return Visits; } }

    public string Title {
      get { return string.Format("{0:N2}% {1}", VisitsPercentage, Source); }
    }


    public string FontWeight {
      get {
        return "Light";
      }
    }
  }

  public class DashboardAcquisitionTheRestViewModel : IDashboardAcquisitionItem {
    private readonly float theRestPercentage;
    public DashboardAcquisitionTheRestViewModel(int theRestTotalVisits, int totalVisits) {
      theRestPercentage = Convert.ToSingle(theRestTotalVisits) / totalVisits * 100;
      Value = theRestTotalVisits;
    }


    public int Value { get; set; }

    public string Title {
      get {
        return string.Format("{0:N2}% ({1})", theRestPercentage, L.TheRestCombined);
      }
    }

    public string FontWeight {
      get { return "Normal"; }
    }
  }
}
