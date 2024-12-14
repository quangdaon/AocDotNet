namespace AdventOfCode.App.Utilities;

public static class InputReader
{
  public static string LoadInput(int day, int year)
  {
    var dayString = day.ToString().PadLeft(2, '0');
    var inputFilePath = $"./inputs/{year}/{dayString}/input.txt";
    var s = File.ReadAllText(inputFilePath);
    return s;
  }
}
