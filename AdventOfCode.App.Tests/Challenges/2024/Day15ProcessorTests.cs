using System;
using AdventOfCode.App.Challenges;
using AdventOfCode.App.Utilities;
using Xunit;
using WarehouseTile = AdventOfCode.App.Challenges.Aoc2024Day15Processor.WarehouseTile;

namespace AdventOfCode.App.Tests.Challenges;

public class Aoc2024Day15ProcessorTest : ChallengeProcessorTests
{
  private readonly Aoc2024Day15Processor _processor;

  public Aoc2024Day15ProcessorTest() : base(2024, 15)
  {
    _processor = new Aoc2024Day15Processor();
  }

  [Fact]
  public void ParseWarehouse_GivenRows_ReturnsWarehouseGrid()
  {
    string[] rows =
    [
      "#####",
      "#..O#",
      "#.@.#",
      "#..O#",
      "#####"
    ];

    WarehouseTile[][] expected =
    [
      [WarehouseTile.Wall, WarehouseTile.Wall, WarehouseTile.Wall, WarehouseTile.Wall, WarehouseTile.Wall],
      [WarehouseTile.Wall, WarehouseTile.Empty, WarehouseTile.Empty, WarehouseTile.Box, WarehouseTile.Wall],
      [WarehouseTile.Wall, WarehouseTile.Empty, WarehouseTile.Robot, WarehouseTile.Empty, WarehouseTile.Wall],
      [WarehouseTile.Wall, WarehouseTile.Empty, WarehouseTile.Empty, WarehouseTile.Box, WarehouseTile.Wall],
      [WarehouseTile.Wall, WarehouseTile.Wall, WarehouseTile.Wall, WarehouseTile.Wall, WarehouseTile.Wall]
    ];

    var result = Aoc2024Day15Processor.ParseWarehouse(rows);

    Assert.Equal(expected, result);
  }

  [Fact]
  public void TryPushLeft_GivenPassableWarehouse_ShiftsLeft()
  {
    string[] initialRows =
    [
      "#######",
      "#.....#",
      "#.....#",
      "#.O@..#",
      "#.....#",
      "#.....#",
      "#######"
    ];

    string[] expectedRows =
    [
      "#######",
      "#.....#",
      "#.....#",
      "#O@...#",
      "#.....#",
      "#.....#",
      "#######"
    ];

    var expected = Aoc2024Day15Processor.ParseWarehouse(expectedRows);

    var warehouse = Aoc2024Day15Processor.ParseWarehouse(initialRows);
    var result = Aoc2024Day15Processor.TryPushX(warehouse, 3, 3, -1);

    Assert.Equal(-1, result);
    Assert.Equal(expected, warehouse);
  }

  [Fact]
  public void TryPushLeft_GivenFreeSpace_ShiftsLeft()
  {
    string[] initialRows =
    [
      "#######",
      "#.....#",
      "#.....#",
      "#..@..#",
      "#.....#",
      "#.....#",
      "#######"
    ];

    string[] expectedRows =
    [
      "#######",
      "#.....#",
      "#.....#",
      "#.@...#",
      "#.....#",
      "#.....#",
      "#######"
    ];

    var expected = Aoc2024Day15Processor.ParseWarehouse(expectedRows);

    var warehouse = Aoc2024Day15Processor.ParseWarehouse(initialRows);
    var result = Aoc2024Day15Processor.TryPushX(warehouse, 3, 3, -1);

    Assert.Equal(-1, result);
    Assert.Equal(expected, warehouse);
  }

  [Fact]
  public void TryPushLeft_GivenBlockedWarehouse_RemainsUnchanged()
  {
    string[] initialRows =
    [
      "#######",
      "#.....#",
      "#.....#",
      "#OO@..#",
      "#.....#",
      "#.....#",
      "#######"
    ];

    string[] expectedRows =
    [
      "#######",
      "#.....#",
      "#.....#",
      "#OO@..#",
      "#.....#",
      "#.....#",
      "#######"
    ];

    var expected = Aoc2024Day15Processor.ParseWarehouse(expectedRows);

    var warehouse = Aoc2024Day15Processor.ParseWarehouse(initialRows);
    var result = Aoc2024Day15Processor.TryPushX(warehouse, 3, 3, -1);

    Assert.Equal(0, result);
    Assert.Equal(expected, warehouse);
  }

