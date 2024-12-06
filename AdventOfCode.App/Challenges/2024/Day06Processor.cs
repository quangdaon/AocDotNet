using AdventOfCode.App.Core;

namespace AdventOfCode.App.Challenges;

[ChallengeProcessor(2024, 6)]
public class Aoc2024Day06Processor : IChallengeProcessor
{
  private const char CHAR_OBSTACLE = '#';
  private const char CHAR_START = '^';

  public static (bool[][] map, int startX, int startY) ParseInput(string input)
  {
    var rows = input.Split(Environment.NewLine);
    var map = new bool[rows.Length][];
    var startX = -1;
    var startY = -1;

    for (var i = 0; i < rows.Length; i++)
    {
      var row = rows[i];
      var indexStart = row.IndexOf(CHAR_START);

      if (indexStart > -1)
      {
        startX = indexStart;
        startY = i;
      }

      map[i] = row.ToCharArray().Select(e => e == CHAR_OBSTACLE).ToArray();
    }

    return (map, startX, startY);
  }

  private static bool[][] Patrol(bool[][] map, int startX, int startY)
  {
    var dir = 1;
    var x = startX;
    var y = startY;
    var mask = map.Select(e => e.Select(f => false).ToArray()).ToArray();
    var next = 0;

    while (next > -1)
    {
      next = dir switch
      {
        1 => PatrolUp(mask, map, x, y),
        2 => PatrolHorizontal(1, mask[y], map[y], x),
        3 => PatrolDown(mask, map, x, y),
        4 => PatrolHorizontal(-1, mask[y], map[y], x),
        _ => throw new InvalidOperationException("Shit's fucked.")
      };

      if (dir % 2 == 0)
      {
        x = next;
      }
      else
      {
        y = next;
      }

      dir += 1;
      if (dir > 4) dir = 1;
    }

    return mask;
  }

  public static int PatrolHorizontal(int dir, bool[] maskRow, bool[] mapRow, int x)
  {
    var i = x;
    while ((dir == -1 && i >= 0) || (dir == 1 && i < mapRow.Length))
    {
      if (mapRow[i]) return i - dir;
      maskRow[i] = true;
      i += dir;
    }

    return -1;
  }

  public static int PatrolUp(bool[][] mask, bool[][] map, int x, int y)
  {
    var obstacleY = Array.FindLastIndex(map.Take(y).ToArray(), m => m[x]);

    for (var i = y; i > obstacleY; i--) mask[i][x] = true;

    return obstacleY > -1 ? obstacleY + 1 : -1;
  }

  public static int PatrolDown(bool[][] mask, bool[][] map, int x, int y)
  {
    var obstacleY = Array.FindIndex(map.Skip(y).ToArray(), m => m[x]);
    var targetY = obstacleY > -1 ? obstacleY + y - 1 : map.Length - 1;

    for (var i = y; i <= targetY; i++) mask[i][x] = true;

    return obstacleY > -1 ? targetY : -1;
  }

  public string ProcessPart1Solution(string input)
  {
    var (map, startX, startY) = ParseInput(input);

    var mask = Patrol(map, startX, startY);
    
    // PrintMap(map, mask);

    return mask.Sum(e => e.Count(c => c)).ToString();
  }
  
  public string ProcessPart2Solution(string input)
  {
    throw new NotImplementedException();
  }

  // ReSharper disable once UnusedMember.Local
  private static void PrintMap(bool[][] map, bool[][] mask)
  {
    foreach (var s in map.Select((m, y) => string.Join("", m.Select((n, x) => n ? '#' : mask[y][x] ? 'X' : '.')))) Console.WriteLine(s);
  }
}