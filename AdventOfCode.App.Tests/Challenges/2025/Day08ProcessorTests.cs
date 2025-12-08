using AdventOfCode.App.Challenges;
using Xunit;

namespace AdventOfCode.App.Tests.Challenges;

public class Aoc2025Day08ProcessorTest : ChallengeProcessorTests
{
  private readonly Aoc2025Day08Processor _processor;
  public Aoc2025Day08ProcessorTest() : base(2025, 8)
  {
    _processor = new Aoc2025Day08Processor();
    Aoc2025Day08Processor.Iterations = 10;
  }

  [Fact]
  public void ProcessPart1Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart1Solution(input);

    Assert.Equal("40", result);
  }

  [Fact]
  public void ProcessPart2Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart2Solution(input);

    Assert.Equal("25272", result);
  }
}
