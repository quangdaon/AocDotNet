using AdventOfCode.App.Exceptions;

namespace AdventOfCode.App.Core;

public static class ExtraCommandResolver
{
  public static T Resolve<T>(string id)
  {
    var processorType = typeof(T).Assembly
      .GetTypes()
      .FirstOrDefault(c => IsMatchingProcessorType<T>(c, id));

    if (processorType == null) throw new UnresolvableProcessorException(id);

    return (T)Activator.CreateInstance(processorType);
  }

  private static bool IsMatchingProcessorType<T>(Type c, string id)
  {
    if (!typeof(T).IsAssignableFrom(c) || c.IsInterface) return false;

    var attrs = Attribute.GetCustomAttributes(c);

    return attrs.OfType<ExtraCommandAttribute>()
      .Select(a => a.Id == id)
      .FirstOrDefault();
  }
}
