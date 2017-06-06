namespace FluentUbl.Interfaces
{
  public interface ILineBuilder
  {
    ILineBuilder BuildId(string id);
    ILineBuilder BuildDescription(string description);
  }
}