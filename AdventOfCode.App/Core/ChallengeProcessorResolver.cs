using AdventOfCode.App.Exceptions;

namespace AdventOfCode.App.Core;

public class ChallengeProcessorResolver
{
  public static T Resolve<T>(int year, int day)
  {
    var processorType = typeof(T).Assembly
      .GetTypes()
      .FirstOrDefault(c => IsMatchingProcessorType<T>(c, year, day));

    if (processorType == null) throw new UnresolvableProcessorException(year, day);

    return ((T)Activator.CreateInstance(processorType));
  }


  private static bool IsMatchingProcessorType<T>(Type c, int year, int day)
  {
    if (!typeof(T).IsAssignableFrom(c) || c.IsInterface) return false;

    System.Attribute[] attrs = System.Attribute.GetCustomAttributes(c);

    foreach (System.Attribute attr in attrs)
    {
      if (attr is ChallengeProcessorAttribute)
      {
        ChallengeProcessorAttribute a = (ChallengeProcessorAttribute)attr;
        return a.Year == year && a.Day == day;
      }
    }

    return false;
  }
}