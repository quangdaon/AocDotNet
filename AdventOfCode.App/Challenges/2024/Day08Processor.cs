using AdventOfCode.App.Core;
using AdventOfCode.App.Utilities;
using Coordinates = (int x, int y);

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
    Coordinates slope = (node2.x - node1.x, node2.y - node1.y);

    List<Coordinates> antinodeCandidates = [node1, node2];

    var tl = node1;

    while (true)
    {
      tl = (tl.x - slope.x, tl.y - slope.y);
      if (!IsInBound(tl, width, height)) break;

      antinodeCandidates.Add(tl);
    }

    var br = node2;
    while (true)
    {
      br = (br.x + slope.x, br.y + slope.y);
      if (!IsInBound(br, width, height)) break;

      antinodeCandidates.Add(br);
    }

    return antinodeCandidates.Where(e => IsInBound(e, width, height)).ToArray();
  }

  public static Coordinates[] GetAntinodes(Coordinates node1, Coordinates node2, int width, int height)
  {
    Coordinates slope = (node2.x - node1.x, node2.y - node1.y);

    Coordinates[] antinodeCandidates =
    [
      (node1.x - slope.x, node1.y - slope.y),
      (node2.x + slope.x, node2.y + slope.y),
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
    return e.x >= 0 && e.x < width && e.y >= 0 && e.y < height;
  }
}
