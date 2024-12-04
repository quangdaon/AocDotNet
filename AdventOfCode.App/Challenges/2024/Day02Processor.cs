using AdventOfCode.App.Core;

namespace AdventOfCode.App.Challenges;

[ChallengeProcessor(2024, 2)]
public class Aoc2024Day02Processor : IChallengeProcessor
{
  public static bool IsSafe(IEnumerable<int> levels)
  {
    var enumerable = levels as int[] ?? levels.ToArray();
    var last = enumerable.First();
    var lastDir = 0;

    foreach (var level in enumerable.Skip(1))
    {
      var diff = level - last;

      if (diff == 0) return false;

      var mag = Math.Abs(diff);

      if (mag > 3) return false;

      var dir = diff / mag;

      if (dir + lastDir == 0) return false;

      lastDir = dir;
      last = level;
    }

    return true;
  }

  public static IEnumerable<IEnumerable<int>> GetPermutations(IEnumerable<int> levels)
  {
    var enumerable = levels as int[] ?? levels.ToArray();
    return enumerable.Select((_, il) => enumerable.Where((_, i) => i != il));
  }

  public string ProcessPart1Solution(string input)
  {
    var rows = input.Split(Environment.NewLine);
    var levels = rows.Select(SplitRow);
    return levels.Count(IsSafe).ToString();
  }

  public string ProcessPart2Solution(string input)
  {
    var rows = input.Split(Environment.NewLine);
    var levels = rows
      .Select(SplitRow)
      .Select(GetPermutations);
    return levels.Count(l => l.Any(IsSafe)).ToString();
  }

  private static IEnumerable<int> SplitRow(string e) => e.Split(' ').Select(int.Parse);
}
