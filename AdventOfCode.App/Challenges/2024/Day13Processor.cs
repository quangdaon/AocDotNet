using AdventOfCode.App.Core;

namespace AdventOfCode.App.Challenges;

[ChallengeProcessor(2024, 13)]
public class Aoc2024Day13Processor : IChallengeProcessor
{
  public class ClawMachineConfiguration
  {
    private const int BUTTON_A_COST = 3;
    private const int BUTTON_B_COST = 1;

    public Coordinates ButtonAOutput { get; set; }
    public Coordinates ButtonBOutput { get; set; }
    public LongCoordinates PrizeTarget { get; set; }

    private ClawMachineConfiguration(Coordinates buttonA, Coordinates buttonB, LongCoordinates prize)
    {
      ButtonAOutput = buttonA;
      ButtonBOutput = buttonB;
      PrizeTarget = prize;
    }

    public long ComputeCost()
    {
      var a = PrizeTarget.X / (decimal)ButtonAOutput.X;
      var b = -ButtonBOutput.X / (decimal)ButtonAOutput.X;
      var c = PrizeTarget.Y / (decimal)ButtonAOutput.Y;
      var d = -ButtonBOutput.Y / (decimal)ButtonAOutput.Y;

      var bPresses = Convert.ToInt64((c - a) / (b - d));
      var aPresses = Convert.ToInt64((PrizeTarget.X - ButtonBOutput.X * bPresses) / ButtonAOutput.X);

      if (aPresses * ButtonAOutput.X + bPresses * ButtonBOutput.X != PrizeTarget.X ||
          aPresses * ButtonAOutput.Y + bPresses * ButtonBOutput.Y != PrizeTarget.Y) return 0;

      return aPresses * BUTTON_A_COST + bPresses * BUTTON_B_COST;
    }

    private static long[] ReadRow(string row, string label, string delimiter) =>
      row.Replace($"{label}: X{delimiter}", string.Empty).Split($", Y{delimiter}").Select(long.Parse).ToArray();

    public static ClawMachineConfiguration Parse(string input, long offset = 0)
    {
      var rows = input.Split(Environment.NewLine);
      var buttonA = ReadRow(rows[0], "Button A", "+");
      var buttonB = ReadRow(rows[1], "Button B", "+");
      var prize = ReadRow(rows[2], "Prize", "=");

      return new ClawMachineConfiguration(
        (Convert.ToInt32(buttonA[0]), Convert.ToInt32(buttonA[1])),
        (Convert.ToInt32(buttonB[0]), Convert.ToInt32(buttonB[1])),
        (prize[0] + offset, prize[1] + offset)
      );
    }
  }

  public string ProcessPart1Solution(string input)
  {
    var configs = input.Split(Environment.NewLine + Environment.NewLine);
    var machines = configs.Select(e => ClawMachineConfiguration.Parse(e));

    return machines.Sum(m => m.ComputeCost()).ToString();
  }

  public string ProcessPart2Solution(string input)
  {
    var configs = input.Split(Environment.NewLine + Environment.NewLine);
    var machines = configs.Select((s) => ClawMachineConfiguration.Parse(s, 10000000000000));

    return machines.Sum(m => m.ComputeCost()).ToString();
  }
}
