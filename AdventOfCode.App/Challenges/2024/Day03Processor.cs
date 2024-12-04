using System.Text.RegularExpressions;
using AdventOfCode.App.Core;

namespace AdventOfCode.App.Challenges;

[ChallengeProcessor(2024, 3)]
public partial class Aoc2024Day03Processor : IChallengeProcessor
{
  [GeneratedRegex(@"mul\((\d+),(\d+)\)")]
  private static partial Regex Part1Regex();

  [GeneratedRegex(@"(mul|do(?:n't)?)\((\d+(?:,\d+)+)?\)")]
  private static partial Regex Part2Regex();
  
  public string ProcessPart1Solution(string input)
  {
    var matches = Part1Regex().Matches(input);
    var sum = 0;

    foreach (Match match in matches)
    {
      var left = int.Parse(match.Groups[1].Value);
      var right = int.Parse(match.Groups[2].Value);

      sum += left * right;
    }

    return sum.ToString();
  }

  public string ProcessPart2Solution(string input)
  {
    var matches = Part2Regex().Matches(input);
    var sum = 0;
    var active = true;

    foreach (Match match in matches)
    {
      var instruction = match.Groups[1].Value;

      active = instruction switch
      {
        "don't" => false,
        "do" => true,
        _ => active
      };

      if (!active || instruction != "mul") continue;

      var args = match.Groups[2].Value.Split(',');

      var left = int.Parse(args[0]);
      var right = int.Parse(args[1]);

      sum += left * right;
    }

    return sum.ToString();
  }
}