using System.Linq;
using AdventOfCode.App.Challenges;
using Xunit;

namespace AdventOfCode.App.Tests.Challenges;

public class Aoc2023Day02ProcessorTest : ChallengeProcessorTests
{
  private readonly Aoc2023Day02Processor _processor;
  private const string LINE = "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red";
    
  public Aoc2023Day02ProcessorTest() : base(2023, 2)
  {
    _processor = new Aoc2023Day02Processor();
  }

  [Fact]
  public void ParseGame_GivenValidInput_GetsGameNumber()
  {
    var result = _processor.ParseGame(LINE);
    
    Assert.Equal(4, result.Number);
  }

  [Fact]
  public void ParseGame_GivenValidInput_GetsCorrectNumberOfRounds()
  {
    var result = _processor.ParseGame(LINE);
    
    Assert.Equal(3, result.Rounds.Count());
  }

  [Fact]
  public void ParseGame_GivenValidInput_ParsesRounds()
  {
    var result = _processor.ParseGame(LINE);
    var roundsArray = result.Rounds.ToArray();
    
    Assert.Equal(3, roundsArray[0].Batches.Count());
    Assert.Equal(2, roundsArray[1].Batches.Count());
    Assert.Equal(3, roundsArray[2].Batches.Count());
  }

  [Fact]
  public void ParseGame_GivenValidInput_ParsesBatches()
  {
    var result = _processor.ParseGame(LINE);
    var roundsArray = result.Rounds.ToArray();

    var round1Batches = roundsArray[0].Batches.ToArray();
    
    var round1Batch1 = round1Batches[0];
    Assert.Equal(1, round1Batch1.Count);
    Assert.Equal(Color.Green, round1Batch1.Color);
    
    var round1Batch2 = round1Batches[1];
    Assert.Equal(3, round1Batch2.Count);
    Assert.Equal(Color.Red, round1Batch2.Color);

    var round1Batch3 = round1Batches[2];
    Assert.Equal(6, round1Batch3.Count);
    Assert.Equal(Color.Blue, round1Batch3.Color);
  }

  [Theory]
  [InlineData("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", 48)]
  [InlineData("Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue", 12)]
  [InlineData("Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red", 1560)]
  [InlineData("Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red", 630)]
  [InlineData("Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green", 36)]
  public void GetPower_GivenSampleInputs_ReturnsProvidedResult(string inputRow, int expectedResult)
  {
    var game = _processor.ParseGame(inputRow);
    var result = _processor.GetPower(game);
    
    Assert.Equal(expectedResult, result);
  }

  [Fact]
  public void ProcessPart1Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();
    var result = _processor.ProcessPart1Solution(input);

    Assert.Equal("8", result);
  }

  [Fact]
  public void ProcessPart2Solution_GivenSampleInputs_ReturnsProvidedResult()
  {
    var input = GetSampleInput();
    var result = _processor.ProcessPart2Solution(input);

    Assert.Equal("2286", result);
  }
}
