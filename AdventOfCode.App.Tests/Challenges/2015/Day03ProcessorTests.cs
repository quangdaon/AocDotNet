using AdventOfCode.App.Challenges;
using Xunit;

namespace AdventOfCode.App.Tests.Challenges;

public class Aoc2015Day03ProcessorTest : ChallengeProcessorTests
{
  private readonly Aoc2015Day03Processor _processor;
  public Aoc2015Day03ProcessorTest() : base(2015, 3)
  {
    _processor = new Aoc2015Day03Processor();
  }
  
  [Theory]
  [InlineData(">", "2")]
  [InlineData("^>v<", "4")]
  [InlineData("^v^v^v^v^v", "2")]
  public void ProcessPart1Solution_GivenSampleInputs_ReturnsProvidedResult(string input, string expected)
  {
    var result = _processor.ProcessPart1Solution(input);
    Assert.Equal(expected, result);
  }
  
  [Theory]
  [InlineData("^v", "3")]
  [InlineData("^>v<", "3")]
  [InlineData("^v^v^v^v^v", "11")]
  public void ProcessPart2Solution_GivenSampleInputs_ReturnsProvidedResult(string input, string expected)
  {
    var result = _processor.ProcessPart2Solution(input);
    Assert.Equal(expected, result);
  }
}
