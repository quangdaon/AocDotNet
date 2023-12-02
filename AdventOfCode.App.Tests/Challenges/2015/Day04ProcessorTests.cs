using AdventOfCode.App.Challenges;
using Xunit;

namespace AdventOfCode.App.Tests.Challenges;

public class Aoc2015Day04ProcessorTest : ChallengeProcessorTests
{
  private readonly Aoc2015Day04Processor _processor;
  public Aoc2015Day04ProcessorTest() : base(2015, 4)
  {
    _processor = new Aoc2015Day04Processor();
  }
  
  [Theory]
  [InlineData("abcdef", "609043")]
  [InlineData("pqrstuv", "1048970")]
  public void ProcessPart1Solution_GivenSampleInputs_ReturnsProvidedResult(string input, string expected)
  {
    var result = _processor.ProcessPart1Solution(input);
    Assert.Equal(expected, result);
  }
  
  [Theory]
  [InlineData("abcdef", "6742839")]
  [InlineData("pqrstuv", "5714438")]
  public void ProcessPart2Solution_GivenSampleInputs_ReturnsProvidedResult(string input, string expected)
  {
    var result = _processor.ProcessPart2Solution(input);
    Assert.Equal(expected, result);
  }
}
