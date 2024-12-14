namespace AdventOfCode.App.Exceptions;

public class UnresolvableProcessorException : Exception
{
  public UnresolvableProcessorException(int year, int day) : 
    base($"The processor for AoC {year} Day {day} cannot be resolved.")
  {
    
  }
  
  public UnresolvableProcessorException(string id) : 
    base($"The processor for \"{id}\" cannot be resolved.")
  {
    
  }
}