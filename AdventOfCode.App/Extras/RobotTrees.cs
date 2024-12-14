using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using AdventOfCode.App.Challenges;
using AdventOfCode.App.Core;
using AdventOfCode.App.Utilities;
using Color = System.Drawing.Color;

namespace AdventOfCode.App.Extras;

[ExtraCommand("robot-tree")]
[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
public class RobotTrees : IExtraCommand
{
  private const int WIDTH = 101;
  private const int HEIGHT = 103;
  private readonly string _outputPath;

  public RobotTrees()
  {
    var outputBase = Environment.GetEnvironmentVariable("AOC_OUTPUT_PATH") ?? "";
    _outputPath = Path.Combine(outputBase, "robots");
  }

  public async Task Execute()
  {
    var input = InputReader.LoadInput(14, 2024);

    var rows = input.ToRows();
    var robots = rows.Select(r => Aoc2024Day14Processor.Robot.Parse(r, WIDTH, HEIGHT));

    var tasks = Enumerable.Range(0, 10000).Select(time => Task.Run(() => SaveState(robots, time)));
    
    await Task.WhenAll(tasks);
  }

  private void SaveState(IEnumerable<Aoc2024Day14Processor.Robot> robots, int time)
  {
    var bmp = new Bitmap(WIDTH, HEIGHT);
    var coords = robots.Select(e => e.ProjectPosition(time)).ToArray();
    if (coords.Count(c => c.X > WIDTH / 2) > 200) return;

    foreach (var (x, y) in coords)
    {
      bmp.SetPixel(x, y, Color.White);
    }
    
    bmp.Save(Path.Combine(_outputPath, $"robots_{time.ToString().PadLeft(6, '0')}.bmp"), ImageFormat.Bmp);
    
    Console.WriteLine($"Complete for {time}.");
  }
}
