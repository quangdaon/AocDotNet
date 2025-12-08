using System.Linq;
using AdventOfCode.App.Utilities;
using Xunit;

namespace AdventOfCode.App.Tests.Utilities;

public class RangeTests
{
  [Theory]
  [InlineData(1, 10, 7, true)]
  [InlineData(100, 1000, 619, true)]
  [InlineData(10, 1000, 10, true)]
  [InlineData(10, 1000, 1000, true)]
  [InlineData(1, 10, 0, false)]
  [InlineData(100, 1000, 1020, false)]
  [InlineData(10, 1000, 9, false)]
  public void Contains_GivenRange_ReturnsContainedValue(long start, long end, long value, bool expectedResult)
  {
    var result = new Range(start, end).Contains(value);

    Assert.Equal(expectedResult, result);
  }

  [Theory]
  [InlineData("1-10", 1, 10)]
  [InlineData("2-30", 2, 30)]
  public void Parse_GivenRangeString_ReturnsRange(string input, long start, long end)
  {
    var range = Range.Parse(input);

    Assert.Equal(start, range.Start);
    Assert.Equal(end, range.End);
  }

  [Theory]
  [InlineData(1, 10, 7, 14, true)]
  [InlineData(1, 10, 10, 14, true)]
  [InlineData(1, 10, -4, 1, true)]
  [InlineData(1, 10, 11, 13, false)]
  [InlineData(1, 100, 11, 13, true)]
  [InlineData(11, 13, 1, 100, true)]
  public void Overlaps_GivenRanges_ReturnsOverlappedValue(long start1, long end1, long start2, long end2,
    bool expectedResult)
  {
    var range1 = new Range(start1, end1);
    var range2 = new Range(start2, end2);
    var result = range1.Overlaps(range2);

    Assert.Equal(expectedResult, result);
  }
}
