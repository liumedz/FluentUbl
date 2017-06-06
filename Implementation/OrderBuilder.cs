using System;
using System.Collections.Generic;
using FluentUbl.Interfaces;
using InExchange.Format.UBL;

namespace FluentUbl.Implementation
{
  public class OrderBuilder : Interfaces.IOrderBuilder, IOrderIdBuilder, IOrderLineBuilder
  {
    private UblOrder _ublOrder = UblDocument.Create<UblOrder>(UblProfiles.Bii6ProcurementExtended);

    public OrderBuilder()
    {
      _ublOrder.BuyerCustomerParty = new UblCustomerParty();
      _ublOrder.OrderLine = new List<UblOrderLine>();
    }

    public Interfaces.IOrderBuilder BuildId(string id)
    {
      _ublOrder.Id = new UblIdentifier(id);
      return this;
    }

    public Interfaces.IOrderBuilder BuildIssueDate(DateTime date)
    {
      _ublOrder.IssueDate = new UblDate(date);
      return this;
    }

    public Interfaces.IOrderBuilder BuildIssueTime(DateTime time)
    {
      _ublOrder.IssueTime = new UblTime(time);
      return this;
    }

    public Interfaces.IOrderBuilder BuildBuyerParty(Func<IBuyerPartyBuilder, IBuyerPartyBuilder> buyerPartyBuilder)
    {
      buyerPartyBuilder.Invoke(new BuyerPartyBuilder(_ublOrder.BuyerCustomerParty));
      return this;
    }
    public Interfaces.ILineBuilder BuildLine()
    {
      return new LineBuilder();
    }

    public Interfaces.IOrderBuilder BuildLines(Func<IEnumerable<Interfaces.ILineBuilder>> lineBuilder)
    {
      foreach (LineBuilder builder in lineBuilder())
      {
        _ublOrder.OrderLine.Add(builder.UblOrderLine);
      }
      return this;
    }

    public Interfaces.IOrderBuilder BuildLines(Action<Interfaces.ILinesBuilder> action)
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