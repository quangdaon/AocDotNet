using AdventOfCode.App.Core;
using Block = (int Value, int Size);

namespace AdventOfCode.App.Challenges;

[ChallengeProcessor(2024, 9)]
public class Aoc2024Day09Processor : IChallengeProcessor
{
  public static IEnumerable<Block> ParseBlocks(string input)
  {
    var digits = ToDigits(input);
    return digits.Select((e, i) => (i % 2 != 0 ? 0 : i / 2, e)).Where(e => e != (0, 0));
  }

  public static IEnumerable<int> ExpandBlocks(IEnumerable<Block> blocks)
  {
    return blocks.SelectMany(b => Enumerable.Repeat(b.Value, b.Size));
  }

  public static IEnumerable<int> RearrangeBlocks(string input, bool fragmented = true)
  {
    var blocks = ParseBlocks(input).ToList();
    var idRight = blocks.Last().Value;

    while (idRight > 0)
    {
      var indexRight = blocks.FindLastIndex(b => b.Value == idRight);
      var processing = blocks[indexRight];
      Block available = (-1, -1);

      int i;

      // i = 1 to skip the first ID=0 block
      for (i = 1; i < indexRight; i++)
      {
        if (blocks[i].Value != 0 || (!fragmented && blocks[i].Size < processing.Size)) continue;

        available = blocks[i];
        break;
      }

      if (available.Value == -1)
      {
        if (fragmented) break;
        idRight--;
        continue;
      }

      if (available.Size >= processing.Size)
      {
        blocks[indexRight] = (0, processing.Size);

        available.Size -= processing.Size;
        blocks[i] = available;

        if (available.Size <= 0) blocks.RemoveAt(i);

        blocks.Insert(i, processing);
        idRight--;
      }
      else
      {
        available.Value = processing.Value;
        processing.Size -= available.Size;

        blocks[i] = available;
        blocks[indexRight] = processing;
      }
    }

    for (var i = blocks.Count - 1; i >= 0; i--)
    {
      if (blocks[i].Value != 0) break;
      blocks.RemoveAt(i);
    }

    return ExpandBlocks(blocks);
  }

  public static IEnumerable<int> ToDigits(string input)
  {
    return input.ToCharArray().Select(e => e - 48);
  }

  public string ProcessPart1Solution(string input)
  {
    var filesystem = RearrangeBlocks(input);

    return filesystem.Select((e, i) => Convert.ToInt64(e * i)).Sum().ToString();
  }

  public string ProcessPart2Solution(string input)
  {
    var filesystem = RearrangeBlocks(input, false);

    return filesystem.Select((e, i) => Convert.ToInt64(e * i)).Sum().ToString();
  }
}