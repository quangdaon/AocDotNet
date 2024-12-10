using AdventOfCode.App.Challenges;
using Xunit;

namespace AdventOfCode.App.Tests.Challenges;

public class Aoc2024Day10ProcessorTest : ChallengeProcessorTests
{
  private readonly Aoc2024Day10Processor _processor;
  public Aoc2024Day10ProcessorTest() : base(2024, 10)
  {
    _processor = new Aoc2024Day10Processor();
  }

  [Fact]
  public void ProcessPart1Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart1Solution(input);

    Assert.Equal("36", result);
  }
  
  [Fact]
  public void ProcessPart2Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart2Solution(input);

    Assert.Equal("81", result);
  }
}
