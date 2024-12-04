using AdventOfCode.App.Challenges;
using Xunit;

namespace AdventOfCode.App.Tests.Challenges;

public class Aoc2024Day03ProcessorTest : ChallengeProcessorTests
{
  private readonly Aoc2024Day03Processor _processor;
  public Aoc2024Day03ProcessorTest() : base(2024, 3)
  {
    _processor = new Aoc2024Day03Processor();
  }

  [Fact]
  public void ProcessPart1Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput(1);

    var result = _processor.ProcessPart1Solution(input);

    Assert.Equal("161", result);
  }
  
  [Fact]
  public void ProcessPart2Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput(2);

    var result = _processor.ProcessPart2Solution(input);

    Assert.Equal("48", result);
  }
}
