using System.CommandLine;
using AdventOfCode.App.Challenges;
using AdventOfCode.App.Core;
using AdventOfCode.App.Exceptions;

var yearOption = new Option<int>(
  new string[] { "--year", "-y" },
  "The Advent of Code event to run."
);

var dayOption = new Option<int>(
  new string[] { "--day", "-d" },
  "The Advent of Code challenge to run."
);

var rootCommand = new RootCommand("Runner of Advent of Code solutions.");
rootCommand.AddOption(yearOption);
rootCommand.AddOption(dayOption);

rootCommand.SetHandler((year, day) =>
    {
      try
      {
        var processor = ChallengeProcessorResolver.Resolve<IChallengeProcessor>(year, day);
        var input = "";

        var part1Result = processor.ProcessPart1Solution(input);
        Console.WriteLine($"Solution to part 1: {part1Result}");

        var part2Result = processor.ProcessPart2Solution(input);
        Console.WriteLine($"Solution to part 2: {part2Result}");
      }
      catch (UnresolvableProcessorException ex)
      {
        Console.WriteLine(ex.Message);
      }
    },
    yearOption, dayOption);

return await rootCommand.InvokeAsync(args);