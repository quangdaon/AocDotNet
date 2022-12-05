namespace AdventOfCode.App.Core;

public class ChallengeProcessorAttribute :  Attribute
{
  public int Year { get; set; }
  public int Day { get; set; }

  public ChallengeProcessorAttribute(int year, int day)
  {
    Year = year;
    Day = day;
  }
}