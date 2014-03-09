﻿using System;

namespace IgooanaApp.Charts {
  /// <summary>
  /// Represents arguments for event raised when Path related properties change.
  /// </summary>
  public class DataPathEventArgs : EventArgs {
    /// <summary>
    /// Instantiates class with specified new path.
    /// </summary>
    /// <param name="newPath"></param>
    public DataPathEventArgs(string newPath) {
      NewPath = newPath;
    }

    /// <summary>
    /// Gets or sets new path.
    /// </summary>
    public string NewPath { get; set; }
  }
}
