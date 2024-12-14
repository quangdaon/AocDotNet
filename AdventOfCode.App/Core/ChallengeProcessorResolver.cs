using AdventOfCode.App.Exceptions;

namespace AdventOfCode.App.Core;

public static class ChallengeProcessorResolver
{
  public static T Resolve<T>(int year, int day)
  {
    var processorType = typeof(T).Assembly
      .GetTypes()
      .FirstOrDefault(c => IsMatchingProcessorType<T>(c, year, day));

    if (processorType == null) throw new UnresolvableProcessorException(year, day);

    return (T)Activator.CreateInstance(processorType);
  }


  private static bool IsMatchingProcessorType<T>(Type c, int year, int day)
  {
    if (!typeof(T).IsAssignableFrom(c) || c.IsInterface) return false;

    var attrs = Attribute.GetCustomAttributes(c);

    return attrs.OfType<ChallengeProcessorAttribute>()
      .Select(a => a.Year == year && a.Day == day)
      .FirstOrDefault();
  }
}
