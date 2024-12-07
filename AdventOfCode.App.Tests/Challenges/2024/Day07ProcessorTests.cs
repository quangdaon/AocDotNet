using AdventOfCode.App.Challenges;
using Xunit;

namespace AdventOfCode.App.Tests.Challenges;

public class Aoc2024Day07ProcessorTest : ChallengeProcessorTests
{
  private readonly Aoc2024Day07Processor _processor;

  public Aoc2024Day07ProcessorTest() : base(2024, 7)
  {
    _processor = new Aoc2024Day07Processor();
  }

  [Fact]
  public void ParseExpression_GivenString_ReturnsParsedExpression()
  {
    var (result, operands) = Aoc2024Day07Processor.ParseExpression("123: 4 8 22");
    long[] expectedOperands = [4, 8, 22];

    Assert.Equal(123, result);
    Assert.Equal(expectedOperands, operands);
  }

  [Theory]
  [InlineData(23, 23, true)]
  [InlineData(23, 24, false)]
  public void Evaluate_GivenSingleOperand_ReturnsMatch(long target, long operand, bool expectedResult)
  {
    var result = Aoc2024Day07Processor.Evaluate(target, [operand]);

    Assert.Equal(expectedResult, result);
  }

  [Theory]
  [InlineData(190, new long[] { 10, 19 })]
  [InlineData(3267, new long[] { 81, 40, 27 })]
  [InlineData(292, new long[] { 11, 6, 16, 20 })]
  public void Evaluate_GivenCompatibleInputs_ReturnsTrue(long target, long[] operands)
  {
    var result = Aoc2024Day07Processor.Evaluate(target, operands);

    Assert.True(result);
  }

  [Theory]
  [InlineData(83, new long[] { 17, 5 })]
  [InlineData(161011, new long[] { 16, 10, 13 })]
  [InlineData(21037, new long[] { 9, 7, 18, 13 })]
  [InlineData(156, new long[] { 15, 6 })]
  [InlineData(7290, new long[] { 6, 8, 6, 15 })]
  public void Evaluate_GivenIncompatibleInputs_ReturnsFalse(long target, long[] operands)
  {
    var result = Aoc2024Day07Processor.Evaluate(target, operands);

    Assert.False(result);
  }

  [Theory]
  [InlineData(190, new long[] { 10, 19 })]
  [InlineData(3267, new long[] { 81, 40, 27 })]
  [InlineData(156, new long[] { 15, 6 })]
  [InlineData(292, new long[] { 11, 6, 16, 20 })]
  [InlineData(7290, new long[] { 6, 8, 6, 15 })]
  [InlineData(486, new long[] { 6, 8, 6 })]
  [InlineData(41257, new long[] { 4, 1, 1, 2, 57 })]
  public void EvaluateWithConcatenation_GivenCompatibleInputs_ReturnsTrue(long target, long[] operands)
  {
    var result = Aoc2024Day07Processor.Evaluate(target, operands, true);

    Assert.True(result);
  }

  [Theory]
  [InlineData(83, new long[] { 17, 5 })]
  [InlineData(161011, new long[] { 16, 10, 13 })]
  [InlineData(21037, new long[] { 9, 7, 18, 13 })]
  [InlineData(127, new long[] { 3, 4, 8 })]
  public void EvaluateWithConcatenation_GivenIncompatibleInputs_ReturnsFalse(long target, long[] operands)
  {
    var result = Aoc2024Day07Processor.Evaluate(target, operands, true);

    Assert.False(result);
  }

  [Theory]
  [InlineData(2334, 34, 23)]
  [InlineData(15, 5, 1)]
  [InlineData(98435, 5, 9843)]
  [InlineData(98435, 35, 984)]
  [InlineData(98435, 435, 98)]
  [InlineData(98435, 8435, 9)]
  [InlineData(92438573925729457, 29457, 924385739257)]
  [InlineData(41, 1, 4)]
  public void Deconcatenate_GivenConcatenatedNumber_ReturnsDeconcatenatedNumber(long full, long operand, long expected)
  {
    var result = Aoc2024Day07Processor.Deconcatenate(full, operand);
    Assert.Equal(expected, result);
  }

  [Theory]
  [InlineData(2334, 35)]
  [InlineData(29347692, 834)]
  [InlineData(34897539257, 8)]
  public void Deconcatenate_GivenUnconcatenatedNumber_ReturnsNegativeOne(long full, long operand)
  {
    var result = Aoc2024Day07Processor.Deconcatenate(full, operand);
    Assert.Equal(-1, result);
  }

  [Fact]
  public void ProcessPart1Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart1Solution(input);

    Assert.Equal("3749", result);
  }

  [Fact]
  public void ProcessPart2Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart2Solution(input);

    Assert.Equal("11387", result);
  }
}
