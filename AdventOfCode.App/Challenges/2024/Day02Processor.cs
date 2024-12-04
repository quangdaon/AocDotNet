using AdventOfCode.App.Core;

namespace AdventOfCode.App.Challenges;

[ChallengeProcessor(2024, 2)]
public class Aoc2024Day02Processor : IChallengeProcessor
{
  public static bool IsSafe(IEnumerable<int> levels)
  {
    var levelArray = levels as int[] ?? levels.ToArray();
    var last = levelArray.First();
    var lastDir = 0;

    foreach (var current in levelArray.Skip(1))
    {
      var diff = current - last;

      if (diff == 0 || Math.Abs(diff) > 3) return false;

      var dir = Math.Sign(diff);
      if (dir + lastDir == 0) return false;

      lastDir = dir;
      last = current;
    }

    return true;
  }

  public static IEnumerable<int[]> GetAlternates(IEnumerable<int> levels)
  {
    var levelArray = levels as int[] ?? levels.ToArray();
    return levelArray.Select((_, il) => levelArray.Where((_, i) => i != il).ToArray());
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
    var levels = rows.Select(SplitRow);
    
    return levels.Count(l => IsSafe(l) || GetAlternates(l).Any(IsSafe)).ToString();
  }

  private static int[] SplitRow(string e) => e.Split(' ').Select(int.Parse).ToArray();
}
