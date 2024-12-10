using AdventOfCode.App.Core;
using AdventOfCode.App.Utilities;

namespace AdventOfCode.App.Challenges;

[ChallengeProcessor(2024, 4)]
public class Aoc2024Day04Processor : IChallengeProcessor
{
  public static bool IsInBound(int dir, int index, int range, int buffer)
  {
    return (dir != -1 || index >= buffer - 1) && (dir != 1 || index <= range - buffer);
  }

  public static bool IsMatch(char[][] grid, char[] target, int i, int j, int x, int y)
  {
    var c = grid[i][j];
    var u = target[0];
    if (c != u) return false;

    var pointer = 0;
    
    while (++pointer < target.Length)
    {
      var nextI = i + y * pointer;
      var nextJ = j + x * pointer;

      var nextLetter = grid[nextI][nextJ];

      if (nextLetter == target[pointer]) continue;

      return false;
    }

    return true;
  }

  public static int FindWord(char[][] grid, string targetString)
  {
    var target = targetString.ToCharArray();
    var count = 0;

    for (var i = 0; i < grid.Length; i++)
    {
      var row = grid[i];
      for (var j = 0; j < row.Length; j++)
      {
        var letter = row[j];

        if (letter != target[0]) continue;

        for (var x = -1; x <= 1; x++)
        {
          if (!IsInBound(x, j, row.Length, target.Length)) continue;

          for (var y = -1; y <= 1; y++)
          {
            if ((y == 0 && x == 0) || !IsInBound(y, i, grid.Length, target.Length)) continue;
            if (IsMatch(grid, target, i, j, x, y)) count++;
          }
        }
      }
    }

    return count;
  }

  public static int FindCross(char[][] grid, string targetString)
  {
    var target = targetString.ToCharArray();
    var count = 0;
    if (target.Length % 2 == 0)
    {
      throw new InvalidOperationException("Unsupported Operation: Target word must be an odd number of letters");
    }

    var key = Convert.ToInt32(Math.Floor(target.Length / 2.0));

    for (var i = 0; i < grid.Length; i++)
    {
      var row = grid[i];
      for (var j = 0; j < row.Length; j++)
      {
        var letter = row[j];

        if (letter != target[key]) continue;

        if (!IsInBound(-1, j, row.Length, key + 1) ||
            !IsInBound(1, j, row.Length, key + 1) ||
            !IsInBound(-1, i, grid.Length, key + 1) ||
            !IsInBound(1, i, grid.Length, key + 1)) continue;

        var nwseDiagonal = IsMatch(grid, target, i - key, j - key, 1, 1) ||
                           IsMatch(grid, target, i + key, j + key, -1, -1);

        if (!nwseDiagonal) continue;

        var neswDiagonal = IsMatch(grid, target, i - key, j + key, -1, 1) ||
                           IsMatch(grid, target, i + key, j - key, 1, -1);

        if (neswDiagonal) count++;
      }
    }

    return count;
  }

  public string ProcessPart1Solution(string input)
  {
    const string targetString = "XMAS";

    var rows = input.ToRows();
    var grid = rows.Select(r => r.ToCharArray()).ToArray();
    return FindWord(grid, targetString).ToString();
  }

  public string ProcessPart2Solution(string input)
  {
    const string targetString = "MAS";

    var rows = input.ToRows();
    var grid = rows.Select(r => r.ToCharArray()).ToArray();
    return FindCross(grid, targetString).ToString();
  }
}