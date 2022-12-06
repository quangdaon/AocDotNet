using AdventOfCode.App.Challenges;
using Xunit;

namespace AdventOfCode.App.Tests.Challenges;

public class Aoc2015Day01ProcessorTest : ChallengeProcessorTests
{
  private readonly Aoc2015Day01Processor processor;
  public Aoc2015Day01ProcessorTest() : base(2015, 1)
  {
    processor = new Aoc2015Day01Processor();
  }

  [Theory]
  [InlineData("(())", "0")]
  [InlineData("()()", "0")]
  [InlineData("(((", "3")]
  [InlineData("(()(()(", "3")]
  [InlineData("))(((((", "3")]
  [InlineData("())", "-1")]
  [InlineData("))(", "-1")]
  [InlineData(")))", "-3")]
  [InlineData(")())())", "-3")]
  public void ProcessPart1Solution_GivenSampleInputs_ReturnsProvidedResult(string input, string expected)
  {
    var result = processor.ProcessPart1Solution(input);
    Assert.Equal(expected, result);
  }
  
  [Theory]
  [InlineData(")", "1")]
  [InlineData("()())", "5")]
  public void ProcessPart2Solution_GivenSampleInputs_ReturnsProvidedResult(string input, string expected)
  {
    var result = processor.ProcessPart2Solution(input);
    Assert.Equal(expected, result);
  }
}