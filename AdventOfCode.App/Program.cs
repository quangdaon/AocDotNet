using System.CommandLine;
using AdventOfCode.App.Challenges;
using AdventOfCode.App.Core;
using AdventOfCode.App.Exceptions;
using AdventOfCode.App.Extras;

var yearOption = new Option<int>(
  ["--year", "-y"],
  "The Advent of Code event to run."
);

var dayOption = new Option<int>(
  ["--day", "-d"],
  "The Advent of Code challenge to run."
);

var rootCommand = new RootCommand("Runner of Advent of Code solutions.");
rootCommand.AddOption(yearOption);
rootCommand.AddOption(dayOption);

rootCommand.SetHandler((Action<int, int>)((year, day) =>
  {
    try
    {
      var processor = ChallengeProcessorResolver.Resolve<IChallengeProcessor>(year, day);

      var dayString = day.ToString().PadLeft(2, '0');
      var inputFilePath = $"./inputs/{year}/{dayString}/input.txt";
      var input = File.ReadAllText(inputFilePath);

      PrintResults(processor, input);
    }
    catch (UnresolvableProcessorException ex)
    {
      Console.WriteLine(ex.Message);
    }
  }),
  yearOption, dayOption);

var idArgument = new Argument<string>("id");
var extrasCommand = new Command("extras", "Extra processors for Advent of Code.");
extrasCommand.AddArgument(idArgument);

extrasCommand.SetHandler(async context =>
{
  var id = context.ParseResult.GetValueForArgument(idArgument);
  try
  {
    var processor = ExtraCommandResolver.Resolve<IExtraCommand>(id);
    await processor.Execute();
  }
  catch (UnresolvableProcessorException ex)
  {
    Console.WriteLine(ex.Message);
  }
});

rootCommand.AddCommand(extrasCommand);

return await rootCommand.InvokeAsync(args);

static void PrintResults(IChallengeProcessor processor, string input)
{
  try
  {
    var part1Result = processor.ProcessPart1Solution(input);
    Console.WriteLine($"Solution to part 1: {part1Result}");
  }
  catch (NotImplementedException)
  {
    Console.WriteLine("Part 1 has not been implemented.");
  }

  try
  {
    var part2Result = processor.ProcessPart2Solution(input);
    Console.WriteLine($"Solution to part 2: {part2Result}");
  }
  catch (NotImplementedException)
  {
    Console.WriteLine("Part 2 has not been implemented.");
  }
}
