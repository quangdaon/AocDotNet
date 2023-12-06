using AdventOfCode.App.Core;

namespace AdventOfCode.App.Challenges;

public enum Color
{
  Red, Blue, Green
}

public record Game(int Number, IEnumerable<Round> Rounds);

public record Round(IEnumerable<Batch> Batches);

public record Batch(int Count, Color Color);

[ChallengeProcessor(2023, 2)]
public class Aoc2023Day02Processor : IChallengeProcessor
{
  public Game ParseGame(string line)
  {
    var gameSplit = line.Split(": ");
    var gameNumber = int.Parse(gameSplit[0].Replace("Game ", ""));
    var gameRow = gameSplit[1];

    var roundsSplit = gameRow.Split("; ");

    var rounds = roundsSplit.Select(ParseRound);

    return new Game(gameNumber, rounds);
  }

  private Round ParseRound(string roundRow)
  {
    var batchSplit = roundRow.Split(", ");
    var batches = batchSplit.Select(ParseBatch);
    return new Round(batches);
  }

  private Batch ParseBatch(string batchRow)
  {
    var batchSplit = batchRow.Split(" ");
    var count = int.Parse(batchSplit[0]);
    var color = batchSplit[1] switch
    {
      "green" => Color.Green,
      "blue" => Color.Blue,
      "red" => Color.Red,
      _ => throw new ArgumentOutOfRangeException(nameof(batchRow), "batchRow is not a valid color")
    };

    return new Batch(count, color);
  }

  public int GetPower(Game game)
  {
    
    var batches = game.Rounds.SelectMany(e => e.Batches).ToArray();
    
    var maxRed = batches.Where(b => b.Color == Color.Red).Max(b => b.Count);
    var maxBlue = batches.Where(b => b.Color == Color.Blue).Max(b => b.Count);
    var maxGreen = batches.Where(b => b.Color == Color.Green).Max(b => b.Count);

    return maxRed * maxBlue * maxGreen;
  }

  public string ProcessPart1Solution(string input)
  {
    const int maxRed = 12;
    const int maxGreen = 13;
    const int maxBlue = 14;
    
    var rows = input.Split(Environment.NewLine);
    var games = rows.Select(ParseGame);
    var sum = 0;
    
    foreach(var game in games)
    {
      var batches = game.Rounds.SelectMany(e => e.Batches);

      if (!batches.Any(b =>
            (b.Color == Color.Red && b.Count > maxRed) ||
            (b.Color == Color.Green && b.Count > maxGreen) ||
            (b.Color == Color.Blue && b.Count > maxBlue)))
        sum += game.Number;
    }

    return sum.ToString();
  }

  public string ProcessPart2Solution(string input)
  {
    var rows = input.Split(Environment.NewLine);
    var games = rows.Select(ParseGame);
    var sum = games.Sum(GetPower);

    return sum.ToString();
  }
}
