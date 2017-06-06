namespace FluentUbl.Interfaces
{
  public interface IBuyerPartyBuilder
  {
    IBuyerPartyBuilder BuildName(string name);
    IBuyerPartyBuilder BuildGln(string gln);
  }
}