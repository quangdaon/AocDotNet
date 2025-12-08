using AdventOfCode.App.Core;
using AdventOfCode.App.Utilities;

namespace AdventOfCode.App.Challenges;

[ChallengeProcessor(2025, 4)]
public class Aoc2025Day04Processor : IChallengeProcessor
{
  private bool IsClear(string[][] rows, int x, int y)
  {
    var cell = rows[x][y];

    if (cell == ".") return false;

    var minX = x == 0 ? 0 : -1;
    var maxX = x == rows.Length - 1 ? 0 : 1;

    var minY = y == 0 ? 0 : -1;
    var maxY = y == rows[0].Length - 1 ? 0 : 1;

    var count = 0;

    for (var i = minX; i <= maxX; i++)
    {
      for (var j = minY; j <= maxY; j++)
      {
        var cellToCheck = rows[x + i][y + j];
        if (cellToCheck != "." && (i != 0 || j != 0)) count++;
      }
    }

    return count < 4;
  }

  private Coordinates[] GetClearableCells(string[][] rows)
  {
    var clearable = new List<Coordinates>();

    for (var x = 0; x < rows.Length; x++)
    {
      var row = rows[x];
      for (var y = 0; y < row.Length; y++)
      {
        if (IsClear(rows, x, y)) clearable.Add((x, y));
      }
    }

    return clearable.ToArray();
  }

  public string ProcessPart1Solution(string input)
  {
    var rows = input.ToGrid();
    var clearable = GetClearableCells(rows);

    return clearable.Length.ToString();
  }

  public string ProcessPart2Solution(string input)
  {
    var rows = input.ToGrid();
    var totalCount = 0;

    while (true)
    {
      var clearable = GetClearableCells(rows);
      var count = clearable.Length;

      if (count == 0) break;
      
      totalCount += count;

      foreach (var coords in clearable)
      {
        rows[coords.X][coords.Y] = ".";
      }
    }

    return totalCount.ToString();
  }
}
