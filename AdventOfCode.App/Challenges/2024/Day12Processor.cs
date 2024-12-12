using AdventOfCode.App.Core;
using AdventOfCode.App.Utilities;
using Cell = (int X, int Y, int Perimeter);

namespace AdventOfCode.App.Challenges;

[ChallengeProcessor(2024, 12)]
public class Aoc2024Day12Processor : IChallengeProcessor
{
  public static int GetPerimeter(char[][] grid, char c, int x, int y)
  {
    var perimeters = 0;

    if (x <= 0 || grid[y][x - 1] != c) perimeters++;
    if (x + 1 >= grid[y].Length || grid[y][x + 1] != c) perimeters++;
    if (y <= 0 || grid[y - 1][x] != c) perimeters++;
    if (y + 1 >= grid.Length || grid[y + 1][x] != c) perimeters++;

    return perimeters;
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
          x > 0 && grid[y][x - 1] == c)
      {
        queue.Enqueue((x - 1, y, GetPerimeter(grid, c, x - 1, y)));
      }

      if (!cells.Any(cell => cell.X == x + 1 && cell.Y == y) &&
          !queue.Any(cell => cell.X == x + 1 && cell.Y == y) &&
          x + 1 < grid[y].Length && grid[y][x + 1] == c)
      {
        queue.Enqueue((x + 1, y, GetPerimeter(grid, c, x + 1, y)));
      }

      if (!cells.Any(cell => cell.X == x && cell.Y == y - 1) &&
          !queue.Any(cell => cell.X == x && cell.Y == y - 1) &&
          y > 0 && grid[y - 1][x] == c)
      {
        queue.Enqueue((x, y - 1, GetPerimeter(grid, c, x, y - 1)));
      }

      if (!cells.Any(cell => cell.X == x && cell.Y == y + 1) &&
          !queue.Any(cell => cell.X == x && cell.Y == y + 1) &&
          y + 1 < grid.Length && grid[y + 1][x] == c)
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

  public static int CalculateRegionValue(IEnumerable<int> input)
  {
    var region = input as int[] ?? input.ToArray();
    return region.Sum() * region.Length;
  }

  public string ProcessPart1Solution(string input)
  {
    var grid = input.ToCharGrid();
    var regions = ProcessRegions(grid);

    return regions.Select(r => CalculateRegionValue(r.Select(e => e.Perimeter))).Sum().ToString();
  }

  public string ProcessPart2Solution(string input)
  {
    throw new NotImplementedException();
  }
}