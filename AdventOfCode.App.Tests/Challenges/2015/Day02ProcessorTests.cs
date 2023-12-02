using AdventOfCode.App.Challenges;
using Xunit;

namespace AdventOfCode.App.Tests.Challenges;

public class Aoc2015Day02ProcessorTest : ChallengeProcessorTests
{
  private readonly Aoc2015Day02Processor _processor;
  public Aoc2015Day02ProcessorTest() : base(2015, 2)
  {
    _processor = new Aoc2015Day02Processor();
  }

  [Theory]
  [InlineData("2x3x4", 58)]
  [InlineData("1x1x10", 43)]
  public void CalculateWrappingPaper_GivenSampleInputs_ReturnsProvidedResult(string input, int expected)
  {
    var result = _processor.CalculateWrappingPaper(input);
    Assert.Equal(expected, result);
  }

  [Theory]
  [InlineData("2x3x4", 34)]
  [InlineData("1x1x10", 14)]
  public void CalculateRibbon_GivenSampleInputs_ReturnsProvidedResult(string input, int expected)
  {
    var result = _processor.CalculateRibbon(input);
    Assert.Equal(expected, result);
  }
}
