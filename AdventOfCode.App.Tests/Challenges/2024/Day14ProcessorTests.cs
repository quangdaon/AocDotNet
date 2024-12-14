using AdventOfCode.App.Challenges;
using Xunit;

namespace AdventOfCode.App.Tests.Challenges;

public class Aoc2024Day14ProcessorTest : ChallengeProcessorTests
{
  private readonly Aoc2024Day14Processor _processor;

  public Aoc2024Day14ProcessorTest() : base(2024, 14)
  {
    _processor = new Aoc2024Day14Processor();
    _processor.SetSize(11, 7);
  }

  [Fact]
  public void ProcessPart1Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart1Solution(input);

    Assert.Equal("12", result);
  }

  [Fact]
  public void ProcessPart2Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart2Solution(input);

    Assert.Equal("???", result);
  }
}
