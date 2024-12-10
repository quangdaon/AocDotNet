using AdventOfCode.App.Core;
using AdventOfCode.App.Utilities;
using Block = (int Value, int Size);

namespace AdventOfCode.App.Challenges;

[ChallengeProcessor(2024, 9)]
public class Aoc2024Day09Processor : IChallengeProcessor
{
  public static IEnumerable<Block> ParseBlocks(string input)
  {
    var digits = Parsing.ToDigits(input);
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

      var indexAvailable = FindAvailableIndex(fragmented, blocks, indexRight);

      if (indexAvailable == -1)
      {
        if (fragmented) break;
        idRight--;
        continue;
      }

      if (blocks[indexAvailable].Size < blocks[indexRight].Size)
      {
        ShardFile(blocks, indexRight, indexAvailable);
        continue;
      }

      MoveFile(blocks, indexRight, indexAvailable);

      idRight--;
    }

    TrimEmptyBlocks(blocks);

    return ExpandBlocks(blocks);
  }

  private static void MoveFile(List<Block> blocks, int target, int destination)
  {
    var file = blocks[target];

    blocks[target] = (0, file.Size);
    blocks[destination] = (blocks[destination].Value, blocks[destination].Size - file.Size);

    if (blocks[destination].Size <= 0) blocks.RemoveAt(destination);

    blocks.Insert(destination, file);
  }

  private static void ShardFile(List<Block> blocks, int target, int destination)
  {
    blocks[destination] = (blocks[target].Value, blocks[destination].Size);
    blocks[target] = (blocks[target].Value, blocks[target].Size - blocks[destination].Size);
  }

  private static void TrimEmptyBlocks(List<Block> blocks)
  {
    for (var i = blocks.Count - 1; i >= 0; i--)
    {
      if (blocks[i].Value != 0) break;
      blocks.RemoveAt(i);
    }
  }

  private static int FindAvailableIndex(bool fragmented, List<Block> blocks, int target)
  {
    var size = blocks[target].Size;
    
    // = 1 to skip the first ID=0 block
    var indexAvailable = 1;

    while (indexAvailable < target)
    {
      if (blocks[indexAvailable].Value == 0 && (fragmented || blocks[indexAvailable].Size >= size))
        return indexAvailable;

      indexAvailable++;
    }

    return -1;
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