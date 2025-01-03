using AdventOfCode.App.Core;
using AdventOfCode.App.Utilities;

namespace AdventOfCode.App.Challenges;

[ChallengeProcessor(2024, 1)]
public class Aoc2024Day01Processor : IChallengeProcessor
{
  public string ProcessPart1Solution(string input)
  {
    var rows = input.ToRows();
    var (left, right) = Unzip(rows);
    var rArray = right.ToArray();
    var diffs = left.Select((val, index) => Math.Abs(rArray[index] - val));

    return diffs.Sum().ToString();
  }

  public string ProcessPart2Solution(string input)
  {
    var rows = input.ToRows();
    var (left, right) = Unzip(rows);
    var rArray = right.ToArray();
    var scores = left.Select(val => val * rArray.Count(x => x == val));

    return scores.Sum().ToString();
  }

  public static (IEnumerable<int> left, IEnumerable<int> right) Unzip(IEnumerable<string> inputs)
  {
    List<int> left = [];
    List<int> right = [];

    foreach (var row in inputs)
    {
      var rowNumbers = row.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
      left.Add(rowNumbers.First());
      right.Add(rowNumbers.Last());
    }

    return (left.Order(), right.Order());
  }
}
