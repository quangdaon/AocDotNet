using System;
using AdventOfCode.App.Challenges;
using Xunit;

namespace AdventOfCode.App.Tests.Challenges;

public class Aoc2024Day13ProcessorTest : ChallengeProcessorTests
{
  private readonly Aoc2024Day13Processor _processor;

  public Aoc2024Day13ProcessorTest() : base(2024, 13)
  {
    _processor = new Aoc2024Day13Processor();
  }

  [Theory]
  [InlineData(0, 94, 34, 22, 67, 8400, 5400)]
  [InlineData(1, 26, 66, 67, 21, 12748, 12176)]
  [InlineData(2, 17, 86, 84, 37, 7870, 6450)]
  [InlineData(3, 69, 23, 27, 71, 18641, 10279)]
  public void ConfigParse_GivenSampleInput_ReturnsProvidedResults(int index, int expectedAx, int expectedAy,
    int expectedBx, int expectedBy, int expectedPrizeX, int expectedPrizeY)
  {
    var input = GetSampleInput();
    var configs = input.Split(Environment.NewLine + Environment.NewLine);

    var selected = configs[index];
    var result = Aoc2024Day13Processor.ClawMachineConfiguration.Parse(selected);

    Assert.Equal(expectedAx, result.ButtonAOutput.X);
    Assert.Equal(expectedAy, result.ButtonAOutput.Y);
    Assert.Equal(expectedBx, result.ButtonBOutput.X);
    Assert.Equal(expectedBy, result.ButtonBOutput.Y);
    Assert.Equal(expectedPrizeX, result.PrizeTarget.X);
    Assert.Equal(expectedPrizeY, result.PrizeTarget.Y);
  }

  [Theory]
  [InlineData(0, 280)]
  [InlineData(2, 200)]
  public void ConfigCompute_GivenPossibleConfiguration_ReturnsProvidedCost(int index, int expected)
  {
    var input = GetSampleInput();
    var configs = input.Split(Environment.NewLine + Environment.NewLine);

    var selected = configs[index];
    var config = Aoc2024Day13Processor.ClawMachineConfiguration.Parse(selected);
    
    var result = config.ComputeCost();
    
    Assert.Equal(expected, result);
  }

  [Theory]
  [InlineData(1)]
  [InlineData(3)]
  public void ConfigCompute_GivenImpossibleConfiguration_ReturnsZero(int index)
  {
    var input = GetSampleInput();
    var configs = input.Split(Environment.NewLine + Environment.NewLine);

    var selected = configs[index];
    var config = Aoc2024Day13Processor.ClawMachineConfiguration.Parse(selected);
    
    var result = config.ComputeCost();
    
    Assert.Equal(0, result);
  }

  [Fact]
  public void ProcessPart1Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart1Solution(input);

    Assert.Equal("480", result);
  }

  [Fact]
  public void ProcessPart2Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart2Solution(input);

    Assert.Equal("???", result);
  }
}