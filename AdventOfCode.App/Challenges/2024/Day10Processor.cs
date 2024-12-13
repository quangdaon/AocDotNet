using AdventOfCode.App.Core;
using AdventOfCode.App.Utilities;

namespace AdventOfCode.App.Challenges;

[ChallengeProcessor(2024, 10)]
public class Aoc2024Day10Processor : IChallengeProcessor
{
  public class Crawler
  {
    private const int TARGET = 9;

    private int _currentValue;
    private readonly int[][] _grid;
    private readonly int _height;
    private readonly int _width;

    private Coordinates CurrentPosition { get; set; }
    private List<Coordinates> Peaks { get; } = [];
    private List<Crawler> Subcrawlers { get; } = [];

    private Crawler(int[][] grid, int val, Coordinates startPosition)
    {
      _currentValue = val;
      _grid = grid;
      _height = _grid.Length;
      _width = _grid[0].Length;
      CurrentPosition = startPosition;
    }

    public IEnumerable<Coordinates> Crawl()
    {
      var next = _currentValue + 1;
      Coordinates[] targets =
      [
        (CurrentPosition.X - 1, CurrentPosition.Y),
        (CurrentPosition.X + 1, CurrentPosition.Y),
        (CurrentPosition.X, CurrentPosition.Y - 1),
        (CurrentPosition.X, CurrentPosition.Y + 1)
      ];

      List<Coordinates> branches = [];

      foreach (var target in targets)
      {
        if (target.X < 0 || target.X >= _width || target.Y < 0 || target.Y >= _height) continue;
        var targetValue = _grid[target.Y][target.X];
        if (targetValue != next) continue;

        if (targetValue == TARGET)
        {
          Peaks.Add(target);
          continue;
        }

        branches.Add(target);
      }

      switch (branches.Count)
      {
        case 0:
          return Peaks;
        case 1:
          _currentValue = next;
          CurrentPosition = branches[0];
          return Crawl();
        default:
        {
          var crawlers = branches.Select(e => Spawn(_grid, next, e)).ToArray();
          // Totally unnecessary, but feels right.
          Subcrawlers.AddRange(crawlers);
          return Subcrawlers.SelectMany(c => c.Crawl());
        }
      }
    }

    public static Crawler[] Crawl(int[][] grid)
    {
      var crawlers = grid.SelectMany((r, y) => r
          .Select((val, x) => new { Value = val, X = x, Y = y })
          .Where(e => e.Value == 0)
          .Select(e => Spawn(grid, e.Value, e.X, e.Y)))
        .ToArray();

      return crawlers;
    }

    private static Crawler Spawn(int[][] grid, int next, Coordinates start)
    {
      return new Crawler(grid, next, start);
    }

    private static Crawler Spawn(int[][] grid, int val, int x, int y)
    {
      return Spawn(grid, val, (x, y));
    }
  }

  public string ProcessPart1Solution(string input)
  {
    var grid = input.ToDigitsGrid();
    var crawlers = Crawler.Crawl(grid);

    return crawlers.Sum(c => c.Crawl().Distinct().Count()).ToString();
  }

  public string ProcessPart2Solution(string input)
  {
    var grid = input.ToDigitsGrid();
    var crawlers = Crawler.Crawl(grid);

    return crawlers.Sum(c => c.Crawl().Count()).ToString();
  }
}