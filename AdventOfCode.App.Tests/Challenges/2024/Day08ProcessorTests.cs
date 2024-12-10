using System;
using System.Collections.Generic;
using AdventOfCode.App.Challenges;
using AdventOfCode.App.Utilities;
using Xunit;
using Coordinates = (int x, int y);

namespace AdventOfCode.App.Tests.Challenges;

public class Aoc2024Day08ProcessorTest : ChallengeProcessorTests
{
  private readonly Aoc2024Day08Processor _processor;

  public Aoc2024Day08ProcessorTest() : base(2024, 8)
  {
    _processor = new Aoc2024Day08Processor();
  }

  [Fact]
  public void GetAntinodes_GivenPair_ReturnsAntinodes()
  {
    var result = Aoc2024Day08Processor.GetAntinodes((4, 3), (5, 5), 10, 10);

    Assert.Equal(2, result.Length);
    Assert.Contains(result, x => x == (3, 1));
    Assert.Contains(result, x => x == (6, 7));
  }

  [Fact]
  public void GetUnboundedAntinodes_GivenPair_ReturnsAntinodes()
  {
    var result = Aoc2024Day08Processor.GetUnboundedAntinodes((0,0), (3,1), 10, 10);

    Assert.Equal(4, result.Length);
    Assert.Contains(result, x => x == (0, 0));
    Assert.Contains(result, x => x == (3, 1));
    Assert.Contains(result, x => x == (6, 2));
    Assert.Contains(result, x => x == (9, 3));
  }

  [Fact]
  public void GetAntinodes_GivenPair_IgnoresOutOfBoundAntinodes()
  {
    var result = Aoc2024Day08Processor.GetAntinodes((4, 3), (8, 4), 10, 10);

    Assert.Single(result);
    Assert.Contains(result, x => x == (0, 2));
  }

  [Fact]
  public void GetAntinodes_GivenList_ReturnsAntinodes()
  {
    Coordinates[] nodes = [(4, 3), (5, 5), (8, 4)];
    var result = Aoc2024Day08Processor.GetAntinodes(nodes, 10, 10);

    Assert.Equal(4, result.Length);
    Assert.Contains(result, x => x == (3, 1));
    Assert.Contains(result, x => x == (6, 7));
    Assert.Contains(result, x => x == (0, 2));
    Assert.Contains(result, x => x == (2, 6));
  }

  [Fact]
  public void GetNodes_GivenSampleInputs_ReturnsNodes()
  {
    var input = GetSampleInput();

    var result = Aoc2024Day08Processor.GetNodes(input.ToRows());

    var expected = new Dictionary<char, Coordinates[]>
    {
      { '0', [(8, 1), (5, 2), (7, 3), (4, 4)] },
      { 'A', [(6, 5), (8, 8), (9, 9)] }
    };

    Assert.Equal(expected, result);
  }

  [Fact]
  public void ProcessPart1Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart1Solution(input);

    Assert.Equal("14", result);
  }

  [Fact]
  public void ProcessPart2Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart2Solution(input);

    Assert.Equal("34", result);
  }
}
