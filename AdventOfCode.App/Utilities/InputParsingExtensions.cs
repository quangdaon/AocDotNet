namespace AdventOfCode.App.Utilities;

public static class InputParsingExtensions
{
  public static IEnumerable<int> ToDigits(this string input) => input.ToCharArray().Select(e => e - 48);

  public static IEnumerable<IEnumerable<string>> ToGrid(this string input, string delimiterX = "",
    string delimiterY = null)
  {
    return input.ToRows(delimiterY).Select(row =>
      delimiterX == string.Empty
        ? row.ToCharArray().Select(e => e.ToString())
        : row.Split(delimiterX, StringSplitOptions.RemoveEmptyEntries));
  }

  public static IEnumerable<IEnumerable<int>> ToDigitsGrid(this string input, string delimiterY = null) =>
    input.ToRows(delimiterY).Select(ToDigits);

  public static string[] ToRows(this string input, string delimiterY = null)
  {
    delimiterY ??= Environment.NewLine;

    var rows = input.Split(delimiterY, StringSplitOptions.RemoveEmptyEntries);

    return rows;
  }
}