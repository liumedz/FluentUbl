using System;
using System.Collections.Generic;

namespace FluentUbl
{
  public class Test
  {
    public void CreateOrder(IOrderBuilder orderBuilder)
    {

      var lines = new List<string>();

      orderBuilder.SetId("Id1")
        .SetName("OrderName")
        .SetCapacity(58)
        .SetLines(linesBuilder =>
        {
          linesBuilder.SetName("SomeCoolName");
          lines.ForEach((line => linesBuilder.AddLine(lineBuider => lineBuider.SetLine(line))));
        })
        .AddDelivery(deliveryBuilder => deliveryBuilder.SetStartDate(DateTime.Now)
          .SetEndDate(DateTime.Now.AddDays(5)))
        .AddDelivery(deliveryBuilder => deliveryBuilder.SetStartDate(DateTime.Now)
          .SetEndDate(DateTime.Now.AddDays(5)))
        .AddDelivery(deliveryBuilder => deliveryBuilder.SetStartDate(DateTime.Now)
          .SetEndDate(DateTime.Now.AddDays(5)));
    }

    public void CreateDelivery(IDeliveryBuilder deliveryBuilder)
    {
      deliveryBuilder.SetStartDate(DateTime.Now)
        .SetEndDate(DateTime.Now.AddDays(5));
    }

    public void CreateLines(ILinesBuilder linesBuilder)
    {
      linesBuilder.SetName("SomeCoolName")
        .AddLine(CreateLine)
        .AddLine(CreateLine)
        .AddLine(CreateLine)
        .AddLine(CreateLine);
    }

    public void CreateLine(ILineBuilder lineBuilder)
    {
      lineBuilder.SetLine("Line" + Guid.NewGuid());
    }
  }

  public class OrderBuilder : IOrderBuilder
  {
    private string _id;
    private string _name;
    private int _capacity;
    private object _lines;
    private List<object> _deliveries;

    public IOrderBuilder SetId(string id)
    {
      _id = id;
      return this;
    }

    public IOrderBuilder SetName(string name)
    {
      _name = name;
      return this;
    }

    public IOrderBuilder SetCapacity(int capacity)
    {
      _capacity = capacity;
      return this;
    }

    public IOrderBuilder SetLines(Action<ILinesBuilder> action)
    {
      var linesBuilder = new LinesBuilder();
      action(linesBuilder);
      _lines = linesBuilder.Assemble();
      return this;
    }

    public IOrderBuilder AddDelivery(Action<IDeliveryBuilder> action)
    {
      var deliveryBuilder = new DeliveryBuilder();
      action(deliveryBuilder);
      _deliveries.Add(deliveryBuilder.Assemble());
      return this;
    }

    public object Assemble()
    {
      return new { Id = _id, Name = _name, Capacity = _capacity, Lines = _lines, Deliveries = _deliveries };
    }
  }

  public class DeliveryBuilder : IDeliveryBuilder
  {
    private DateTime _startDate;
    private DateTime _endDate;
    
    public IDeliveryBuilder SetStartDate(DateTime startDate)
    {
      _startDate = startDate;
      return this;
    }

    public IDeliveryBuilder SetEndDate(DateTime endDate)
    {
      _endDate = endDate;
      return this;
    }

    public object Assemble()
    {
      return new { StartDate = _startDate, EndDate = _endDate };
    }
  }

  public class LinesBuilder : ILinesBuilder
  {
    private string _name;
    private List<string> _lines;

    public LinesBuilder()
    {
      _lines = new List<string>();
    }

    public ILinesBuilder SetName(string name)
    {
      _name = name;
      return this;
    }

    public ILinesBuilder AddLine(Action<ILineBuilder> action)
    {
      var lineBuilder = new LineBuilder();
      action(lineBuilder);
      _lines.Add(lineBuilder.Assemble());
      return this;
    }

    public object Assemble()
    {
      return new { Lines = _lines, Name = _name };
    }
  }

  public class LineBuilder : ILineBuilder
  {
    private string _line;

    public ILineBuilder SetLine(string line)
    {
      _line = line;
      return this;
    }

    public string Assemble()
    {
      return _line;
    }
  }

  public interface ILinesBuilder
  {
    ILinesBuilder SetName(string name);
    ILinesBuilder AddLine(Action<ILineBuilder> action);
    object Assemble();
  }

  public interface ILineBuilder
  {
    ILineBuilder SetLine(string line);
    string Assemble();
  }

  public interface IOrderBuilder
  {
    IOrderBuilder SetId(string id);
    IOrderBuilder SetName(string name);
    IOrderBuilder SetCapacity(int capacity);
    IOrderBuilder SetLines(Action<ILinesBuilder> action);
    IOrderBuilder AddDelivery(Action<IDeliveryBuilder> action);
    object Assemble();
  }

  public interface IDeliveryBuilder
  {
    IDeliveryBuilder SetStartDate(DateTime startDate);
    IDeliveryBuilder SetEndDate(DateTime endDate);
    object Assemble();
  }
}
