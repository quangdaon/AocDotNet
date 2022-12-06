using System;
using System.IO;
using System.Reflection;

namespace AdventOfCode.App.Tests.Challenges;

public abstract class ChallengeProcessorTests
{
  public int Year { get; private set; }
  public int Day { get; private set; }

  public ChallengeProcessorTests(int year, int day)
  {
    Year = year;
    Day = day;
  }

  protected string GetSampleInput()
  {
    var dayString = Day.ToString().PadLeft(2, '0');
    var inputFilePath = $"./inputs/{Year}/{dayString}/input.sample.txt";

    var codeBaseUrl = new Uri(Assembly.GetExecutingAssembly().Location);
    var codeBasePath = Uri.UnescapeDataString(codeBaseUrl.AbsolutePath);
    var dirPath = Path.GetDirectoryName(codeBasePath);
    var absolutePath = Path.Combine(dirPath, inputFilePath);

    try
    {
      return File.ReadAllText(absolutePath);
    }
    catch
    {
      throw new Exception("No sample inputs available.");
    }
  }
}