using AdventOfCode.App.Challenges;
using Xunit;

namespace AdventOfCode.App.Tests.Challenges;

public class Aoc2023Day01ProcessorTest : ChallengeProcessorTests
{
  private readonly Aoc2023Day01Processor _processor;
  public Aoc2023Day01ProcessorTest() : base(2023, 1)
  {
    _processor = new Aoc2023Day01Processor();
  }

  [Theory]
  [InlineData("1abc2", 12)]
  [InlineData("pqr3stu8vwx", 38)]
  [InlineData("a1b2c3d4e5f", 15)]
  [InlineData("treb7uchet", 77)]
  public void GetPart1CalibrationCode_GivenSampleInputs_ReturnsProvidedResult(string line, int expectedResult)
  {
    var result = _processor.GetPart1CalibrationCode(line);
    
    Assert.Equal(expectedResult, result);
  }

  [Fact]
  public void ProcessPart1Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput(1);

    var result = _processor.ProcessPart1Solution(input);

    Assert.Equal("142", result);
  }

  [Theory]
  [InlineData("two1nine", 29)]
  [InlineData("eightwothree", 83)]
  [InlineData("abcone2threexyz", 13)]
  [InlineData("xtwone3four", 24)]
  [InlineData("4nineeightseven2", 42)]
  [InlineData("zoneight234", 14)]
  [InlineData("7pqrstsixteen", 76)]
  public void GetPart2CalibrationCode_GivenSampleInputs_ReturnsProvidedResult(string line, int expectedResult)
  {
    var result = _processor.GetPart2CalibrationCode(line);
    
    Assert.Equal(expectedResult, result);
  }

  [Fact]
  public void ProcessPart2Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput(2);

    var result = _processor.ProcessPart2Solution(input);

    Assert.Equal("281", result);
  }
}
