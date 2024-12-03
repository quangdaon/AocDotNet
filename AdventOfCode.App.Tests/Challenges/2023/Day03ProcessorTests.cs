using System.Collections.Generic;
using AdventOfCode.App.Challenges;
using Xunit;

namespace AdventOfCode.App.Tests.Challenges;

public class Aoc2023Day03ProcessorTest : ChallengeProcessorTests
{
  private readonly Aoc2023Day03Processor _processor;

  public Aoc2023Day03ProcessorTest() : base(2023, 3)
  {
    _processor = new Aoc2023Day03Processor();
  }


  public static IEnumerable<object[]> SchematicsTestData()
  {
    yield return new object[]
    {
      "..*.876.",
      new[] { new SchematicComponent(876, 3, 4) },
      new[] { 2 }
    };

    yield return new object[]
    {
      ".54^.#.",
      new[] { new SchematicComponent(54, 2, 1) },
      new[] { 3, 5 }
    };

    yield return new object[]
    {
      ".#8391..",
      new[] { new SchematicComponent(8391, 4, 2) },
      new[] { 1 }
    };

    yield return new object[]
    {
      "........................",
      System.Array.Empty<SchematicComponent>(),
      System.Array.Empty<int>(),
    };

    yield return new object[]
    {
      "*****..............*****",
      System.Array.Empty<SchematicComponent>(),
      new[] { 0, 1, 2, 3, 4, 19, 20, 21, 22, 23 }
    };

    yield return new object[]
    {
      "..84..5*...927..45..%...",
      new[]
      {
        new SchematicComponent(84, 2, 2),
        new SchematicComponent(5, 1, 6),
        new SchematicComponent(927, 3, 11),
        new SchematicComponent(45, 2, 16)
      },
      new[] { 7, 20 }
    };
  }

  [Theory]
  [MemberData(nameof(SchematicsTestData))]
  public void ParseSchematic_GivenRow_ReturnsPositionalNumbers(string input, SchematicComponent[] expectedResults,
    int[] _)
  {
    var result = _processor.ParseSchematics(input);

    Assert.Equal(expectedResults, result.Components);
  }

  [Theory]
  [MemberData(nameof(SchematicsTestData))]
  public void ParseSchematic_GivenRow_ReturnsSymbolIndices(string input, SchematicComponent[] _, int[] expectedResults)
  {
    var result = _processor.ParseSchematics(input);

    Assert.Equal(expectedResults, result.SymbolIndices);
  }

  [Theory]
  [InlineData("........", new int[] { })] // Parses no part numbers
  [InlineData(".234....", new int[] { })] // Skips unsymboled
  [InlineData(".234#...", new[] { 234 })] // Adjacent right
  [InlineData(".#.^234.", new[] { 234 })] // Adjacent left
  [InlineData(".....^...#..234.........", new[] { 234 })] // Adjacent top
  [InlineData("............234......*..", new[] { 234 })] // Adjacent bottom
  [InlineData("...*........234.........", new[] { 234 })] // Adjacent diagonal TL
  [InlineData(".......*....234.........", new[] { 234 })] // Adjacent diagonal TR
  [InlineData("............234....*....", new[] { 234 })] // Adjacent diagonal BL
  [InlineData("............234........*", new[] { 234 })] // Adjacent diagonal BR
  [InlineData("...16*......234..77.....", new[] { 16, 234 })] // Multiple
  [InlineData(".......*46....74........", new[] { 74 })] // Does not wrap back
  [InlineData("........46....74^.......", new[] { 46 })] // Does not wrap forward
  public void GetPartNumbers_GivenSchematics_ReturnsPartNumbers(string input, int[] expectedResults)
  {
    const int rowSize = 8;

    var schematic = _processor.ParseSchematics(input);
    var result = _processor.GetPartNumbers(rowSize, schematic);
    
    Assert.Equal(expectedResults, result);
  }

  [Fact]
  public void ProcessPart1Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();
    var result = _processor.ProcessPart1Solution(input);

    Assert.Equal("4361", result);
  }
}