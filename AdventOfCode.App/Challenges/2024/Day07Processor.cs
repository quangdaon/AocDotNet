using AdventOfCode.App.Core;

namespace AdventOfCode.App.Challenges;

[ChallengeProcessor(2024, 7)]
public class Aoc2024Day07Processor : IChallengeProcessor
{
  public static (long Result, long[] Operands) ParseExpression(string expression)
  {
    var components = expression.Split(": ");

    var result = long.Parse(components[0]);
    var operands = components[1].Split(' ').Select(long.Parse).ToArray();

    return (result, operands);
  }

  public static bool Evaluate(long expected, long[] operands, bool concatenate = false)
  {
    if (operands.Length == 1) return expected == operands[0];

    var last = operands[^1];
    var remaining = operands[..^1];

    var divided = expected / last;
    var subtracted = expected - last;
    var deconcatenated = Deconcatenate(expected, last);

    return
      (concatenate && deconcatenated > -1 && Evaluate(deconcatenated, remaining, true)) ||
      Evaluate(subtracted, remaining, concatenate) ||
      (expected % last == 0 && Evaluate(divided, remaining, concatenate));
  }

  public static long Deconcatenate(long full, long operand)
  {
    var power = Convert.ToInt64(Math.Pow(10, operand.ToString().Length));
    var concatenated = full % power == operand;

    return concatenated ? full / power : -1;
  }

  public string ProcessPart1Solution(string input)
  {
    var rows = input.Split(Environment.NewLine);
    var expressions = rows.Select(ParseExpression);
    var result = expressions.Where(e => Evaluate(e.Result, e.Operands)).Sum(e => e.Result);

    return result.ToString();
  }

  public string ProcessPart2Solution(string input)
  {
    var rows = input.Split(Environment.NewLine);
    var expressions = rows.Select(ParseExpression);
    var result = expressions.Where(e => Evaluate(e.Result, e.Operands, true)).Sum(e => e.Result);

    return result.ToString();
  }

  // ReSharper disable once UnusedMember.Local
  private static bool EvaluateBruteForce(long expected, long[] operands)
  {
    var remaining = operands.ToList();

    var possibleResults = new List<long>();

    while (remaining.Count > 0)
    {
      var next = remaining.First();
      remaining.RemoveAt(0);

      if (possibleResults.Count == 0)
      {
        possibleResults.Add(next);
      }
      else
      {
        possibleResults = possibleResults
          .SelectMany(e => new[] { e * next, e + next, Convert.ToInt64($"{e}{next}") }).ToList();
      }
    }

    return possibleResults.Contains(expected);
  }
}
