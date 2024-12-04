using System.Linq;
using AdventOfCode.App.Challenges;
using Xunit;

namespace AdventOfCode.App.Tests.Challenges;

public class Aoc2024Day04ProcessorTest : ChallengeProcessorTests
{
  private readonly Aoc2024Day04Processor _processor;

  public Aoc2024Day04ProcessorTest() : base(2024, 4)
  {
    _processor = new Aoc2024Day04Processor();
  }

  [Theory]
  [InlineData(-1, 9, 10, 1)]
  public void IsInBound_GivenInBoundInputs_ReturnsTrue(int dir, int index, int range, int buffer)
  {
    var result = Aoc2024Day04Processor.IsInBound(dir, index, range, buffer);

    Assert.True(result);
  }

  [Theory]
  [InlineData(-1, 0, 10, 2)]
  [InlineData(-1, 2, 10, 4)]
  [InlineData(1, 7, 10, 4)]
  [InlineData(1, 9, 10, 2)]
  public void IsInBound_GivenOutOfBoundInputs_ReturnsFalse(int dir, int index, int range, int buffer)
  {
    var result = Aoc2024Day04Processor.IsInBound(dir, index, range, buffer);

    Assert.False(result);
  }

  [Theory]
  [InlineData(3, 0, 1, 0, true)]
  [InlineData(5, 0, -1, 0, true)]
  [InlineData(6, 3, 0, 1, true)]
  [InlineData(6, 5, 0, -1, true)]
  [InlineData(2, 1, 1, 1, true)]
  [InlineData(4, 3, -1, -1, true)]
  [InlineData(4, 3, -1, 1, true)]
  [InlineData(2, 5, 1, -1, true)]
  [InlineData(0, 0, 1, 0, false)]
  public void IsMatch(int i, int j, int x, int y, bool expected)
  {
    string[] letters =
    [
      "AAAWOWAAAA",
      "AAWAAAAAAA",
      "AAAOAAAAAA",
      "AAAAWAWAAA",
      "AAAOAAOAAA",
      "AAWAAAWAAA",
    ];

    var grid = letters.Select(e => e.ToCharArray()).ToArray();
    var target = "WOW".ToCharArray();

    Assert.Equal(expected, Aoc2024Day04Processor.IsMatch(grid, target, j, i, x, y));
  }

  [Theory]
  [InlineData(new[] { "LA" }, 1)]
  [InlineData(new[] { "LA", "LA" }, 4)]
  [InlineData(new[] { "LA", "AA" }, 3)]
  [InlineData(new[] { "TRALALALA" }, 6)]
  public void FindWord_GivenGrid_CountsMatches(string[] rows, int expectedCount)
  {
    var grid = rows.Select(r => r.ToCharArray()).ToArray();

    var result = Aoc2024Day04Processor.FindWord(grid, "LA");

    Assert.Equal(expectedCount, result);
  }

  [Theory]
  [InlineData(new[] { "OMG" }, 1)]
  [InlineData(new[] { "OMG", "MAA", "GAA" }, 2)]
  [InlineData(new[] { "OMG", "OMG", "OMG" }, 5)]
  [InlineData(new[] { "AAGAA", "AAMAA", "GMOMG", "AAMAA", "AAGAA" }, 4)]
  [InlineData(new[] { "GAGAG", "AMMMA", "GMOMG", "AMMMA", "GAGAG" }, 8)]
  public void FindWord_GivenLargerGrid_CountsMatches(string[] rows, int expectedCount)
  {
    var grid = rows.Select(r => r.ToCharArray()).ToArray();

    var result = Aoc2024Day04Processor.FindWord(grid, "OMG");

    Assert.Equal(expectedCount, result);
  }

  [Theory]
  [InlineData(new[] { "OMG", "OMG", "OMG" }, 1)]
  [InlineData(new[] { "OAG", "AMA", "OAG" }, 1)]
  [InlineData(new[] { "OAO", "AMA", "GAG" }, 1)]
  [InlineData(new[] { "AOAOA", "AAMAA", "AGAGA" }, 1)]
  public void FindCross_GivenGrid_CountsMatches(string[] rows, int expectedCount)
  {
    var grid = rows.Select(r => r.ToCharArray()).ToArray();

    var result = Aoc2024Day04Processor.FindCross(grid, "OMG");

    Assert.Equal(expectedCount, result);
  }

  [Fact]
  public void ProcessPart1Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart1Solution(input);

    Assert.Equal("18", result);
  }


  [Fact]
  public void ProcessPart2Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart2Solution(input);

    Assert.Equal("9", result);
  }
}