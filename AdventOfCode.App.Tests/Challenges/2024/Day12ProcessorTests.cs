using System.Linq;
using AdventOfCode.App.Challenges;
using AdventOfCode.App.Utilities;
using Xunit;
using Cell = (int X, int Y, AdventOfCode.App.Challenges.Border[] Perimeter);

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

    Assert.Equal(expected, result.Length);
  }

  [Fact]
  public void Crawl_GivenInputs_CrawlsRegion()
  {
    const string map = "AAABBB\r\nAAABCB\r\nCACDDD\r\nCCCDDD";

    var grid = map.ToCharGrid();
    var result = Aoc2024Day12Processor.Crawl(grid, 'A', 0, 0);
    
    Assert.Equal(7, result.Count);
    Assert.Contains(result, e => e is { X: 0, Y: 0 });
    Assert.Contains(result, e => e is { X: 1, Y: 0 });
    Assert.Contains(result, e => e is { X: 2, Y: 0 });
    Assert.Contains(result, e => e is { X: 0, Y: 1 });
    Assert.Contains(result, e => e is { X: 1, Y: 1 });
    Assert.Contains(result, e => e is { X: 2, Y: 1 });
    Assert.Contains(result, e => e is { X: 1, Y: 2 });
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
    Cell[] region = [
      (0, 0, [Border.Left, Border.Top]),
      (1, 0, [Border.Top]),
      (2, 0, [Border.Top]),
      (3, 0, [Border.Top, Border.Right]),
      (0, 1, [Border.Left, Border.Bottom]),
      (1, 1, [Border.Bottom]),
      (2, 1, []),
      (3, 1, [Border.Right]),
      (2, 2, [Border.Left]),
      (3, 2, [Border.Bottom]),
      (4, 2, [Border.Top, Border.Right, Border.Bottom]),
      (2, 3, [Border.Right, Border.Bottom, Border.Left])
    ];
    
    var result = Aoc2024Day12Processor.CalculateRegionValue(region);
    
    Assert.Equal(216, result);
  }

  [Fact]
  public void CalculateDiscountedRegionValue_GivenRegion_ReturnsSidesTimesLength()
  {
    Cell[] region = [
      (0, 0, [Border.Left, Border.Top]),
      (1, 0, [Border.Top]),
      (2, 0, [Border.Top]),
      (3, 0, [Border.Top, Border.Right]),
      (0, 1, [Border.Left, Border.Bottom]),
      (1, 1, [Border.Bottom]),
      (2, 1, []),
      (3, 1, [Border.Right]),
      (2, 2, [Border.Left]),
      (3, 2, [Border.Bottom]),
      (4, 2, [Border.Top, Border.Right, Border.Bottom]),
      (2, 3, [Border.Right, Border.Bottom, Border.Left])
    ];
    
    var result = Aoc2024Day12Processor.CalculateDiscountedRegionValue(region);
    
    Assert.Equal(120, result);
  }

  [Fact]
  public void GetSides_GivenRegion_ReturnsNumberOfSides()
  {
    Cell[] region = [
      (0, 0, [Border.Left, Border.Top]),
      (1, 0, [Border.Top]),
      (2, 0, [Border.Top]),
      (3, 0, [Border.Top, Border.Right]),
      (0, 1, [Border.Left, Border.Bottom]),
      (1, 1, [Border.Bottom]),
      (2, 1, []),
      (3, 1, [Border.Right]),
      (2, 2, [Border.Left]),
      (3, 2, [Border.Bottom]),
      (4, 2, [Border.Top, Border.Right, Border.Bottom]),
      (2, 3, [Border.Right, Border.Bottom, Border.Left])
    ];
    
    var result = Aoc2024Day12Processor.GetSides(region);
    
    Assert.Equal(10, result);
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
  
  [Theory]
  [InlineData(1,"1206")]
  [InlineData(2,"436")]
  [InlineData(3,"80")]
  [InlineData(4,"236")]
  [InlineData(5,"368")]
  public void ProcessPart2Solution_GivenSampleInputs_ReturnsProvidedResult(byte sample, string expectedResult)
  {
    var input = GetSampleInput(sample);

    var result = _processor.ProcessPart2Solution(input);

    Assert.Equal(expectedResult, result);
  }
}