using System;
using Igooana.Extensions;

namespace IgooanaApp.WP8 {
  public enum ImageFormat { JPG, PNG }
  public static class MultiResImage {
    /// <summary>
    /// Gets the full path to the image asset, suitable for current screen resolution
    /// </summary>
    /// <param name="path">Path to image asset, relative to app root</param>
    /// <returns></returns>
    public static Uri GetUri(string path, ImageFormat imageFormat = ImageFormat.JPG) {
      switch (ResolutionHelper.CurrentResolution) {
        case Resolutions.HD:
          return new Uri("{0}.Screen-720p.{1}".FormatWith(path, imageFormat.ToString().ToLowerInvariant()), UriKind.Relative);
        case Resolutions.WXGA:
          return new Uri("{0}.Screen-WXGA.{1}".FormatWith(path, imageFormat.ToString().ToLowerInvariant()), UriKind.Relative);
        case Resolutions.WVGA:
          return new Uri("{0}.Screen-WVGA.{1}".FormatWith(path, imageFormat.ToString().ToLowerInvariant()), UriKind.Relative);
        default:
          throw new InvalidOperationException("Unknown resolution type");
      }
    }
  }
}
