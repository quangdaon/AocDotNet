using AdventOfCode.App.Core;
using AdventOfCode.App.Utilities;

namespace AdventOfCode.App.Challenges;

[ChallengeProcessor(2025, 7)]
public class Aoc2025Day07Processor : IChallengeProcessor
{
  public string ProcessPart1Solution(string input)
  {
    var rows = input.ToRows();
    var beams = rows[0].Select(e => e == 'S').ToArray();
    var splits = 0;


    foreach (var row in rows)
    {
      for (var i = 0; i < row.Length; i++)
      {
        var c = row[i];

        if (c != '^' || !beams[i]) continue;

        splits++;

        beams[i] = false;

        if (i >= 0)
        {
          beams[i - 1] = true;
        }

        if (i <= row.Length - 1)
        {
          beams[i + 1] = true;
        }
      }
    }

    return splits.ToString();
  }

  public string ProcessPart2Solution(string input)
  {
    var rows = input.ToRows();
    var positionCounts = rows[0].Select(e => e == 'S' ? 1L : 0L).ToArray();

    foreach (var row in rows)
    {
      var splitterIndexes = row.ToCharArray().Select((e, i) => e == '^' ? i : -1).Where(e => e != -1).ToArray();
      if (splitterIndexes.Length == 0) continue;
      var newPositions = rows[0].Select(_ => 0L).ToArray();

      for (var i = 0; i < newPositions.Length; i++)
      {
        var currentCount = positionCounts[i];
        if (!splitterIndexes.Contains(i))
        {
          newPositions[i] += currentCount;
          continue;
        }

        if (currentCount == 0) continue;

        if (i >= 0)
        {
          newPositions[i - 1] += currentCount;
        }

        if (i <= row.Length - 1)
        {
          newPositions[i + 1] += currentCount;
        }
      }

      positionCounts = newPositions;
    }

    return positionCounts.Sum().ToString();
  }
}
