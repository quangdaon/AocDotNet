using System;
using AdventOfCode.App.Challenges;
using AdventOfCode.App.Utilities;
using Xunit;

namespace AdventOfCode.App.Tests.Challenges;

public class Aoc2024Day14ProcessorTest : ChallengeProcessorTests
{
  private readonly Aoc2024Day14Processor _processor;

  private const int WIDTH = 11;
  private const int HEIGHT = 7;

  public Aoc2024Day14ProcessorTest() : base(2024, 14)
  {
    _processor = new Aoc2024Day14Processor();
    _processor.SetSize(WIDTH, HEIGHT);
  }

  [Theory]
  [InlineData(0, 0, 4, 3, -3)]
  [InlineData(1, 6, 3, -1, -3)]
  public void RobotParse_GivenInputRow_ReturnDefinedRobot(int index, int expectedStartX, int expectedStartY,
    int expectedVelocityX, int expectedVelocityY)
  {
    var input = GetSampleInput();
    var rows = input.ToRows();

    var row = rows[index];
    var result = Aoc2024Day14Processor.Robot.Parse(row, WIDTH, HEIGHT);

    Assert.Equal(expectedStartX, result.Start.X);
    Assert.Equal(expectedStartY, result.Start.Y);
    Assert.Equal(expectedVelocityX, result.Velocity.X);
    Assert.Equal(expectedVelocityY, result.Velocity.Y);
  }

  [Theory]
  [InlineData(0, 2, 4)]
  [InlineData(1, 4, 1)]
  [InlineData(2, 6, 5)]
  [InlineData(3, 8, 2)]
  public void ProjectPosition_GivenTime_ReturnsProjectedPosition(int time, int expectedX, int expectedY)
  {
    var robot = new Aoc2024Day14Processor.Robot((2, 4), (2, -3), WIDTH, HEIGHT);
    var result = robot.ProjectPosition(time);

    Assert.Equal(expectedX, result.X);
    Assert.Equal(expectedY, result.Y);
  }

  [Fact]
  public void ProcessPart1Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart1Solution(input);

    Assert.Equal("12", result);
  }

  [Fact(Skip = "Part 2 is Untestable")]
  public void ProcessPart2Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart2Solution(input);

    Assert.Equal("???", result);
  }
}
