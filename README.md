# Advent of Code

My solutions to Advent of Code challenges.

## Generator

https://github.com/quangdaon/generator-aocdotnet

This is a Yeoman generator to create challenge processors.

## Running Tests

```
dotnet test ./AdventOfCode.App.Tests/
```

## Running Program

You will need to create a file called `input.txt` inside the `inputs` folder for a valid input from AOC for this challenge.

```
dotnet run --project ./AdventOfCode.App/ -- --year {year} --day {day}
```

Warning: This program outputs the solutions to BOTH parts of each day. Beware of spoilers.
