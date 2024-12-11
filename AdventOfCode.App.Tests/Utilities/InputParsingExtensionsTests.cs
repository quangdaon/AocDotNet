using AdventOfCode.App.Utilities;
using Xunit;

namespace AdventOfCode.App.Tests.Utilities;

public class InputParsingExtensionsTests
{
  [Fact]
  public void ToDigits_GivenString_ReturnsDigitsArray()
  {
    const string input = "40592876039";

    var result = input.ToDigits();
    int[] expected = [4, 0, 5, 9, 2, 8, 7, 6, 0, 3, 9];

    Assert.Equal(expected, result);
  }

  [Fact]
  public void ToNumbers_GivenString_ReturnsNumbersArray()
  {
    const string input = "1 1 2 22 33 84";

    var result = input.ToInts();
    int[] expected = [1, 1, 2, 22, 33, 84];

    Assert.Equal(expected, result);
  }

  [Fact]
  public void ToNumbers_GivenDelimiter_ReturnsNumbersArray()
  {
    const string input = "54|213|67|23";

    var result = input.ToInts('|');
    int[] expected = [54, 213, 67, 23];

    Assert.Equal(expected, result);
  }

  [Fact]
  public void ToGrid_GivenString_ReturnsGridEnumerable()
  {
    const string input = """
                         2468
                         3693
                         1234
                         7654
                         """;

    string[][] expected =
    [
      ["2", "4", "6", "8"],
      ["3", "6", "9", "3"],
      ["1", "2", "3", "4"],
      ["7", "6", "5", "4"]
    ];

    var result = input.ToGrid();

    Assert.Equal(expected, result);
  }

  [Fact]
  public void ToGrid_GivenDelimiters_ReturnsGridEnumerable()
  {
    const string input = "2,4,6,8|3,6,9,3|1,2,3,4|7,6,5,4";

    string[][] expected =
    [
      ["2", "4", "6", "8"],
      ["3", "6", "9", "3"],
      ["1", "2", "3", "4"],
      ["7", "6", "5", "4"]
    ];

    var result = input.ToGrid(",", "|");

    Assert.Equal(expected, result);
  }

  [Fact]
  public void ToDigitsGrid_GivenString_ReturnsGridEnumerable()
  {
    const string input = """
                         2468
                         3693
                         1234
                         7654
                         """;

    int[][] expected =
    [
      [2, 4, 6, 8],
      [3, 6, 9, 3],
      [1, 2, 3, 4],
      [7, 6, 5, 4]
    ];

    var result = input.ToDigitsGrid();

    Assert.Equal(expected, result);
  }
}