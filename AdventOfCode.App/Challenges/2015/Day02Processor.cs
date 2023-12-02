using AdventOfCode.App.Core;

namespace AdventOfCode.App.Challenges;

[ChallengeProcessor(2015, 2)]
public class Aoc2015Day02Processor : IChallengeProcessor
{
  public int CalculateWrappingPaper(string row)
  {
    var dimensions = row.Split('x').Select(int.Parse).ToArray();
    var areas = dimensions.Select((e, d) => e * dimensions[(d + 1) % dimensions.Length]).ToArray();
    var total = areas.Min();

    foreach (var a in areas) total += 2 * a;

    return total;
  }
  public int CalculateRibbon(string row)
  {
    var dimensions = row.Split('x').Select(int.Parse).ToArray();
    var volume = dimensions.Aggregate(1, (x, y) => x * y);
    var sorted = dimensions.OrderBy(e => e).ToArray();
    var total = 2 * (sorted[0] + sorted[1]) + volume;

    return total;
  }

  public string ProcessPart1Solution(string input)
  {
    var rows = input.Split(Environment.NewLine);
    var sum = 0;

    foreach (var row in rows) sum += CalculateWrappingPaper(row);

    return sum.ToString();
  }

  public string ProcessPart2Solution(string input)
  {
    var rows = input.Split(Environment.NewLine);
    var sum = 0;

    foreach (var row in rows) sum += CalculateRibbon(row);

    return sum.ToString();
  }
}