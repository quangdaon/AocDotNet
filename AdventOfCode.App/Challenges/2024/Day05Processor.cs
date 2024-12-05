using AdventOfCode.App.Core;

namespace AdventOfCode.App.Challenges;

[ChallengeProcessor(2024, 5)]
public class Aoc2024Day05Processor : IChallengeProcessor
{
  public static IEnumerable<int[]> GetNoncompliantRules(int[][] ruleset, int[] pages) =>
    ruleset.Where(r => !IsCompliant(r, pages));

  public static bool IsCompliant(int[][] ruleset, int[] pages) => !GetNoncompliantRules(ruleset, pages).Any();

  public static bool IsCompliant(int[] rule, int[] pages) => !pages.Contains(rule[0]) ||
                                                             !pages.Contains(rule[1]) ||
                                                             Array.IndexOf(pages, rule[0]) <
                                                             Array.IndexOf(pages.ToArray(), rule[1]);

  public static T GetCenter<T>(T[] arr) => arr[arr.Length / 2];

  public static int GetCompliantCenter(int[][] ruleset, int[] pages)
  {
    var noncompliantRules = GetNoncompliantRules(ruleset, pages).ToList();
    if (noncompliantRules.Count == 0) return 0;
    var center = GetCenter(pages);

    while (noncompliantRules.Count > 0)
    {
      var centerSwap = noncompliantRules.FirstOrDefault(e => e.Contains(center));
      if (centerSwap is null) return center;
      
      pages[Array.IndexOf(pages, centerSwap[0])] = centerSwap[1];
      pages[Array.IndexOf(pages, centerSwap[1])] = centerSwap[0];

      noncompliantRules = GetNoncompliantRules(ruleset, pages).ToList();
      center = GetCenter(pages);
    }

    return center;
  }

  public string ProcessPart1Solution(string input)
  {
    var pieces = input.Split(Environment.NewLine + Environment.NewLine);
    var rulesRows = pieces[0].Trim().Split(Environment.NewLine);
    var updatesRows = pieces[1].Trim().Split(Environment.NewLine);

    var ruleset = rulesRows.Select(e => e.Split('|').Select(int.Parse).ToArray()).ToArray();
    var updates = updatesRows.Select(e => e.Split(',').Select(int.Parse).ToArray()).ToArray();

    var compliantUpdates = updates.Where(e => IsCompliant(ruleset, e));
    var centers = compliantUpdates.Select(GetCenter);

    return centers.Sum().ToString();
  }

  public string ProcessPart2Solution(string input)
  {
    var pieces = input.Split(Environment.NewLine + Environment.NewLine);
    var rulesRows = pieces[0].Trim().Split(Environment.NewLine);
    var updatesRows = pieces[1].Trim().Split(Environment.NewLine);

    var ruleset = rulesRows.Select(e => e.Split('|').Select(int.Parse).ToArray()).ToArray();
    var updates = updatesRows.Select(e => e.Split(',').Select(int.Parse).ToArray()).ToArray();

    var centers = updates.Select(pages => GetCompliantCenter(ruleset, pages));

    return centers.Sum().ToString();
  }
}