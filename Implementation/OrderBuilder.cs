using System;
using System.Collections.Generic;
using InExchange.Format.UBL;

namespace FluentUbl.Implementation
{
  public class OrderBuilder : IOrderBuilder, IOrderIdBuilder, IOrderLineBuilder
  {
    private UblOrder _ublOrder = UblDocument.Create<UblOrder>(UblProfiles.Bii6ProcurementExtended);

    public OrderBuilder()
    {
      _ublOrder.BuyerCustomerParty = new UblCustomerParty();
      _ublOrder.OrderLine = new List<UblOrderLine>();
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
    public ILineBuilder BuildLine()
    {
      return new LineBuilder();
    }

    public IOrderBuilder BuildLines(Func<IEnumerable<ILineBuilder>> lineBuilder)
    {
      foreach (LineBuilder builder in lineBuilder())
      {
        _ublOrder.OrderLine.Add(builder.UblOrderLine);
      }
      return this;
    }

    public IOrderBuilder BuildLines(Action<ILinesBuilder> action)
    {
      var linesBuilder = new LinesBuilder(_ublOrder.OrderLine);
      action(linesBuilder);
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
}