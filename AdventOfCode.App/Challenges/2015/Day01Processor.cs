using AdventOfCode.App.Core;

namespace AdventOfCode.App.Challenges;

[ChallengeProcessor(2015, 1)]
public class Aoc2015Day01Processor : IChallengeProcessor
{
  public string ProcessPart1Solution(string input)
  {
    int currentValue = 0;
    int pointer = 0;

    while (pointer < input.Length)
    {
      currentValue += input[pointer] == '(' ? 1 : -1;
      pointer++;
    }

    return currentValue.ToString();
  }

  public string ProcessPart2Solution(string input)
  {
    int currentValue = 0;
    int pointer = 0;

    while (pointer < input.Length)
    {
      currentValue += input[pointer] == '(' ? 1 : -1;
      if (currentValue < 0) return (pointer + 1).ToString();
      pointer++;
    }

    return "Undetermined";
  }
}