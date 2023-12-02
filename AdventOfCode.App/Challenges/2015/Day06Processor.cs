using System.Text.RegularExpressions;
using AdventOfCode.App.Core;

namespace AdventOfCode.App.Challenges;

[ChallengeProcessor(2015, 6)]
public class Aoc2015Day06Processor : IChallengeProcessor
{
    private static readonly Regex InstructionExpression = new(@"^(?<cmd>(?:\w| )+) (?<x1>\d+),(?<y1>\d+) through (?<x2>\d+),(?<y2>\d+)$");

    private const int Rows = 1000;
    private const int Columns = 1000;
    private const string TurnOnCommand = "turn on";
    private const string TurnOffCommand = "turn off";
    private const string ToggleCommand = "toggle";

    public static int GetOnCount(IEnumerable<int> lights) => lights.Sum();
    private static int GetIndex(int x, int y) => x * Rows + y;

    public void ProcessPart1Instruction(int[] lights, string row)
    {
        var match = InstructionExpression.Match(row);
        var command = match.Groups["cmd"].Value;

        var x1 = int.Parse(match.Groups["x1"].Value);
        var y1 = int.Parse(match.Groups["y1"].Value);
        var x2 = int.Parse(match.Groups["x2"].Value);
        var y2 = int.Parse(match.Groups["y2"].Value);
        
        for (var x = x1; x <= x2; x++)
        {
            for (var y = y1; y <= y2; y++)
            {
                var current = lights[GetIndex(x, y)];
                lights[GetIndex(x, y)] = command switch
                {
                    TurnOnCommand => 1,
                    TurnOffCommand => 0,
                    ToggleCommand => -1 * current + 1,
                    _ => current
                };
            }
        }
    }

    public void ProcessPart2Instruction(int[] lights, string row)
    {
        var match = InstructionExpression.Match(row);
        var command = match.Groups["cmd"].Value;

        var x1 = int.Parse(match.Groups["x1"].Value);
        var y1 = int.Parse(match.Groups["y1"].Value);
        var x2 = int.Parse(match.Groups["x2"].Value);
        var y2 = int.Parse(match.Groups["y2"].Value);
        
        for (var x = x1; x <= x2; x++)
        {
            for (var y = y1; y <= y2; y++)
            {
                var current = lights[GetIndex(x, y)];
                lights[GetIndex(x, y)] = command switch
                {
                    TurnOnCommand => current+ 1,
                    TurnOffCommand => Math.Max(current - 1, 0),
                    ToggleCommand => current + 2,
                    _ => current
                };
            }
        }
    }

    public string ProcessPart1Solution(string input)
    {
        var rows = input.Split(Environment.NewLine);
        var lights = new int[Rows * Columns]; 
        foreach (var row in rows) ProcessPart1Instruction(lights, row);

        return GetOnCount(lights).ToString();
    }

    public string ProcessPart2Solution(string input)
    {
        var rows = input.Split(Environment.NewLine);
        var lights = new int[Rows * Columns]; 
        foreach (var row in rows) ProcessPart2Instruction(lights, row);

        return GetOnCount(lights).ToString();
    }
}