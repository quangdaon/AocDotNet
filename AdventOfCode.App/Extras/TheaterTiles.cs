using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using AdventOfCode.App.Challenges;
using AdventOfCode.App.Core;
using AdventOfCode.App.Utilities;
using Color = System.Drawing.Color;

namespace AdventOfCode.App.Extras;

[ExtraCommand("theater-tiles")]
[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
public class TheaterTiles : IExtraCommand
{
  private const double SCALE = 100D;
  private readonly string _outputPath;

  public TheaterTiles()
  {
    var outputBase = Environment.GetEnvironmentVariable("AOC_OUTPUT_PATH") ?? "";
    _outputPath = Path.Combine(outputBase, "theater");
  }

  public Task Execute()
  {
    var input = InputReader.LoadInput(9, 2025);
    var coordinates = input.ToRows().Select(Aoc2025Day09Processor.ParseCoordinates).ToArray();

    var last = coordinates.Last();
    var bmp = new Bitmap(1000, 1000);

    foreach (var c in coordinates)
    {
      var diffX = c.X - last.X;
      var diffY = c.Y - last.Y;

      if (diffX != 0 && diffY != 0) throw new Exception("FUCK");

      if (diffX != 0)
      {
        var start = Math.Min(last.X, c.X);
        var end = Math.Max(last.X, c.X);
        for (var x = start; x < end; x++)
        {
          bmp.SetPixel(Convert.ToInt32(x / SCALE), Convert.ToInt32(c.Y / SCALE), Color.White);
        }
      }

      if (diffY != 0)
      {
        var start = Math.Min(last.Y, c.Y);
        var end = Math.Max(last.Y, c.Y);
        for (var y = start; y < end; y++)
        {
          bmp.SetPixel(Convert.ToInt32(c.X / SCALE), Convert.ToInt32(y / SCALE), Color.Red);
        }
      }

      last = c;
    }


    var pairs = Aoc2025Day09Processor.CreatePairs(coordinates);
    var segments = pairs.Where(e => e.IsSegment).ToArray();

    var sortedPairs = pairs.Where(p => Aoc2025Day09Processor.IsInBound(p, segments)).OrderBy(p => p.Area).ToArray();
    var rect = sortedPairs.MaxBy(e => e.Area);

    var minX = Math.Min(rect.Coordinates1.X, rect.Coordinates2.X);
    var maxX = Math.Max(rect.Coordinates1.X, rect.Coordinates2.X);
    var minY = Math.Min(rect.Coordinates1.Y, rect.Coordinates2.Y);
    var maxY = Math.Max(rect.Coordinates1.Y, rect.Coordinates2.Y);


    for (var x = minX; x < maxX; x++)
    {
      bmp.SetPixel(Convert.ToInt32(x / SCALE), Convert.ToInt32(minY / SCALE), Color.Green);
      bmp.SetPixel(Convert.ToInt32(x / SCALE), Convert.ToInt32(maxY / SCALE), Color.Green);
    }
    
    for (var y = minY; y < maxY; y++)
    {
      bmp.SetPixel(Convert.ToInt32(minX / SCALE), Convert.ToInt32(y / SCALE), Color.Green);
      bmp.SetPixel(Convert.ToInt32(maxX / SCALE), Convert.ToInt32(y / SCALE), Color.Green);
    }

    bmp.Save(Path.Combine(_outputPath, "img.bmp"), ImageFormat.Bmp);

    return Task.CompletedTask;
  }
}
