using AdventOfCode.App.Challenges;
using Xunit;

namespace AdventOfCode.App.Tests.Challenges;

public class Aoc2024Day15ProcessorTest : ChallengeProcessorTests
{
  private readonly Aoc2024Day15Processor _processor;
  public Aoc2024Day15ProcessorTest() : base(2024, 15)
  {
    _processor = new Aoc2024Day15Processor();
  }

  [Theory]
  [InlineData(1,"10092")]
  [InlineData(2,"2028")]
  public void ProcessPart1Solution_GivenSampleInputs_ReturnsProvidedResult(byte sample, string expectedResult)
  {
    var input = GetSampleInput(sample);

    var result = _processor.ProcessPart1Solution(input);

    Assert.Equal(expectedResult, result);
  }

  [Fact]
  public void ProcessPart2Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart2Solution(input);

    Assert.Equal("???", result);
  }
}
