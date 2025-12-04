using AdventOfCode.App.Core;
using AdventOfCode.App.Utilities;

namespace AdventOfCode.App.Challenges;

[ChallengeProcessor(2025, 2)]
public class Aoc2025Day02Processor : IChallengeProcessor
{
  public bool Repeats(long input, int factor = 2)
  {
    var digits = Convert.ToInt64(Math.Floor(Math.Log10(input))) + 1;
    return Repeats(input, digits, factor);
  }
  
  public bool Repeats(long input, long digits, int factor = 2)
  {
    if (digits % factor != 0) return false;
    
    var componentSize = digits / factor;
    var componentMultiplier = Math.Pow(10, componentSize);
    var componentValue = Convert.ToInt64(input % Math.Pow(10, componentSize));
    var computedValue = 0L;

    for (var i = 0; i < factor; i++)
    {
      computedValue *= Convert.ToInt64(componentMultiplier);
      computedValue += componentValue;
    }

    return computedValue == input;
  }

  public long ProcessRangePart1(long[] range)
  {
    var min = range.Min();
    var max = range.Max();
    var total = 0L;

    for (var i = min; i <= max; i++)
    {
      if (Repeats(i)) total += i;
    }

    return total;
  }

  public string ProcessPart1Solution(string input)
  {
    var ranges = input.Split(',').Select(r => r.ToLongs('-')).ToArray();
    var results = ranges.Sum(ProcessRangePart1);

    return results.ToString();
  }

  private static readonly Dictionary<int, int[]> FactorsCache = new();

  public int[] GetFactors(int val)
  {
    if (FactorsCache.TryGetValue(val, out var cached)) return cached;
    
    var factors = Enumerable.Range(2, val).Where(e => val % e == 0).ToArray();
    
    FactorsCache[val] = factors;
    
    return factors;
  }

  public bool IsInvalidPart2(long input)
  {
    var digits = Convert.ToInt32(Math.Floor(Math.Log10(input))) + 1;
    var factors = GetFactors(digits);

    return factors.Any(factor => Repeats(input, digits, factor));
  }

  public long ProcessRangePart2(long[] range)
  {
    var min = range.Min();
    var max = range.Max();
    var total = 0L;

    for (var i = min; i <= max; i++)
    {
      if (IsInvalidPart2(i)) total += i;
    }

    return total;
  }

  public string ProcessPart2Solution(string input)
  {
    var ranges = input.Split(',').Select(r => r.ToLongs('-')).ToArray();
    var results = ranges.Sum(ProcessRangePart2);

    return results.ToString();
  }
}
