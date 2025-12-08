using System.Text.RegularExpressions;
using AdventOfCode.App.Core;
using AdventOfCode.App.Utilities;

namespace AdventOfCode.App.Challenges;

[ChallengeProcessor(2025, 6)]
public partial class Aoc2025Day06Processor : IChallengeProcessor
{

  [GeneratedRegex(@"\s+")]
  private static partial Regex VariableWhitespaceRegex();
  
  public string ProcessPart1Solution(string input)
  {
    var rows = input.ToRows()
      .Select(e => VariableWhitespaceRegex()
        .Split(e)
        .Where(i => !string.IsNullOrWhiteSpace(i))
        .ToArray())
      .ToArray();
    var sum = 0L;

    for (var i = 0; i < rows[0].Length; i++)
    {
      List<long> numbers = [];
      numbers.AddRange(rows.Take(rows.Length - 1).Select(row => long.Parse(row[i])));

      var operation = rows.Last()[i];

      var aggregate = operation == "*" ? numbers.Aggregate(1L, (a, b) => a * b) : numbers.Sum();

      sum += aggregate;
    }

    return sum.ToString();
  }

  public string ProcessPart2Solution(string input)
  {
    var grid = input.ToGrid();
    var sum = 0L;

    var digits = grid.Take(grid.Length - 1).ToArray();
    var operators = grid.Last().ToArray();

    var numbers = new List<long>();

    for (var i = digits[0].Length - 1; i >= 0; i--)
    {
      var number = digits
        .Select(row => row[i])
        .Where(digit => !string.IsNullOrWhiteSpace(digit))
        .Aggregate("", (current, digit) => current + digit);

      if (string.IsNullOrWhiteSpace(number)) continue;

      numbers.Add(long.Parse(number));

      if (operators.Length - 1 < i || string.IsNullOrWhiteSpace(operators[i])) continue;

      var operation = operators[i];
      var aggregate = operation == "*" ? numbers.Aggregate(1L, (a, b) => a * b) : numbers.Sum();

      sum += aggregate;

      numbers.Clear();
    }

    return sum.ToString();
  }
}
