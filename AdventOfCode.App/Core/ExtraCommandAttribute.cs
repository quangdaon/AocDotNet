namespace AdventOfCode.App.Core;

public class ExtraCommandAttribute :  Attribute
{
  public string Id { get; set; }

  public ExtraCommandAttribute(string id)
  {
    Id = id;
  }
}
