using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InExchange.Format.UBL;

namespace FluentUbl
{

  public interface IOrderBuilder
  {
    IOrderBuilder BuildId(string id);
    IOrderBuilder BuildIssueDate(DateTime date);
    IOrderBuilder BuildIssueTime(DateTime time);
    IOrderBuilder BuildBuyerParty(Func<IBuyerPartyBuilder, IBuyerPartyBuilder> buyerPartyBuilder);
    IOrderBuilder BuildLines(Func<IEnumerable<ILineBuilder>> lineBuilder);
    UblOrder GetUblOrder();
  }

  public interface IBuyerPartyBuilder
  {
    IBuyerPartyBuilder SetName(string name);
    IBuyerPartyBuilder SetGln(string gln);
  }

  public interface ILinesBuilder
  {
    ILineBuilder BuildLine();
  }

  public interface ILineBuilder
  {
    ILineBuilder SetId(string id);
    ILineBuilder SetDescription(string description);
  }

  public class OrderBuilder : IOrderBuilder
  {
    private UblOrder _ublOrder = UblDocument.Create<UblOrder>(UblProfiles.Bii6ProcurementExtended);

    public OrderBuilder()
    {
      _ublOrder.BuyerCustomerParty = new UblCustomerParty();
    }

    public IOrderBuilder BuildId(string id)
    {
      _ublOrder.Id = new UblIdentifier(id);
      return this;
    }

    public IOrderBuilder BuildIssueDate(DateTime date)
    {
      _ublOrder.IssueDate = new UblDate(date);
      return this;
    }

    public IOrderBuilder BuildIssueTime(DateTime time)
    {
      _ublOrder.IssueTime = new UblTime(time);
      return this;
    }

    public IOrderBuilder BuildBuyerParty(Func<IBuyerPartyBuilder, IBuyerPartyBuilder> buyerPartyBuilder)
    {
      buyerPartyBuilder.Invoke(new BuyerPartyBuilder(_ublOrder.BuyerCustomerParty));
      return this;
    }

    public IOrderBuilder BuildLines(Func<IEnumerable<ILineBuilder>> lineBuilder)
    {
      _ublOrder.OrderLine = new List<UblOrderLine>();
      foreach (LineBuilder builder in lineBuilder())
      {
        _ublOrder.OrderLine.Add(builder.UblOrderLine);
      }
      return this;
    }

    public IOrderBuilder BuildLines(object unknown)
    {
      return this;
    }

    public OrderBuilder BuyerCustomerParty(UblCustomerParty ublCustomerParty)
    {
      this._ublOrder.BuyerCustomerParty = ublCustomerParty;
      return this;
    }

    public UblOrder GetUblOrder()
    {
      return _ublOrder;
    }
  }

  public class BuyerPartyBuilder : IBuyerPartyBuilder
  {
    private UblCustomerParty _ublCustomerParty;

    public BuyerPartyBuilder(UblCustomerParty ublCustomerParty)
    {
      _ublCustomerParty = ublCustomerParty;
      _ublCustomerParty.Party = new UblParty();
    }
    public IBuyerPartyBuilder SetName(string name)
    {
      _ublCustomerParty.Party.PartyName = new UblPartyName(name);
      return this;
    }

    public IBuyerPartyBuilder SetGln(string gln)
    {
      _ublCustomerParty.Party.PartyIdentifications = new List<UblPartyIdentification>()
      {
        new UblPartyIdentification(new UblIdentifier
        {
          Value = gln,
          SchemeAgencyId = "GLN",
          SchemeAgencyName = "INX"
        })
      };
      return this;
    }
  }

  public class LinesBuilder : ILinesBuilder
  {
    private List<UblOrderLine> _ublOrderLines;

    public LinesBuilder(List<UblOrderLine> ublOrderLines)
    {
      _ublOrderLines = ublOrderLines;
    }

    public ILineBuilder BuildLine()
    {
      var line = new UblOrderLine();
      _ublOrderLines.Add(line);
      return new LineBuilder(line.LineItem);
    }
  }

  
  public class LineBuilder : ILineBuilder
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

    public ILineBuilder SetId(string id)
    {
      _ublOrderLine.LineItem.Id = new UblIdentifier(id);
      return this;
    }
    
    public ILineBuilder SetDescription(string description)
    {
      _ublOrderLine.LineItem.Item = new UblItem() { Description = new UblText(description) };
      return this;
    }

    public UblOrderLine UblOrderLine
    {
      get { return _ublOrderLine; }
    }
  }

  public class MyLine
  {
    public string Id { set; get; }
    public string Name { set; get; }
  }


  public class Program
  {
    static void Main(string[] args)
    {
      List<MyLine> myLines = new List<MyLine>()
      {
        new MyLine() { Id = "Id1", Name = "Name1" },
        new MyLine() { Id = "Id2", Name = "Name2" }
      };

      IOrderBuilder order = new OrderBuilder();

      order.BuildId("1")
        .BuildIssueDate(new DateTime())
        .BuildIssueTime(new DateTime()).BuildBuyerParty(buyer => buyer.SetGln("60").SetName("CompanyName"))
        .BuildLines(() => myLines.Select(a => new LineBuilder().SetDescription("d").SetId("id")));

      var ublOrder = order.GetUblOrder();


    }
  }
}
