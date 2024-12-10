using AdventOfCode.App.Core;
using AdventOfCode.App.Utilities;

namespace AdventOfCode.App.Challenges;

[ChallengeProcessor(2015, 5)]
public class Aoc2015Day05Processor : IChallengeProcessor
{
  private readonly char[] _vowels = { 'a', 'e', 'i', 'o', 'u' };
  private readonly string[] _disallowedStrings = { "ab", "cd", "pq", "xy" };
  
  public bool MeetsPart1Rules(string input)
  {
    var currentVowelsCount = 0;
    char? lastChar = null;
    var vowelsRuleMet = false;
    var doublesRuleMet = false;
    var chars = input.ToCharArray();

    if (_disallowedStrings.Any(input.Contains)) return false;

    foreach(var chr in chars)
    {
      if (chr == lastChar) doublesRuleMet = true;
      lastChar = chr;

      if (_vowels.Contains(chr))
      {
        currentVowelsCount++;
        if (currentVowelsCount >= 3) vowelsRuleMet = true;
      }
      
      if (vowelsRuleMet && doublesRuleMet) return true;
    }
    
    return false;
  }
  
  public bool MeetsPart2Rules(string input)
  {
    var current = input[..2];
    var rest = input[2..];

    var pairsRuleMet = false;
    var repeatRuleMet = false;

    while (rest.Length > 0)
    {
      if (current[0] == rest[0]) repeatRuleMet = true;
      if (rest.Contains(current)) pairsRuleMet = true;

      if (repeatRuleMet && pairsRuleMet) return true;

      current = current[1..] + rest[..1];
      rest = rest[1..];
    }
    
    return false;
  }
  
  public string ProcessPart1Solution(string input)
  {
    var rows = input.ToRows();
    return rows.Count(MeetsPart1Rules).ToString();
  }

  public string ProcessPart2Solution(string input)
  {
    var rows = input.ToRows();
    return rows.Count(MeetsPart2Rules).ToString();
  }
}
