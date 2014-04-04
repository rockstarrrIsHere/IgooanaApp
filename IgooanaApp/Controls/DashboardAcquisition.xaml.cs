﻿using IgooanaApp.Core.ViewModels;
using System;

namespace IgooanaApp.WP8.Controls {
  public partial class DashboardAcquisition {
    public DashboardAcquisition() {
      InitializeComponent();
      Loaded += OnLoaded;
    }

    private async void OnLoaded(object sender, EventArgs e) {
      var viewModel = new DashboardAcquisitionViewModel();
      await viewModel.InitAsync();
      DataContext = viewModel;
    }
  }
}
