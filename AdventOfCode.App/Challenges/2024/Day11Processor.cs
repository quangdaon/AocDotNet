using AdventOfCode.App.Core;
using AdventOfCode.App.Utilities;
using NodeResult = (int Iterations, long[] Branches);

namespace AdventOfCode.App.Challenges;

[ChallengeProcessor(2024, 11)]
public class Aoc2024Day11Processor : IChallengeProcessor
{
  private const long RULE_THREE_SCALE = 2024;
  private static Dictionary<(long Node, int Blinks), long> _cache = new();

  public static IEnumerable<long> ApplyRule(long input)
  {
    if (input == 0) return [1];
    return input.ToString().Length % 2 == 0 ? SplitNumber(input) : [input * RULE_THREE_SCALE];
  }

  public static IEnumerable<long> SplitNumber(long input)
  {
    var length = input.ToString().Length;
    if (length % 2 != 0) throw new InvalidOperationException("Input length must be divisible by 2");

    var power = Convert.ToInt64(Math.Pow(10, length / 2));
    var left = Convert.ToInt64(input / power);
    var right = Convert.ToInt64(input % power);

    return [left, right];
  }

  public static IEnumerable<long> Blink(long[] input)
  {
    List<long> result = [];

    foreach (var num in input)
    {
      result.AddRange(ApplyRule(num));
    }

    return result;
  }

  public static IEnumerable<long> Blink(long[] input, long count)
  {
    var result = input;
    var remaining = count;

    while (--remaining >= 0)
    {
      result = Blink(result).ToArray();
    }

    return result;
  }

  private static long BlinkOptimized(long node, int blinks)
  {
    if (blinks == 0) return 1;
    if (_cache.TryGetValue((node, blinks), out var cached)) return cached;

    var next = ApplyRule(node).ToArray();
    var result = next.Select(e => BlinkOptimized(e, blinks - 1)).Sum();

    _cache[(node, blinks)] = result;

    return result;
  }

  public string ProcessPart1Solution(string input)
  {
    var stones = input.ToLongs();
    return stones.Sum(node => BlinkOptimized(node, 25)).ToString();
  }

  public string ProcessPart2Solution(string input)
  {
    var stones = input.ToLongs();
    return stones.Sum(node => BlinkOptimized(node, 75)).ToString();
  }
}