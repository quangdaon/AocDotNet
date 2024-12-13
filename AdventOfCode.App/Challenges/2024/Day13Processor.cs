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

    private ClawMachineConfiguration(Coordinates buttonA, Coordinates buttonB, Coordinates prize)
    {
      ButtonAOutput = buttonA;
      ButtonBOutput = buttonB;
      PrizeTarget = prize;
    }

    public static ClawMachineConfiguration Parse(string input)
    {
      var rows = input.Split(Environment.NewLine);
      var buttonA = rows[0].Replace("Button A: X+", string.Empty).Split(", Y+").Select(int.Parse).ToArray();
      var buttonB = rows[1].Replace("Button B: X+", string.Empty).Split(", Y+").Select(int.Parse).ToArray();
      var prize = rows[2].Replace("Prize: X=", string.Empty).Split(", Y=").Select(int.Parse).ToArray();
      
      return new ClawMachineConfiguration((buttonA[0], buttonA[1]), (buttonB[0], buttonB[1]), (prize[0], prize[1]));
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