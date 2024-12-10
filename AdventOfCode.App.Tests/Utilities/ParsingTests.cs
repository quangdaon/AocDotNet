using AdventOfCode.App.Utilities;
using Xunit;

namespace AdventOfCode.App.Tests.Utilities;

public class ParsingTests
{
  [Fact]
  public void ToDigits_GivenString_ReturnsDigitsArray()
  {
    const string input = "40592876039";

    var result = Parsing.ToDigits(input);

    Assert.Equal([4, 0, 5, 9, 2, 8, 7, 6, 0, 3, 9], result);
  }
}