using System.Text.RegularExpressions;
using AdventOfCode.App.Core;
using AdventOfCode.App.Utilities;

namespace AdventOfCode.App.Challenges;

[ChallengeProcessor(2015, 6)]
public class Aoc2015Day06Processor : IChallengeProcessor
{
    private static readonly Regex InstructionExpression = new(@"^(?<cmd>(?:\w| )+) (?<x1>\d+),(?<y1>\d+) through (?<x2>\d+),(?<y2>\d+)$");

    private const int ROWS = 1000;
    private const int COLUMNS = 1000;
    private const string TURN_ON_COMMAND = "turn on";
    private const string TURN_OFF_COMMAND = "turn off";
    private const string TOGGLE_COMMAND = "toggle";

    public static int GetOnCount(IEnumerable<int> lights) => lights.Sum();
    private static int GetIndex(int x, int y) => x * ROWS + y;

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
                    TURN_ON_COMMAND => 1,
                    TURN_OFF_COMMAND => 0,
                    TOGGLE_COMMAND => -1 * current + 1,
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
                    TURN_ON_COMMAND => current+ 1,
                    TURN_OFF_COMMAND => Math.Max(current - 1, 0),
                    TOGGLE_COMMAND => current + 2,
                    _ => current
                };
            }
        }
    }

    public string ProcessPart1Solution(string input)
    {
        var rows = input.ToRows();
        var lights = new int[ROWS * COLUMNS]; 
        foreach (var row in rows) ProcessPart1Instruction(lights, row);

        return GetOnCount(lights).ToString();
    }

    public string ProcessPart2Solution(string input)
    {
        var rows = input.ToRows();
        var lights = new int[ROWS * COLUMNS]; 
        foreach (var row in rows) ProcessPart2Instruction(lights, row);

        return GetOnCount(lights).ToString();
    }
}