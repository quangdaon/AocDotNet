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

    PatrolUp(mask, map, x, y);
    return mask;
  }

  public static int PatrolUp(bool[][] mask, bool[][] map, int x, int y)
  {
    var targetY = Array.FindLastIndex(map.Take(y).ToArray(), m => m[x]);

    for (var i = y; i >= targetY; i--) mask[y][x] = true;

    return targetY;
  }

  public string ProcessPart1Solution(string input)
  {
    var (map, startX, startY) = ParseInput(input);
    
    var mask = Patrol(map, startX, startY);

    return mask.Sum(e => e.Count(c => c)).ToString();
  }

  public string ProcessPart2Solution(string input)
  {
    throw new NotImplementedException();
  }
}
