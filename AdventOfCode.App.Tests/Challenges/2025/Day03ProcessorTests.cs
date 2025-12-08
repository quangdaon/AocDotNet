using AdventOfCode.App.Challenges;
using Xunit;

namespace AdventOfCode.App.Tests.Challenges;

public class Aoc2025Day03ProcessorTest : ChallengeProcessorTests
{
  private readonly Aoc2025Day03Processor _processor;
  public Aoc2025Day03ProcessorTest() : base(2025, 3)
  {
    _processor = new Aoc2025Day03Processor();
  }

  [Theory]
  [InlineData("987654321111111", 98)]
  [InlineData("811111111111119", 89)]
  [InlineData("234234234234278", 78)]
  [InlineData("818181911112111", 92)]
  public void GetMaxJoltage_GivenBattery_ReturnsMaxJoltage(string input, int expected)
  {
    var result = _processor.GetMaxJoltage(input);
    Assert.Equal(expected, result);
  }

  [Theory]
  [InlineData("987654321111111", 987654321111)]
  [InlineData("811111111111119", 811111111119)]
  [InlineData("234234234234278", 434234234278)]
  [InlineData("818181911112111", 888911112111)]
  public void GetSlidingMaxJoltage_GivenBattery_ReturnsMaxJoltage(string input, long expected)
  {
    var result = _processor.GetSlidingMaxJoltage(input, 12);
    Assert.Equal(expected, result);
  }

  [Fact]
  public void ProcessPart1Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart1Solution(input);

    Assert.Equal("357", result);
  }

  [Fact]
  public void ProcessPart2Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart2Solution(input);

    Assert.Equal("3121910778619", result);
  }
}
