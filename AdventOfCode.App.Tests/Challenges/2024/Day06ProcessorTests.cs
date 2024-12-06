using System.Collections.Generic;
using System.Linq;
using AdventOfCode.App.Challenges;
using Xunit;

namespace AdventOfCode.App.Tests.Challenges;

public class Aoc2024Day06ProcessorTest : ChallengeProcessorTests
{
  private readonly Aoc2024Day06Processor _processor;

  public Aoc2024Day06ProcessorTest() : base(2024, 6)
  {
    _processor = new Aoc2024Day06Processor();
  }

  [Fact]
  public void ParseInput_GivenInput_ProducesCorrectOutput()
  {
    const string input = """
                         ...#.
                         ..#..
                         .#.^#
                         ...#.
                         #.#.#
                         """;

    var expected = CreateMap([
      "00010",
      "00100",
      "01001",
      "00010",
      "10101",
    ]);

    var (map, startX, startY) = Aoc2024Day06Processor.ParseInput(input);

    Assert.Equal(expected, map);
    Assert.Equal(3, startX);
    Assert.Equal(2, startY);
  }

  [Fact]
  public void PatrolUp_GivenObstacle_Stops()
  {
    var map = CreateMap([
      "00010",
      "00000",
      "00000"
    ]);
    var mask = CreateMap([
      "00000",
      "00000",
      "00000"
    ]);
    var expected = CreateMap([
      "00000",
      "00010",
      "00010"
    ]);

    var result = Aoc2024Day06Processor.PatrolUp(mask, map, 3, 2);

    Assert.Equal(expected, mask);
    Assert.Equal(1, result);
  }

  [Fact]
  public void PatrolUp_GivenMask_ReservesMask()
  {
    var map = CreateMap([
      "00010",
      "00000",
      "00000"
    ]);
    var mask = CreateMap([
      "00000",
      "00111",
      "00000"
    ]);
    var expected = CreateMap([
      "00000",
      "00111",
      "00010"
    ]);

    var result = Aoc2024Day06Processor.PatrolUp(mask, map, 3, 2);

    Assert.Equal(expected, mask);
    Assert.Equal(1, result);
  }

  [Fact]
  public void PatrolUp_GivenClearance_ReturnsNegativeOne()
  {
    var map = CreateMap([
      "00000",
      "00000",
      "00000"
    ]);
    var mask = CreateMap([
      "00000",
      "00000",
      "00000"
    ]);
    var expected = CreateMap([
      "00010",
      "00010",
      "00000"
    ]);

    var result = Aoc2024Day06Processor.PatrolUp(mask, map, 3, 1);

    Assert.Equal(expected, mask);
    Assert.Equal(-1, result);
  }

  [Fact]
  public void PatrolDown_GivenObstacle_Stops()
  {
    var map = CreateMap([
      "00000",
      "00000",
      "00010"
    ]);
    var mask = CreateMap([
      "00000",
      "00000",
      "00000"
    ]);
    var expected = CreateMap([
      "00010",
      "00010",
      "00000",
    ]);

    var result = Aoc2024Day06Processor.PatrolDown(mask, map, 3, 0);

    Assert.Equal(expected, mask);
    Assert.Equal(1, result);
  }

  [Fact]
  public void PatrolDown_GivenMask_ReservesMask()
  {
    var map = CreateMap([
      "00000",
      "00000",
      "00010",
    ]);
    var mask = CreateMap([
      "00000",
      "00111",
      "00000"
    ]);
    var expected = CreateMap([
      "00010",
      "00111",
      "00000"
    ]);

    var result = Aoc2024Day06Processor.PatrolDown(mask, map, 3, 0);

    Assert.Equal(expected, mask);
    Assert.Equal(1, result);
  }

  [Fact]
  public void PatrolDown_GivenClearance_ReturnsNegativeOne()
  {
    var map = CreateMap([
      "00000",
      "00000",
      "00000"
    ]);
    var mask = CreateMap([
      "00000",
      "00000",
      "00000"
    ]);
    var expected = CreateMap([
      "00000",
      "00010",
      "00010"
    ]);

    var result = Aoc2024Day06Processor.PatrolDown(mask, map, 3, 1);

    Assert.Equal(expected, mask);
    Assert.Equal(-1, result);
  }

  [Theory]
  [InlineData("00000000", "00000000", "11111111", 1, 0, -1)] // LTR No Obstacle
  [InlineData("00000000", "00000000", "00011111", 1, 3, -1)] // LTR No Obstacle, Offset Start
  [InlineData("00000100", "00000000", "11111000", 1, 0, 4)] // LTR Obstacle
  [InlineData("00000100", "00000000", "00011000", 1, 3, 4)] // LTR Obstacle, Offset Start
  [InlineData("00000100", "00000010", "11111010", 1, 0, 4)] // LTR Keep Existing Mask
  [InlineData("00000000", "00000000", "11111111", -1, 7, -1)] // RTL No Obstacle
  [InlineData("00000000", "00000000", "11110000", -1, 3, -1)] // RTL No Obstacle, Offset Start
  [InlineData("00100000", "00000000", "00011111", -1, 7, 3)] // RTL Obstacle
  [InlineData("00100000", "00000000", "00011100", -1, 5, 3)] // RTL Obstacle, Offset Start
  [InlineData("00100000", "01000000", "01011111", -1, 7, 3)] // RTL Keep Existing Mask
  public void PatrolHorizontal_GivenDirections_ExecutesExpectedBehavior(string mapRow, string maskRow, string expectedRow, int dir, int x, int expectedIndex)
  {
    var map = CreateMapRow(mapRow);
    var mask = CreateMapRow(maskRow);
    var expected = CreateMapRow(expectedRow);

    var result = Aoc2024Day06Processor.PatrolHorizontal(dir, mask, map, x);

    Assert.Equal(expected, mask);
    Assert.Equal(expectedIndex, result);
  }

  [Fact]
  public void ProcessPart1Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart1Solution(input);

    Assert.Equal("41", result);
  }


  [Fact]
  public void ProcessPart2Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart2Solution(input);

    Assert.Equal("???", result);
  }

  private bool[] CreateMapRow(string row) => row.ToCharArray().Select(c => c == '1').ToArray();
  private bool[][] CreateMap(string[] map) => map.Select(CreateMapRow).ToArray();
}