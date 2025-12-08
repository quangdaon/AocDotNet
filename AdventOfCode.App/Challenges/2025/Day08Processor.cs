using AdventOfCode.App.Core;
using AdventOfCode.App.Utilities;

namespace AdventOfCode.App.Challenges;

[ChallengeProcessor(2025, 8)]
public class Aoc2025Day08Processor : IChallengeProcessor
{
  public static int Iterations = 1000;

  private class Pair
  {
    public LongCoordinates3D Coordinates1;
    public LongCoordinates3D Coordinates2;

    public double Distance;

    public Pair(LongCoordinates3D coordinates1, LongCoordinates3D coordinates2)
    {
      Coordinates1 = coordinates1;
      Coordinates2 = coordinates2;

      var dX = Math.Pow(coordinates1.X - coordinates2.X, 2);
      var dY = Math.Pow(coordinates1.Y - coordinates2.Y, 2);
      var dZ = Math.Pow(coordinates1.Z - coordinates2.Z, 2);

      Distance = Math.Sqrt(dX + dY + dZ);
    }
  }

  private LongCoordinates3D ParseCoordinates(string line)
  {
    var coordinates = line.Split(',');

    return (long.Parse(coordinates[0]), long.Parse(coordinates[1]), long.Parse(coordinates[2]));
  }

  private Pair[] CreatePairs(LongCoordinates3D[] coordinates)
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

    var sortedPairs = pairs.OrderBy(p => p.Distance).ToArray();

    var circuits = coordinates.Select(c => new List<LongCoordinates3D> { c }).ToList();

    foreach (var pair in sortedPairs.Take(Iterations))
    {
      var circuit1 = circuits.First(c => c.Contains(pair.Coordinates1));
      var circuit2 = circuits.First(c => c.Contains(pair.Coordinates2));

      if (circuit1 == circuit2) continue;

      var newCircuit = circuit1.Concat(circuit2).Distinct().ToList();

      circuits.Remove(circuit1);
      circuits.Remove(circuit2);
      circuits.Add(newCircuit);
    }

    return circuits.Select(c => c.Count).OrderDescending().Take(3).Aggregate(1L, (a, b) => a * b).ToString();
  }

  public string ProcessPart2Solution(string input)
  {
    var coordinates = input.ToRows().Select(ParseCoordinates).ToArray();
    var pairs = CreatePairs(coordinates);

    var sortedPairs = pairs.OrderBy(p => p.Distance).ToArray();

    var circuits = coordinates.Select(c => new List<LongCoordinates3D> { c }).ToList();
    Pair lastPair = null;

    foreach (var pair in sortedPairs)
    {
      var circuit1 = circuits.First(c => c.Contains(pair.Coordinates1));
      var circuit2 = circuits.First(c => c.Contains(pair.Coordinates2));

      if (circuit1 == circuit2) continue;

      var newCircuit = circuit1.Concat(circuit2).Distinct().ToList();

      circuits.Remove(circuit1);
      circuits.Remove(circuit2);
      circuits.Add(newCircuit);

      if (circuits.Count > 1) continue;
      
      lastPair = pair;
      break;
    }

    return (lastPair!.Coordinates1.X * lastPair!.Coordinates2.X).ToString();
  }
}
