using System.Linq;
using AdventOfCode.App.Challenges;
using AdventOfCode.App.Utilities;
using Xunit;

namespace AdventOfCode.App.Tests.Challenges;

public class Aoc2024Day12ProcessorTest : ChallengeProcessorTests
{
  private readonly Aoc2024Day12Processor _processor;

  public Aoc2024Day12ProcessorTest() : base(2024, 12)
  {
    _processor = new Aoc2024Day12Processor();
  }

  [Theory]
  [InlineData('A', 0, 0, 2)]
  [InlineData('B', 5, 0, 2)]
  [InlineData('C', 0, 3, 2)]
  [InlineData('D', 5, 3, 2)]
  [InlineData('A', 1, 0, 1)]
  [InlineData('A', 1, 1, 0)]
  [InlineData('B', 3, 1, 3)]
  [InlineData('D', 3, 2, 2)]
  [InlineData('A', 1, 2, 3)]
  [InlineData('C', 4, 1, 4)]
  public void GetPerimeter_GivenCoordinates_ReturnsPerimeter(char c, int x, int y, int expected)
  {
    const string map = "AAABBB\r\nAAABCB\r\nCACDDD\r\nCCCDDD";

    var grid = map.ToCharGrid();
    var result = Aoc2024Day12Processor.GetPerimeter(grid, c, x, y);

    Assert.Equal(expected, result);
  }

  [Fact]
  public void Crawl_GivenInputs_CrawlsRegion()
  {
    const string map = "AAABBB\r\nAAABCB\r\nCACDDD\r\nCCCDDD";

    var grid = map.ToCharGrid();
    var result = Aoc2024Day12Processor.Crawl(grid, 'A', 0, 0);
    
    Assert.Equal(7, result.Count);
    Assert.Contains((0, 0, 2), result);
    Assert.Contains((1, 0, 1), result);
    Assert.Contains((2, 0, 2), result);
    Assert.Contains((0, 1, 2), result);
    Assert.Contains((1, 1, 0), result);
    Assert.Contains((2, 1, 2), result);
    Assert.Contains((1, 2, 3), result);
  }

  [Fact]
  public void ProcessRegions_GivenGrid_ReturnsRegionData()
  {
    const string map = "AAABBB\r\nAAABCB\r\nCACDDD\r\nCCCDDD";

    var grid = map.ToCharGrid();
    var result = Aoc2024Day12Processor.ProcessRegions(grid);

    Assert.Equal(5, result.Count);
  }

  [Fact]
  public void CalculateRegionValue_GivenRegion_ReturnsSumTimesLength()
  {
    int[] region = [2, 1, 1, 2, 2, 1, 0, 1, 1, 1, 3, 3];
    
    var result = Aoc2024Day12Processor.CalculateRegionValue(region);
    
    Assert.Equal(216, result);
  }
  
  [Theory]
  [InlineData(1,"1930")]
  [InlineData(2,"772")]
  public void ProcessPart1Solution_GivenSampleInputs_ReturnsProvidedResult(byte sample, string expectedResult)
  {
    var input = GetSampleInput(sample);

    var result = _processor.ProcessPart1Solution(input);

    Assert.Equal(expectedResult, result);
  }

  [Fact]
  public void ProcessPart2Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput(1);

    var result = _processor.ProcessPart2Solution(input);

    Assert.Equal("???", result);
  }
}