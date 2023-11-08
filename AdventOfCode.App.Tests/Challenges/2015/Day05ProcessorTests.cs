using AdventOfCode.App.Challenges;
using Xunit;

namespace AdventOfCode.App.Tests.Challenges;

public class Aoc2015Day05ProcessorTest : ChallengeProcessorTests
{
  private readonly Aoc2015Day05Processor processor;
  public Aoc2015Day05ProcessorTest() : base(2015, 5)
  {
    processor = new Aoc2015Day05Processor();
  }

  [Theory]
  [InlineData("ugknbfddgicrmopn", true)]
  [InlineData("aaa", true)]
  [InlineData("jchzalrnumimnmhp", false)]
  [InlineData("haegwjzuvuyypxyu", false)]
  [InlineData("dvszwmarrgswjxmb", false)]
  public void Part1Rules_GivenSampleInput_ReturnsProvidedResult(string input, bool expected)
  {
    var result = processor.MeetsPart1Rules(input);
    Assert.Equal(expected, result);
  }
  
  [Theory]
  [InlineData("qjhvhtzxzqqjkmpb", true)]
  [InlineData("xxyxx", true)]
  [InlineData("uurcxstgmygtbstg", false)]
  [InlineData("ieodomkazucvgmuy", false)]
  public void Part2Rules_GivenSampleInput_ReturnsProvidedResult(string input, bool expected)
  {
    var result = processor.MeetsPart2Rules(input);
    Assert.Equal(expected, result);
  }
}
