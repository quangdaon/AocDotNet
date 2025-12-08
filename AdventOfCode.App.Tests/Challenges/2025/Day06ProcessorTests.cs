using AdventOfCode.App.Challenges;
using Xunit;

namespace AdventOfCode.App.Tests.Challenges;

public class Aoc2025Day06ProcessorTest : ChallengeProcessorTests
{
  private readonly Aoc2025Day06Processor _processor;
  public Aoc2025Day06ProcessorTest() : base(2025, 6)
  {
    _processor = new Aoc2025Day06Processor();
  }

  [Fact]
  public void ProcessPart1Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart1Solution(input);

    Assert.Equal("4277556", result);
  }

  [Fact]
  public void ProcessPart2Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart2Solution(input);

    Assert.Equal("3263827", result);
  }
}
