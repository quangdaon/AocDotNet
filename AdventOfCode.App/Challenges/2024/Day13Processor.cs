using AdventOfCode.App.Core;

namespace AdventOfCode.App.Challenges;

[ChallengeProcessor(2024, 13)]
public class Aoc2024Day13Processor : IChallengeProcessor
{
  public class ClawMachineConfiguration
  {
    public Coordinates ButtonAOutput { get; set; }
    public Coordinates ButtonBOutput { get; set; }
    public Coordinates PrizeTarget { get; set; }

    public static ClawMachineConfiguration Parse(string input)
    {
      return new ClawMachineConfiguration();
    }
  }

  public string ProcessPart1Solution(string input)
  {
    throw new NotImplementedException();
  }

  public string ProcessPart2Solution(string input)
  {
    throw new NotImplementedException();
  }
}