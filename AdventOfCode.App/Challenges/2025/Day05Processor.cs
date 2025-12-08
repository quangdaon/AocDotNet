using AdventOfCode.App.Core;
using AdventOfCode.App.Utilities;
using Range = AdventOfCode.App.Utilities.Range;

namespace AdventOfCode.App.Challenges;

[ChallengeProcessor(2025, 5)]
public class Aoc2025Day05Processor : IChallengeProcessor
{
  public string ProcessPart1Solution(string input)
  {
    var blocks = input.Split($"{Environment.NewLine}{Environment.NewLine}");
    var freshRanges = blocks[0].ToRows().Select(Range.Parse).ToArray();
    var availableIngredients = blocks[1].ToLongs('\n');
    return availableIngredients.Count(e => freshRanges.Any(r => r.Contains(e))).ToString();
  }

  public string ProcessPart2Solution(string input)
  {
    var blocks = input.Split($"{Environment.NewLine}{Environment.NewLine}");
    var freshRanges = blocks[0].ToRows().Select(Range.Parse).ToList();

    var rangesToEvaluate = new List<Range>();

    while (freshRanges.Count != 0)
    {
      var next = freshRanges.First();
      freshRanges.Remove(next);
      var overlaps = freshRanges.Where(r => next.Overlaps(r)).ToList();
      overlaps.Add(next);

      while (overlaps.Count > 1)
      {
        var min = overlaps.Min(e => e.Start);
        var max = overlaps.Max(e => e.End);
        
        next = new Range(min, max);
        
        freshRanges.RemoveAll(e => overlaps.Contains(e));
        
        overlaps = freshRanges.Where(r => next.Overlaps(r)).ToList();
        overlaps.Add(next);
      }
      
      rangesToEvaluate.Add(next);
    }

    return rangesToEvaluate.Sum(e => e.Size()).ToString();
  }
}
