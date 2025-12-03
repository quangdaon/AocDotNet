using AdventOfCode.App.Challenges;
using Xunit;
using Direction = AdventOfCode.App.Challenges.Aoc2025Day01Processor.Direction;
using Instruction = AdventOfCode.App.Challenges.Aoc2025Day01Processor.Instruction;

namespace AdventOfCode.App.Tests.Challenges;

public class Aoc2025Day01ProcessorTest : ChallengeProcessorTests
{
  private readonly Aoc2025Day01Processor _processor;
  public Aoc2025Day01ProcessorTest() : base(2025, 1)
  {
    _processor = new Aoc2025Day01Processor();
  }

  [Theory]
  [InlineData("L29", Direction.Left, 29)]
  [InlineData("R13", Direction.Right, 13)]
  public void InstructionParse_GivenRow_ParsesInstruction(string row, Direction expectedDirection, int expectedSteps)
  {
    var result = Instruction.Parse(row);
    
    Assert.Equal(expectedDirection, result.Direction);
    Assert.Equal(expectedSteps, result.Steps);
  }

  [Fact]
  public void ProcessPart1Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart1Solution(input);

    Assert.Equal("3", result);
  }


  [Fact]
  public void ProcessPart2Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart2Solution(input);

    Assert.Equal("6", result);
  }
}
