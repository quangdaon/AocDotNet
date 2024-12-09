using System.Collections.Generic;
using System.Linq;
using AdventOfCode.App.Challenges;
using Xunit;

namespace AdventOfCode.App.Tests.Challenges;

public class Aoc2024Day09ProcessorTest : ChallengeProcessorTests
{
  private readonly Aoc2024Day09Processor _processor;

  public Aoc2024Day09ProcessorTest() : base(2024, 9)
  {
    _processor = new Aoc2024Day09Processor();
  }

  [Theory]
  [InlineData("0,2|0,3|1,3|0,3|2,1|0,3|3,3|0,1|4,2|0,1|5,4|0,1|6,4|0,1|7,3|0,1|8,4|9,2", "000001110002000333044055550666607770888899")]
  [InlineData( "0,1|0,2|1,3|0,4|2,5", "000111000022222")]
  [InlineData( "0,9|1,9|2,9", "000000000111111111222222222")]
  public void ExpandBlocks_GivenBlocks_ReturnsFilesystem(string inputString, string expectedString)
  {
    var input = ParseBlocksString(inputString);
    var result = Aoc2024Day09Processor.ExpandBlocks(input);
    var expected = Aoc2024Day09Processor.ToDigits(expectedString);

    Assert.Equal(expected, result);
  }

  [Theory]
  [InlineData("2333133121414131402", "0,2|0,3|1,3|0,3|2,1|0,3|3,3|0,1|4,2|0,1|5,4|0,1|6,4|0,1|7,3|0,1|8,4|9,2")]
  [InlineData("12345", "0,1|0,2|1,3|0,4|2,5")]
  [InlineData("90909", "0,9|1,9|2,9")]
  public void ParseBlocks_GivenDiskMap_ReturnsBlocks(string input, string expectedString)
  {
    var result = Aoc2024Day09Processor.ParseBlocks(input);
    var expected = ParseBlocksString(expectedString);

    Assert.Equal(expected, result);
  }

  [Theory]
  [InlineData("2333133121414131402", "0099811188827773336446555566")]
  [InlineData("12345", "022111222")]
  [InlineData("90909", "000000000111111111222222222")]
  public void RearrangeBlocks_GivenDiskMap_ReturnsRearrangedFilesystem(string input, string expectedString)
  {
    var result = Aoc2024Day09Processor.RearrangeBlocks(input);
    var expected = Aoc2024Day09Processor.ToDigits(expectedString);

    Assert.Equal(expected, result);
  }

  [Theory]
  [InlineData("2333133121414131402", "0099211177704403330000555506666000008888")]
  [InlineData("12345", "000111000022222")]
  public void RearrangeBlocksWithoutFragmentation_GivenDiskMap_ReturnsRearrangedFilesystem(string input, string expectedString)
  {
    var result = Aoc2024Day09Processor.RearrangeBlocks(input, false);
    var expected = Aoc2024Day09Processor.ToDigits(expectedString);

    Assert.Equal(string.Join("", expected), string.Join("", result));
  }

  [Fact]
  public void ProcessPart1Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart1Solution(input);

    Assert.Equal("1928", result);
  }

  [Fact]
  public void ProcessPart2Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart2Solution(input);

    Assert.Equal("2858", result);
  }

  private static IEnumerable<(int, int)> ParseBlocksString(string expectedString)
  {
    return expectedString.Split('|').Select(e => e.Split(',').Select(int.Parse).ToArray()).Select(e => (e[0], e[1]));
  }
}