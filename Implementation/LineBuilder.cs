using InExchange.Format.UBL;

namespace FluentUbl.Implementation
{
  public class LineBuilder : Interfaces.ILineBuilder
  {
    private UblOrderLine _ublOrderLine = new UblOrderLine();

    public LineBuilder()
    {
      _ublOrderLine.LineItem = new UblLineItem();
    }
    public LineBuilder(UblLineItem line)
    {
      _ublOrderLine.LineItem = line;
    }

    public Interfaces.ILineBuilder BuildId(string id)
    {
      _ublOrderLine.LineItem.Id = new UblIdentifier(id);
      return this;
    }
    
    public Interfaces.ILineBuilder BuildDescription(string description)
    {
      _ublOrderLine.LineItem.Item = new UblItem() { Description = new UblText(description) };
      return this;
    }

    public UblOrderLine UblOrderLine
    {
      get { return _ublOrderLine; }
    }
  }
}