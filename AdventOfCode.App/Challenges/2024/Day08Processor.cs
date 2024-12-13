using AdventOfCode.App.Core;
using AdventOfCode.App.Utilities;

namespace AdventOfCode.App.Challenges;

[ChallengeProcessor(2024, 8)]
public class Aoc2024Day08Processor : IChallengeProcessor
{
  public static Dictionary<char, Coordinates[]> GetNodes(string[] input)
  {
    var nodes = new Dictionary<char, List<(int x, int y)>>();

    for (var y = 0; y < input.Length; y++)
    {
      var row = input[y].ToCharArray();
      for (var x = 0; x < row.Length; x++)
      {
        var node = row[x];
        if (node == '.') continue;

        if (!nodes.ContainsKey(node)) nodes.Add(node, []);

        nodes[node].Add((x, y));
      }
    }

    return nodes.ToDictionary(x => x.Key, x => x.Value.ToArray());
  }

  public static Coordinates[] GetUnboundedAntinodes(Coordinates node1, Coordinates node2, int width, int height)
  {
    Coordinates slope = (node2.X - node1.X, node2.Y - node1.Y);

    List<Coordinates> antinodeCandidates = [node1, node2];

    var tl = node1;

    while (true)
    {
      tl = (tl.X - slope.X, tl.Y - slope.Y);
      if (!IsInBound(tl, width, height)) break;

      antinodeCandidates.Add(tl);
    }

    var br = node2;
    while (true)
    {
      br = (br.X + slope.X, br.Y + slope.Y);
      if (!IsInBound(br, width, height)) break;

      antinodeCandidates.Add(br);
    }

    return antinodeCandidates.Where(e => IsInBound(e, width, height)).ToArray();
  }

  public static Coordinates[] GetAntinodes(Coordinates node1, Coordinates node2, int width, int height)
  {
    Coordinates slope = (node2.X - node1.X, node2.Y - node1.Y);

    Coordinates[] antinodeCandidates =
    [
      (node1.X - slope.X, node1.Y - slope.Y),
      (node2.X + slope.X, node2.Y + slope.Y),
    ];

    return antinodeCandidates.Where(e => IsInBound(e, width, height)).ToArray();
  }

  public static Coordinates[] GetAntinodes(Coordinates[] nodes, int width, int height, bool unbounded = false)
  {
    var pairs = nodes
      .SelectMany((n1, i) => nodes
        .Skip(i + 1)
        .Select(n2 => new[] { n1, n2 }));

    return pairs.SelectMany(pair => unbounded
        ? GetUnboundedAntinodes(pair[0], pair[1], width, height)
        : GetAntinodes(pair[0], pair[1], width, height))
      .Distinct().ToArray();
  }

  public string ProcessPart1Solution(string input)
  {
    var rows = input.ToRows();
    var nodes = GetNodes(rows);
    var antinodes = nodes.ToDictionary(n => n.Key, n => GetAntinodes(n.Value, rows.Length, rows[0].Length));

    var allAntinodes = antinodes.SelectMany(a => a.Value).Distinct().ToArray();

    return allAntinodes.Length.ToString();
  }

  public string ProcessPart2Solution(string input)
  {
    var rows = input.ToRows();
    var nodes = GetNodes(rows);
    var antinodes = nodes.ToDictionary(n => n.Key, n => GetAntinodes(n.Value, rows.Length, rows[0].Length, true));

    var allAntinodes = antinodes.SelectMany(a => a.Value).Distinct().ToArray();

    return allAntinodes.Length.ToString();
  }

  private static bool IsInBound(Coordinates e, int width, int height)
  {
    return e.X >= 0 && e.X < width && e.Y >= 0 && e.Y < height;
  }
}
