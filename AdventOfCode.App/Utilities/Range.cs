namespace AdventOfCode.App.Utilities;

public class Range<T>(T start, T end)
  where T : IComparable<T>
{
  public T Start { get; init; } = start;
  public T End { get; init; } = end;

  public bool Contains(T value)
  {
    return value.CompareTo(Start) >= 0 && value.CompareTo(End) <= 0;
  }

  public bool Overlaps(Range<T> other)
  {
    return Start.CompareTo(other.End) <= 0 && End.CompareTo(other.Start) >= 0;
  }
  
  public static Range Parse(string input)
  {
    var pieces = input.ToLongs('-');
    return new Range(pieces[0], pieces[1]);
  }
}

public class Range(long start, long end) : Range<long>(start, end)
{
  public long Size() => End - Start + 1;
}
