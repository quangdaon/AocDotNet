using System;
using System.IO;
using System.Reflection;

namespace AdventOfCode.App.Tests.Challenges;

public abstract class ChallengeProcessorTests
{
  private int Year { get; }
  private int Day { get; }

  protected ChallengeProcessorTests(int year, int day)
  {
    Year = year;
    Day = day;
  }

  protected string GetSampleInput(byte part) => GetSampleInput(part.ToString());
  
  protected string GetSampleInput(string part = "")
  {
    var dayString = Day.ToString().PadLeft(2, '0');
    var suffix = string.IsNullOrEmpty(part) ? string.Empty : $"-{part}";
    var inputFilePath = $"./inputs/{Year}/{dayString}/input.sample{suffix}.txt";

    var codeBaseUrl = new Uri(Assembly.GetExecutingAssembly().Location);
    var codeBasePath = Uri.UnescapeDataString(codeBaseUrl.AbsolutePath);
    var dirPath = Path.GetDirectoryName(codeBasePath);

    try
    {
      var absolutePath = Path.Combine(dirPath, inputFilePath);
      return File.ReadAllText(absolutePath);
    }
    catch
    {
      throw new Exception("No sample inputs available.");
    }
  }
}