using System.Linq;
using AdventOfCode.App.Challenges;
using Xunit;

namespace AdventOfCode.App.Tests.Challenges;

public class Aoc2024Day05ProcessorTest : ChallengeProcessorTests
{
  private readonly Aoc2024Day05Processor _processor;

  public Aoc2024Day05ProcessorTest() : base(2024, 5)
  {
    _processor = new Aoc2024Day05Processor();
  }

  [Theory]
  [InlineData(new[] { 3, 4 }, new[] { 1, 3, 5, 4 })] // Fulfills Rule
  [InlineData(new[] { 4, 3 }, new[] { 1, 4, 5, 3 })] // Fulfills Rule
  [InlineData(new[] { 3, 4 }, new[] { 1, 2, 3 })] // Irrelevant Rule
  public void IsCompliant_GivenCompliantPages_ReturnsTrue(int[] rule, int[] pages)
  {
    var result = Aoc2024Day05Processor.IsCompliant(rule, pages);

    Assert.True(result);
  }

  [Theory]
  [InlineData(new[] { 3, 4 }, new[] { 1, 4, 5, 3 })] // Breaks Rule
  public void IsCompliant_GivenNoncompliantPages_ReturnsFalse(int[] rule, int[] pages)
  {
    var result = Aoc2024Day05Processor.IsCompliant(rule, pages);

    Assert.False(result);
  }

  [Theory]
  [InlineData(new[] { 1, 8, 5, 4, 3 })]
  public void IsCompliant_GivenCompliantPagesForRuleset_ReturnsTrue(int[] pages)
  {
    int[][] ruleset =
    [
      [1, 3],
      [5, 4],
      [8, 4]
    ];

    var result = Aoc2024Day05Processor.IsCompliant(ruleset, pages);

    Assert.True(result);
  }

  [Theory]
  [InlineData(new[] { 3, 8, 5, 4, 1 })] // Breaks Rule 1
  [InlineData(new[] { 1, 4, 5, 3 })] // Breaks Rule 2
  [InlineData(new[] { 4, 8, 1, 3 })] // Breaks Rule 3
  [InlineData(new[] { 3, 5, 4, 1, 8 })] // Breaks Rules 1 & 3
  public void IsCompliant_GivenNoncompliantPagesForRuleset_ReturnsFalse(int[] pages)
  {
    int[][] ruleset =
    [
      [1, 3],
      [5, 4],
      [8, 4]
    ];

    var result = Aoc2024Day05Processor.IsCompliant(ruleset, pages);

    Assert.False(result);
  }

  [Fact]
  public void GetNoncompliantRules_GivenCompliantPagesForRuleset_ReturnsEmptyIEnumerable()
  {
    int[][] ruleset =
    [
      [1, 3],
      [5, 4],
      [8, 4]
    ];

    var result = Aoc2024Day05Processor.GetNoncompliantRules(ruleset, [1, 8, 5, 4, 3]);

    Assert.NotNull(result);
    Assert.Empty(result);
  }

  [Theory]
  [InlineData(new[] { 3, 8, 5, 4, 1 }, 0)]
  [InlineData(new[] { 1, 4, 5, 3 }, 1)]
  [InlineData(new[] { 4, 8, 1, 3 }, 2)]
  public void GetNoncompliantRules_GivenNoncompliantPagesForRuleset_ReturnsBrokenRule(int[] pages, int brokenRule)
  {
    int[][] ruleset =
    [
      [1, 3],
      [5, 4],
      [8, 4]
    ];

    var result = Aoc2024Day05Processor.GetNoncompliantRules(ruleset, pages).ToArray();

    Assert.Equal(1, result.Length);
    Assert.Equal(result[0], ruleset[brokenRule]);
  }

  [Fact]
  public void GetNoncompliantRules_GivenNoncompliantPagesForMultipleRules_ReturnsBrokenRules()
  {
    int[][] ruleset =
    [
      [1, 3],
      [5, 4],
      [8, 4]
    ];

    var result = Aoc2024Day05Processor.GetNoncompliantRules(ruleset, [3, 5, 4, 1, 8]).ToArray();

    Assert.Equal(2, result.Length);
    Assert.Equal(ruleset[0], result[0]);
    Assert.Equal(ruleset[2], result[1]);
  }

  [Theory]
  [InlineData(new[] { 1, 2, 3, 4, 5 }, 3)] // Sorted
  [InlineData(new[] { 1, 2, 9, 3, 4 }, 9)] // Unsorted Array
  public void GetCenter_GivenValidArray_ReturnsCenter(int[] pages, int expected)
  {
    var result = Aoc2024Day05Processor.GetCenter(pages);

    Assert.Equal(expected, result);
  }

  [Theory]
  [InlineData(new[] { 1, 2, 3, 5, 4 })]
  [InlineData(new[] { 1, 2, 9, 3, 4 })]
  public void GetCompliantCenter_GivenCompliantPages_ReturnsZero(int[] pages)
  {
    int[][] ruleset =
    [
      [1, 3],
      [5, 4],
      [8, 4]
    ];

    var result = Aoc2024Day05Processor.GetCompliantCenter(ruleset, pages);

    Assert.Equal(0, result);
  }

  [Theory]
  [InlineData(new[] { 5, 4, 3, 1, 2 }, 1)] // Center Noncompliant
  [InlineData(new[] { 1, 4, 9, 3, 8 }, 9)] // Center Untouched
  [InlineData(new[] { 3, 2, 1, 5, 4 }, 5)] // Multiple Swaps
  public void GetCompliantCenter_GivenNoncompliantPages_ReturnsSwapped(int[] pages, int expected)
  {
    int[][] ruleset =
    [
      [1, 3],
      [5, 4],
      [8, 4],
      [4, 3]
    ];

    var result = Aoc2024Day05Processor.GetCompliantCenter(ruleset, pages);

    Assert.Equal(expected, result);
  }

  [Fact]
  public void ProcessPart1Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart1Solution(input);

    Assert.Equal("143", result);
  }

  [Fact]
  public void ProcessPart2Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart2Solution(input);

    Assert.Equal("123", result);
  }
}