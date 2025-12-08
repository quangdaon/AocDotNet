using AdventOfCode.App.Challenges;
using Xunit;

namespace AdventOfCode.App.Tests.Challenges;

public class Aoc2025Day04ProcessorTest : ChallengeProcessorTests
{
  private readonly Aoc2025Day04Processor _processor;
  public Aoc2025Day04ProcessorTest() : base(2025, 4)
  {
    _processor = new Aoc2025Day04Processor();
  }

  [Fact]
  public void ProcessPart1Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart1Solution(input);

    Assert.Equal("13", result);
  }

  [Fact]
  public void ProcessPart2Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart2Solution(input);

    Assert.Equal("43", result);
  }
}
