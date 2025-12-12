using AdventOfCode.App.Core;
using AdventOfCode.App.Utilities;

namespace AdventOfCode.App.Challenges;

[ChallengeProcessor(2025, 9)]
public class Aoc2025Day09Processor : IChallengeProcessor
{
  public class Pair
  {
    public LongCoordinates Coordinates1;
    public LongCoordinates Coordinates2;

    public double Area;
    public bool IsSegment;

    public Pair(LongCoordinates coordinates1, LongCoordinates coordinates2)
    {
      Coordinates1 = coordinates1;
      Coordinates2 = coordinates2;

      var dX = Math.Abs(coordinates1.X - coordinates2.X) + 1;
      var dY = Math.Abs(coordinates1.Y - coordinates2.Y) + 1;

      Area = dX * dY;
      IsSegment = coordinates1.X == coordinates2.X || coordinates1.Y == coordinates2.Y;
    }
  }

  public static LongCoordinates ParseCoordinates(string line)
  {
    var coordinates = line.Split(',');

    return (long.Parse(coordinates[0]), long.Parse(coordinates[1]));
  }

  public static Pair[] CreatePairs(LongCoordinates[] coordinates)
  {
    var pairs = new List<Pair>();
    for (var i = 0; i < coordinates.Length; i++)
    {
      var coordinate1 = coordinates[i];
      foreach (var coordinate2 in coordinates.Skip(i + 1))
      {
        pairs.Add(new Pair(coordinate1, coordinate2));
      }
    }

    return pairs.ToArray();
  }

  public string ProcessPart1Solution(string input)
  {
    var coordinates = input.ToRows().Select(ParseCoordinates).ToArray();
    var pairs = CreatePairs(coordinates);

    var sortedPairs = pairs.OrderBy(p => p.Area).ToArray();
    return sortedPairs.Max(e => e.Area).ToString();
  }

  public static bool IsInBound(Pair pair, Pair[] segments)
  {
    return segments.All(s => !Intersects(pair, s));
  }

  public static bool Intersects(Pair pair, Pair s)
  {
    var minX = Math.Min(pair.Coordinates1.X, pair.Coordinates2.X);
    var maxX = Math.Max(pair.Coordinates1.X, pair.Coordinates2.X);
    var minY = Math.Min(pair.Coordinates1.Y, pair.Coordinates2.Y);
    var maxY = Math.Max(pair.Coordinates1.Y, pair.Coordinates2.Y);

    if (s.Coordinates1.X <= minX && s.Coordinates2.X <= minX) return false;
    if (s.Coordinates1.X >= maxX && s.Coordinates2.X >= maxX) return false;

    if (s.Coordinates1.Y <= minY && s.Coordinates2.Y <= minY) return false;
    if (s.Coordinates1.Y >= maxY && s.Coordinates2.Y >= maxY) return false;

    return true;
  }

  public string ProcessPart2Solution(string input)
  {
    var coordinates = input.ToRows().Select(ParseCoordinates).ToArray();
    var pairs = CreatePairs(coordinates);
    var segments = pairs.Where(e => e.IsSegment).ToArray();

    var sortedPairs = pairs.Where(p => IsInBound(p, segments)).OrderBy(p => p.Area).ToArray();
    return sortedPairs.Max(e => e.Area).ToString();
  }
}
