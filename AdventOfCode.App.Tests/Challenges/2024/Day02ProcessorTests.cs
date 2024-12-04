using AdventOfCode.App.Challenges;
using Xunit;

namespace AdventOfCode.App.Tests.Challenges;

public class Aoc2024Day02ProcessorTest : ChallengeProcessorTests
{
  private readonly Aoc2024Day02Processor _processor;

  public Aoc2024Day02ProcessorTest() : base(2024, 2)
  {
    _processor = new Aoc2024Day02Processor();
  }

  [Theory]
  [InlineData(new[] { 1, 2, 3, 4, 5 })] // All Ascending
  [InlineData(new[] { 1, 4, 7, 10, 13 })] // Asc by 3
  [InlineData(new[] { 5, 4, 3, 2, 1 })] // All Descending
  [InlineData(new[] { 13, 10, 7, 4, 1 })] // Dsc by 3
  public void IsSafe_GivenSafeInput_ReturnsTrue(int[] input) => Assert.True(Aoc2024Day02Processor.IsSafe(input));

  [Theory]
  [InlineData(new[] { 1, 2, 3, 3, 5 })] // Contains Repeat
  [InlineData(new[] { 1, 2, 3, 2, 1 })] // Alternates Dir
  [InlineData(new[] { 1, 3, 5, 9, 10 })] // Big Jump
  [InlineData(new[] { 9, 5, 3, 2, 1 })] // Big Dip
  public void IsSafe_GivenUnsafeInput_ReturnsFalse(int[] input) => Assert.False(Aoc2024Day02Processor.IsSafe(input));

  [Fact]
  public void GetAlternates_GivenInput_ReturnsPermutations()
  {
    var permutations = Aoc2024Day02Processor.GetAlternates([1, 2, 3, 4, 5]);
    Assert.Equal(permutations, [[2, 3, 4, 5], [1, 3, 4, 5], [1, 2, 4, 5], [1, 2, 3, 5], [1, 2, 3, 4]]);
  }

  [Fact]
  public void ProcessPart1Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart1Solution(input);

    Assert.Equal("2", result);
  }


  [Fact]
  public void ProcessPart2Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart2Solution(input);

    Assert.Equal("4", result);
  }
}
