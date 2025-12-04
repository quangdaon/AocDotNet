using AdventOfCode.App.Challenges;
using Xunit;

namespace AdventOfCode.App.Tests.Challenges;

public class Aoc2025Day02ProcessorTest : ChallengeProcessorTests
{
  private readonly Aoc2025Day02Processor _processor;

  public Aoc2025Day02ProcessorTest() : base(2025, 2)
  {
    _processor = new Aoc2025Day02Processor();
  }

  [Theory]
  [InlineData(2025, 2)]
  [InlineData(123, 2)]
  [InlineData(123454321, 2)]
  [InlineData(56767, 2)]
  [InlineData(56756, 2)]
  [InlineData(123124, 2)]
  [InlineData(121212, 2)]
  [InlineData(233233, 3)]
  public void Repeats_GivenNonRepeatingInput_ReturnsFalse(int input, int factor) =>
    Assert.False(_processor.Repeats(input, factor));

  [Theory]
  [InlineData(11, 2)]
  [InlineData(233233, 2)]
  [InlineData(9292, 2)]
  [InlineData(100100, 2)]
  [InlineData(1234512345, 2)]
  [InlineData(121212, 3)]
  [InlineData(1111, 4)]
  [InlineData(12121212, 4)]
  public void Repeats_GivenRepeatingInput_ReturnsTrue(int input, int factor) =>
    Assert.True(_processor.Repeats(input, factor));

  [Theory]
  [InlineData(12121212)]
  [InlineData(146146)]
  [InlineData(146146146)]
  public void IsInvalidPart2_GivenRepeatingInput_ReturnsTrue(int input) =>
    Assert.True(_processor.IsInvalidPart2(input));

  [Theory]
  [InlineData(121212127)]
  [InlineData(1461467)]
  [InlineData(1461461467)]
  public void IsInvalidPart2_GivenNonRepeatingInput_ReturnsFalse(int input) =>
    Assert.False(_processor.IsInvalidPart2(input));

  [Fact]
  public void ProcessPart1Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart1Solution(input);

    Assert.Equal("1227775554", result);
  }

  [Fact]
  public void ProcessPart2Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart2Solution(input);

    Assert.Equal("4174379265", result);
  }
}