  [Fact]
  public void TryPushLeft_GivenWall_RemainsUnchanged()
  {
    string[] initialRows =
    [
      "#######",
      "#.....#",
      "#.....#",
      "#.#@..#",
      "#.....#",
      "#.....#",
      "#######"
    ];

    string[] expectedRows =
    [
      "#######",
      "#.....#",
      "#.....#",
      "#.#@..#",
      "#.....#",
      "#.....#",
      "#######"
    ];

    var expected = Aoc2024Day15Processor.ParseWarehouse(expectedRows);

    var warehouse = Aoc2024Day15Processor.ParseWarehouse(initialRows);
    var result = Aoc2024Day15Processor.TryPushX(warehouse, 3, 3, -1);

    Assert.Equal(0, result);
    Assert.Equal(expected, warehouse);
  }

  [Fact]
  public void TryPushRight_GivenPassableWarehouse_ShiftsRight()
  {
    string[] initialRows =
    [
      "#######",
      "#.....#",
      "#.....#",
      "#..@O.#",
      "#.....#",
      "#.....#",
      "#######"
    ];

    string[] expectedRows =
    [
      "#######",
      "#.....#",
      "#.....#",
      "#...@O#",
      "#.....#",
      "#.....#",
      "#######"
    ];

    var expected = Aoc2024Day15Processor.ParseWarehouse(expectedRows);

    var warehouse = Aoc2024Day15Processor.ParseWarehouse(initialRows);
    var result = Aoc2024Day15Processor.TryPushX(warehouse, 3, 3, 1);

    Assert.Equal(1, result);
    Assert.Equal(expected, warehouse);
  }

  [Fact]
  public void TryPushRight_GivenFreeSpace_ShiftsRight()
  {
    string[] initialRows =
    [
      "#######",
      "#.....#",
      "#.....#",
      "#..@..#",
      "#.....#",
      "#.....#",
      "#######"
    ];

    string[] expectedRows =
    [
      "#######",
      "#.....#",
      "#.....#",
      "#...@.#",
      "#.....#",
      "#.....#",
      "#######"
    ];

    var expected = Aoc2024Day15Processor.ParseWarehouse(expectedRows);

    var warehouse = Aoc2024Day15Processor.ParseWarehouse(initialRows);
    var result = Aoc2024Day15Processor.TryPushX(warehouse, 3, 3, 1);

    Assert.Equal(1, result);
    Assert.Equal(expected, warehouse);
  }

  [Fact]
  public void TryPushRight_GivenBlockedWarehouse_RemainsUnchanged()
  {
    string[] initialRows =
    [
      "#######",
      "#.....#",
      "#.....#",
      "#..@OO#",
      "#.....#",
      "#.....#",
      "#######"
    ];

    string[] expectedRows =
    [
      "#######",
      "#.....#",
      "#.....#",
      "#..@OO#",
      "#.....#",
      "#.....#",
      "#######"
    ];

    var expected = Aoc2024Day15Processor.ParseWarehouse(expectedRows);

    var warehouse = Aoc2024Day15Processor.ParseWarehouse(initialRows);
    var result = Aoc2024Day15Processor.TryPushX(warehouse, 3, 3, 1);

    Assert.Equal(0, result);
    Assert.Equal(expected, warehouse);
  }

  [Fact]
  public void TryPushRight_GivenWall_RemainsUnchanged()
  {
    string[] initialRows =
    [
      "#######",
      "#.....#",
      "#.....#",
      "#..@#.#",
      "#.....#",
      "#.....#",
      "#######"
    ];

    string[] expectedRows =
    [
      "#######",
      "#.....#",
      "#.....#",
      "#..@#.#",
      "#.....#",
      "#.....#",
      "#######"
    ];

    var expected = Aoc2024Day15Processor.ParseWarehouse(expectedRows);

    var warehouse = Aoc2024Day15Processor.ParseWarehouse(initialRows);
    var result = Aoc2024Day15Processor.TryPushX(warehouse, 3, 3, 1);

    Assert.Equal(0, result);
    Assert.Equal(expected, warehouse);
  }

