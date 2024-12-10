namespace AdventOfCode.App.Utilities;

public static class Parsing
{
  public static IEnumerable<int> ToDigits(string input)
  {
    return input.ToCharArray().Select(e => e - 48);
  }
}