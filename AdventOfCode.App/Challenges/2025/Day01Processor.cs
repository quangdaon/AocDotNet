using AdventOfCode.App.Core;
using AdventOfCode.App.Utilities;

namespace AdventOfCode.App.Challenges;

[ChallengeProcessor(2025, 1)]
public class Aoc2025Day01Processor : IChallengeProcessor
{
  public enum Direction
  {
    Left,
    Right
  }

  public class Instruction
  {
    public Direction Direction { get; set; }
    public int Steps { get; set; }

    public static Instruction Parse(string row) => new()
    {
      Direction = row[0] == 'R' ? Direction.Right : Direction.Left,
      Steps = int.Parse(row[1..])
    };
  }

  public string ProcessPart1Solution(string input)
  {
    var rows = input.ToRows();
    var instructions = rows.Select(Instruction.Parse).ToArray();

    var position = 50;
    var hits = 0;

    foreach (var instruction in instructions)
    {
      var amount = instruction.Direction == Direction.Left ? 100 - instruction.Steps : instruction.Steps;

      position += amount;

      if (position % 100 == 0) hits++;
    }

    return hits.ToString();
  }

  public string ProcessPart2Solution(string input)
  {
    var rows = input.ToRows();
    var instructions = rows.Select(Instruction.Parse).ToArray();

    var position = 50;
    var hits = 0;

    foreach (var instruction in instructions)
    {
      var amount = instruction.Steps;
      var rotations = amount / 100;
      hits += rotations;
      amount -= rotations * 100;

      if (instruction.Direction == Direction.Left)
      {
        amount *= -1;
        if (position == 0) position = 100;
      }

      position += amount;

      if (position is > 0 and < 100) continue;
      
      hits++;
      position = (position + 1000) % 100;
    }

    return hits.ToString();
  }
}
