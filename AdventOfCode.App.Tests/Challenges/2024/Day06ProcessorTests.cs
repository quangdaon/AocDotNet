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
    
    Assert.Equal(expected, map);
    Assert.Equal(1, result);
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

  private bool[][] CreateMap(string[] map)
  {
    return map.Select(e => e.ToCharArray().Select(c => c == '1').ToArray()).ToArray();
  }
}
