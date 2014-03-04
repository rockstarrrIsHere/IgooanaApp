﻿using Igooana;
using System;

namespace IgooanaApp {
  internal sealed class AppState {
    private static readonly Lazy<AppState> lazy = new Lazy<AppState>(() => new AppState());
    private AppState(){
      // By default showing for last 30 days
      StartDate = new DateTime(2014, 2, 23);
      EndDate = new DateTime(2014, 2, 23);
    }

    internal static AppState Current { get { return lazy.Value; } }

    internal Profile Profile { get; set; }
    internal DateTime StartDate { get; set; }
    internal DateTime EndDate { get; set; }
  }
}