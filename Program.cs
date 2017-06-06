using System;
using System.Collections.Generic;
using System.Linq;
using FluentUbl.Implementation;
using FluentUbl.Interfaces;
using InExchange.Format.UBL;

namespace FluentUbl
{
  public class MyLine
  {
    public string Id { set; get; }
    public string Name { set; get; }
  }

  public class OrderDomain
  {
    public UblOrder Order { set; get; }
    public Guid OrderId { set; get; }
  }

  public abstract class IncomingOrderBuilder
  {
    protected abstract void BuildId(IOrderIdBuilder orderIdBuilder);
    protected abstract void BuildLines(IOrderLineBuilder orderLineBuilder);

    protected virtual void Build(Interfaces.IOrderBuilder orderBuilder)
    {
      
    }
    public virtual OrderDomain BuildIncomingOrder()
    {
      IOrderBuilder order = new OrderBuilder();

      Build(order);
      BuildId((IOrderIdBuilder) order);
      BuildLines((IOrderLineBuilder) order);

      return new OrderDomain { Order = order.GetUblOrder(), OrderId = Guid.NewGuid() };
    }
  }

  public class PyramidOrderBuilder : IncomingOrderBuilder
  {
    protected override void Build(Interfaces.IOrderBuilder orderBuilder)
    {
    }

    protected override void BuildId(IOrderIdBuilder orderIdBuilder)
    {

    }

    protected override void BuildLines(IOrderLineBuilder orderLineBuilder)
    {
      List<MyLine> lines = new List<MyLine>()
      {
        new MyLine() { Id = "Id1", Name = "Name1" },
        new MyLine() { Id = "Id2", Name = "Name2" }
      };
      orderLineBuilder.BuildLines(() => lines.Select(a => orderLineBuilder.BuildLine().BuildDescription("d").BuildId("id")));
    }
  }


  public class Program
  {
    static void Main(string[] args)
    {
      List<MyLine> lines = new List<MyLine>()
      {
        new MyLine() { Id = "Id1", Name = "Name1" },
        new MyLine() { Id = "Id2", Name = "Name2" }
      };

      IOrderBuilder order = new OrderBuilder();

      order.BuildId("1")
        .BuildIssueDate(new DateTime())
        .BuildIssueTime(new DateTime()).BuildBuyerParty(buyer => buyer.BuildGln("60").BuildName("CompanyName"))
        .BuildLines(() => lines.Select(a => order.BuildLine().BuildDescription(a.Id).BuildId(a.Name))) 
        .BuildLines(linesBuilder =>
        {
          lines.ForEach((line => linesBuilder.AddLine(lineBuider => lineBuider.BuildId(line.Id).BuildDescription(line.Name))));
        });

      var ublOrder = order.GetUblOrder();

    }
  }

}
