using AdventOfCode.App.Core;
using AdventOfCode.App.Utilities;

namespace AdventOfCode.App.Challenges;

[ChallengeProcessor(2024, 15)]
public class Aoc2024Day15Processor : IChallengeProcessor
{
  private const int GPS_SCALE = 100;

  public enum WarehouseTile
  {
    Empty,
    Wall,
    Robot,
    Box,
    BoxLeft,
    BoxRight
  }

  public static WarehouseTile[][] ParseWarehouse(string[] rows)
  {
    return rows.Select(e => e.ToCharArray().Select(c => c switch
    {
      '#' => WarehouseTile.Wall,
      'O' => WarehouseTile.Box,
      '@' => WarehouseTile.Robot,
      // Following two are for debugging
      '[' => WarehouseTile.BoxLeft,
      ']' => WarehouseTile.BoxRight,
      _ => WarehouseTile.Empty
    }).ToArray()).ToArray();
  }

  public static WarehouseTile[][] ParseWarehouseScaled(string[] rows)
  {
    return rows.Select(e => e.ToCharArray().SelectMany<char, WarehouseTile>(c => c switch
    {
      '#' => [WarehouseTile.Wall, WarehouseTile.Wall],
      'O' => [WarehouseTile.BoxLeft, WarehouseTile.BoxRight],
      '@' => [WarehouseTile.Robot, WarehouseTile.Empty],
      _ => [WarehouseTile.Empty, WarehouseTile.Empty]
    }).ToArray()).ToArray();
  }

  public static int TryPushX(WarehouseTile[][] warehouse, int robotX, int y, int dir)
  {
    var targetX = robotX;

    while ((dir != -1 || targetX > 0) && (dir != 1 || targetX < warehouse[0].Length - 1))
    {
      targetX += dir;

      var target = warehouse[y][targetX];

      if (target == WarehouseTile.Wall) return 0;
      if (target != WarehouseTile.Empty) continue;

      for (var x = targetX; dir * (x - robotX) >= 0; x -= dir)
      {
        var next = warehouse[y][x - dir];
        warehouse[y][x] = x == robotX ? WarehouseTile.Empty : next;
      }

      return dir;
    }

    return 0;
  }

  public static int TryPushY(WarehouseTile[][] warehouse, int robotX, int robotY, int dir)
  {
    var targetY = robotY;
    List<int[]> xsToMove = [[robotX]];

    while ((dir != -1 || targetY > 0) && (dir != 1 || targetY < warehouse.Length - 1))
    {
      targetY += dir;
      var xsLastChecked = xsToMove.Last().ToList();

      var targets = xsLastChecked.Select(x => warehouse[targetY][x]).ToArray();

      if (targets.Any(t => t == WarehouseTile.Wall)) return 0;
      if (targets.Any(t => t != WarehouseTile.Empty))
      {
        List<int> next = [];
        next.AddRange(xsLastChecked.SelectMany<int, int>(x => warehouse[targetY][x] switch
        {
          WarehouseTile.Box => [x],
          WarehouseTile.BoxLeft => [x, x + 1],
          WarehouseTile.BoxRight => [x, x - 1],
          _ => []
        }));

        xsToMove.Add(next.Distinct().Order().ToArray());
        continue;
      }
      
      for (var y = targetY; dir * (y - robotY) > 0; y -= dir)
      {
        var indexY = dir * (y - robotY) - 1;
        var xsToShift = xsToMove[indexY];
        var xsToClear = y == targetY ? [] : xsToMove[indexY + 1];
        var xsToUpdate = xsToClear.Concat(xsToShift).Distinct().Order().ToArray();

        foreach (var x in xsToUpdate)
        {
          if (y == robotY && x != robotX) continue;

          warehouse[y][x] = !xsToShift.Contains(x) || (xsToMove.Count == 1 && x != robotX)
            ? WarehouseTile.Empty
            : warehouse[y - dir][x];
        }
      }

      warehouse[robotY][robotX] = WarehouseTile.Empty;

      return dir;
    }

    return 0;
  }

  public static char[] ParseInstructions(string components)
  {
    return components.ToCharArray().Where(e => !char.IsWhiteSpace(e)).ToArray();
  }

  public static void ApplyInstructions(WarehouseTile[][] warehouse, char[] instructions)
  {
    var robotY = Array.FindIndex(warehouse, r => r.Contains(WarehouseTile.Robot));
    var robotX = Array.IndexOf(warehouse[robotY], WarehouseTile.Robot);

    foreach (var instruction in instructions)
    {
      switch (instruction)
      {
        case '<':
          robotX += TryPushX(warehouse, robotX, robotY, -1);
          break;
        case '>':
          robotX += TryPushX(warehouse, robotX, robotY, 1);
          break;
        case '^':
          robotY += TryPushY(warehouse, robotX, robotY, -1);
          break;
        case 'v':
          robotY += TryPushY(warehouse, robotX, robotY, 1);
          break;
        default:
          Console.WriteLine("Unknown instruction: " + instruction);
          break;
      }
    }
  }

  private static int CalculateGps(WarehouseTile[][] warehouse)
  {
    var values = warehouse
      .SelectMany((row, y) =>
        row.Select((t, x) => t is WarehouseTile.Box or WarehouseTile.BoxLeft ? y * GPS_SCALE + x : 0));

    return values.Sum();
  }

  public string ProcessPart1Solution(string input)
  {
    var components = input.Split(Environment.NewLine + Environment.NewLine);
    var warehouse = ParseWarehouse(components[0].ToRows());
    var instructions = ParseInstructions(components[1]);

    ApplyInstructions(warehouse, instructions);

    return CalculateGps(warehouse).ToString();
  }

  public string ProcessPart2Solution(string input)
  {
    var components = input.Split(Environment.NewLine + Environment.NewLine);
    var warehouse = ParseWarehouseScaled(components[0].ToRows());
    var instructions = ParseInstructions(components[1]);

    ApplyInstructions(warehouse, instructions);

    return CalculateGps(warehouse).ToString();
  }

  // ReSharper disable once UnusedMember.Local
  private static void PrintGrid(WarehouseTile[][] warehouse)
  {
    var x = warehouse.Select(r => r.Select(t => t switch
      {
        WarehouseTile.Box => 'O',
        WarehouseTile.BoxLeft => '[',
        WarehouseTile.BoxRight => ']',
        WarehouseTile.Wall => '#',
        WarehouseTile.Robot => '@',
        _ => '.'
      }
    ));

    foreach (var z in x)
    {
      Console.WriteLine(new string(z.ToArray()));
    }
  }
}