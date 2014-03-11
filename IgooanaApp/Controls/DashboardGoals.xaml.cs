using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace IgooanaApp.Controls {
  public partial class DashboardGoals : UserControl {
    private Random r = new Random();
    public DashboardGoals() {
      InitializeComponent();
      Loaded += DashboardGoals_Loaded;
    }

    void DashboardGoals_Loaded(object sender, RoutedEventArgs e) {
      for (int i = 1; i < 31; i++) {
        _data.Add(new TestDataItem(r) { X = i.ToString() });
      }
      this.DataContext = this;
    }
    private ObservableCollection<TestDataItem> _data = new ObservableCollection<TestDataItem>();

    public ObservableCollection<TestDataItem> Data { get { return _data; } }


  }
  public class TestDataItem {
    private readonly string y;
    public TestDataItem(Random r) {
      y = r.Next(0, 50).ToString();
    }
    public string X { get; set; }
    public string Y { get { return y; } }
  }
}