  [Fact]
  public void TryPushUp_GivenPassableWarehouse_ShiftsUp()
  {
    string[] initialRows =
    [
      "#######",
      "#.....#",
      "#..O..#",
      "#..@..#",
      "#.....#",
      "#.....#",
      "#######"
    ];

    string[] expectedRows =
    [
      "#######",
      "#..O..#",
      "#..@..#",
      "#.....#",
      "#.....#",
      "#.....#",
      "#######"
    ];

    var expected = Aoc2024Day15Processor.ParseWarehouse(expectedRows);

    var warehouse = Aoc2024Day15Processor.ParseWarehouse(initialRows);
    var result = Aoc2024Day15Processor.TryPushY(warehouse, 3, 3, -1);

    Assert.Equal(-1, result);
    Assert.Equal(expected, warehouse);
  }

  [Fact]
  public void TryPushUp_GivenFreeSpace_ShiftsUp()
  {
    string[] initialRows =
    [
      "#######",
      "#.....#",
      "#.....#",
      "#..@..#",
      "#.....#",
      "#.....#",
      "#######"
    ];

    string[] expectedRows =
    [
      "#######",
      "#.....#",
      "#..@..#",
      "#.....#",
      "#.....#",
      "#.....#",
      "#######"
    ];

    var expected = Aoc2024Day15Processor.ParseWarehouse(expectedRows);

    var warehouse = Aoc2024Day15Processor.ParseWarehouse(initialRows);
    var result = Aoc2024Day15Processor.TryPushY(warehouse, 3, 3, -1);

    Assert.Equal(-1, result);
    Assert.Equal(expected, warehouse);
  }

  [Fact]
  public void TryPushUp_GivenBlockedWarehouse_RemainsUnchanged()
  {
    string[] initialRows =
    [
      "#######",
      "#..O..#",
      "#..O..#",
      "#..@..#",
      "#.....#",
      "#.....#",
      "#######"
    ];

    string[] expectedRows =
    [
      "#######",
      "#..O..#",
      "#..O..#",
      "#..@..#",
      "#.....#",
      "#.....#",
      "#######"
    ];

    var expected = Aoc2024Day15Processor.ParseWarehouse(expectedRows);

    var warehouse = Aoc2024Day15Processor.ParseWarehouse(initialRows);
    var result = Aoc2024Day15Processor.TryPushY(warehouse, 3, 3, -1);

    Assert.Equal(0, result);
    Assert.Equal(expected, warehouse);
  }

  [Fact]
  public void TryPushUp_GivenWall_RemainsUnchanged()
  {
    string[] initialRows =
    [
      "#######",
      "#.....#",
      "#..#..#",
      "#..@..#",
      "#.....#",
      "#.....#",
      "#######"
    ];

    string[] expectedRows =
    [
      "#######",
      "#.....#",
      "#..#..#",
      "#..@..#",
      "#.....#",
      "#.....#",
      "#######"
    ];

    var expected = Aoc2024Day15Processor.ParseWarehouse(expectedRows);

    var warehouse = Aoc2024Day15Processor.ParseWarehouse(initialRows);
    var result = Aoc2024Day15Processor.TryPushY(warehouse, 3, 3, -1);

    Assert.Equal(0, result);
    Assert.Equal(expected, warehouse);
  }

  [Fact]
  public void TryPushDown_GivenPassableWarehouse_ShiftsDown()
  {
    string[] initialRows =
    [
      "#######",
      "#.....#",
      "#.....#",
      "#..@..#",
      "#..O..#",
      "#.....#",
      "#######"
    ];

    string[] expectedRows =
    [
      "#######",
      "#.....#",
      "#.....#",
      "#.....#",
      "#..@..#",
      "#..O..#",
      "#######"
    ];

    var expected = Aoc2024Day15Processor.ParseWarehouse(expectedRows);

    var warehouse = Aoc2024Day15Processor.ParseWarehouse(initialRows);
    var result = Aoc2024Day15Processor.TryPushY(warehouse, 3, 3, 1);

    Assert.Equal(1, result);
    Assert.Equal(expected, warehouse);
  }

  [Fact]
  public void TryPushDown_GivenFreeSpace_ShiftsDown()
  {
    string[] initialRows =
    [
      "#######",
      "#.....#",
      "#.....#",
      "#..@..#",
      "#.....#",
      "#.....#",
      "#######"
    ];

    string[] expectedRows =
    [
      "#######",
      "#.....#",
      "#.....#",
      "#.....#",
      "#..@..#",
      "#.....#",
      "#######"
    ];

    var expected = Aoc2024Day15Processor.ParseWarehouse(expectedRows);

    var warehouse = Aoc2024Day15Processor.ParseWarehouse(initialRows);
    var result = Aoc2024Day15Processor.TryPushY(warehouse, 3, 3, 1);

    Assert.Equal(1, result);
    Assert.Equal(expected, warehouse);
  }

