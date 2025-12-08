using AdventOfCode.App.Core;
using AdventOfCode.App.Utilities;

namespace AdventOfCode.App.Challenges;

[ChallengeProcessor(2025, 3)]
public class Aoc2025Day03Processor : IChallengeProcessor
{
  public int GetMaxJoltage(string battery)
  {
    var jolts = battery.ToDigits();
    var currentMaxLeft = 0;
    var currentMaxRight = 0;

    for (var i = 0; i < jolts.Length - 1; i++)
    {
      var jolt = jolts[i];
      var next = jolts[i + 1];
      if (jolt > currentMaxLeft)
      {
        currentMaxLeft = jolt;
        currentMaxRight = 0;
      }

      if (next > currentMaxRight)
      {
        currentMaxRight = next;
      }
    }
    return currentMaxLeft * 10 + currentMaxRight;
  }
  
  public string ProcessPart1Solution(string input)
  {
    return input.ToRows().Sum(GetMaxJoltage).ToString();
  }

  public long GetSlidingMaxJoltage(string battery, int scale)
  {
    var jolts = battery.ToDigits();
    var size = jolts.Length;
    var buffer = size - scale;
    var windowLeft = 0;

    var result = 0L;
    var magnitude = scale - 1;

    while (magnitude >= 0)
    {
      var candidates = jolts.Skip(windowLeft).Take(buffer + 1).ToList();
      var max = candidates.Max();
      var maxIndex = candidates.IndexOf(max);
      
      result += max * Convert.ToInt64(Math.Pow(10, magnitude));
      buffer -= maxIndex;
      windowLeft += maxIndex + 1;
      magnitude--;
    }
    
    return result;
  }

  public string ProcessPart2Solution(string input)
  {
    return input.ToRows().Sum(e => GetSlidingMaxJoltage(e, 12)).ToString();
  }
}
