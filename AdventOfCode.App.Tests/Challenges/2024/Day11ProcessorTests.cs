using System;
using System.Linq;
using AdventOfCode.App.Challenges;
using AdventOfCode.App.Utilities;
using Xunit;

namespace AdventOfCode.App.Tests.Challenges;

public class Aoc2024Day11ProcessorTest : ChallengeProcessorTests
{
  private readonly Aoc2024Day11Processor _processor;

  public Aoc2024Day11ProcessorTest() : base(2024, 11)
  {
    _processor = new Aoc2024Day11Processor();
  }

  [Fact]
  public void ApplyRule_GivenZero_ReturnsOne()
  {
    var result = Aoc2024Day11Processor.ApplyRule(0).ToArray();

    Assert.Single(result);
    Assert.Equal(1, result.First());
  }

  [Theory]
  [InlineData(1024, 10, 24)]
  [InlineData(1204, 12, 4)]
  [InlineData(8888, 88, 88)]
  [InlineData(11, 1, 1)]
  public void ApplyRule_GivenEvenDigits_ReturnsCorrectResult(int input, int expectedLeft, int expectedRight)
  {
    var result = Aoc2024Day11Processor.ApplyRule(input).ToArray();

    Assert.Equal(2, result.Length);
    Assert.Equal(expectedLeft, result.First());
    Assert.Equal(expectedRight, result.Last());
  }

  [Theory]
  [InlineData(1, 2024)]
  [InlineData(253, 512072)]
  public void ApplyRule_GivenUnmatchedRule_ReturnsInputMultiplied(int input, int expected)
  {
    var result = Aoc2024Day11Processor.ApplyRule(input).ToArray();

    Assert.Single(result);
    Assert.Equal(expected, result.First());
  }

  [Theory]
  [InlineData(1024, 10, 24)]
  [InlineData(1204, 12, 4)]
  [InlineData(8888, 88, 88)]
  [InlineData(11, 1, 1)]
  public void SplitNumber_GivenEvenDigits_ReturnsCorrectResult(int input, int expectedLeft, int expectedRight)
  {
    var result = Aoc2024Day11Processor.SplitNumber(input).ToArray();

    Assert.Equal(2, result.Length);
    Assert.Equal(expectedLeft, result.First());
    Assert.Equal(expectedRight, result.Last());
  }

  [Theory]
  [InlineData(88858)]
  [InlineData(234)]
  public void SplitNumber_GivenOddDigits_ThrowsInvalidOperation(int input)
  {
    Assert.Throws<InvalidOperationException>(() => Aoc2024Day11Processor.SplitNumber(input));
  }

  [Theory]
  [InlineData("0 1 10 99 999", "1 2024 1 0 9 9 2021976")]
  [InlineData("125 17", "253000 1 7")]
  [InlineData("253000 1 7", "253 0 2024 14168")]
  public void Blink_GivenSampleInputs_ReturnsProvidedResult(string inputString, string expectedString)
  {
    var input = inputString.ToLongs();
    var expected = expectedString.ToLongs();
    var result = Aoc2024Day11Processor.Blink(input);

    Assert.Equal(expected, result);
  }

  [Theory]
  [InlineData(1, "253000 1 7")]
  [InlineData(2, "253 0 2024 14168")]
  [InlineData(3, "512072 1 20 24 28676032")]
  [InlineData(6, "2097446912 14168 4048 2 0 2 4 40 48 2024 40 48 80 96 2 8 6 7 6 0 3 2")]
  public void Blink_GivenCount_ReturnsProvidedResult(int count, string expectedString)
  {
    long[] initial = [125, 17];
    var expected = expectedString.ToLongs();
    var result = Aoc2024Day11Processor.Blink(initial, count);

    Assert.Equal(expected, result);
  }

  [Fact]
  public void ProcessPart1Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart1Solution(input);

    Assert.Equal("55312", result);
  }
  
  [Fact]
  public void ProcessPart2Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart2Solution(input);

    Assert.Equal("65601038650482", result);
  }
}