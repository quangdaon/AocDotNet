using AdventOfCode.App.Core;

namespace AdventOfCode.App.Challenges;

[ChallengeProcessor(2015, 3)]
public class Aoc2015Day03Processor : IChallengeProcessor
{
  private const char UP = '^';
  private const char DOWN = 'v'; 
  private const char LEFT = '<'; 
  private const char RIGHT = '>';
  
  public string ProcessPart1Solution(string input) => CountHouses(input, new [] { (0,0) });

  public string ProcessPart2Solution(string input) => CountHouses(input, new [] { (0, 0), (0,0) });

  private static string CountHouses(string input, (int X, int Y)[] coords)
  {
    var set = new HashSet<(int X, int Y)>();
    foreach (var c in coords) set.Add(c);
    var commands = input.ToCharArray();
    var step = 0;

    foreach (var cmd in commands)
    {
      var index = step % coords.Length;
      switch (cmd)
      {
        case UP:
          coords[index].Y--;
          break;
        case DOWN:
          coords[index].Y++;
          break;
        case LEFT:
          coords[index].X--;
          break;
        case RIGHT:
          coords[index].X++;
          break;
      }

      set.Add(coords[index]);
      step++;
    }

    return set.Count.ToString();
  }
}
