using AdventOfCode.App.Core;
using AdventOfCode.App.Utilities;
using Cell = (int X, int Y, AdventOfCode.App.Challenges.Border[] Perimeter);

namespace AdventOfCode.App.Challenges;

public enum Border
{
  Top,
  Left,
  Bottom,
  Right
}

[ChallengeProcessor(2024, 12)]
public class Aoc2024Day12Processor : IChallengeProcessor
{
  public static Border[] GetPerimeter(char[][] grid, char c, int x, int y)
  {
    List<Border> perimeters = [];

    if (x <= 0 || grid[y][x - 1] != c) perimeters.Add(Border.Left);
    if (x + 1 >= grid[y].Length || grid[y][x + 1] != c) perimeters.Add(Border.Right);
    if (y <= 0 || grid[y - 1][x] != c) perimeters.Add(Border.Top);
    if (y + 1 >= grid.Length || grid[y + 1][x] != c) perimeters.Add(Border.Bottom);

    return perimeters.ToArray();
  }

  // I will refactor this later™️
  public static List<Cell> Crawl(char[][] grid, char c, int startX, int startY)
  {
    List<Cell> cells = [];
    var queue = new Queue<Cell>([(startX, startY, GetPerimeter(grid, c, startX, startY))]);

    while (queue.Count > 0)
    {
      var center = queue.Dequeue();
      cells.Add(center);
      var (x, y, _) = center;

      if (!cells.Any(cell => cell.X == x - 1 && cell.Y == y) &&
          !queue.Any(cell => cell.X == x - 1 && cell.Y == y) &&
          !center.Perimeter.Contains(Border.Left))
      {
        queue.Enqueue((x - 1, y, GetPerimeter(grid, c, x - 1, y)));
      }

      if (!cells.Any(cell => cell.X == x + 1 && cell.Y == y) &&
          !queue.Any(cell => cell.X == x + 1 && cell.Y == y) &&
          !center.Perimeter.Contains(Border.Right))
      {
        queue.Enqueue((x + 1, y, GetPerimeter(grid, c, x + 1, y)));
      }

      if (!cells.Any(cell => cell.X == x && cell.Y == y - 1) &&
          !queue.Any(cell => cell.X == x && cell.Y == y - 1) &&
          !center.Perimeter.Contains(Border.Top))
      {
        queue.Enqueue((x, y - 1, GetPerimeter(grid, c, x, y - 1)));
      }

      if (!cells.Any(cell => cell.X == x && cell.Y == y + 1) &&
          !queue.Any(cell => cell.X == x && cell.Y == y + 1) &&
          !center.Perimeter.Contains(Border.Bottom))
      {
        queue.Enqueue((x, y + 1, GetPerimeter(grid, c, x, y + 1)));
      }
    }

    return cells;
  }

  public static List<List<Cell>> ProcessRegions(char[][] grid)
  {
    var regions = new List<List<Cell>>();

    for (var y = 0; y < grid.Length; y++)
    {
      var row = grid[y];
      for (var x = 0; x < row.Length; x++)
      {
        var c = row[x];
        if (regions.SelectMany(r => r).Any(e => e.X == x && e.Y == y)) continue;
        regions.Add(Crawl(grid, c, x, y));
      }
    }

    return regions;
  }

  public static int CalculateRegionValue(IEnumerable<Cell> input)
  {
    var region = input as Cell[] ?? input.ToArray();
    return region.Sum(e => e.Perimeter.Count()) * region.Length;
  }


  public static int CountBorders(Cell[] region, Border side)
  {
    var isHorizontal = side is Border.Top or Border.Bottom;
    var groups = region
      .Where(c => c.Perimeter.Contains(side))
      .GroupBy(c => isHorizontal ? c.Y : c.X)
      .Select(g => g.Select(e => isHorizontal ? e.X : e.Y));

    var count = 0;

    foreach (var group in groups)
    {
      var last = -1;
      foreach (var b in group.Order())
      {
        if (last == -1 || b - last > 1)
        {
          count++;
        }

        last = b;
      }
    }

    return count;
  }

  public static int GetSides(Cell[] region)
  {
    var total = CountBorders(region, Border.Top)
                + CountBorders(region, Border.Bottom)
                + CountBorders(region, Border.Left)
                + CountBorders(region, Border.Right);
    return total;
  }

  public static int CalculateDiscountedRegionValue(IEnumerable<Cell> input)
  {
    var region = input as Cell[] ?? input.ToArray();
    return GetSides(region) * region.Length;
  }

  public string ProcessPart1Solution(string input)
  {
    var grid = input.ToCharGrid();
    var regions = ProcessRegions(grid);

    return regions.Select(CalculateRegionValue).Sum().ToString();
  }

  public string ProcessPart2Solution(string input)
  {
    var grid = input.ToCharGrid();
    var regions = ProcessRegions(grid);

    return regions.Select(CalculateDiscountedRegionValue).Sum().ToString();
  }
}