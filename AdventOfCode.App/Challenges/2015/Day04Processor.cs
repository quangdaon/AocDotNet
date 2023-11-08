using System.Security.Cryptography;
using System.Text;
using AdventOfCode.App.Core;

namespace AdventOfCode.App.Challenges;

[ChallengeProcessor(2015, 4)]
public class Aoc2015Day04Processor : IChallengeProcessor
{
  public string ProcessPart1Solution(string input) => FindHashThatStartsWith(input, "00000");

  public string ProcessPart2Solution(string input) => FindHashThatStartsWith(input, "000000");

  private static string FindHashThatStartsWith(string input, string start)
  {
    var num = 1;

    while (true)
    {
      var str = input + num;
      var byteArray = Encoding.UTF8.GetBytes(str);
      var hash = MD5.HashData(byteArray);
      var hex = Convert.ToHexString(hash);

      if (hex.StartsWith(start)) return num.ToString();

      num++;
    }
  }
}
