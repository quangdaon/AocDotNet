using AdventOfCode.App.Core;

namespace AdventOfCode.App.Challenges;

public record Schematic(IEnumerable<SchematicComponent> Components, IEnumerable<int> SymbolIndices);

public record SchematicComponent(int Number, int Length, int StartingIndex);

[ChallengeProcessor(2023, 3)]
public class Aoc2023Day03Processor : IChallengeProcessor
{
  public Schematic ParseSchematics(string schematic)
  {
    var components = new List<SchematicComponent>();
    var symbolIndices = new List<int>();
    var currentNumber = string.Empty;

    for (var i = 0; i < schematic.Length; i++)
    {
      var character = schematic[i];
      if (char.IsNumber(character))
      {
        currentNumber += character;
        continue;
      }

      if (!string.IsNullOrEmpty(currentNumber))
      {
        components.Add(new SchematicComponent(int.Parse(currentNumber), currentNumber.Length,
          i - currentNumber.Length));
        currentNumber = string.Empty;
      }

      if (character != '.') symbolIndices.Add(i);
    }

    return new Schematic(components, symbolIndices);
  }

  public IEnumerable<int> GetPartNumbers(int rowSize, Schematic schematic)
  {
    return schematic.Components
      .Where(comp => IsPartNumber(rowSize, schematic.SymbolIndices.ToArray(), comp))
      .Select(comp => comp.Number);
  }

  private bool IsPartNumber(int rowSize, int[] symbolIndices, SchematicComponent component)
  {
    var startIndex = component.StartingIndex;
    var endIndex = component.StartingIndex + component.Length - 1;

    var notTouchingLeft = startIndex % rowSize > 0;
    var notTouchingRight = endIndex % rowSize < rowSize - 1;
    
    if (notTouchingLeft && symbolIndices.Contains(startIndex - 1)) return true;
    if (notTouchingRight && symbolIndices.Contains(endIndex + 1)) return true;

    var offsetLeft = notTouchingLeft ? 1 : 0;
    var offsetRight = notTouchingRight ? 1 : 0;

    var topStartIndex = startIndex - rowSize;

    if (topStartIndex > 0)
    {
      var topEndIndex = endIndex - rowSize;

      for (var i = topStartIndex - offsetLeft; i <= topEndIndex + offsetRight; i++)
      {
        if (symbolIndices.Contains(i)) return true;
      }
    }

    var bottomStartIndex = startIndex + rowSize;
    var bottomEndIndex = endIndex + rowSize;

    for (var i = bottomStartIndex - offsetLeft; i <= bottomEndIndex + offsetRight; i++)
    {
      if (symbolIndices.Contains(i)) return true;
    }

    return false;
  }

  public string ProcessPart1Solution(string input)
  {
    var rows = input.Split(Environment.NewLine);
    var rowSize = rows[0].Length;
    var schematicsString = string.Join("", rows);
    var schematic = ParseSchematics(schematicsString);

    var partNumbers = GetPartNumbers(rowSize, schematic);

    return partNumbers.Sum().ToString();
  }

  public string ProcessPart2Solution(string input)
  {
    throw new NotImplementedException();
  }
}