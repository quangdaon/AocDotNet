using AdventOfCode.App.Challenges;
using Xunit;
using static AdventOfCode.App.Challenges.Aoc2015Day06Processor;

namespace AdventOfCode.App.Tests.Challenges;

public class Aoc2015Day06ProcessorTest : ChallengeProcessorTests
{
    private readonly Aoc2015Day06Processor processor;

    public Aoc2015Day06ProcessorTest() : base(2015, 6)
    {
        processor = new Aoc2015Day06Processor();
    }

    [Theory]
    [InlineData("turn on 0,0 through 999,999", "1000000")]
    [InlineData("toggle 0,0 through 999,0", "1000")]
    [InlineData("turn off 499,499 through 500,500", "0")]
    public void ProcessPart1Solution_GivenSampleInputs_ReturnsProvidedResult(string input, string expected)
    {
        var result = processor.ProcessPart1Solution(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ProcessPart1Instruction_GivenMultipleInstructions_IsCumulative()
    {
        var lights = new int[1000000];
        
        processor.ProcessPart1Instruction(lights, "turn on 0,0 through 999,0");
        Assert.Equal(1000, GetOnCount(lights));

        processor.ProcessPart1Instruction(lights, "turn off 0,0 through 499,0");
        Assert.Equal(500, GetOnCount(lights));

        processor.ProcessPart1Instruction(lights, "toggle 450,0 through 549,0");
        Assert.Equal(500, GetOnCount(lights));
    }

    [Theory]
    [InlineData("turn on 0,0 through 0,0", "1")]
    [InlineData("toggle 0,0 through 999,999", "2000000")]
    public void ProcessPart2Solution_GivenSampleInputs_ReturnsProvidedResult(string input, string expected)
    {
        var result = processor.ProcessPart2Solution(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ProcessPart2Instruction_GivenMultipleInstructions_IsCumulative()
    {
        var lights = new int[1000000];

        processor.ProcessPart2Instruction(lights, "turn on 0,0 through 999,0");
        Assert.Equal(1000, GetOnCount(lights));

        processor.ProcessPart2Instruction(lights, "turn off 0,0 through 499,0");
        Assert.Equal(500, GetOnCount(lights));

        processor.ProcessPart2Instruction(lights, "toggle 450,0 through 549,0");
        Assert.Equal(700, GetOnCount(lights));

        processor.ProcessPart2Instruction(lights, "turn off 440,0 through 459,0");
        Assert.Equal(690, GetOnCount(lights));
    }
}