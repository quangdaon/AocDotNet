# Advent of Code

My solutions to Advent of Code challenges.

## Setup

After cloning the repo, create an `inputs` folder at the root of the repo. For each input you need to run, save it to `inputs/{year}/{day}/input.txt`, where year is the 4-digit `{year}` of the event and `{day}` is the 2-digit day (e.g. `inputs/2023/02`).

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

## Generator

https://github.com/quangdaon/generator-aocdotnet

This is a Yeoman generator to create challenge processors.