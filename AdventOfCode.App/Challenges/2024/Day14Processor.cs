using AdventOfCode.App.Core;
using AdventOfCode.App.Utilities;

namespace AdventOfCode.App.Challenges;

[ChallengeProcessor(2024, 14)]
public class Aoc2024Day14Processor : IChallengeProcessor
{
  private const int TIME_SKIP = 100;
  private int _width = 101;
  private int _height = 103;

  public void SetSize(int width, int height)
  {
    _width = width;
    _height = height;
  }

  public class Robot
  {
    private readonly int _gridWidth;
    private readonly int _gridHeight;

    public Coordinates Start { get; set; }
    public Coordinates Velocity { get; set; }

    public Robot(Coordinates start, Coordinates velocity, int gridWidth, int gridHeight)
    {
      Start = start;
      Velocity = velocity;
      _gridWidth = gridWidth;
      _gridHeight = gridHeight;
    }

    public Coordinates ProjectPosition(int time)
    {
      var x = (Start.X + (_gridWidth + Velocity.X) * time) % _gridWidth;
      var y = (Start.Y + (_gridHeight + Velocity.Y) * time) % _gridHeight;
      return (x, y);
    }

    public static Robot Parse(string row, int width, int height)
    {
      var components = row.Replace("p=", string.Empty).Split(" v=");
      var p = components[0].Split(',').Select(int.Parse).ToArray();
      var v = components[1].Split(',').Select(int.Parse).ToArray();

      return new Robot((p[0], p[1]), (v[0], v[1]), width, height);
    }
  }

  private int CalculateSafety(IEnumerable<Robot> robots)
  {
    var coords = robots.Select(r => r.ProjectPosition(TIME_SKIP));

    var quadNW = 0;
    var quadNE = 0;
    var quadSW = 0;
    var quadSE = 0;

    var centerX = _width / 2;
    var centerY = _height / 2;


    foreach (var (x, y) in coords)
    {
      if (x < centerX)
      {
        if (y < centerY) quadNW++;
        if (y > centerY) quadSW++;
      }

      if (x > centerX)
      {
        if (y < centerY) quadNE++;
        if (y > centerY) quadSE++;
      }
    }
    
    return quadNW * quadNE * quadSW * quadSE;
  }

  public string ProcessPart1Solution(string input)
  {
    var rows = input.ToRows();
    var robots = rows.Select(r => Robot.Parse(r, _width, _height));

    return CalculateSafety(robots).ToString();
  }

  public string ProcessPart2Solution(string input)
  {
    throw new NotImplementedException();
  }
}
