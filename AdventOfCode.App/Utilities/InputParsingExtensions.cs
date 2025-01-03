﻿namespace AdventOfCode.App.Utilities;

public static class InputParsingExtensions
{
  public static int[] ToDigits(this string input) => input.ToCharArray().Select(e => e - 48).ToArray();
  public static int[] ToInts(this string input, char delimiter = ' ') => input.Split(delimiter).Select(int.Parse).ToArray();
  public static long[] ToLongs(this string input, char delimiter = ' ') => input.Split(delimiter).Select(long.Parse).ToArray();

  public static string[][] ToGrid(this string input, string delimiterX = "",
    string delimiterY = null)
  {
    return input.ToRows(delimiterY).Select(row =>
      delimiterX == string.Empty
        ? row.ToCharArray().Select(e => e.ToString()).ToArray()
        : row.Split(delimiterX, StringSplitOptions.RemoveEmptyEntries).ToArray()).ToArray();
  }
  
  public static char[][] ToCharGrid(this string input, string delimiterY = null)
  {
    return input.ToRows(delimiterY).Select(row => row.ToCharArray()).ToArray();
  }

  public static int[][] ToDigitsGrid(this string input, string delimiterY = null) =>
    input.ToRows(delimiterY).Select(ToDigits).ToArray();

  public static string[] ToRows(this string input, string delimiterY = null)
  {
    delimiterY ??= Environment.NewLine;

    var rows = input.Split(delimiterY, StringSplitOptions.RemoveEmptyEntries);

    return rows;
  }
}