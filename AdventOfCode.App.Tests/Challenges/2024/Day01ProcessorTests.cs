using AdventOfCode.App.Challenges;
using Xunit;

namespace AdventOfCode.App.Tests.Challenges;

public class Aoc2024Day01ProcessorTest : ChallengeProcessorTests
{
  private readonly Aoc2024Day01Processor _processor;

  public Aoc2024Day01ProcessorTest() : base(2024, 1)
  {
    _processor = new Aoc2024Day01Processor();
  }

  [Fact]
  public void ProcessPart1Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart1Solution(input);

    Assert.Equal("11", result);
  }

  [Fact]
  public void ProcessPart2Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart2Solution(input);

    Assert.Equal("31", result);
  }

  [Theory]
  [InlineData(new[] { "1 4", "4  9", "2 6" }, new[] { 1, 2, 4 }, new[] { 4, 6, 9 })]
  [InlineData(new[] { "6    8", "1  2", "8  5" }, new[] { 1, 6, 8 }, new[] { 2, 5, 8 })]
  [InlineData(new[] { "37 83", "2   923", "872346 82" }, new[] { 2, 37, 872346 }, new[] { 82, 83, 923 })]
  public void SplitRows_GivenValidInputs_ShouldReturnSortedArrays(string[] inputs, int[] expectedLeft,
    int[] expectedRight)
  {
    var (left, right) = _processor.SplitRows(inputs);

    Assert.Equal(expectedLeft, left);
    Assert.Equal(expectedRight, right);
  }
}