  [Fact]
  public void TryPushDown_GivenBlockedWarehouse_RemainsUnchanged()
  {
    string[] initialRows =
    [
      "#######",
      "#.....#",
      "#.....#",
      "#..@..#",
      "#..O..#",
      "#..O..#",
      "#######"
    ];

    string[] expectedRows =
    [
      "#######",
      "#.....#",
      "#.....#",
      "#..@..#",
      "#..O..#",
      "#..O..#",
      "#######"
    ];

    var expected = Aoc2024Day15Processor.ParseWarehouse(expectedRows);

    var warehouse = Aoc2024Day15Processor.ParseWarehouse(initialRows);
    var result = Aoc2024Day15Processor.TryPushY(warehouse, 3, 3, 1);

    Assert.Equal(0, result);
    Assert.Equal(expected, warehouse);
  }

  [Fact]
  public void TryPushDown_GivenWall_RemainsUnchanged()
  {
    string[] initialRows =
    [
      "#######",
      "#.....#",
      "#.....#",
      "#..@..#",
      "#..#..#",
      "#.....#",
      "#######"
    ];

    string[] expectedRows =
    [
      "#######",
      "#.....#",
      "#.....#",
      "#..@..#",
      "#..#..#",
      "#.....#",
      "#######"
    ];

    var expected = Aoc2024Day15Processor.ParseWarehouse(expectedRows);

    var warehouse = Aoc2024Day15Processor.ParseWarehouse(initialRows);
    var result = Aoc2024Day15Processor.TryPushY(warehouse, 3, 3, 1);

    Assert.Equal(0, result);
    Assert.Equal(expected, warehouse);
  }

  [Fact]
  public void ApplyInstructions_GivenSmallerSampleInputs_ReturnsProvidedResults()
  {
    var input = GetSampleInput(2);
    var components = input.Split(Environment.NewLine + Environment.NewLine);
    var warehouse = Aoc2024Day15Processor.ParseWarehouse(components[0].ToRows());
    var instructions = Aoc2024Day15Processor.ParseInstructions(components[1]);
    
    string[] expectedRows =
    [
      "########",
      "#....OO#",
      "##.....#",
      "#.....O#",
      "#.#O@..#",
      "#...O..#",
      "#...O..#",
      "########"
    ];

    var expected = Aoc2024Day15Processor.ParseWarehouse(expectedRows);

    Aoc2024Day15Processor.ApplyInstructions(warehouse, instructions);
    
    Assert.Equal(expected, warehouse);
  }

  [Fact]
  public void ApplyInstructions_GivenLargerSampleInputs_ReturnsProvidedResults()
  {
    var input = GetSampleInput(1);
    var components = input.Split(Environment.NewLine + Environment.NewLine);
    var warehouse = Aoc2024Day15Processor.ParseWarehouse(components[0].ToRows());
    var instructions = Aoc2024Day15Processor.ParseInstructions(components[1]);

    string[] expectedRows =
    [
      "##########",
      "#.O.O.OOO#",
      "#........#",
      "#OO......#",
      "#OO@.....#",
      "#O#.....O#",
      "#O.....OO#",
      "#O.....OO#",
      "#OO....OO#",
      "##########"
    ];

    var expected = Aoc2024Day15Processor.ParseWarehouse(expectedRows);
    
    Aoc2024Day15Processor.ApplyInstructions(warehouse, instructions);
    
    Assert.Equal(expected, warehouse);
  }

  [Theory]
  [InlineData(1, "10092")]
  [InlineData(2, "2028")]
  public void ProcessPart1Solution_GivenSampleInputs_ReturnsProvidedResult(byte sample, string expectedResult)
  {
    var input = GetSampleInput(sample);

    var result = _processor.ProcessPart1Solution(input);

    Assert.Equal(expectedResult, result);
  }

  [Fact]
  public void ProcessPart2Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();

    var result = _processor.ProcessPart2Solution(input);

    Assert.Equal("???", result);
  }
}