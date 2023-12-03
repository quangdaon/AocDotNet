using System.Buffers;
using System.Text.RegularExpressions;
using AdventOfCode.App.Core;

namespace AdventOfCode.App.Challenges;

[ChallengeProcessor(2023, 1)]
public class Aoc2023Day01Processor : IChallengeProcessor
{
  private Dictionary<string, char> _numMap = new()
  {
    { "one", '1' },
    { "two", '2' },
    { "three", '3' },
    { "four", '4' },
    { "five", '5' },
    { "six", '6' },
    { "seven", '7' },
    { "eight", '8' },
    { "nine", '9' }
  };

  public int GetPart1CalibrationCode(string line)
  {
    var numChars = line.Where(char.IsNumber).ToArray();
    return int.Parse(string.Concat(numChars.First(), numChars.Last()));
  }

  public int GetPart2CalibrationCode(string line)
  {
    var beginningIndex = 0;
    char beginningDigit = default;
    var endIndex = line.Length - 1;
    char endDigit = default;

    while (beginningIndex <= endIndex)
    {
      var digit = ParsePart2Digit(line[beginningIndex..]);
      if (digit != default)
      {
        beginningDigit = digit;
        break;
      }

      beginningIndex++;
    }

    while (endIndex >= beginningIndex)
    {
      var digit = ParsePart2Digit(line[endIndex..]);
      if (digit != default)
      {
        endDigit = digit;
        break;
      }

      endIndex--;
    }

    return int.Parse(string.Concat(beginningDigit, endDigit));
  }

  private char ParsePart2Digit(string substring)
  {
    if (char.IsNumber(substring[0])) return substring[0];

    foreach (var value in _numMap)
    {
      if (substring.StartsWith(value.Key)) return value.Value;
    }

    return default;
  }

  public string ProcessPart1Solution(string input)
  {
    var rows = input.Split(Environment.NewLine);
    return rows.Select(GetPart1CalibrationCode)
      .Sum()
      .ToString();
  }

  public string ProcessPart2Solution(string input)
  {
    var rows = input.Split(Environment.NewLine);
    return rows.Select(GetPart2CalibrationCode)
      .Sum()
      .ToString();
  }
}